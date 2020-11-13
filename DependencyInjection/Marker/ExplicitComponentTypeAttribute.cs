using System;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// InjectionPointAttribute�����Ń}�[�L���O���ꂽ���\�b�h�́A����
    /// �C���W�F�N�V�������������ɂ��̑��������Ă����ꍇ�A����������
    /// �^�̃R���|�[�l���g���C���W�F�N�V��������܂�
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ExplicitComponentTypeAttribute : ExplicitComponentAttribute
    {
        /// <summary>
        /// �����C���W�F�N�V���������R���|�[�l���g�̌^
        /// </summary>
        private Type componentType;

        /// <summary>
        /// �����C���W�F�N�V���������R���|�[�l���g�̌^
        /// </summary>
        public Type ComponentType
        {
            get { return componentType; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="type">
        /// �����C���W�F�N�V���������R���|�[�l���g�̌^
        /// </param>
        public ExplicitComponentTypeAttribute(Type type)
        {
            componentType = type;
        }

        /// <summary>
        /// �ˑ����𒍓�����IArgumentComponentProvider�𐶐�����
        /// </summary>
        /// <param name="container">�ˑ����𒍓�����̂ɂ���IComponentContainer�̃C���X�^���X</param>
        /// <returns>�ˑ����𒍓�����IArgumentComponentProvider�̃C���X�^���X</returns>
        public override IArgumentComponentProvider CreateProvider(IComponentContainer container)
        {
            return new TypedArgumentComponentProvider(container, componentType);
        }
    }
}
