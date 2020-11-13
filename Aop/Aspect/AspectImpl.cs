using System;
using Kodama.Aop.Interceptor;
using Kodama.Aop.JoinPoint;
using Kodama.Aop.Pointcut;

namespace Kodama.Aop.Aspect
{
    /// <summary>
    /// IAspect�C���^�[�t�F�C�X�̋�ۃN���X�BInterceptor��Pointcut�����ѕt���܂��B
    /// </summary>
    /// <remarks>
    /// ���̃N���X�Œ�`�����A�X�y�N�g��AspectWeaver��Weave���܂��B
    /// </remarks>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class AspectImpl : IAspect 
    {
        /// <summary>
        /// Interceptor = ���荞�ݏ���
        /// </summary>
        private IInterceptor interceptor;

        /// <summary>
        /// Pointcut = ���荞�݃|�C���g
        /// </summary>
        private IPointcut pointcut;

        /// <summary>
        /// Aspect�Ɍ��ѕt�����Ă���Interceptor
        /// </summary>
        public IInterceptor Interceptor
        {
            get { return interceptor; }
        }

        /// <summary>
        /// Aspect�Ɍ��ѕt�����Ă���Pointcut
        /// </summary>
        public IPointcut Pointcut
        {
            get { return pointcut; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="i">Intercepter = ���荞�ݏ���</param>
        /// <param name="p">Pointcut = ���荞�݃|�C���g</param>
        public AspectImpl(IInterceptor i, IPointcut p) 
        {
            interceptor = i;
            pointcut    = p;
        }
    }
}