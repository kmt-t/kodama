using System;
using Kodama.Aop.JoinPoint;

namespace Kodama.Aop.Interceptor
{
    /// <summary>
    /// ConstructorInterceptor
    /// </summary>
    /// <remarks>
    /// ����ł�IConstructorInterceptor�͗��p�ł��܂���B
    /// </remarks>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IConstructorInterceptor : IInterceptor
    {
        /// <summary>
        /// JoinPoint���Ăяo�����ƁA���̃��\�b�h�����荞�݂܂�
        /// </summary>
        /// <param name="invocation">�Ăяo���ꂽ�R���X�g���N�^</param>
        /// <param name="outArguments">���\�b�h��out����</param>
        /// <returns>���\�b�h�̖߂�l</returns>
        object Construct(ConstructorInvocation invocation, out object[] outArguments);
    }
}
