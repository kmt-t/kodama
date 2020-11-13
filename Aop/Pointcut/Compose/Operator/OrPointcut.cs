using System;
using System.Reflection;
using Kodama.Aop.Pointcut;
using Kodama.Aop.Pointcut.Compose;

namespace Kodama.Aop.Pointcut.Compose.Operator
{
    /// <summary>
    /// 2��Pointcut��_�����Zor�ō������܂�
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class OrPointcut : ComposiblePointcut
    {
        /// <summary>
        /// ���Ӓl��Pointcut
        /// </summary>
        private IPointcut left;

        /// <summary>
        /// �E�Ӓl��Pointcut
        /// </summary>
        private IPointcut right;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="l">�E�Ӓl�ƂȂ�Pointcut</param>
        /// <param name="r">���Ӓl�ƂȂ�Pointcut</param>
        public OrPointcut(IPointcut l, IPointcut r)
        {
            left  = l;
            right = r;
        }

        /// <summary>
        /// �w�肳�ꂽ���\�b�h���A�X�y�N�g�̑ΏۂƂȂ邩�`�F�b�N���܂�
        /// </summary>
        /// <param name="method">�`�F�b�N���郁�\�b�h</param>
        /// <returns>�w�肳�ꂽ���\�b�h���A�X�y�N�g�̑ΏۂƂȂ邩�ǂ���</returns>
        public override bool IsApplied(MethodBase method)
        {
            return left.IsApplied(method) || right.IsApplied(method);
        }
    }
}
