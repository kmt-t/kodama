using System;
using System.Reflection;
using Kodama.Aop.Pointcut.Compose;

namespace Kodama.Aop.Pointcut.Compose.Operator
{
    /// <summary>
    /// Pointcutの論理を反転します
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class NotPointcut : ComposiblePointcut
    {
        /// <summary>
        /// 論理を反転するPointcut
        /// </summary>
        private IPointcut pointcut;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="p">右辺値となるPointcut</param>
        public NotPointcut(IPointcut p)
        {
            pointcut = p;
        }

        /// <summary>
        /// 指定されたメソッドがアスペクトの対象となるかチェックします
        /// </summary>
        /// <param name="method">チェックするメソッド</param>
        /// <returns>指定されたメソッドがアスペクトの対処となるかどうか</returns>
        public override bool IsApplied(MethodBase method)
        {
            return !pointcut.IsApplied(method);
        }
    }
}
