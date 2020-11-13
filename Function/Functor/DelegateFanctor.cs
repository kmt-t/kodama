using System;

namespace Kodama.Function.Functor
{
    /// <summary>
    /// デリゲートを関数オブジェクトに変換したものです
    /// </summary>
    public class DelegateFunctor : IFunctor
    {
        /// <summary>
        /// 関数オブジェクトの呼び出しは内部的にはこのデリゲートが処理する
        /// </summary>
        private FunctorHandler internalDelegate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="initialDelegate">関数オブジェクトが呼び出されるときに実行されるデリゲート</param>
        public DelegateFunctor(FunctorHandler initialDelegate)
        {
            internalDelegate = initialDelegate;
        }

        /// <summary>
        /// 関数オブジェクトをデリゲートに変換して返す
        /// </summary>
        /// <returns>関数オブジェクトから変換されたデリゲート</returns>
        public FunctorHandler ConvertToDelegate()
        {
            return internalDelegate;
        }

        /// <summary>
        /// 関数オブジェクトの呼び出し
        /// </summary>
        /// <returns>関数オブジェクトの処理の戻り値</returns>
        public object Invoke()
        {
            return internalDelegate(null);
        }

        /// <summary>
        /// 関数オブジェクトの呼び出し
        /// </summary>
        /// <param name="arguments">関数オブジェクトの処理の引数</param>
        /// <returns>関数オブジェクトの処理の戻り値</returns>
        public object Invoke(params object[] arguments)
        {
            return internalDelegate(arguments);
        }
    }
}
