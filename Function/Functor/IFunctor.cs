using System;
using System.ComponentModel;

namespace Kodama.Function.Functor
{
    /// <summary>
    /// �֐��I�u�W�F�N�g���f���Q�[�g�ɕϊ���������
    /// </summary>
    public delegate object FunctorHandler(params object[] arguments);

    /// <summary>
    /// �֐��I�u�W�F�N�g�C���^�[�t�F�C�X
    /// </summary>
    /// <remarks>
    /// C++�ł����֐��I�u�W�F�N�g�̊��ƂȂ�C���^�[�t�F�C�X�B
    /// </remarks>
    public interface IFunctor
    {
        /// <summary>
        /// �֐��I�u�W�F�N�g���f���Q�[�g�ɕϊ����ĕԂ�
        /// </summary>
        /// <returns>�֐��I�u�W�F�N�g����ϊ����ꂽ�f���Q�[�g</returns>
        FunctorHandler ConvertToDelegate();

        /// <summary>
        /// �֐��I�u�W�F�N�g�̌Ăяo��
        /// </summary>
        /// <param name="arguments">�֐��I�u�W�F�N�g�̏����̈���</param>
        /// <returns>�֐��I�u�W�F�N�g�̏����̖߂�l</returns>
        object Invoke(params object[] arguments);

        /// <summary>
        /// �֐��I�u�W�F�N�g�̌Ăяo��
        /// </summary>
        /// <returns>�֐��I�u�W�F�N�g�̏����̖߂�l</returns>
        object Invoke();
    }
}
