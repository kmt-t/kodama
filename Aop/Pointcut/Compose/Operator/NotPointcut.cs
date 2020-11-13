using System;
using System.Reflection;
using Kodama.Aop.Pointcut.Compose;

namespace Kodama.Aop.Pointcut.Compose.Operator
{
    /// <summary>
    /// Pointcut�̘_���𔽓]���܂�
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class NotPointcut : ComposiblePointcut
    {
        /// <summary>
        /// �_���𔽓]����Pointcut
        /// </summary>
        private IPointcut pointcut;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="p">�E�Ӓl�ƂȂ�Pointcut</param>
        public NotPointcut(IPointcut p)
        {
            pointcut = p;
        }

        /// <summary>
        /// �w�肳�ꂽ���\�b�h���A�X�y�N�g�̑ΏۂƂȂ邩�`�F�b�N���܂�
        /// </summary>
        /// <param name="method">�`�F�b�N���郁�\�b�h</param>
        /// <returns>�w�肳�ꂽ���\�b�h���A�X�y�N�g�̑Ώ��ƂȂ邩�ǂ���</returns>
        public override bool IsApplied(MethodBase method)
        {
            return !pointcut.IsApplied(method);
        }
    }
}
