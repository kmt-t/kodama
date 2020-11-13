using System;
using System.Runtime.Remoting.Messaging;

namespace Kodama.Aop.JoinPoint
{
    /// <summary>
    /// JoinPoin
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IJoinPoint
    {
        /// <summary>
        /// 最初のInterceptorのチェーンの呼び出しを行います
        /// </summary>
        /// <param name="outArgumemnts">メソッドのout引数</param>
        /// <returns>メソッドの戻り値</returns>
        /// <remarks>
        /// 複数のInterceptorが設定されている場合、Interceptorの数だけ
        /// IJoinPoint#Proceed→Interceptor#Invokeが再帰的に
        /// 繰り返し呼び出される。このメソッドはその最初の呼び出しを
        /// おこなう。基本的にメッセージシンクからしか呼ばれない。
        /// </remarks>
        object FirstProceed(out object[] outArgumemnts);

        /// <summary>
        /// Intercepterのチェーンの呼び出しを行います
        /// </summary>
        /// <param name="outArgumemnts">メソッドのout引数</param>
        /// <returns>メソッドの戻り値</returns>
        object Proceed(out object[] outArgumemnts);
    }
}
