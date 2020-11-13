using System;
using System.Collections;
using Microsoft.Vsa;
using Kodama.Script.Engine;

namespace Kodama.Script.Engine.Vsa
{
    /// <summary>
    /// �X�N���v�g�̃R���p�C���G���[��O
    /// </summary>
    public class VsaCompileErrorException : CompileErrorException
    {
        /// <summary>
        /// �R���p�C���G���[
        /// </summary>
        private CompileErrorInfo[] errors;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="vsaErrs">IVsaSite���񍐂����R���p�C���G���[</param>
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
        /// �R���p�C���G���[�̔z���Ԃ�
        /// </summary>
        /// <returns>�R���p�C���G���[�̔z��</returns>
        public override CompileErrorInfo[] GetCompileErrorInfos()
        {
            return errors;
        }
    }
}
