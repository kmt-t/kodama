using System;
using System.Collections;
using System.CodeDom.Compiler;
using Kodama.Script.Engine;

namespace Kodama.Script.Engine.CodeDom
{
    /// <summary>
    /// �X�N���v�g�̃R���p�C���G���[��O
    /// </summary>
    public class CodeDomCompileErrorException : CompileErrorException
    {
        /// <summary>
        /// �R���p�C���G���[
        /// </summary>
        private CompileErrorInfo[] errors;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="results">CodeDom�̃R���p�C������</param>
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
        /// �R���p�C���G���[�̔z���Ԃ�
        /// </summary>
        /// <returns>�R���p�C���G���[�̔z��</returns>
        public override CompileErrorInfo[] GetCompileErrorInfos()
        {
            return errors;
        }
    }
}
