using System;

namespace Kodama.Script.Engine
{
    /// <summary>
    /// スクリプトのエントリーポイント(Main関数)が見つからない場合に発生する例外
    /// </summary>
    public class ScriptEntryPointNotFoundException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ScriptEntryPointNotFoundException() : base("EntryPointNotFoundException")
        {
        }
    }
}
