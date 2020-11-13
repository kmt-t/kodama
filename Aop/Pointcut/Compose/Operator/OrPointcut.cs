using System;
using System.Reflection;
using Kodama.Aop.Pointcut;
using Kodama.Aop.Pointcut.Compose;

namespace Kodama.Aop.Pointcut.Compose.Operator
{
    /// <summary>
    /// 2つのPointcutを論理演算orで合成します
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class OrPointcut : ComposiblePointcut
    {
        /// <summary>
        /// 左辺値のPointcut
        /// </summary>
        private IPointcut left;

        /// <summary>
        /// 右辺値のPointcut
        /// </summary>
        private IPointcut right;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="l">右辺値となるPointcut</param>
        /// <param name="r">左辺値となるPointcut</param>
        public OrPointcut(IPointcut l, IPointcut r)
        {
            left  = l;
            right = r;
        }

        /// <summary>
        /// 指定されたメソッドがアスペクトの対象となるかチェックします
        /// </summary>
        /// <param name="method">チェックするメソッド</param>
        /// <returns>指定されたメソッドがアスペクトの対象となるかどうか</returns>
        public override bool IsApplied(MethodBase method)
        {
            return left.IsApplied(method) || right.IsApplied(method);
        }
    }
}
