using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting;
using System.IO;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Marker;

namespace Kodama.DependencyInjection.Builder
{
    /// <summary>
    /// 自動的に、DependencyInjectionコンテナを生成して
    /// コンポーネントを登録し、そのインスタンスを返すビルダ
    /// </summary>
    public class AutoComponentContainerBuilder : IComponentContainerBuilder
    {
        /// <summary>
        /// 自動登録するコンポーネントのカテゴリ
        /// </summary>
        private string componentCategory = "";

        /// <summary>
        /// 自動登録されるアセンブリ
        /// </summary>
        private ArrayList assemblies = new ArrayList();

        /// <summary>
        /// 自動登録するコンポーネントのカテゴリ
        /// </summary>
        public string ComponentCategory
        {
            get { return componentCategory; }
            set { componentCategory = value; }
        }

        /// <summary>
        /// 自動登録されるアセンブリを追加する
        /// </summary>
        /// <param name="assemblyName">自動登録するアセンブリの完全名</param>
        public void AddAssemblyFile(AssemblyName assemblyName)
        {
            assemblies.Add(Assembly.Load(assemblyName));
        }

        /// <summary>
        /// 自動登録されるアセンブリを追加する
        /// </summary>
        /// <param name="filename">自動登録するアセンブリのファイル名</param>
        public void AddAssemblyFile(string filename)
        {
            assemblies.Add(Assembly.LoadFile(filename));
        }

        /// <summary>
        /// 自動登録されるアセンブリを追加する
        /// </summary>
        /// <param name="folder">自動登録するアセンブリが含まれるフォルダ</param>
        public void AddAssemblyFolder(string folder)
        {
            foreach (string name in Directory.GetFiles(folder, "*.dll"))
            {
                assemblies.Add(Assembly.LoadFile(Path.Combine(folder, name)));
            }
        }

        /// <summary>
        /// 自動登録されるリモーティングオブジェクトを含むアセンブリを追加する
        /// </summary>
        /// <param name="filename">リモーティングオブジェクトについて記述される設定ファイルのパス</param>
        /// <seealso cref="System.Runtime.Remoting.RemotingConfiguration">RemotingConfigurationクラス</seealso>
        public static void RemotingConfigure(string filename)
        {
            RemotingConfiguration.Configure(filename);
        }

        /// <summary>
        /// 自動的に、DependencyInjectionコンテナを生成して
        /// コンポーネントを登録し、そのインスタンスを返す
        /// </summary>
        /// <returns>生成されたDependencyInjectionコンテナ</returns>
        public IComponentContainer Build()
        {
            // アセンブリをAutoRegistrationAssembly属性が設定されてるものを選別する
            ArrayList filteredAssemblies = new ArrayList();
            foreach (Assembly assembly in assemblies)
            {
                if (Attribute.IsDefined(assembly, typeof(AutoRegistrationAssemblyAttribute)))
                {
                    filteredAssemblies.Add(assembly);
                }
            }

            // コンテナのインスタンスの生成
            IMutableComponentContainer container = new ComponentContainerImpl();

            // 選別されたアセンブリからAutoRegistrationComponent属性が設定されているものだけコンテナに登録する
            foreach (Assembly assembly in filteredAssemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (Attribute.IsDefined(type, typeof(AutoRegistratonComponentAttribute)))
                    {
                        AutoRegistratonComponentAttribute aca =
                            (AutoRegistratonComponentAttribute)Attribute.GetCustomAttribute
                            (type, typeof(AutoRegistratonComponentAttribute));
                        if (string.Compare(aca.ComponentCategory, componentCategory, true) == 0)
                        {
                            aca.Register(container, type);
                        }
                    }
                }
            }

            // リモーティングオブジェクトの登録
            foreach (ActivatedClientTypeEntry entry in RemotingConfiguration.GetRegisteredActivatedClientTypes())
            {
                if (Attribute.IsDefined(entry.ObjectType, typeof(AutoRegistratonComponentAttribute)))
                {
                    AutoRegistratonComponentAttribute aca =
                        (AutoRegistratonComponentAttribute)Attribute.GetCustomAttribute
                        (entry.ObjectType, typeof(AutoRegistratonComponentAttribute));
                    if (string.Compare(aca.ComponentCategory, componentCategory, true) == 0)
                    {
                        aca.Register(container, entry.ObjectType);
                    }
                }
            }
            foreach (ActivatedServiceTypeEntry entry in RemotingConfiguration.GetRegisteredActivatedServiceTypes())
            {
                if (Attribute.IsDefined(entry.ObjectType, typeof(AutoRegistratonComponentAttribute)))
                {
                    AutoRegistratonComponentAttribute aca =
                        (AutoRegistratonComponentAttribute)Attribute.GetCustomAttribute
                        (entry.ObjectType, typeof(AutoRegistratonComponentAttribute));
                    if (string.Compare(aca.ComponentCategory, componentCategory, true) == 0)
                    {
                        aca.Register(container, entry.ObjectType);
                    }
                }
            }
            foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
            {
                if (Attribute.IsDefined(entry.ObjectType, typeof(AutoRegistratonComponentAttribute)))
                {
                    AutoRegistratonComponentAttribute aca =
                        (AutoRegistratonComponentAttribute)Attribute.GetCustomAttribute
                        (entry.ObjectType, typeof(AutoRegistratonComponentAttribute));
                    if (string.Compare(aca.ComponentCategory, componentCategory, true) == 0)
                    {
                        aca.Register(container, entry.ObjectType);
                    }
                }
            }
            foreach (WellKnownServiceTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownServiceTypes())
            {
                if (Attribute.IsDefined(entry.ObjectType, typeof(AutoRegistratonComponentAttribute)))
                {
                    AutoRegistratonComponentAttribute aca =
                        (AutoRegistratonComponentAttribute)Attribute.GetCustomAttribute
                        (entry.ObjectType, typeof(AutoRegistratonComponentAttribute));
                    if (string.Compare(aca.ComponentCategory, componentCategory, true) == 0)
                    {
                        aca.Register(container, entry.ObjectType);
                    }
                }
            }

            return container;
        }
    }
}
