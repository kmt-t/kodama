using System;
using Kodama.Aop.JoinPoint;

namespace Kodama.Aop.Interceptor.Delegate
{
    /// <summary>
    /// Intercept���s���f���Q�[�g
    /// </summary>
    public delegate object InterceptorHandler(MethodInvocation invocation, out object[] outArguments);

    /// <summary>
    /// �f���Q�[�g�ɂ��Interceptor
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class DelegateInterceptor : IMethodInterceptor
    {
        /// <summary>
        /// Intercept���s���f���Q�[�g
        /// </summary>
        private InterceptorHandler interceptorHandler;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="ih">Intercept���s���f���Q�[�g</param>
        public DelegateInterceptor(InterceptorHandler ih)
        {
            interceptorHandler = ih;
        }

        /// <summary>
        /// JoinPoint���Ăяo�����ƁA���̃��\�b�h�����荞�݂܂�
        /// </summary>
        /// <param name="invocation">�Ăяo���ꂽ���\�b�h</param>
        /// <param name="outArguments">���\�b�h��out����</param>
        /// <returns>���\�b�h�̖߂�l</returns>
        public object Invoke(MethodInvocation invocation, out object[] outArguments)
        {
            return interceptorHandler(invocation, out outArguments);
        }
    }
}
