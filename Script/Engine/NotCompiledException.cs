using System;

namespace Kodama.Script.Engine
{
    /// <summary>
    /// スクリプトがコンパイルされていないのに実行されようとした場合に発生する例外
    /// </summary>
    public class NotCompiledException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NotCompiledException() : base("NotCompiledException")
        {
        }
    }
}
