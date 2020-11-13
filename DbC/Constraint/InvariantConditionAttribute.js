import System;
import System.Reflection;
import System.Runtime.Remoting;
import System.Runtime.Remoting.Messaging;
import System.Text;
import Kodama.Aop.JoinPoint;

package Kodama.DbC.Constraint
{
    /// <summary>
    /// �s�Ϗ���
    /// </summary>
    public AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)
    class InvariantConditionAttribute extends Attribute
    {
        /// <summary>
        /// ��
        /// </summary>
        private var expression : String;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="exp">�s�Ϗ����̎�</param>
        /// <remarks>
        /// ����JScript�`���Ŏw�肵�܂��B
        /// </remarks>
        public function InvariantConditionAttribute(exp : String)
        {
            expression = exp;
        }

        /// <summary>
        /// �s�Ϗ����̕]��
        /// </summary>
        /// <param name="invocation">�s�Ϗ����𔻒肷�郁�\�b�h</param>
        /// <returns>
        /// �s�Ϗ����𖞂������ǂ���
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


