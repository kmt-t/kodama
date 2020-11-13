using System;
using Kodama.Aop.JoinPoint;

namespace Kodama.Aop.Interceptor
{
    /// <summary>
    /// MethodInterceptor
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IMethodInterceptor : IInterceptor
    {
        /// <summary>
        /// JoinPoint���Ăяo�����ƁA���̃��\�b�h�����荞�݂܂�
        /// </summary>
        /// <param name="invocation">�Ăяo���ꂽ���\�b�h</param>
        /// <param name="outArguments">���\�b�h��out����</param>
        /// <returns>���\�b�h�̖߂�l</returns>
        object Invoke(MethodInvocation invocation, out object[] outArguments);
    }
}
