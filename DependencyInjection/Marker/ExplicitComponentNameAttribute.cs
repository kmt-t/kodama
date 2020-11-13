using System;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// InjectionPointAttribute�����Ń}�[�L���O���ꂽ���\�b�h�́A����
    /// �C���W�F�N�V�������������ɂ��̑��������Ă����ꍇ�A����������
    /// ���O�̃R���|�[�l���g���C���W�F�N�V��������܂�
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ExplicitComponentNameAttribute : ExplicitComponentAttribute
    {
        /// <summary>
        /// �����C���W�F�N�V���������R���|�[�l���g�̖��O
        /// </summary>
        private string componentName;

        /// <summary>
        /// �����C���W�F�N�V���������R���|�[�l���g�̖��O
        /// </summary>
        public string ComponentName
        {
            get { return componentName; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="name">
        /// �����C���W�F�N�V���������R���|�[�l���g�̖��O
        /// </param>
        public ExplicitComponentNameAttribute(string name)
        {
            componentName = name;
        }

        /// <summary>
        /// �ˑ����𒍓�����IArgumentComponentProvider�𐶐�����
        /// </summary>
        /// <param name="container">�ˑ����𒍓�����̂ɂ���IComponentContainer�̃C���X�^���X</param>
        /// <returns>�ˑ����𒍓�����IArgumentComponentProvider�̃C���X�^���X</returns>
        public override IArgumentComponentProvider CreateProvider(IComponentContainer container)
        {
            return new NamedArgumentComponentProvider(container, componentName);
        }
    }
}
