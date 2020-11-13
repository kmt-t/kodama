using System;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// DependencyInjectionコンテナの基底インターフェイスです
    /// </summary>
    /// <remarks>
    /// このインターフェイスはMutableです。
    /// </remarks>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public interface IMutableComponentContainer : IComponentContainer
    {
        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <param name="componentType">登録するコンポーネントの型</param>
        void Register(Type componentType);

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <param name="componentInstance">登録するコンポーネントのインスタンス</param>
        void RegisterInstance(object componentInstance);

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <param name="componentType">登録するコンポーネントの型</param>
        /// <param name="componentName">登録するコンポーネントの名前</param>
        void Register(Type componentType, string componentName);

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <param name="componentInstance">登録するコンポーネントのインスタンス</param>
        /// <param name="componentName">登録するコンポーネントの名前</param>
        void RegisterInstance(object componentInstance, string componentName);

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <param name="componentEntry">登録するコンポーネントの情報</param>
        void Register(IComponentEntry componentEntry);

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <remarks>
        /// 特定のインターフェイスに優先して割り当てるコンポーネントを指定します。
        /// </remarks>
        /// <param name="interfaceType">優先して割り当てるインターフェイス</param>
        /// <param name="implementComponentType">優先して割り当てるコンポーネントの型</param>
        void Register(Type interfaceType, Type implementComponentType);

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <remarks>
        /// 特定のインターフェイスに優先して割り当てるコンポーネントを指定します。
        /// </remarks>
        /// <param name="interfaceType">優先して割り当てるインターフェイス</param>
        /// <param name="implementComponentInstance">優先して割り当てるコンポーネントのインスタンス</param>
        void RegisterInstance(Type interfaceType, object implementComponentInstance);

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <remarks>
        /// 特定のインターフェイスに優先して割り当てるコンポーネントを指定します。
        /// </remarks>
        /// <param name="interfaceType">優先して割り当てるインターフェイス</param>
        /// <param name="implementComponentType">優先して割り当てるコンポーネントの型</param>
        /// <param name="componentName">優先して割り当てるコンポーネントの名前</param>
        void Register(Type interfaceType, Type implementComponentType, string componentName);

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <remarks>
        /// 特定のインターフェイスに優先して割り当てるコンポーネントを指定します。
        /// </remarks>
        /// <param name="interfaceType">優先して割り当てるインターフェイス</param>
        /// <param name="implementComponentInstance">優先して割り当てるコンポーネントのインスタンス</param>
        /// <param name="componentName">優先して割り当てるコンポーネントの名前</param>
        void RegisterInstance(Type interfaceType, object implementComponentInstance, string componentName);

        /// <summary>
        /// コンポーネントを登録する
        /// </summary>
        /// <remarks>
        /// 特定のインターフェイスに優先して割り当てるコンポーネントを指定します。
        /// </remarks>
        /// <param name="interfaceType">優先して割り当てるインターフェイス</param>
        /// <param name="implementComponentEntry">優先して割り当てるコンポーネントの情報</param>
        void Register(Type interfaceType, IComponentEntry implementComponentEntry);

        /// <summary>
        /// 登録されているコンポーネントのインスタンスを破棄する
        /// </summary>
        /// <remarks>
        /// 登録されているコンポーネントのうちSingletonでインスタンスが
        /// 管理されているもののインスタンスを破棄する。破棄するコンポーネントは
        /// IDisposableを実装していること。
        /// </remarks>
        void Discard();

        /// <summary>
        /// 子コンテナを追加する
        /// </summary>
        /// <param name="child">追加する子コンテナ</param>
        void AddChild(IComponentContainer child);
    }
}
