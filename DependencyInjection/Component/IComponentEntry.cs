using System;
using System.Collections;
using System.Reflection;
using Kodama.Function.Functor;
using Kodama.Function.Functor.Bind;
using Kodama.Function.Functor.Member;
using Kodama.DependencyInjection.Container;

namespace Kodama.DependencyInjection.Component
{
    /// <summary>
    /// DependencyInjectionコンテナに登録されているコンポーネント情報
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public interface IComponentEntry
    {
        /// <summary>
        /// 登録されているコンポーネントの型
        /// </summary>
        Type ComponentType
        {
            get;
        }

        /// <summary>
        /// 登録されているコンポーネントの名前
        /// </summary>
        string ComponentName
        {
            get;
        }

        /// <summary>
        /// 依存性を注入するためのコンストラクタの引数をバインドしたファンクタ
        /// </summary>
        BindFunctor InjectionConstructor
        {
            set;
        }

        /// <summary>
        /// 依存性を注入するための引数をバインド済みの関数オブジェクトを追加する
        /// </summary>
        /// <param name="functor">追加する関数オブジェクト</param>
        void AddInjectionFanctor(BindFunctor functor);

        /// <summary>
        /// コンポーネントを初期化するための引数をバインド済みの関数オブジェクトを追加する
        /// </summary>
        /// <param name="functor">追加する関数オブジェクト</param>
        void AddInitializationFactor(IFunctor functor);

        /// <summary>
        /// コンポーネントが依存しているコンポーネントの登録情報を返す
        /// </summary>
        /// <returns>コンポーネントが依存しているコンポーネントの登録情報</returns>
        IComponentEntry[] GetDependencies();

        /// <summary>
        /// コンポーネントのインスタンスを返す
        /// </summary>
        /// <returns>コンポーネントのインスタンス</returns>
        /// <remarks>
        /// このメソッドで新しいインスタンスを返すか既存のインスタンスを返すかは
        /// 実装で選択します。
        /// </remarks>
        object GetInstance();

        /// <summary>
        /// コンポーネントのインスタンスを破棄する
        /// </summary>
        /// <remarks>
        /// 破棄するコンポーネントはIDisposableを実装していること。
        /// </remarks>
        void Discard();
    }
}
