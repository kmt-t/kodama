using System;
using Kodama.DependencyInjection.Container;

namespace Kodama.DependencyInjection.Builder
{
    /// <summary>
    /// DependencyInjection�R���e�i�𐶐����A����ɃR���|�[�l���g��o�^���A���̃C���X�^���X��Ԃ��r���_
    /// </summary>
    public interface IComponentContainerBuilder
    {
        /// <summary>
        /// DependencyInjection�R���e�i�𐶐����A����ɃR���|�[�l���g��o�^���A���̃C���X�^���X��Ԃ�
        /// </summary>
        /// <returns>�������ꂽDependencyInjection�R���e�i</returns>
        IComponentContainer Build();
    }
}
