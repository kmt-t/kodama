using System;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// �R���|�[�l���g�̎����o�^�ɑΉ������R���|�[�l���g�ɕt���鑮��
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegistratonComponentAttribute : Attribute
    {
        /// <summary>
        /// �o�^����R���|�[�l���g�̃J�e�S��
        /// </summary>
        /// <remarks>
        /// ����ɂ�莩���o�^�����R���|�[�l���g��I�ʂ��邱�Ƃ��ł���
        /// </remarks>
        private string componentCategory;

        /// <summary>
        /// �o�^����R���|�[�l���g�̃C���X�^���X����
        /// </summary>
        private ComponentInstanceMode instanceMode;

        /// <summary>
        /// �D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X�̌^
        /// </summary>
        private Type interfaceType;

        /// <summary>
        /// �o�^����R���|�[�l���g�̖��O
        /// </summary>
        private string componentName;

        /// <summary>
        /// �o�^����R���|�[�l���g�̃J�e�S��
        /// </summary>
        /// <remarks>
        /// ����ɂ�莩���o�^�����R���|�[�l���g��I�ʂ��邱�Ƃ��ł���
        /// </remarks>
        public string ComponentCategory
        {
            get { return componentCategory; }
        }

        /// <summary>
        /// �o�^����R���|�[�l���g�̃C���X�^���X����
        /// </summary>
        /// <remarks>
        /// ���̑����̖��O�t�������Ƃ��ė��p���܂�
        /// </remarks>
        public ComponentInstanceMode InstanceMode
        {
            get { return instanceMode; }
            set { instanceMode = value; }
        }

        /// <summary>
        /// �D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X�̌^
        /// </summary>
        /// <remarks>
        /// ���̑����̖��O�t�������Ƃ��ė��p���܂�
        /// </remarks>
        public Type InterfaceType
        {
            get { return interfaceType; }
            set { interfaceType = value; }
        }

        /// <summary>
        /// �o�^����R���|�[�l���g�̖��O
        /// </summary>
        /// <remarks>
        /// ���̑����̖��O�t�������Ƃ��ė��p���܂�
        /// </remarks>
        public string ComponentName
        {
            get { return componentName; }
            set { componentName = value; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="category">�R���|�[�l���g�̃J�e�S��</param>
        public AutoRegistratonComponentAttribute(string category)
        {
            componentCategory = category;
            instanceMode      = ComponentInstanceMode.Prototype;
            interfaceType     = null;
            componentName     = null;
        }

        /// <summary>
        /// �R���|�[�l���g���R���e�i�ɓo�^����
        /// </summary>
        /// <param name="container">�R���|�[�l���g��o�^����R���e�i</param>
        /// <param name="type">�o�^����R���|�[�l���g</param>
        public void Register(IMutableComponentContainer container, Type type)
        {
            IComponentEntry entry = null;
            switch (instanceMode)
            {
                case ComponentInstanceMode.Prototype :
                    if (componentName == null)
                    {
                        entry = new PrototypeComponentEntry(container, type);
                    }
                    else
                    {
                        entry = new PrototypeComponentEntry(container, type, componentName);
                    }
                    break;
                case ComponentInstanceMode.Singleton :
                    if (componentName == null)
                    {
                        entry = new SingletonComponentEntry(container, type);
                    }
                    else
                    {
                        entry = new SingletonComponentEntry(container, type, componentName);
                    }
                    break;
                default :
                    throw new InvlidComponentInstanceModeException();
            }

            if (interfaceType == null)
            {
                container.Register(entry);
            }
            else
            {
                container.Register(interfaceType, entry);
            }
        }
    }
}
