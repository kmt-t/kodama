using System;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// �s���Ȏ����o�^����R���|�[�l���g�̃C���X�^���X�������ݒ肳�ꂽ�Ƃ��ɔ��������O
    /// </summary>
    public class InvlidComponentInstanceModeException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public InvlidComponentInstanceModeException() :
            base("InvlidComponentInstanceModeException")
        {
        }
    }
}
