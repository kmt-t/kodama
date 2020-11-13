using System;

namespace Kodama.Function.Functor
{
    /// <summary>
    /// 関数オブジェクトの呼び出しが互換性のない型を返してきた場合に発生する例外
    /// </summary>
    public class UnmatchReturnTypeException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnmatchReturnTypeException() : base("UnmatchResturnTypeException")
        {
        }
    }
}
