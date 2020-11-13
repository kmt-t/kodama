using System;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// DependencyInjection�R���e�i�̊��C���^�[�t�F�C�X�ł�
    /// </summary>
    /// <remarks>
    /// ���̃C���^�[�t�F�C�X��Mutable�ł��B
    /// </remarks>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public interface IMutableComponentContainer : IComponentContainer
    {
        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <param name="componentType">�o�^����R���|�[�l���g�̌^</param>
        void Register(Type componentType);

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <param name="componentInstance">�o�^����R���|�[�l���g�̃C���X�^���X</param>
        void RegisterInstance(object componentInstance);

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <param name="componentType">�o�^����R���|�[�l���g�̌^</param>
        /// <param name="componentName">�o�^����R���|�[�l���g�̖��O</param>
        void Register(Type componentType, string componentName);

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <param name="componentInstance">�o�^����R���|�[�l���g�̃C���X�^���X</param>
        /// <param name="componentName">�o�^����R���|�[�l���g�̖��O</param>
        void RegisterInstance(object componentInstance, string componentName);

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <param name="componentEntry">�o�^����R���|�[�l���g�̏��</param>
        void Register(IComponentEntry componentEntry);

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <remarks>
        /// ����̃C���^�[�t�F�C�X�ɗD�悵�Ċ��蓖�Ă�R���|�[�l���g���w�肵�܂��B
        /// </remarks>
        /// <param name="interfaceType">�D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X</param>
        /// <param name="implementComponentType">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̌^</param>
        void Register(Type interfaceType, Type implementComponentType);

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <remarks>
        /// ����̃C���^�[�t�F�C�X�ɗD�悵�Ċ��蓖�Ă�R���|�[�l���g���w�肵�܂��B
        /// </remarks>
        /// <param name="interfaceType">�D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X</param>
        /// <param name="implementComponentInstance">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̃C���X�^���X</param>
        void RegisterInstance(Type interfaceType, object implementComponentInstance);

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <remarks>
        /// ����̃C���^�[�t�F�C�X�ɗD�悵�Ċ��蓖�Ă�R���|�[�l���g���w�肵�܂��B
        /// </remarks>
        /// <param name="interfaceType">�D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X</param>
        /// <param name="implementComponentType">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̌^</param>
        /// <param name="componentName">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̖��O</param>
        void Register(Type interfaceType, Type implementComponentType, string componentName);

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <remarks>
        /// ����̃C���^�[�t�F�C�X�ɗD�悵�Ċ��蓖�Ă�R���|�[�l���g���w�肵�܂��B
        /// </remarks>
        /// <param name="interfaceType">�D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X</param>
        /// <param name="implementComponentInstance">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̃C���X�^���X</param>
        /// <param name="componentName">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̖��O</param>
        void RegisterInstance(Type interfaceType, object implementComponentInstance, string componentName);

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <remarks>
        /// ����̃C���^�[�t�F�C�X�ɗD�悵�Ċ��蓖�Ă�R���|�[�l���g���w�肵�܂��B
        /// </remarks>
        /// <param name="interfaceType">�D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X</param>
        /// <param name="implementComponentEntry">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̏��</param>
        void Register(Type interfaceType, IComponentEntry implementComponentEntry);

        /// <summary>
        /// �o�^����Ă���R���|�[�l���g�̃C���X�^���X��j������
        /// </summary>
        /// <remarks>
        /// �o�^����Ă���R���|�[�l���g�̂���Singleton�ŃC���X�^���X��
        /// �Ǘ�����Ă�����̂̃C���X�^���X��j������B�j������R���|�[�l���g��
        /// IDisposable���������Ă��邱�ƁB
        /// </remarks>
        void Discard();

        /// <summary>
        /// �q�R���e�i��ǉ�����
        /// </summary>
        /// <param name="child">�ǉ�����q�R���e�i</param>
        void AddChild(IComponentContainer child);
    }
}
