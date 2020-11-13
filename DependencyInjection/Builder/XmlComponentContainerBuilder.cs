using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Kodama.DependencyInjection.Builder.Schema;
using Kodama.DependencyInjection.Component;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Marker;
using Kodama.DependencyInjection.Loader;
using Kodama.Aop.Aspect;
using Kodama.Aop.Interceptor;
using Kodama.Aop.Pointcut;
using Kodama.Aop.Pointcut.Compose;
using Kodama.Aop.Pointcut.Compose.Class;
using Kodama.Aop.Pointcut.Compose.Method;
using Kodama.Aop.Weaver;
using Kodama.Script.Expression;
using Kodama.Function.Functor.Bind;

namespace Kodama.DependencyInjection.Builder
{
    /// <summary>
    /// XMLを利用して、DependencyInjectionコンテナを生成して
    /// コンポーネントを登録し、そのインスタンスを返すビルダ
    /// </summary>
    /// <remarks>
    /// XMLの設定ファイルの詳細はKodama\DependencyInjection\Builder\Schema\ComponentConfig.xsd参照。
    /// </remarks>
    public class XmlComponentContainerBuilder : IComponentContainerBuilder
    {
        /// <summary>
        /// XML設定ファイルのパス
        /// </summary>
        private string configPath = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="path">XML設定ファイルのパス</param>
        public XmlComponentContainerBuilder(string path)
        {
            configPath = path;
        }

