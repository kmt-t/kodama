using System;

namespace Kodama.Function.Functor
{
    /// <summary>
    /// 関数オブジェクトが引数の数が違ったり、互換性のない型を渡されて
    /// 呼び出された場合に発生する例外
    /// </summary>
    public class UnmatchArgumentException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnmatchArgumentException() : base("UnmatchArgumentException")
        {
        }
    }
}
