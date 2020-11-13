using System;
using Kodama.Function.Functor.Bind;
using Kodama.DependencyInjection.Container;

namespace Kodama.DependencyInjection.Component
{
    /// <summary>
    /// �o�C���h���������BindFunctor#Invoke�Ăяo�����ɖ���
    /// IComponentContainer#GetComponent�Œ񋟂���C���^�[�t�F�C�X
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public interface IArgumentComponentProvider : IArgumentProvider
    {
        /// <summary>
        /// �R���|�[�l���g���o�^����Ă���R���e�i
        /// </summary>
        IComponentContainer ComponentContainer
        {
            get;
        }

        /// <summary>
        /// �o�^����Ă���R���|�[�l���g���
        /// </summary>
        IComponentEntry ComponentEntry
        {
            get;
        }

        /// <summary>
        /// �R���|�[�l���g�̌^
        /// </summary>
        Type ComponentType
        {
            get;
        }

        /// <summary>
        /// �R���|�[�l���g�̖��O
        /// </summary>
        string ComponentName
        {
            get;
        }
    }
}
