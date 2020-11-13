using System;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// DependencyInjectionコンテナの基底インターフェイスです
    /// </summary>
    /// <remarks>
    /// このインターフェイスはImmutableです。
    /// </remarks>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public interface IComponentContainer
    {
        /// <summary>
        /// コンポーネントを取得する
        /// </summary>
        /// <param name="componentType">取得するコンポーネントの型</param>
        /// <returns>引数にわたされた型のコンポーネント</returns>
        object GetComponent(Type componentType);

        /// <summary>
        /// コンポーネントを取得する
        /// </summary>
        /// <param name="componentName">取得するコンポーネントの名前</param>
        /// <returns>引数にわたされた名前のコンポーネント</returns>
        object GetComponent(string componentName);

        /// <summary>
        /// コンポーネントが登録されているかチェックする
        /// </summary>
        /// <param name="componentType">登録されているか確認するコンポーネントの型</param>
        /// <returns>コンポーネントが登録されているかどうか</returns>
        bool Contains(Type componentType);

        /// <summary>
        /// コンポーネントが登録されているかチェックする
        /// </summary>
        /// <param name="componentName">登録されているか確認するコンポーネントの名前</param>
        /// <returns>コンポーネントが登録されているかどうか</returns>
        bool Contains(string componentName);

        /// <summary>
        /// 指定されたインターフェイスを実装するコンポーネントの登録情報を取得する
        /// </summary>
        /// <param name="interfaceType">取得する実装コンポーネントのインターフェイス</param>
        /// <returns>指定されたインターフェイスを実装するコンポーネントの登録情報</returns>
        IComponentEntry GetComponentEntry(Type interfaceType);

        /// <summary>
        /// 指定された名前をもつコンポーネントの登録情報を取得する
        /// </summary>
        /// <param name="componentName">取得するコンポーネントの名前</param>
        /// <returns>指定された名前をもつコンポーネントの登録情報</returns>
        IComponentEntry GetComponentEntry(string componentName);

        /// <summary>
        /// すべての子コンテナを取得する
        /// </summary>
        /// <returns>子コンテナの配列</returns>
        IComponentContainer[] GetChildren();
    }
}
