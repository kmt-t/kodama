using System;
using System.Reflection;

namespace Kodama.Aop.Pointcut.Compose.Delegate
{
    /// <summary>
    /// Pontcut�̃t�B���^�����O���s���f���Q�[�g
    /// </summary>
    public delegate bool PointcutHandler(MethodBase method);

    /// <summary>
    /// �f���Q�[�g�Ńt�B���^�����O����Pointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class DelegatePointcut : ComposiblePointcut
    {
        /// <summary>
        /// �t�B���^�����O����f���Q�[�g
        /// </summary>
        private PointcutHandler pointcutHandler;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="ph">�t�B���^�����O����f���Q�[�g</param>
        public DelegatePointcut(PointcutHandler ph)
        {
            pointcutHandler = ph;
        }

        /// <summary>
        /// �w�肳�ꂽ���\�b�h���A�X�y�N�g�̑ΏۂƂȂ邩�`�F�b�N���܂�
        /// </summary>
        /// <param name="method">�`�F�b�N���郁�\�b�h</param>
        /// <returns>�w�肳�ꂽ���\�b�h���A�X�y�N�g�̑ΏۂƂȂ邩�ǂ���</returns>
        public override bool IsApplied(MethodBase method)
        {
            return pointcutHandler(method);
        }
    }
}
