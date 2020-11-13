using System;

namespace Kodama.Aop.Weaver
{
    /// <summary>
    /// AspectがWeaveできない場合に発生する例外
    /// </summary>
    public class CanNotWeaveAspectException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CanNotWeaveAspectException() : base("CanNotWeaveAspectException")
        {
        }
    }
}
