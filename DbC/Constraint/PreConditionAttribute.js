import System;
import System.Reflection;
import System.Runtime.Remoting;
import System.Runtime.Remoting.Messaging;
import System.Text;
import Kodama.Aop.JoinPoint;

package Kodama.DbC.Constraint
{
    /// <summary>
    /// ���O����
    /// </summary>
    public AttributeUsage(AttributeTargets.Method)
    class PreConditionAttribute extends Attribute
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
        public function PreConditionAttribute(exp : String)
        {
            expression = exp;
        }

        /// <summary>
        /// ���O�����̕]��
        /// </summary>
        /// <param name="invocation">���O�����𔻒肷�郁�\�b�h</param>
        /// <returns>
        /// ���O�����𖞂������ǂ���
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

