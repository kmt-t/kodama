using System;

namespace Kodama.Script.Engine
{
    /// <summary>
    /// スクリプトのコンパイルエラー例外
    /// </summary>
    public abstract class CompileErrorException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CompileErrorException() : base("CompileErrorException")
        {
        }

        /// <summary>
        /// コンパイルエラー情報を返す
        /// </summary>
        /// <returns>コンパイルエラー情報</returns>
        public abstract CompileErrorInfo[] GetCompileErrorInfos();
    }
}
