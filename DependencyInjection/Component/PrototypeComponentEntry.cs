using System;
using System.Collections;
using System.Reflection;
using Kodama.Function.Functor;
using Kodama.Function.Functor.Bind;
using Kodama.Function.Functor.Member;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Marker;

namespace Kodama.DependencyInjection.Component
{
    /// <summary>
    /// DependencyInjectionコンテナに登録されているコンポーネント情報
    /// </summary>
    /// <remarks>
    /// このコンポーネント情報で登録されているコンポーネントは、
    /// IComponentContainer#GetComponent毎に常に新しいインスタンスを返す。
    /// </remarks>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public class PrototypeComponentEntry : IComponentEntry
    {
        /// <summary>
        /// 登録されているコンポーネントの型
        /// </summary>
        private Type componentType;

        /// <summary>
        /// 登録されているコンポーネントの名前
        /// </summary>
        private string componentName;

        /// <summary>
        /// 依存性を注入するためのコンストラクタをバインドしたファンクタ
        /// </summary>
        private BindFunctor injectionConstructorFunctor;

        /// <summary>
        /// 依存性を注入するための引数をバインド済みの関数オブジェクト
        /// </summary>
        private ArrayList injectionFactors = new ArrayList();

        /// <summary>
        /// コンポーネントを初期化するための引数をバインド済みの関数オブジェクト
        /// </summary>
        private ArrayList initializationFactors = new ArrayList();

        /// <summary>
        /// インスタンスの生成中かどうかのフラグ。循環参照を検出するのにつかわれる
        /// </summary>
        private bool instantiating = false;

        /// <summary>
        /// 登録されているコンポーネントの型
        /// </summary>
        public Type ComponentType
        {
            get { return componentType; }
        }

        /// <summary>
        /// 登録されているコンポーネントの名前
        /// </summary>
        public string ComponentName
        {
            get { return componentName; }
        }

        /// <summary>
        /// 依存性を注入するためのコンストラクタの引数をバインドしたファンクタ
        /// </summary>
        public BindFunctor InjectionConstructor
        {
            set { injectionConstructorFunctor = value; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container">コンポーネントが登録するコンテナ</param>
        /// <param name="type">登録するコンポーネントの型</param>
        public PrototypeComponentEntry(IComponentContainer container, Type type)
        {
            componentType = type;
            componentName = type.FullName;

            injectionConstructorFunctor = null;
            foreach (ConstructorInfo ci in componentType.GetConstructors())
            {
                if (Attribute.IsDefined(ci, typeof(InjectionPointAttribute)))
                {
                    ArrayList arguments = new ArrayList();
                    foreach (ParameterInfo param in ci.GetParameters())
                    {
                        if (Attribute.IsDefined(param, typeof(ExplicitComponentAttribute)))
                        {
                            ExplicitComponentAttribute eca =
                                (ExplicitComponentAttribute)Attribute.GetCustomAttribute
                                (param, typeof(ExplicitComponentAttribute));
                            arguments.Add(eca.CreateProvider(container));
                        }
                        else
                        {
                            arguments.Add(new TypedArgumentComponentProvider(container, param.ParameterType));
                        }    
                    }
                    injectionConstructorFunctor = new BindFunctor(new ConstructorFunctor(ci), arguments.ToArray());
                }
            }

            foreach (MethodInfo mi in componentType.GetMethods())
            {
                if (Attribute.IsDefined(mi, typeof(InjectionPointAttribute)))
                {
                    ArrayList arguments = new ArrayList();
                    arguments.Add(new NotBoundArgument(0));
                    foreach (ParameterInfo param in mi.GetParameters())
                    {
                        if (Attribute.IsDefined(param, typeof(ExplicitComponentAttribute)))
                        {
                            ExplicitComponentAttribute eca =
                                (ExplicitComponentAttribute)Attribute.GetCustomAttribute
                                (param, typeof(ExplicitComponentAttribute));
                            arguments.Add(eca.CreateProvider(container));
                        }
                        else
                        {
                            arguments.Add(new TypedArgumentComponentProvider(container, param.ParameterType));
                        }    
                    }
                    injectionFactors.Add(new BindFunctor(new MemberFunctor(mi), arguments.ToArray()));
                }
            }

            foreach (MethodInfo mi in componentType.GetMethods())
            {
                if (Attribute.IsDefined(mi, typeof(InitializationPointAttribute)))
                {
                    initializationFactors.Add(new MemberFunctor(mi));
                }
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container">コンポーネントを登録するコンテナ</param>
        /// <param name="type">登録するコンポーネントの型</param>
        /// <param name="name">登録するコンポーネントの名前</param>
        public PrototypeComponentEntry(IComponentContainer container, Type type, string name)
        {
            componentType = type;
            componentName = name;

            injectionConstructorFunctor = null;
            foreach (ConstructorInfo ci in componentType.GetConstructors())
            {
                if (Attribute.IsDefined(ci, typeof(InjectionPointAttribute)))
                {
                    ArrayList arguments = new ArrayList();
                    foreach (ParameterInfo param in ci.GetParameters())
                    {
                        if (Attribute.IsDefined(param, typeof(ExplicitComponentAttribute)))
                        {
                            ExplicitComponentAttribute eca =
                                (ExplicitComponentAttribute)Attribute.GetCustomAttribute
                                (param, typeof(ExplicitComponentAttribute));
                            arguments.Add(eca.CreateProvider(container));
                        }
                        else
                        {
                            arguments.Add(new TypedArgumentComponentProvider(container, param.ParameterType));
                        }    
                    }
                    injectionConstructorFunctor = new BindFunctor(new ConstructorFunctor(ci), arguments.ToArray());
                }
            }

            foreach (MethodInfo mi in componentType.GetMethods())
            {
                if (Attribute.IsDefined(mi, typeof(InjectionPointAttribute)))
                {
                    ParameterInfo[] paramerters = mi.GetParameters();
                    ArrayList       arguments   = new ArrayList();
                    arguments.Add(new NotBoundArgument(0));
                    foreach (ParameterInfo param in mi.GetParameters())
                    {
                        if (Attribute.IsDefined(param, typeof(ExplicitComponentAttribute)))
                        {
                            ExplicitComponentAttribute explicitComponentAttribute =
                                (ExplicitComponentAttribute)Attribute.GetCustomAttribute
                                (param, typeof(ExplicitComponentAttribute));
                            arguments.Add(explicitComponentAttribute.CreateProvider(container));
                        }
                        else
                        {
                            arguments.Add(new TypedArgumentComponentProvider(container, param.ParameterType));
                        }    
                    }
                    injectionFactors.Add(new BindFunctor(new MemberFunctor(mi), arguments.ToArray()));
                }
            }

            foreach (MethodInfo mi in componentType.GetMethods())
            {
                if (Attribute.IsDefined(mi, typeof(InitializationPointAttribute)))
                {
                    initializationFactors.Add(new MemberFunctor(mi));
                }
            }
        }

        /// <summary>
        /// 依存性を注入するための引数をバインド済みの関数オブジェクトを追加する
        /// </summary>
        /// <param name="functor">追加する関数オブジェクト</param>
        public void AddInjectionFanctor(BindFunctor functor)
        {
            injectionFactors.Add(functor);
        }

        /// <summary>
        /// コンポーネントを初期化するための引数をバインド済みの関数オブジェクトを追加する
        /// </summary>
        /// <param name="functor">追加する関数オブジェクト</param>
        public void AddInitializationFactor(IFunctor functor)
        {
            initializationFactors.Add(functor);
        }

        /// <summary>
        /// コンポーネントが依存しているコンポーネントの登録情報を返す
        /// </summary>
        /// <returns>コンポーネントが依存しているコンポーネントの登録情報</returns>
        public IComponentEntry[] GetDependencies()
        {
            ArrayList list = new ArrayList();
            foreach (object arg in injectionConstructorFunctor.GetBoundArguments())
            {
                IArgumentComponentProvider provider = arg as IArgumentComponentProvider;
                if (provider != null)
                {
                    list.Add(provider.ComponentEntry);
                }
            }
            foreach (BindFunctor functor in injectionFactors)
            {
                foreach (object arg in functor.GetBoundArguments())
                {
                    IArgumentComponentProvider provider = arg as IArgumentComponentProvider;
                    if (provider != null)
                    {
                        list.Add(provider.ComponentEntry);
                    }
                }
            }
            return (IComponentEntry[])list.ToArray(typeof(IComponentEntry));
        }

        /// <summary>
        /// コンポーネントのインスタンスを返す
        /// </summary>
        /// <returns>コンポーネントのインスタンス</returns>
        /// <exception cref="CyclicDependencyException">
        /// コンポーネントの依存関係が循環していてかつ、循環しているコンポーネントが
        /// Prototypeモードの場合に発生する例外
        /// </exception>
        /// <remarks>
        /// このメソッドで新しいインスタンスを返すか既存のインスタンスを返すかは
        /// 実装で選択します。
        /// </remarks>
        public object GetInstance()
        {
            if (instantiating)
            {
                instantiating = false;
                throw new CyclicDependencyException();
            }

            instantiating = true;

            object instance = injectionConstructorFunctor == null ?
                Activator.CreateInstance(componentType) :
                injectionConstructorFunctor.Invoke();

            foreach (IFunctor functor in injectionFactors)
            {
                functor.Invoke(instance);
            }

            foreach (IFunctor functor in initializationFactors)
            {
                functor.Invoke(instance);
            }

            instantiating = false;

            return instance;
        }

        /// <summary>
        /// コンポーネントのインスタンスを破棄する
        /// </summary>
        /// <remarks>
        /// 破棄するコンポーネントはIDisposableを実装していること。
        /// </remarks>
        public void Discard()
        {
            // この種類のエントリでは何もしない
        }
    }
}
