using System;
using Kodama.Function.Functor.Bind;
using Kodama.DependencyInjection.Container;

namespace Kodama.DependencyInjection.Component
{
    /// <summary>
    /// バインドする引数をBindFunctor#Invoke呼び出し時に毎に
    /// コンポーネントの型をキーにIComponentContainer#GetComponent
    /// で提供するクラス
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public class TypedArgumentComponentProvider : IArgumentComponentProvider
    {
        /// <summary>
        /// コンポーネントが登録されているコンテナ
        /// </summary>
        private IComponentContainer componentContainer;

        /// <summary>
        /// コンポーネントの型
        /// </summary>
        /// <remarks>
        /// この型でIComponentContainer#GetComponentします。
        /// </remarks>
        private Type componentType;

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
            get { return componentContainer.GetComponentEntry(componentType); }
        }

        /// <summary>
        /// コンポーネントの型
        /// </summary>
        public Type ComponentType
        {
            get { return componentType; }
        }

        /// <summary>
        /// コンポーネントの名前
        /// </summary>
        /// <remarks>
        /// このプロパティを参照する時点でコンポーネントはコンテナに登録されていなければならない。
        /// </remarks>
        public string ComponentName
        {
            get { return componentContainer.GetComponentEntry(componentType).ComponentName; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="container">コンポーネントが登録されているコンテナ</param>
        /// <param name="type">コンポーネントの型</param>
        public TypedArgumentComponentProvider(IComponentContainer container, Type type)
        {
            componentContainer = container;
            componentType      = type;
        }

        /// <summary>
        /// BindFunctor#Invokeが呼び出されたときにIComponentContainer#GetComponentし、バインドされた引数とする
        /// </summary>
        /// <returns>コンテナから取得したコンポーネントのインスタンス</returns>
        public object Provide()
        {
            return componentContainer.GetComponent(componentType);
        }
    }
}
