using System;

namespace Kodama.Function.Functor
{
    /// <summary>
    /// �֐��I�u�W�F�N�g�������̐����������A�݊����̂Ȃ��^��n�����
    /// �Ăяo���ꂽ�ꍇ�ɔ��������O
    /// </summary>
    public class UnmatchArgumentException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public UnmatchArgumentException() : base("UnmatchArgumentException")
        {
        }
    }
}
