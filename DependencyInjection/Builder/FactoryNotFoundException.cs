using System;

namespace Kodama.DependencyInjection.Builder
{
    /// <summary>
    /// �X�N���v�g����t�@�N�g���[���\�b�h���݂���Ȃ��ꍇ�ɔ��������O
    /// </summary>
    public class FactoryNotFoundException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public FactoryNotFoundException() : base("FactoryNotFoundException")
        {
        }
    }
}