        /// <summary>
        /// XML設定ファイルから、DependencyInjectionコンテナを生成して
        /// コンポーネントを登録し、そのインスタンスを返す
        /// </summary>
        /// <returns>生成されたDependencyInjectionコンテナ</returns>
        /// <exception cref="System.InvalidOperationException">
        /// XML設定ファイルがスキーマに準じていない場合はこの例外を投げる
        /// </exception>
        /// <exception cref="TypeNotFoundException">
        /// 指定された名前の型がみつからなかった場合に発生する例外
        /// </exception>
        public IComponentContainer Build()
        {
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream
                ("Kodama.DependencyInjection.Builder.Schema.ComponentConfig.xsd");
            XmlSchemaCollection schema = new XmlSchemaCollection();
            schema.Add(null, new XmlTextReader(stream));

            XmlValidatingReader reader = new XmlValidatingReader(new XmlTextReader(configPath));
            reader.ValidationType = ValidationType.Schema;
            reader.Schemas.Add(schema);

            XmlSerializer serializer = new XmlSerializer(typeof(componentConfig));

            // XML設定ファイルの読み出し
            componentConfig config = null;
            try
            {
                config = (componentConfig)serializer.Deserialize(reader);
            }
            finally
            {
                reader.Close();
            }

            // 参照アセンブリのロード
            TypeLoader loader = new TypeLoader();
            foreach (object item in config.Items)
            {
                componentConfigAssembly assembly = item as componentConfigAssembly;
                if (assembly == null)
                {
                    continue;
                }
                loader.AddAssemblyFile(assembly.name);
            }

            // コンテナの作成
            IMutableComponentContainer container = new ComponentContainerImpl();

            // 子コンテナの作成
            foreach (object item in config.Items)
            {
                componentConfigInclude include = item as componentConfigInclude;
                if (include == null)
                {
                    continue;
                }
                container.AddChild(new XmlComponentContainerBuilder(include.path).Build());
            }

            // リモートオブジェクトの登録
            foreach (object item in config.Items)
            {
                componentConfigRemotingConfig remotingConfig = item as componentConfigRemotingConfig;
                if (remotingConfig == null)
                {
                    continue;
                }
                RemotingConfiguration.Configure(remotingConfig.path);
            }

            // コンポーネントの自動登録
            foreach (object item in config.Items)
            {
                componentConfigAutoDiscovery autoDiscovery = item as componentConfigAutoDiscovery;
                if (autoDiscovery == null)
                {
                    continue;
                }
                AutoComponentContainerBuilder builder = new AutoComponentContainerBuilder();
                builder.ComponentCategory = autoDiscovery.category;
                foreach (componentConfigAutoDiscoverySearchPath searchPath in autoDiscovery.Items)
                {
                    builder.AddAssemblyFolder(searchPath.path);
                }
                container.AddChild(builder.Build());
            }

            // アスペクトの登録
            foreach (object item in config.Items)
            {
                aspectType aspect = item as aspectType;
                if (aspect == null)
                {
                    continue;
                }

                Type interceptorType = loader.LoadType(aspect.interceptor);
                IInterceptor interceptor = (IInterceptor)Activator.CreateInstance(interceptorType);

                ComposiblePointcut classPointcut = null;
                if (aspect.classFilterType == filterType.name)
                {
                    classPointcut = new ClassNamePointcut(aspect.classFilter);
                }
                else
                {
                    Type attributeType = loader.LoadType(aspect.classFilter);
                    classPointcut = new ClassAttributePointcut(attributeType);
                }

                ComposiblePointcut methodPointcut = null;
                if (aspect.methodFilterType == filterType.name)
                {
                    methodPointcut = new MethodNamePointcut(aspect.methodFilter);
                }
                else
                {
                    Type attributeType = loader.LoadType(aspect.methodFilter);
                    methodPointcut = new MethodAttributePointcut(attributeType);
                }

                AspectWeaver.Instance().Register
                    (new AspectImpl(interceptor, classPointcut & methodPointcut));
            }

            // コンポーネントの登録
            foreach (object item in config.Items)
            {
                componentConfigComponent component = item as componentConfigComponent;
                if (component == null)
                {
                    continue;
                }

                // コンポーネント名が設定されていない場合はクラス名がデフォルト
                if (component.name == null || component.name == "")
                {
                    component.name = component.@class;
                }

                // コンポーネントのエントリを作成する
                IComponentEntry entry = null;
                Type componentType = loader.LoadType(component.@class);
                switch (component.instance)
                {
                    case instanceType.prototype :
                        entry = new PrototypeComponentEntry
                            (container, componentType, component.name);
                        break;
                    case instanceType.singleton :
                        entry = new SingletonComponentEntry
                            (container, componentType, component.name);
                        break;
                    default :
                        throw new NotSupportedException("outer instance mode is not supported.");
                }

                // コンポーネントを登録する
                if (component.primary == null || component.primary == "")
                {
                    container.Register(entry);
                }
                else
                {
                    Type primaryType = loader.LoadType(component.primary);
                    container.Register(primaryType, entry);
                }

                // アスペクトの登録
                foreach (object componentItem in component.Items)
                {
                    componentAspectType aspect = componentItem as componentAspectType;
                    if (aspect == null)
                    {
                        continue;
                    }

                    Type interceptorType = loader.LoadType(aspect.interceptor);
                    IInterceptor interceptor = (IInterceptor)Activator.CreateInstance(interceptorType);

                    ComposiblePointcut classPointcut = new ClassNamePointcut(component.@class);

                    ComposiblePointcut methodPointcut = null;
                    if (aspect.methodFilterType == filterType.name)
                    {
                        methodPointcut = new MethodNamePointcut(aspect.methodFilter);
                    }
                    else
                    {
                        Type attributeType = loader.LoadType(aspect.methodFilter);
                        methodPointcut = new MethodAttributePointcut(attributeType);
                    }

                    AspectWeaver.Instance().Register
                        (new AspectImpl(interceptor, classPointcut & methodPointcut));
                }

                // コンストラクタインジェクションの設定
                foreach (object componentItem in component.Items)
                {
                    injectorArgument[] arguments = componentItem as injectorArgument[];
                    if (arguments == null)
                    {
                        continue;
                    }
                    ArrayList boundArguments = new ArrayList();
                    foreach (injectorArgument argument in arguments)
                    {
                        injectorArgumentInjectionName injectionName =
                            argument.Item as injectorArgumentInjectionName;
                        if (injectionName != null)
                        {
                            boundArguments.Add(new NamedArgumentComponentProvider
                                (container, injectionName.name));
                        }
                        injectorArgumentInjectionType injectionTypeName =
                            argument.Item as injectorArgumentInjectionType;
                        if (injectionTypeName != null)
                        {
                            Type injectionType = loader.LoadType(injectionTypeName.@class);
                            boundArguments.Add(new TypedArgumentComponentProvider
                                (container, injectionType));
                        }
                        string expression = argument.Item as string;
                        if (expression != null)
                        {
                            boundArguments.Add(Evaluator.Eval(expression));
                        }
                    }
                    entry.InjectionConstructor = BindUtility.CreateBindConstructor
                        (entry.ComponentType, boundArguments.ToArray());
                }

                // セッターインジェクションの設定
                foreach (object componentItem in component.Items)
                {
                    componentConfigComponentMethod method =
                        componentItem as componentConfigComponentMethod;
                    if (method == null)
                    {
                        continue;
                    }
                    ArrayList boundArguments = new ArrayList();
                    boundArguments.Add(new NotBoundArgument(0));
                    foreach (injectorArgument argument in method.argument)
                    {
                        injectorArgumentInjectionName injectionName =
                             argument.Item as injectorArgumentInjectionName;
                        if (injectionName != null)
                        {
                            boundArguments.Add(new NamedArgumentComponentProvider
                                (container, injectionName.name));
                        }
                        injectorArgumentInjectionType injectionTypeName =
                            argument.Item as injectorArgumentInjectionType;
                        if (injectionTypeName != null)
                        {
                            Type injectionType = loader.LoadType(injectionTypeName.@class);
                            boundArguments.Add(new TypedArgumentComponentProvider
                                (container, injectionType));
                        }
                        string expression = argument.Item as string;
                        if (expression != null)
                        {
                            boundArguments.Add(Evaluator.Eval(expression));
                        }
                    }
                    entry.AddInjectionFanctor(BindUtility.CreateBindMember(
                        entry.ComponentType,
                        method.name,
                        boundArguments.ToArray()));
                }

                // 初期化メソッドの設定
                foreach (object componentItem in component.Items)
                {
                    componentConfigComponentInitialize initialize =
                        componentItem as componentConfigComponentInitialize;
                    if (initialize == null)
                    {
                        continue;
                    }
                    ArrayList boundArguments = new ArrayList();
                    boundArguments.Add(new NotBoundArgument(0));
                    foreach (initializerArgument argument in initialize.argument)
                    {
                        string expression = argument.Item as string;
                        if (expression != null)
                        {
                            boundArguments.Add(Evaluator.Eval(expression));
                        }
                    }
                    entry.AddInitializationFactor(BindUtility.CreateBindMember(
                        entry.ComponentType,
                        initialize.name,
                        boundArguments.ToArray()));
                }
            } // コンポーネントの登録終了

            return container;
        }
    }
}
