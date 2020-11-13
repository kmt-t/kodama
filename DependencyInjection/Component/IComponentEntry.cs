using System;
using System.Collections;
using System.Reflection;
using Kodama.Function.Functor;
using Kodama.Function.Functor.Bind;
using Kodama.Function.Functor.Member;
using Kodama.DependencyInjection.Container;

namespace Kodama.DependencyInjection.Component
{
    /// <summary>
    /// DependencyInjection�R���e�i�ɓo�^����Ă���R���|�[�l���g���
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public interface IComponentEntry
    {
        /// <summary>
        /// �o�^����Ă���R���|�[�l���g�̌^
        /// </summary>
        Type ComponentType
        {
            get;
        }

        /// <summary>
        /// �o�^����Ă���R���|�[�l���g�̖��O
        /// </summary>
        string ComponentName
        {
            get;
        }

        /// <summary>
        /// �ˑ����𒍓����邽�߂̃R���X�g���N�^�̈������o�C���h�����t�@���N�^
        /// </summary>
        BindFunctor InjectionConstructor
        {
            set;
        }

        /// <summary>
        /// �ˑ����𒍓����邽�߂̈������o�C���h�ς݂̊֐��I�u�W�F�N�g��ǉ�����
        /// </summary>
        /// <param name="functor">�ǉ�����֐��I�u�W�F�N�g</param>
        void AddInjectionFanctor(BindFunctor functor);

        /// <summary>
        /// �R���|�[�l���g�����������邽�߂̈������o�C���h�ς݂̊֐��I�u�W�F�N�g��ǉ�����
        /// </summary>
        /// <param name="functor">�ǉ�����֐��I�u�W�F�N�g</param>
        void AddInitializationFactor(IFunctor functor);

        /// <summary>
        /// �R���|�[�l���g���ˑ����Ă���R���|�[�l���g�̓o�^����Ԃ�
        /// </summary>
        /// <returns>�R���|�[�l���g���ˑ����Ă���R���|�[�l���g�̓o�^���</returns>
        IComponentEntry[] GetDependencies();

        /// <summary>
        /// �R���|�[�l���g�̃C���X�^���X��Ԃ�
        /// </summary>
        /// <returns>�R���|�[�l���g�̃C���X�^���X</returns>
        /// <remarks>
        /// ���̃��\�b�h�ŐV�����C���X�^���X��Ԃ��������̃C���X�^���X��Ԃ�����
        /// �����őI�����܂��B
        /// </remarks>
        object GetInstance();

        /// <summary>
        /// �R���|�[�l���g�̃C���X�^���X��j������
        /// </summary>
        /// <remarks>
        /// �j������R���|�[�l���g��IDisposable���������Ă��邱�ƁB
        /// </remarks>
        void Discard();
    }
}
