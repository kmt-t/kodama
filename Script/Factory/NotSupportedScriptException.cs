using System;

namespace Kodama.Script.Factory
{
    /// <summary>
    /// サポートしていないスクリプト形式の場合に発生する例外
    /// </summary>
    public class NotSupportedScriptException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NotSupportedScriptException() : base("NotSupportedScriptException")
        {
        }
    }
}
