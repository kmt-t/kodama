using System;
using Kodama.Function.Functor.Bind;
using Kodama.DependencyInjection.Container;

namespace Kodama.DependencyInjection.Component
{
    /// <summary>
    /// �o�C���h���������BindFunctor#Invoke�Ăяo�����ɖ���
    /// �R���|�[�l���g�̌^���L�[��IComponentContainer#GetComponent
    /// �Œ񋟂���N���X
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public class TypedArgumentComponentProvider : IArgumentComponentProvider
    {
        /// <summary>
        /// �R���|�[�l���g���o�^����Ă���R���e�i
        /// </summary>
        private IComponentContainer componentContainer;

        /// <summary>
        /// �R���|�[�l���g�̌^
        /// </summary>
        /// <remarks>
        /// ���̌^��IComponentContainer#GetComponent���܂��B
        /// </remarks>
        private Type componentType;

        /// <summary>
        /// �R���|�[�l���g���o�^����Ă���R���e�i
        /// </summary>
        public IComponentContainer ComponentContainer
        {
            get { return componentContainer; }
        }

        /// <summary>
        /// �o�^����Ă���R���|�[�l���g���
        /// </summary>
        /// <remarks>
        /// ���̃v���p�e�B���Q�Ƃ��鎞�_�ŃR���|�[�l���g�̓R���e�i�ɓo�^����Ă��Ȃ���΂Ȃ�Ȃ��B
        /// </remarks>
        public IComponentEntry ComponentEntry
        {
            get { return componentContainer.GetComponentEntry(componentType); }
        }

        /// <summary>
        /// �R���|�[�l���g�̌^
        /// </summary>
        public Type ComponentType
        {
            get { return componentType; }
        }

        /// <summary>
        /// �R���|�[�l���g�̖��O
        /// </summary>
        /// <remarks>
        /// ���̃v���p�e�B���Q�Ƃ��鎞�_�ŃR���|�[�l���g�̓R���e�i�ɓo�^����Ă��Ȃ���΂Ȃ�Ȃ��B
        /// </remarks>
        public string ComponentName
        {
            get { return componentContainer.GetComponentEntry(componentType).ComponentName; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="container">�R���|�[�l���g���o�^����Ă���R���e�i</param>
        /// <param name="type">�R���|�[�l���g�̌^</param>
        public TypedArgumentComponentProvider(IComponentContainer container, Type type)
        {
            componentContainer = container;
            componentType      = type;
        }

        /// <summary>
        /// BindFunctor#Invoke���Ăяo���ꂽ�Ƃ���IComponentContainer#GetComponent���A�o�C���h���ꂽ�����Ƃ���
        /// </summary>
        /// <returns>�R���e�i����擾�����R���|�[�l���g�̃C���X�^���X</returns>
        public object Provide()
        {
            return componentContainer.GetComponent(componentType);
        }
    }
}
