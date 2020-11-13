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
        /// ���\�b�h�Ăяo���I�u�W�F�N�g
        /// </summary>
        object Target
        {
            get;
        }

        /// <summary>
        /// �Ăяo���̈���
        /// </summary>
        object[] Arguments
        {
            get;
        }

        /// <summary>
        /// �Ăяo���ɓ���̑��������ԕt�����Ă��邩�ǂ����`�F�b�N����
        /// </summary>
        /// <param name="attrType">���т����`�F�b�N���鑮���̌^</param>
        /// <returns>���ѕt�����Ă��邩�ǂ���</returns>
        bool IsDefined(Type attrType);
    }
}
