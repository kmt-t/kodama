using System;

namespace Kodama.Script.Engine
{
    /// <summary>
    /// �X�N���v�g�̃R���p�C���G���[��O
    /// </summary>
    public abstract class CompileErrorException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public CompileErrorException() : base("CompileErrorException")
        {
        }

        /// <summary>
        /// �R���p�C���G���[����Ԃ�
        /// </summary>
        /// <returns>�R���p�C���G���[���</returns>
        public abstract CompileErrorInfo[] GetCompileErrorInfos();
    }
}
