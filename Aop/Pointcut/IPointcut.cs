using System;
using System.Reflection;

namespace Kodama.Aop.Pointcut
{
    /// <summary>
    /// Pointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IPointcut
    {
        /// <summary>
        /// メソッドがPointcutの条件に一致するかどうかチェックする
        /// </summary>
        /// <param name="method">チェックするメソッド</param>
        /// <returns>メソッドがPointcutの条件に一致するかどうか</returns>
        bool IsApplied(MethodBase method);
    }
}
