import System;
import System.Reflection;
import System.Runtime.Remoting;
import System.Runtime.Remoting.Messaging;
import System.Text;
import Kodama.Aop.JoinPoint;

package Kodama.DbC.Constraint
{
    /// <summary>
    /// 不変条件
    /// </summary>
    public AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)
    class InvariantConditionAttribute extends Attribute
    {
        /// <summary>
        /// 式
        /// </summary>
        private var expression : String;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="exp">不変条件の式</param>
        /// <remarks>
        /// 式はJScript形式で指定します。
        /// </remarks>
        public function InvariantConditionAttribute(exp : String)
        {
            expression = exp;
        }

        /// <summary>
        /// 不変条件の評価
        /// </summary>
        /// <param name="invocation">不変条件を判定するメソッド</param>
        /// <returns>
        /// 不変条件を満たすかどうか
        /// </returns>
        public function CheckConstraint(invocation : MethodInvocation) : boolean
        {
            var result : boolean = false;

            var script : StringBuilder = new StringBuilder(1024);

            script.Append("var target = invocation.Target;\n");
            script.Append("result = (" + expression + ");\n");

            eval(script.ToString(), "unsafe");

            return result;
        }
    }
}


