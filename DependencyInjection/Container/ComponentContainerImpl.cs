using System;
using System.Collections;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// DependencyInjectionコンテナの実装クラスです
    /// </summary>
    /// <example>
    /// <code lang="C#">
    /// using System;
    /// using Kodama.DependencyInjection.Component;
    /// using Kodama.DependencyInjection.Container;
    /// using Kodama.DependencyInjection.Factory;
    /// using Kodama.Function;
    /// using Kodama.Function.Bind;
    /// using Kodama.Function.Member;
    ///
    /// ....
    ///
    /// public interface IBar
    /// {
    ///     void Print();
    /// }
    ///
    /// public class BarImpl : IBar
    /// {
    ///     public void Print()
    ///     {
    ///         Console.WriteLine("BarImpl");
    ///     }
    /// }
    ///
    /// public class Foo
    /// {
    ///     private IBar dependency1;
    ///     private IBar dependency2;
    ///     private int  val;
    ///
    ///     // InjectionPoint属性のついたメソッドは自動的に
    ///     // セッターインジェクションのメソッドに設定される
    ///     [InjectionPoint]
    ///     public void SetDependency1(IBar dep)
    ///     {
    ///         dependency1 = dep;
    ///     }
    ///
    ///     public void SetDependency2(IBar dep, int v)
    ///     {
    ///         dependency2 = dep;
    ///         val         = v;
    ///     }
    ///
    ///     // InitializationPoint属性のついたメソッドは自動的に
    ///     // 初期化メソッドに設定される
    ///     [InitializationPoint]
    ///     public void Initialize1()
    ///     {
    ///         Console.WriteLine("Init1");
    ///     }
    ///
    ///     public void Initialize2(int v)
    ///     {
    ///         Console.WriteLine("Init2 val = " + v.ToString());
    ///     }
    /// }
    ///
    /// ...
    ///
    /// // 通常はコンテナへのコンポーネントの登録、セッターインジェクション及び
    /// // 初期化メソッドの設定はスクリプトで行う。
    /// // 詳細はKodama.DependencyInjection.Factory.DefaultComponetContainerFactory#Create
    /// // メソッドのオーバーロードを参照。
    ///
    /// IMutableComponentContainer container = new ComponentContainerImpl();
    ///
    /// continer.Register(typeof(BarImpl));
    ///
    /// IComponentEntry entry = new PrototypeComponetEntry(container, typeof(Foo));
    /// 
    /// // 手動によるセッターインジェクションの設定
    /// entry.AddInjectionFanctor(
    ///     new BindFunctor(
    ///         new MemberFunctor(typeof(Foo).GetMethod("SetDependency2")),
    ///         new NotBoundArgument(0),
    ///         new TypedArgumentComponentProvider(container, typeof(IBar)),
    ///         1));
    ///
    /// // 手動による初期化メソッドの設定
    /// entry.AddInitializationFactor(
    ///     new BindFunctor(
    ///         new MemberFunctor(typeof(Foo).GetMethod("Initialize2")),
    ///         new NotBoundArgument(0),
    ///         2));
    ///
    /// container.Register(entry);
    ///
    /// Foo foo = (Foo)continer.GetComponent(typeof(Foo));
    /// </code>
    /// </example>
    public class ComponentContainerImpl : IMutableComponentContainer
    {
        /// <summary>
        /// 特定のインターフェイスに優先して割り当てられるコンポーネントの情報
        /// </summary>
        private Hashtable primaryComponentEntries = new Hashtable();

        /// <summary>
        /// 登録されているコンポーネントの情報
        /// </summary>
        private ArrayList secondaryComponentEntries = new ArrayList();

        /// <summary>
        /// 子コンテナ
        /// </summary>
        private ArrayList children = new ArrayList();

        /// <summary>
        /// コンポーネントを取得する
        /// </summary>
        /// <param name="componentType">取得するコンポーネントの型</param>
        /// <returns>引数にわたされた型のコンポーネント</returns>
        /// <exception cref="CyclicDependencyException">
        /// 依存関係が循環しているときに発生する例外
        /// </exception>
        /// <exception cref="TooManyRegistrationException">
        /// 指定されたインターフェイスを実装するコンポーネントが複数合った場合に発生する例外
        /// </exception>
        /// <exception cref="ComponentNotFoundException">
        /// 指定されたインターフェイスを実装するコンポーネントがなかった場合に発生する例外
        /// </exception>
        /// <exception cref="CyclicDependencyException">
        /// コンポーネントの依存関係が循環していてかつ、循環しているコンポーネントが
        /// Prototypeモードの場合に発生する例外
        /// </exception>
        public object GetComponent(Type componentType)
        {
            return GetComponentEntry(componentType).GetInstance();
        }

        /// <summary>
        /// コンポーネントを取得する
        /// </summary>
        /// <param name="componentName">取得するコンポーネントの名前</param>
        /// <returns>引数にわたされた名前のコンポーネント</returns>
        /// <exception cref="TooManyRegistrationException">
        /// 指定された名前のコンポーネントが複数合った場合に発生する例外
        /// </exception>
        /// <exception cref="ComponentNotFoundException">
        /// 指定された名前のコンポーネントがなかった場合に発生する例外
        /// </exception>
        /// <exception cref="CyclicDependencyException">
        /// コンポーネントの依存関係が循環していてかつ、循環しているコンポーネントが
        /// Prototypeモードの場合に発生する例外
        /// </exception>
        public object GetComponent(string componentName)
        {
            return GetComponentEntry(componentName).GetInstance();
        }

        /// <summary>
        /// コンポーネントが登録されているかチェックする
        /// </summary>
        /// <param name="componentType">登録されているか確認するコンポーネントの型</param>
        /// <returns>コンポーネントが登録されているかどうか</returns>
        public bool Contains(Type componentType)
        {
            foreach (IComponentEntry entry in primaryComponentEntries.Values)
            {
                if (object.Equals(componentType, entry.ComponentType))
                {
                    return true;
                }
            }
            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (object.Equals(componentType, entry.ComponentType))
                {
                    return true;
                }
            }
            foreach (IComponentContainer child in children)
            {
                if (child.Contains(componentType))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// コンポーネントが登録されているかチェックする
        /// </summary>
        /// <param name="componentName">登録されているか確認するコンポーネントの名前</param>
        /// <returns>コンポーネントが登録されているかどうか</returns>
        public bool Contains(string componentName)
        {
            foreach (IComponentEntry entry in primaryComponentEntries.Values)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0) 
                {
                    return true;
                }
            }
            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0) 
                {
                    return true;
                }
            }
            foreach (IComponentContainer child in children)
            {
                if (child.Contains(componentName))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <param name="componentType">登録するコンポーネントの型</param>
        public void Register(Type componentType)
        {
            Register(new PrototypeComponentEntry(this, componentType));
        }

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <param name="componentInstance">登録するコンポーネントのインスタンス</param>
        public void RegisterInstance(object componentInstance)
        {
            Register(new OuterComponentEntry(this, componentInstance));
        }

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <param name="componentType">登録するコンポーネントの型</param>
        /// <param name="componentName">登録するコンポーネントの名前</param>
        public void Register(Type componentType, string componentName)
        {
            Register(new PrototypeComponentEntry(this, componentType, componentName));
        }

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <param name="componentInstance">登録するコンポーネントのインスタンス</param>
        /// <param name="componentName">登録するコンポーネントの名前</param>
        public void RegisterInstance(object componentInstance, string componentName)
        {
            Register(new OuterComponentEntry(this, componentInstance, componentName));
        }

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <param name="componentEntry">登録するコンポーネントの情報</param>
        public void Register(IComponentEntry componentEntry)
        {
            secondaryComponentEntries.Add(componentEntry);
        }

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <remarks>
        /// 特定のインターフェイスに優先して割り当てるコンポーネントを指定します。
        /// </remarks>
        /// <param name="interfaceType">優先して割り当てるインターフェイス</param>
        /// <param name="implementComponentType">優先して割り当てるコンポーネント</param>
        /// <exception cref="TooManyRegistrationException">
        /// 指定されたインターフェイスを実装するコンポーネントが複数合った場合に発生する例外
        /// </exception>
        public void Register(Type interfaceType, Type implementComponentType)
        {
            Register(interfaceType, new PrototypeComponentEntry(this, implementComponentType));
        }

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <remarks>
        /// 特定のインターフェイスに優先して割り当てるコンポーネントを指定します。
        /// </remarks>
        /// <param name="interfaceType">優先して割り当てるインターフェイス</param>
        /// <param name="implementComponentInstance">優先して割り当てるコンポーネントのインスタンス</param>
        public void RegisterInstance(Type interfaceType, object implementComponentInstance)
        {
            Register(interfaceType, new OuterComponentEntry(this, implementComponentInstance));
        }

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <remarks>
        /// 特定のインターフェイスに優先して割り当てるコンポーネントを指定します。
        /// </remarks>
        /// <param name="interfaceType">優先して割り当てるインターフェイス</param>
        /// <param name="implementComponentType">優先して割り当てるコンポーネントの型</param>
        /// <param name="componentName">優先して割り当てるコンポーネントの名前</param>
        /// <exception cref="TooManyRegistrationException">
        /// 指定されたインターフェイスを実装するコンポーネントが複数合った場合に発生する例外
        /// </exception>
        public void Register(Type interfaceType, Type implementComponentType, string componentName)
        {
            Register(interfaceType, new PrototypeComponentEntry(this, implementComponentType, componentName));
        }

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <remarks>
        /// 特定のインターフェイスに優先して割り当てるコンポーネントを指定します。
        /// </remarks>
        /// <param name="interfaceType">優先して割り当てるインターフェイス</param>
        /// <param name="implementComponentInstance">優先して割り当てるコンポーネントのインスタンス</param>
        /// <param name="componentName">優先して割り当てるコンポーネントの名前</param>
        public void RegisterInstance(Type interfaceType, object implementComponentInstance, string componentName)
        {
            Register(interfaceType, new OuterComponentEntry(this, implementComponentInstance, componentName));
        }

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <remarks>
        /// 特定のインターフェイスに優先して割り当てるコンポーネントを指定します。
        /// </remarks>
        /// <param name="interfaceType">優先して割り当てるインターフェイス</param>
        /// <param name="implementComponentEntry">優先して割り当てるコンポーネントの情報</param>
        /// <exception cref="TooManyRegistrationException">
        /// 指定されたインターフェイスを実装するコンポーネントが複数合った場合に発生する例外
        /// </exception>
        public void Register(Type interfaceType, IComponentEntry implementComponentEntry)
        {
            if (primaryComponentEntries.Contains(interfaceType))
            {
                throw new TooManyRegistrationException();
            }

            primaryComponentEntries.Add(interfaceType, implementComponentEntry);
        }

        /// <summary>
        /// 指定されたインターフェイスを実装するコンポーネントの登録情報を取得する
        /// </summary>
        /// <param name="interfaceType">取得する実装コンポーネントのインターフェイス</param>
        /// <returns>指定されたインターフェイスを実装するコンポーネントの登録情報</returns>
        /// <exception cref="TooManyRegistrationException">
        /// 指定されたインターフェイスを実装するコンポーネントが複数あった場合に発生する例外
        /// </exception>
        /// <exception cref="ComponentNotFoundException">
        /// 指定されたインターフェイスを実装するコンポーネントがなかった場合に発生する例外
        /// </exception>
        public IComponentEntry GetComponentEntry(Type interfaceType)
        {
            if (primaryComponentEntries.Contains(interfaceType))
            {
                return (IComponentEntry)primaryComponentEntries[interfaceType];
            }

            if (CheckTooManyRegistration(interfaceType))
            {
                throw new TooManyRegistrationException();
            }

            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (interfaceType.IsAssignableFrom(entry.ComponentType))
                {
                    return entry;
                }
            }

            foreach (IComponentContainer child in children)
            {
                try
                {
                    return child.GetComponentEntry(interfaceType);
                }
                catch (ComponentNotFoundException)
                {
                    // 何もなし
                }
            }

            throw new ComponentNotFoundException();
        }

        /// <summary>
        /// 指定された名前をもつコンポーネントの登録情報を取得する
        /// </summary>
        /// <param name="componentName">取得するコンポーネントの名前</param>
        /// <returns>指定された名前をもつコンポーネントの登録情報</returns>
        /// <exception cref="TooManyRegistrationException">
        /// 指定された名前をもつコンポーネントが複数あった場合に発生する例外
        /// </exception>
        /// <exception cref="ComponentNotFoundException">
        /// 指定された名前をもつコンポーネントがなかった場合に発生する例外
        /// </exception>
        public IComponentEntry GetComponentEntry(string componentName)
        {
            if (CheckTooManyRegistration(componentName))
            {
                throw new TooManyRegistrationException();
            }

            foreach (IComponentEntry entry in primaryComponentEntries.Values)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0)
                {
                    return entry;
                }
            }

            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0)
                {
                    return entry;
                }
            }

            foreach (IComponentContainer child in children)
            {
                try
                {
                    return child.GetComponentEntry(componentName);
                }
                catch (ComponentNotFoundException)
                {
                    // 何もなし
                }
            }

            throw new ComponentNotFoundException();
        }

        /// <summary>
        /// 指定されたインターフェイスを持つコンポーネントが複数登録されてないかチェックする
        /// </summary>
        /// <param name="interfaceType">チェックするインターフェイス</param>
        /// <returns>指定されたインターフェイスを持つコンポーネントがすでに複数登録されていないかどうか</returns>
        private bool CheckTooManyRegistration(Type interfaceType)
        {
            int count = 0;
            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (interfaceType.IsAssignableFrom(entry.ComponentType)) 
                {
                    ++count;
                }
            }

            // ややトリッキーなカウントの仕方だけど...
            // 子のIComponentContainer#GetComponentEntryを呼び出すことで
            // 子がIComponentContainerImpl#CheckTooManyRegistrationで
            // 重複テストをやり、重複があれば例外を投げる
            foreach (IComponentContainer child in children)
            {
                try
                {
                    child.GetComponentEntry(interfaceType);
                    ++count;
                }
                catch (ComponentNotFoundException)
                {
                    // 何もなし
                }
                catch (TooManyRegistrationException)
                {
                    return true;
                }
            }

            return count > 1;
        }

        /// <summary>
        /// 名前を持つコンポーネントが複数登録されてないかチェックする
        /// </summary>
        /// <param name="componentName">コンポーネントの名前</param>
        /// <returns>名前を持つコンポーネントがすでに複数登録されていないかどうか</returns>
        private bool CheckTooManyRegistration(string componentName)
        {
            int count = 0;
            foreach (IComponentEntry entry in primaryComponentEntries.Values)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0) 
                {
                    ++count;
                }
            }
            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0) 
                {
                    ++count;
                }
            }

            // ややトリッキーなカウントの仕方だけど...
            // 子のIComponentContainer#GetComponentEntryを呼び出すことで
            // 子がIComponentContainerImpl#CheckTooManyRegistrationで
            // 重複テストをやり、重複があれば例外を投げる
            foreach (IComponentContainer child in children)
            {
                try
                {
                    child.GetComponentEntry(componentName);
                    ++count;
                }
                catch (ComponentNotFoundException)
                {
                    // 何もなし
                }
                catch (TooManyRegistrationException)
                {
                    return true;
                }
            }

            return count > 1;
        }

        /// <summary>
        /// 登録されているコンポーネントのインスタンスを破棄する
        /// </summary>
        /// <remarks>
        /// 登録されているコンポーネントのうちSingletonでインスタンスが
        /// 管理されているもののインスタンスを破棄する。破棄するコンポーネントは
        /// IDisposableを実装していること。
        /// </remarks>
        public void Discard()
        {
            foreach (IComponentEntry entry in primaryComponentEntries.Values)
            {
                entry.Discard();
            }
            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                entry.Discard();
            }
        }

        /// <summary>
        /// すべての子コンテナを取得する
        /// </summary>
        /// <returns>子コンテナの配列</returns>
        public IComponentContainer[] GetChildren()
        {
            return (IComponentContainer[])children.ToArray(typeof(IComponentContainer));
        }

        /// <summary>
        /// 子コンテナを追加する
        /// </summary>
        /// <param name="child">追加する子コンテナ</param>
        public void AddChild(IComponentContainer child)
        {
            children.Add(child);
        }
    }
}
