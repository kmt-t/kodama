using System;

namespace Kodama.Script.Engine
{
    /// <summary>
    /// �X�N���v�g���R���p�C������Ă��Ȃ��̂Ɏ��s����悤�Ƃ����ꍇ�ɔ��������O
    /// </summary>
    public class NotCompiledException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public NotCompiledException() : base("NotCompiledException")
        {
        }
    }
}
