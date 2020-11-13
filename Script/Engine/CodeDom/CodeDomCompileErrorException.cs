using System;
using System.Collections;
using System.CodeDom.Compiler;
using Kodama.Script.Engine;

namespace Kodama.Script.Engine.CodeDom
{
    /// <summary>
    /// スクリプトのコンパイルエラー例外
    /// </summary>
    public class CodeDomCompileErrorException : CompileErrorException
    {
        /// <summary>
        /// コンパイルエラー
        /// </summary>
        private CompileErrorInfo[] errors;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="results">CodeDomのコンパイル結果</param>
        public CodeDomCompileErrorException(CompilerResults results)
        {
            ArrayList list = new ArrayList();
            foreach (CompilerError err in results.Errors)
            {
                list.Add(new CompileErrorInfo(
                    err.FileName == null ? "" : err.FileName,
                    err.ErrorText,
                    err.Line,
                    ""));
            }
            errors = (CompileErrorInfo[])list.ToArray(typeof(CompileErrorInfo));
        }

        /// <summary>
        /// コンパイルエラーの配列を返す
        /// </summary>
        /// <returns>コンパイルエラーの配列</returns>
        public override CompileErrorInfo[] GetCompileErrorInfos()
        {
            return errors;
        }
    }
}
