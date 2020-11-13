using System;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// DependencyInjection�R���e�i�̊��C���^�[�t�F�C�X�ł�
    /// </summary>
    /// <remarks>
    /// ���̃C���^�[�t�F�C�X��Immutable�ł��B
    /// </remarks>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public interface IComponentContainer
    {
        /// <summary>
        /// �R���|�[�l���g���擾����
        /// </summary>
        /// <param name="componentType">�擾����R���|�[�l���g�̌^</param>
        /// <returns>�����ɂ킽���ꂽ�^�̃R���|�[�l���g</returns>
        object GetComponent(Type componentType);

        /// <summary>
        /// �R���|�[�l���g���擾����
        /// </summary>
        /// <param name="componentName">�擾����R���|�[�l���g�̖��O</param>
        /// <returns>�����ɂ킽���ꂽ���O�̃R���|�[�l���g</returns>
        object GetComponent(string componentName);

        /// <summary>
        /// �R���|�[�l���g���o�^����Ă��邩�`�F�b�N����
        /// </summary>
        /// <param name="componentType">�o�^����Ă��邩�m�F����R���|�[�l���g�̌^</param>
        /// <returns>�R���|�[�l���g���o�^����Ă��邩�ǂ���</returns>
        bool Contains(Type componentType);

        /// <summary>
        /// �R���|�[�l���g���o�^����Ă��邩�`�F�b�N����
        /// </summary>
        /// <param name="componentName">�o�^����Ă��邩�m�F����R���|�[�l���g�̖��O</param>
        /// <returns>�R���|�[�l���g���o�^����Ă��邩�ǂ���</returns>
        bool Contains(string componentName);

        /// <summary>
        /// �w�肳�ꂽ�C���^�[�t�F�C�X����������R���|�[�l���g�̓o�^�����擾����
        /// </summary>
        /// <param name="interfaceType">�擾��������R���|�[�l���g�̃C���^�[�t�F�C�X</param>
        /// <returns>�w�肳�ꂽ�C���^�[�t�F�C�X����������R���|�[�l���g�̓o�^���</returns>
        IComponentEntry GetComponentEntry(Type interfaceType);

        /// <summary>
        /// �w�肳�ꂽ���O�����R���|�[�l���g�̓o�^�����擾����
        /// </summary>
        /// <param name="componentName">�擾����R���|�[�l���g�̖��O</param>
        /// <returns>�w�肳�ꂽ���O�����R���|�[�l���g�̓o�^���</returns>
        IComponentEntry GetComponentEntry(string componentName);

        /// <summary>
        /// ���ׂĂ̎q�R���e�i���擾����
        /// </summary>
        /// <returns>�q�R���e�i�̔z��</returns>
        IComponentContainer[] GetChildren();
    }
}
