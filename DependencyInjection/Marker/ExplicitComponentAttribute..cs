using System;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// InjectionPointAttribute�����Ń}�[�L���O���ꂽ���\�b�h�́A����
    /// �C���W�F�N�V�������������ɂ��̑��������Ă����ꍇ�A����������
    /// IArgumentComponentProvider�������Ĉˑ�������������܂�
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public abstract class ExplicitComponentAttribute : Attribute
    {
        /// <summary>
        /// �ˑ����𒍓�����IArgumentComponentProvider�𐶐�����
        /// </summary>
        /// <param name="container">�ˑ����𒍓�����̂ɂ���IComponentContainer�̃C���X�^���X</param>
        /// <returns>�ˑ����𒍓�����IArgumentComponentProvider�̃C���X�^���X</returns>
        public abstract IArgumentComponentProvider CreateProvider(IComponentContainer container);
    }
}
