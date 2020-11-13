using System;
using Kodama.Aop.Interceptor;
using Kodama.Aop.JoinPoint;
using Kodama.Aop.Pointcut;

namespace Kodama.Aop.Aspect
{
    /// <summary>
    /// IAspectインターフェイスの具象クラス。InterceptorとPointcutを結び付けます。
    /// </summary>
    /// <remarks>
    /// このクラスで定義したアスペクトをAspectWeaverでWeaveします。
    /// </remarks>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class AspectImpl : IAspect 
    {
        /// <summary>
        /// Interceptor = 割り込み処理
        /// </summary>
        private IInterceptor interceptor;

        /// <summary>
        /// Pointcut = 割り込みポイント
        /// </summary>
        private IPointcut pointcut;

        /// <summary>
        /// Aspectに結び付けられているInterceptor
        /// </summary>
        public IInterceptor Interceptor
        {
            get { return interceptor; }
        }

        /// <summary>
        /// Aspectに結び付けられているPointcut
        /// </summary>
        public IPointcut Pointcut
        {
            get { return pointcut; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="i">Intercepter = 割り込み処理</param>
        /// <param name="p">Pointcut = 割り込みポイント</param>
        public AspectImpl(IInterceptor i, IPointcut p) 
        {
            interceptor = i;
            pointcut    = p;
        }
    }
}