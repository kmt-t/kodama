using System;

namespace Kodama.Function.Functor
{
    /// <summary>
    /// �֐��I�u�W�F�N�g�̌Ăяo�����݊����̂Ȃ��^��Ԃ��Ă����ꍇ�ɔ��������O
    /// </summary>
    public class UnmatchReturnTypeException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public UnmatchReturnTypeException() : base("UnmatchResturnTypeException")
        {
        }
    }
}
