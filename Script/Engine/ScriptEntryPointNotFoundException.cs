using System;

namespace Kodama.Script.Engine
{
    /// <summary>
    /// �X�N���v�g�̃G���g���[�|�C���g(Main�֐�)��������Ȃ��ꍇ�ɔ��������O
    /// </summary>
    public class ScriptEntryPointNotFoundException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ScriptEntryPointNotFoundException() : base("EntryPointNotFoundException")
        {
        }
    }
}
