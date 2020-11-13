using System;
using System.Reflection;
using Kodama.Aop.Pointcut;
using Kodama.Aop.Pointcut.Compose.Operator;

namespace Kodama.Aop.Pointcut.Compose
{
    /// <summary>
    /// �_�����Z�ɂ�鍇�����\��Pointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public abstract class ComposiblePointcut : IPointcut
    {
        /// <summary>
        /// �w�肳�ꂽ���\�b�h���A�X�y�N�g�̑ΏۂƂȂ邩�`�F�b�N���܂�
        /// </summary>
        /// <param name="method">�`�F�b�N���郁�\�b�h</param>
        /// <returns>�w�肳�ꂽ���\�b�h���A�X�y�N�g�̑ΏۂƂȂ邩�ǂ���</returns>
        public abstract bool IsApplied(MethodBase method);

        /// <summary>
        /// Pointcut�̘_���𔽓]���܂�
        /// </summary>
        /// <param name="pointcut">�_���𔽓]����Pointcut</param>
        /// <returns>�_�������]���ꂽPointcut</returns>
        public static ComposiblePointcut operator ! (ComposiblePointcut pointcut)
        {
            return new NotPointcut(pointcut);
        }

        /// <summary>
        /// 2��Pointcut�̘_�����Zand���Ƃ�Pointcut��Ԃ��܂�
        /// </summary>
        /// <param name="left">�E�Ӓl��Pointcut</param>
        /// <param name="right">���Ӓl��Pointcut</param>
        /// <returns>2��Pointcut�̘_�����Zand���Ƃ�Pointcut</returns>
        public static ComposiblePointcut operator & (ComposiblePointcut left, ComposiblePointcut right)
        {
            return new AndPointcut(left, right);
        }

        /// <summary>
        /// 2��Pointcut�̘_�����Zor���Ƃ�Pointcut��Ԃ��܂�
        /// </summary>
        /// <param name="left">�E�Ӓl��Pointcut</param>
        /// <param name="right">���Ӓl��Pointcut</param>
        /// <returns>2��Pointcut�̘_�����Zor���Ƃ�Pointcut</returns>
        public static ComposiblePointcut operator | (ComposiblePointcut left, ComposiblePointcut right)
        {
            return new OrPointcut(left, right);
        }
    }
}
