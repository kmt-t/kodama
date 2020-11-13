using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using Kodama.Aop.Interceptor;
using Kodama.Aop.JoinPoint;

namespace Kodama.DbC.Constraint
{
    /// <summary>
    /// DbC���������銄�荞�ݏ���
    /// </summary>
    public class ConstraintInterceptor : IMethodInterceptor
    {
        /// <summary>
        /// JoinPoint���Ăяo�����ƁA���̃��\�b�h�����荞�݂܂�
        /// </summary>
        /// <param name="invocation">�Ăяo���ꂽ���\�b�h</param>
        /// <param name="outArguments">���\�b�h��out����</param>
        /// <returns>���\�b�h�̖߂�l</returns>
        public object Invoke(MethodInvocation invocation, out object[] outArguments)
        {
            InvariantConditionAttribute invariantCondition = (InvariantConditionAttribute)Attribute.GetCustomAttribute
                (invocation.Target.GetType(), typeof(InvariantConditionAttribute), true);
            PreConditionAttribute preCondition = (PreConditionAttribute)Attribute.GetCustomAttribute
                (invocation.Message.MethodBase, typeof(PreConditionAttribute), true);
            PostConditionAttribute postCondition = (PostConditionAttribute)Attribute.GetCustomAttribute
                (invocation.Message.MethodBase, typeof(PostConditionAttribute), true);

            if (invariantCondition != null)
            {
                Debug.Assert(invariantCondition.CheckConstraint(invocation));
            }

            if (preCondition != null)
            {
                Debug.Assert(preCondition.CheckConstraint(invocation));
            }

            object ret = invocation.Proceed(out outArguments);

            if (postCondition != null)
            {
                Debug.Assert(postCondition.CheckConstraint(invocation, ret, outArguments));
            }

            if (invariantCondition != null)
            {
                Debug.Assert(invariantCondition.CheckConstraint(invocation));
            }

            return ret;
        }
    }
}
