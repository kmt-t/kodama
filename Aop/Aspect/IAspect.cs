using System;
using Kodama.Aop.Interceptor;
using Kodama.Aop.JoinPoint;
using Kodama.Aop.Pointcut;

namespace Kodama.Aop.Aspect
{
    /// <summary>
    /// Aspect�̊��C���^�[�t�F�C�X
    /// </summary>
    /// <remarks>
    /// ���̃C���^�[�t�F�C�X�Œ�`�����A�X�y�N�g��AspectWeaver��Weave���܂��B
    /// </remarks>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IAspect
    {
        /// <summary>
        /// Aspect�Ɍ��ѕt�����Ă���Interceptor
        /// </summary>
        IInterceptor Interceptor
        {
            get;
        }

        /// <summary>
        /// Aspect�Ɍ��ѕt�����Ă���Pointcut
        /// </summary>
        IPointcut Pointcut
        {
            get;
        }
    }
}
