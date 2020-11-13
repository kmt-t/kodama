import System;
import System.Reflection;
import System.Runtime.Remoting;
import System.Runtime.Remoting.Messaging;
import System.Text;
import Kodama.Aop.JoinPoint;

package Kodama.DbC.Constraint
{
    /// <summary>
    /// �������
    /// </summary>
    public AttributeUsage(AttributeTargets.Method)
    class PostConditionAttribute extends Attribute
    {
        /// <summary>
        /// ��
        /// </summary>
        private var expression : String;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="exp">���O�����̎�</param>
        /// <remarks>
        /// ����JScript�`���Ŏw�肵�܂��B
        /// </remarks>
        public function PostConditionAttribute(exp : String)
        {
            expression = exp;
        }

        /// <summary>
        /// ��������̕]��
        /// </summary>
        /// <param name="invocation">��������𔻒肷�郁�\�b�h</param>
        /// <param name="returnValue">��������𔻒肷��߂�l</param>
        /// <param name="outArguments">��������𔻒肷��o�͈���</param>
        /// <returns>
        /// ��������𖞂������ǂ���
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

