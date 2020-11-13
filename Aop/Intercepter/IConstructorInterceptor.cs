using System;
using Kodama.Aop.JoinPoint;

namespace Kodama.Aop.Interceptor
{
    /// <summary>
    /// ConstructorInterceptor
    /// </summary>
    /// <remarks>
    /// 現状ではIConstructorInterceptorは利用できません。
    /// </remarks>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IConstructorInterceptor : IInterceptor
    {
        /// <summary>
        /// JoinPointが呼び出されると、このメソッドが割り込みます
        /// </summary>
        /// <param name="invocation">呼び出されたコンストラクタ</param>
        /// <param name="outArguments">メソッドのout引数</param>
        /// <returns>メソッドの戻り値</returns>
        object Construct(ConstructorInvocation invocation, out object[] outArguments);
    }
}
