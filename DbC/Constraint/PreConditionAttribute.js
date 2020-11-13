import System;
import System.Reflection;
import System.Runtime.Remoting;
import System.Runtime.Remoting.Messaging;
import System.Text;
import Kodama.Aop.JoinPoint;

package Kodama.DbC.Constraint
{
    /// <summary>
    /// 事前条件
    /// </summary>
    public AttributeUsage(AttributeTargets.Method)
    class PreConditionAttribute extends Attribute
    {
        /// <summary>
        /// 式
        /// </summary>
        private var expression : String;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="exp">事前条件の式</param>
        /// <remarks>
        /// 式はJScript形式で指定します。
        /// </remarks>
        public function PreConditionAttribute(exp : String)
        {
            expression = exp;
        }

        /// <summary>
        /// 事前条件の評価
        /// </summary>
        /// <param name="invocation">事前条件を判定するメソッド</param>
        /// <returns>
        /// 事前条件を満たすかどうか
        /// </returns>
        public function CheckConstraint(invocation : MethodInvocation) : boolean
        {
            var result : boolean = false;

            var script : StringBuilder = new StringBuilder(1024);

            script.Append("var target = invocation.Target;\n");

            for (var i : int = 0; i < invocation.Message.InArgCount; ++i)
            {
                script.Append(
                    "var " +
                    invocation.Message.GetInArgName(i) +
                    " = invocation.Message.GetInArg(" + i.ToString() + ");\n");
            }

            script.Append("result = (" + expression + ");\n");

            eval(script.ToString(), "unsafe");

            return result;
        }
    }
}

