using System;

namespace Kodama.Aop.Weaver
{
    /// <summary>
    /// Aspect��Weave�ł��Ȃ��ꍇ�ɔ��������O
    /// </summary>
    public class CanNotWeaveAspectException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public CanNotWeaveAspectException() : base("CanNotWeaveAspectException")
        {
        }
    }
}
