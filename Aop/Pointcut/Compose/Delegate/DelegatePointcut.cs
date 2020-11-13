using System;
using System.Reflection;

namespace Kodama.Aop.Pointcut.Compose.Delegate
{
    /// <summary>
    /// Pontcutのフィルタリングを行うデリゲート
    /// </summary>
    public delegate bool PointcutHandler(MethodBase method);

    /// <summary>
    /// デリゲートでフィルタリングするPointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class DelegatePointcut : ComposiblePointcut
    {
        /// <summary>
        /// フィルタリングするデリゲート
        /// </summary>
        private PointcutHandler pointcutHandler;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="ph">フィルタリングするデリゲート</param>
        public DelegatePointcut(PointcutHandler ph)
        {
            pointcutHandler = ph;
        }

        /// <summary>
        /// 指定されたメソッドがアスペクトの対象となるかチェックします
        /// </summary>
        /// <param name="method">チェックするメソッド</param>
        /// <returns>指定されたメソッドがアスペクトの対象となるかどうか</returns>
        public override bool IsApplied(MethodBase method)
        {
            return pointcutHandler(method);
        }
    }
}
