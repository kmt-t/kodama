using System;
using Kodama.Function.Functor.Bind;
using Kodama.DependencyInjection.Container;

namespace Kodama.DependencyInjection.Component
{
    /// <summary>
    /// バインドする引数をBindFunctor#Invoke呼び出し時に毎に
    /// コンポーネントの名前をキーにIComponentContainer#GetComponent
    /// で提供するクラス
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public class NamedArgumentComponentProvider : IArgumentComponentProvider
    {
        /// <summary>
        /// コンポーネントが登録されているコンテナ
        /// </summary>
        private IComponentContainer componentContainer;

        /// <summary>
        /// コンポーネントの名前
        /// </summary>
        /// <remarks>
        /// この名前でIComponentContainer#GetComponentします。
        /// </remarks>
        private string componentName;

        /// <summary>
        /// コンポーネントが登録されているコンテナ
        /// </summary>
        public IComponentContainer ComponentContainer
        {
            get { return componentContainer; }
        }

        /// <summary>
        /// 登録されているコンポーネント情報
        /// </summary>
        /// <remarks>
        /// このプロパティを参照する時点でコンポーネントはコンテナに登録されていなければならない。
        /// </remarks>
        public IComponentEntry ComponentEntry
        {
            get { return componentContainer.GetComponentEntry(componentName); }
        }

        /// <summary>
        /// コンポーネントの型
        /// </summary>
        /// <remarks>
        /// このプロパティを参照する時点でコンポーネントはコンテナに登録されていなければならない。
        /// </remarks>
        public Type ComponentType
        {
            get { return componentContainer.GetComponentEntry(componentName).ComponentType; }
        }

        /// <summary>
        /// コンポーネントの名前
        /// </summary>
        public string ComponentName
        {
            get { return componentName; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container">コンポーネントが登録されているコンテナ</param>
        /// <param name="name">コンポーネントの名前</param>
        public NamedArgumentComponentProvider(IComponentContainer container, string name)
        {
            componentContainer = container;
            componentName      = name;
        }

        /// <summary>
        /// BindFunctor#Invokeが呼び出されたときにIComponentContainer#GetComponentし、バインドされた引数とする
        /// </summary>
        /// <returns>コンテナから取得したコンポーネントのインスタンス</returns>
        public object Provide()
        {
            return componentContainer.GetComponent(componentName);
        }
    }
}
