using System;
using Kodama.Aop.JoinPoint;

namespace Kodama.Aop.Interceptor.Delegate
{
    /// <summary>
    /// Interceptを行うデリゲート
    /// </summary>
    public delegate object InterceptorHandler(MethodInvocation invocation, out object[] outArguments);

    /// <summary>
    /// デリゲートによるInterceptor
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class DelegateInterceptor : IMethodInterceptor
    {
        /// <summary>
        /// Interceptを行うデリゲート
        /// </summary>
        private InterceptorHandler interceptorHandler;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ih">Interceptを行うデリゲート</param>
        public DelegateInterceptor(InterceptorHandler ih)
        {
            interceptorHandler = ih;
        }

        /// <summary>
        /// JoinPointが呼び出されると、このメソッドが割り込みます
        /// </summary>
        /// <param name="invocation">呼び出されたメソッド</param>
        /// <param name="outArguments">メソッドのout引数</param>
        /// <returns>メソッドの戻り値</returns>
        public object Invoke(MethodInvocation invocation, out object[] outArguments)
        {
            return interceptorHandler(invocation, out outArguments);
        }
    }
}
