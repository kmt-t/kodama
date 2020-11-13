using System;
using Kodama.Aop.Interceptor;
using Kodama.Aop.JoinPoint;
using Kodama.Aop.Pointcut;

namespace Kodama.Aop.Aspect
{
    /// <summary>
    /// Aspectの基底インターフェイス
    /// </summary>
    /// <remarks>
    /// このインターフェイスで定義したアスペクトをAspectWeaverでWeaveします。
    /// </remarks>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IAspect
    {
        /// <summary>
        /// Aspectに結び付けられているInterceptor
        /// </summary>
        IInterceptor Interceptor
        {
            get;
        }

        /// <summary>
        /// Aspectに結び付けられているPointcut
        /// </summary>
        IPointcut Pointcut
        {
            get;
        }
    }
}
