using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Kodama.Aop.Pointcut.Compose;

namespace Kodama.Aop.Pointcut.Compose.Class
{
    /// <summary>
    /// ���K�\���ƃN���X�̖��O�Ńt�B���^�����O����Pointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class ClassNamePointcut : ComposiblePointcut
    {
        /// <summary>
        /// �t�B���^�����O����N���X���̐��K�\��
        /// </summary>
        private Regex regex;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="name">�t�B���^�����O����N���X���̐��K�\��</param>
        public ClassNamePointcut(string name)
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
            Match m = regex.Match(method.ReflectedType.FullName);
            return m.Success && (string.Compare(method.ReflectedType.FullName, m.Value) == 0);
        }
    }
}
