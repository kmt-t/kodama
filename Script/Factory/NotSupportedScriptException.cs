using System;

namespace Kodama.Script.Factory
{
    /// <summary>
    /// �T�|�[�g���Ă��Ȃ��X�N���v�g�`���̏ꍇ�ɔ��������O
    /// </summary>
    public class NotSupportedScriptException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public NotSupportedScriptException() : base("NotSupportedScriptException")
        {
        }
    }
}
