using System;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// �R���|�[�l���g���R���e�i�ɓo�^����Ă��Ȃ��Ƃ��ɔ��������O
    /// </summary>
    public class ComponentNotFoundException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ComponentNotFoundException() : base("ComponentNotFoundException")
        {
        }
    }
}
