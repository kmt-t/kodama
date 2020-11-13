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
    /// 既にあるコンポーネントのインスタンスをDependencyInjectionコンテナに
    /// 登録するのに使用します。
    /// このコンポーネント情報で登録されているコンポーネントは、
    /// IComponentContainer#GetComponent毎に常に同じインスタンスを返す。
    /// </remarks>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public class OuterComponentEntry : IComponentEntry
    {
        /// <summary>
        /// 登録されているコンポーネントのインスタンス
        /// </summary>
        private object componentInstance;

        /// <summary>
        /// 登録されているコンポーネントの型
        /// </summary>
        private Type componentType;

        /// <summary>
        /// 登録されているコンポーネントの名前
        /// </summary>
        private string componentName;

        /// <summary>
        /// 既に依存性が注入されているかどうか
        /// </summary>
        private bool isSolvedDependency;

        /// <summary>
        /// 依存性を注入するための引数をバインド済みの関数オブジェクト
        /// </summary>
        private ArrayList injectionFactors = new ArrayList();

        /// <summary>
        /// コンポーネントを初期化するための引数をバインド済みの関数オブジェクト
        /// </summary>
        private ArrayList initializationFactors = new ArrayList();

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
        /// <exception cref="NotSupportedException">このメソッドはサポートしていないので必ずこの例外を投げる</exception>
        public BindFunctor InjectionConstructor
        {
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container">コンポーネントが登録するコンテナ</param>
        /// <param name="instance">登録するコンポーネントのインスタンス</param>
        public OuterComponentEntry(IComponentContainer container, object instance)
        {
            componentType      = instance.GetType();
            componentName      = instance.GetType().FullName;
            componentInstance  = instance;
            isSolvedDependency = false;

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
        /// <param name="instance">登録するコンポーネントのインスタンス</param>
        /// <param name="name">登録するコンポーネントの名前</param>
        public OuterComponentEntry(IComponentContainer container, object instance, string name)
        {
            componentType      = instance.GetType();
            componentName      = name;
            componentInstance  = instance;
            isSolvedDependency = false;

            foreach (MethodInfo mi in componentType.GetMethods())
            {
                if (Attribute.IsDefined(mi, typeof(InjectionPointAttribute)))
                {
                    ArrayList arguments = new ArrayList();
                    arguments.Add(new NotBoundArgument(0));
                    foreach (ParameterInfo param in mi.GetParameters())
                    {
                        arguments.Add(new TypedArgumentComponentProvider(container, param.ParameterType));
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
        /// コンポーネントのインスタンスを取得する
        /// </summary>
        /// <returns>コンポーネントのインスタンス</returns>
        /// <remarks>
        /// このメソッドで新しいインスタンスを返すか既存のインスタンスを返すかは
        /// 実装で選択します。
        /// </remarks>
        public object GetInstance()
        {
            if (!isSolvedDependency)
            {
                foreach (IFunctor functor in injectionFactors)
                {
                    functor.Invoke(componentInstance);
                }

                foreach (IFunctor functor in initializationFactors)
                {
                    functor.Invoke(componentInstance);
                }

                isSolvedDependency = true;
            }

            return componentInstance;
        }

        /// <summary>
        /// コンポーネントのインスタンスを破棄する
        /// </summary>
        /// <remarks>
        /// 破棄するコンポーネントはIDisposableを実装していること。
        /// </remarks>
        public void Discard()
        {
/*            IDisposable disposable = componentInstance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
                componentInstance = null;
                isSolvedDependency = false;
            }*/
        }
    }
}
