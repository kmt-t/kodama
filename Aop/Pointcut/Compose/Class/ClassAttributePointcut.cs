using System;
using System.Reflection;
using Kodama.Aop.Pointcut.Compose;

namespace Kodama.Aop.Pointcut.Compose.Class
{
    /// <summary>
    /// 属性でクラスをフィルタリングするPointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class ClassAttributePointcut : ComposiblePointcut
    {
        /// <summary>
        /// フィルタリングする属性
        /// </summary>
        private Type attributeType;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="attrType">フィルタリングする属性</param>
        public ClassAttributePointcut(Type attrType)
        {
            attributeType = attrType;
        }

        /// <summary>
        /// 指定されたメソッドがアスペクトの対象となるかチェックします
        /// </summary>
        /// <param name="method">チェックするメソッド</param>
        /// <returns>指定されたメソッドがアスペクトの対象となるかどうか</returns>
        public override bool IsApplied(MethodBase method)
        {
            return Attribute.IsDefined(method.ReflectedType, attributeType);
        }
    }
}
