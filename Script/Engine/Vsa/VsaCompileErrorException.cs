using System;
using System.Collections;
using Microsoft.Vsa;
using Kodama.Script.Engine;

namespace Kodama.Script.Engine.Vsa
{
    /// <summary>
    /// スクリプトのコンパイルエラー例外
    /// </summary>
    public class VsaCompileErrorException : CompileErrorException
    {
        /// <summary>
        /// コンパイルエラー
        /// </summary>
        private CompileErrorInfo[] errors;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vsaErrs">IVsaSiteが報告したコンパイルエラー</param>
        public VsaCompileErrorException(IVsaError[] vsaErrs)
        {
            ArrayList list = new ArrayList();
            foreach (IVsaError err in vsaErrs)
            {
                list.Add(new CompileErrorInfo(
                    err.SourceItem == null ? "" : err.SourceItem.Name,
                    err.Description,
                    err.Line,
                    err.LineText));
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
