using System;
using Kodama.Function.Functor.Bind;
using Kodama.DependencyInjection.Container;

namespace Kodama.DependencyInjection.Component
{
    /// <summary>
    /// バインドする引数をBindFunctor#Invoke呼び出し時に毎に
    /// IComponentContainer#GetComponentで提供するインターフェイス
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public interface IArgumentComponentProvider : IArgumentProvider
    {
        /// <summary>
        /// コンポーネントが登録されているコンテナ
        /// </summary>
        IComponentContainer ComponentContainer
        {
            get;
        }

        /// <summary>
        /// 登録されているコンポーネント情報
        /// </summary>
        IComponentEntry ComponentEntry
        {
            get;
        }

        /// <summary>
        /// コンポーネントの型
        /// </summary>
        Type ComponentType
        {
            get;
        }

        /// <summary>
        /// コンポーネントの名前
        /// </summary>
        string ComponentName
        {
            get;
        }
    }
}
