using System;
using System.Reflection;

namespace Kodama.Aop.Pointcut
{
    /// <summary>
    /// Pointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IPointcut
    {
        /// <summary>
        /// ���\�b�h��Pointcut�̏����Ɉ�v���邩�ǂ����`�F�b�N����
        /// </summary>
        /// <param name="method">�`�F�b�N���郁�\�b�h</param>
        /// <returns>���\�b�h��Pointcut�̏����Ɉ�v���邩�ǂ���</returns>
        bool IsApplied(MethodBase method);
    }
}
