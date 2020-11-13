using System;
using System.Collections;
using System.Reflection;
using Kodama.Script.Engine;
using Kodama.Script.Factory;
using Kodama.Script.Engine.CodeDom.CSharpScript;
using Kodama.Script.Engine.CodeDom.JScript;
using Kodama.Script.Engine.CodeDom.VBScript;

using Kodama.DependencyInjection.Container;

namespace Kodama.DependencyInjection.Builder
{
    /// <summary>
    /// スクリプトを利用して、DependencyInjectionコンテナを生成して
    /// コンポーネントを登録し、そのインスタンスを返すビルダ
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public class ScriptComponentContainerBuilder : IComponentContainerBuilder
    {
        /// <summary>
        /// スクリプトファイルのパス
        /// </summary>
        private string scriptPath = null;

        /// <summary>
        /// スクリプトから参照するアセンブリの名前
        /// </summary>
        private ArrayList assemblyNames = new ArrayList();

        /// <summary>
        /// スクリプトファイルのパス
        /// </summary>
        public string Script
        {
            set { scriptPath = value; }
        }

        /// <summary>
        /// スクリプトから参照するアセンブリの追加
        /// </summary>
        /// <param name="assemblyName">スクリプトから参照するアセンブリ</param>
        public void AddReference(string assemblyName)
        {
            assemblyNames.Add(assemblyName);
        }

        /// <summary>
        /// スクリプトを利用して、DependencyInjectionコンテナを生成して
        /// コンポーネントを登録し、そのインスタンスを返す
        /// </summary>
        /// <returns>生成されたDependencyInjectionコンテナ</returns>
        /// <exception cref="CompileErrorException">
        /// スクリプトのコンパイルエラーが発生した場合はこの例外を投げる
        /// </exception>
        /// <exception cref="FactoryNotFoundException">
        /// DependencyInjectionコンテナのファクトリーメソッドがスクリプトから
        /// みつからない場合はこの例外を投げる
        /// </exception>
        /// <remarks>
        /// <p>スクリプトにはC#、VBScriptとJScriptが利用できます。スクリプト上の
        /// ComponentContainerFactoryAttribute属性をもちIComponentContainerインターフェイス
        /// と互換性のある値を返すStaticな関数が、DependencyInjectionコンテナを生成し
        /// 依存関係が設定したものを返すファクトリーメソッドとなります。スクリプト上に
        /// このファクトリーメソッドが存在しない場合はFactoryNotFoundException例外を
        /// 投げます。以下にスクリプトの例をあげます。</p>
        /// <code lang="C#">
        /// using System;
        /// using Kodama.DependencyInjection.Container;
        /// using Kodama.DependencyInjection.Factory;
        /// using HogeHoge;
        ///
        /// public class ComponentContainerBuilder
        /// {
        ///     [ComponentContainerFactory]
        ///     public IMutableComponentContainer Build()
        ///     {
        ///         IMutableComponentContainer container = new ComponentContainerImpl();
        ///         container.Register(typeof(ClassA));
        ///         container.Register(typeof(ClassB));
        ///         return container;
        ///     }
        /// }
        /// </code>
        /// </remarks>
        public IComponentContainer Build()
        {
            IScriptEngineFactory[] factories = new IScriptEngineFactory[]
                {new CSharpScriptEngineFactory(), new JScriptEngineFactory(), new VBScriptEngineFactory()};
            foreach (IScriptEngineFactory factory in factories)
            {
                if (!factory.CanCompile(scriptPath))
                {
                    continue;
                }

                IScriptEngine engine = factory.CreateFromFile(scriptPath);

                engine.AddReference(Assembly.GetExecutingAssembly().Location);
                foreach (string assemblyName in assemblyNames)
                {
                    engine.AddReference(assemblyName);
                }

                try 
                {
                    engine.Compile();
                }
                finally
                {
                    engine.Close();
                }

                foreach (Type type in engine.ScriptAssembly.GetTypes())
                {
                    foreach (MethodInfo mi in type.GetMethods())
                    {
                        if (!mi.IsStatic)
                        {
                            continue;
                        }
                        if (mi.GetParameters().Length != 0)
                        {
                            continue;
                        }
                        if (!typeof(IComponentContainer).IsAssignableFrom(mi.ReturnType))
                        {
                            continue;
                        }
                        if (!Attribute.IsDefined(mi, typeof(ComponentContainerFactoryAttribute)))
                        {
                            continue;
                        }

                        IComponentContainer container = (IComponentContainer)mi.Invoke(null, null);

                        engine.Close();

                        return container;
                    }
                }

                engine.Close();

                throw new FactoryNotFoundException();
            }

            throw new NotSupportedScriptException();
        }
    }
}
