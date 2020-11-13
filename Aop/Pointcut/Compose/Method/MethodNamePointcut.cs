using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Kodama.Aop.Pointcut.Compose;

namespace Kodama.Aop.Pointcut.Compose.Method
{
    /// <summary>
    /// ���K�\���ƃ��\�b�h�̖��O�Ńt�B���^�����O����Pointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class MethodNamePointcut : ComposiblePointcut
    {
        /// <summary>
        /// �t�B���^�����O���郁�\�b�h���̐��K�\��
        /// </summary>
        private Regex regex;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="name">�t�B���^�����O���郁�\�b�h���̐��K�\��</param>
        public MethodNamePointcut(string name)
        {
            regex = new Regex(name);
        }

        /// <summary>
        /// �w�肳�ꂽ���\�b�h���A�X�y�N�g�̑ΏۂƂȂ邩�`�F�b�N���܂�
        /// </summary>
        /// <param name="method">�`�F�b�N���郁�\�b�h</param>
        /// <returns>�w�肳�ꂽ���\�b�h���A�X�y�N�g�̑ΏۂƂȂ邩�ǂ���</returns>
        public override bool IsApplied(MethodBase method)
        {
            Match m = regex.Match(method.Name);
            return m.Success && (string.Compare(method.Name, m.Value) == 0);
        }
    }
}
