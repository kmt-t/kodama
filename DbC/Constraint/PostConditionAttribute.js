import System;
import System.Reflection;
import System.Runtime.Remoting;
import System.Runtime.Remoting.Messaging;
import System.Text;
import Kodama.Aop.JoinPoint;

package Kodama.DbC.Constraint
{
    /// <summary>
    /// 事後条件
    /// </summary>
    public AttributeUsage(AttributeTargets.Method)
    class PostConditionAttribute extends Attribute
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
        public function PostConditionAttribute(exp : String)
        {
            expression = exp;
        }

        /// <summary>
        /// 事後条件の評価
        /// </summary>
        /// <param name="invocation">事後条件を判定するメソッド</param>
        /// <param name="returnValue">事後条件を判定する戻り値</param>
        /// <param name="outArguments">事後条件を判定する出力引数</param>
        /// <returns>
        /// 事後条件を満たすかどうか
        /// </returns>
        public function CheckConstraint(invocation : MethodInvocation, returnValue : Object, outArguments : Object[]) : boolean
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

            script.Append("var ret = returnValue;\n");

            for (var j : int = 0; j < outArguments.Length; ++j)
            {
                script.Append("var outArg" + j.ToString() + " = outArguments[" + j.ToString() + "];\n");
            }

            script.Append("result = (" + expression + ");\n");

            eval(script.ToString(), "unsafe");

            return result;
        }
    }
}

