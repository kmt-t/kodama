using System;
using System.ComponentModel;

namespace Kodama.Function.Functor
{
    /// <summary>
    /// 関数オブジェクトをデリゲートに変換したもの
    /// </summary>
    public delegate object FunctorHandler(params object[] arguments);

    /// <summary>
    /// 関数オブジェクトインターフェイス
    /// </summary>
    /// <remarks>
    /// C++でいう関数オブジェクトの基底となるインターフェイス。
    /// </remarks>
    public interface IFunctor
    {
        /// <summary>
        /// 関数オブジェクトをデリゲートに変換して返す
        /// </summary>
        /// <returns>関数オブジェクトから変換されたデリゲート</returns>
        FunctorHandler ConvertToDelegate();

        /// <summary>
        /// 関数オブジェクトの呼び出し
        /// </summary>
        /// <param name="arguments">関数オブジェクトの処理の引数</param>
        /// <returns>関数オブジェクトの処理の戻り値</returns>
        object Invoke(params object[] arguments);

        /// <summary>
        /// 関数オブジェクトの呼び出し
        /// </summary>
        /// <returns>関数オブジェクトの処理の戻り値</returns>
        object Invoke();
    }
}
