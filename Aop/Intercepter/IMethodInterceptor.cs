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
        /// JoinPointが呼び出されると、このメソッドが割り込みます
        /// </summary>
        /// <param name="invocation">呼び出されたメソッド</param>
        /// <param name="outArguments">メソッドのout引数</param>
        /// <returns>メソッドの戻り値</returns>
        object Invoke(MethodInvocation invocation, out object[] outArguments);
    }
}
