using System;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// �����C���^�[�t�F�C�X���������Ă���R���|�[�l���g����������ꍇ�ɔ��������O
    /// </summary>
    public class TooManyRegistrationException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public TooManyRegistrationException() : base("TooManyRegistrationException")
        {
        }
    }
}
