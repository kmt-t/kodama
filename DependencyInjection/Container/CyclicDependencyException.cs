using System;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// �ˑ��֌W���z���Ă���Ƃ��ɔ��������O
    /// </summary>
    public class CyclicDependencyException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public CyclicDependencyException() : base("CyclicDependencyException")
        {
        }
    }
}
