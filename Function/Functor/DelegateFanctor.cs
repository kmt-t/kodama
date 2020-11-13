using System;

namespace Kodama.Function.Functor
{
    /// <summary>
    /// �f���Q�[�g���֐��I�u�W�F�N�g�ɕϊ��������̂ł�
    /// </summary>
    public class DelegateFunctor : IFunctor
    {
        /// <summary>
        /// �֐��I�u�W�F�N�g�̌Ăяo���͓����I�ɂ͂��̃f���Q�[�g����������
        /// </summary>
        private FunctorHandler internalDelegate;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="initialDelegate">�֐��I�u�W�F�N�g���Ăяo�����Ƃ��Ɏ��s�����f���Q�[�g</param>
        public DelegateFunctor(FunctorHandler initialDelegate)
        {
            internalDelegate = initialDelegate;
        }

        /// <summary>
        /// �֐��I�u�W�F�N�g���f���Q�[�g�ɕϊ����ĕԂ�
        /// </summary>
        /// <returns>�֐��I�u�W�F�N�g����ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorHandler ConvertToDelegate()
        {
            return internalDelegate;
        }

        /// <summary>
        /// �֐��I�u�W�F�N�g�̌Ăяo��
        /// </summary>
        /// <returns>�֐��I�u�W�F�N�g�̏����̖߂�l</returns>
        public object Invoke()
        {
            return internalDelegate(null);
        }

        /// <summary>
        /// �֐��I�u�W�F�N�g�̌Ăяo��
        /// </summary>
        /// <param name="arguments">�֐��I�u�W�F�N�g�̏����̈���</param>
        /// <returns>�֐��I�u�W�F�N�g�̏����̖߂�l</returns>
        public object Invoke(params object[] arguments)
        {
            return internalDelegate(arguments);
        }
    }
}
