using System;
using System.Reflection;
using Kodama.Aop.Pointcut;
using Kodama.Aop.Pointcut.Compose.Operator;

namespace Kodama.Aop.Pointcut.Compose
{
    /// <summary>
    /// 論理演算による合成が可能なPointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public abstract class ComposiblePointcut : IPointcut
    {
        /// <summary>
        /// 指定されたメソッドがアスペクトの対象となるかチェックします
        /// </summary>
        /// <param name="method">チェックするメソッド</param>
        /// <returns>指定されたメソッドがアスペクトの対象となるかどうか</returns>
        public abstract bool IsApplied(MethodBase method);

        /// <summary>
        /// Pointcutの論理を反転します
        /// </summary>
        /// <param name="pointcut">論理を反転するPointcut</param>
        /// <returns>論理が反転されたPointcut</returns>
        public static ComposiblePointcut operator ! (ComposiblePointcut pointcut)
        {
            return new NotPointcut(pointcut);
        }

        /// <summary>
        /// 2つのPointcutの論理演算andをとるPointcutを返します
        /// </summary>
        /// <param name="left">右辺値のPointcut</param>
        /// <param name="right">左辺値のPointcut</param>
        /// <returns>2つのPointcutの論理演算andをとるPointcut</returns>
        public static ComposiblePointcut operator & (ComposiblePointcut left, ComposiblePointcut right)
        {
            return new AndPointcut(left, right);
        }

        /// <summary>
        /// 2つのPointcutの論理演算orをとるPointcutを返します
        /// </summary>
        /// <param name="left">右辺値のPointcut</param>
        /// <param name="right">左辺値のPointcut</param>
        /// <returns>2つのPointcutの論理演算orをとるPointcut</returns>
        public static ComposiblePointcut operator | (ComposiblePointcut left, ComposiblePointcut right)
        {
            return new OrPointcut(left, right);
        }
    }
}
