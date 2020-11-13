using System;

namespace Kodama.Aop.JoinPoint
{
    /// <summary>
    /// Invocation
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IInvocation : IJoinPoint
    {
        /// <summary>
        /// メソッド呼び出しオブジェクト
        /// </summary>
        object Target
        {
            get;
        }

        /// <summary>
        /// 呼び出しの引数
        /// </summary>
        object[] Arguments
        {
            get;
        }

        /// <summary>
        /// 呼び出しに特定の属性が結ぶ付けられているかどうかチェックする
        /// </summary>
        /// <param name="attrType">結びつきをチェックする属性の型</param>
        /// <returns>結び付けられているかどうか</returns>
        bool IsDefined(Type attrType);
    }
}
