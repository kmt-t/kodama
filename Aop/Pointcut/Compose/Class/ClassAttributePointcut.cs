using System;
using System.Reflection;
using Kodama.Aop.Pointcut.Compose;

namespace Kodama.Aop.Pointcut.Compose.Class
{
    /// <summary>
    /// �����ŃN���X���t�B���^�����O����Pointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class ClassAttributePointcut : ComposiblePointcut
    {
        /// <summary>
        /// �t�B���^�����O���鑮��
        /// </summary>
        private Type attributeType;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="attrType">�t�B���^�����O���鑮��</param>
        public ClassAttributePointcut(Type attrType)
        {
            attributeType = attrType;
        }

        /// <summary>
        /// �w�肳�ꂽ���\�b�h���A�X�y�N�g�̑ΏۂƂȂ邩�`�F�b�N���܂�
        /// </summary>
        /// <param name="method">�`�F�b�N���郁�\�b�h</param>
        /// <returns>�w�肳�ꂽ���\�b�h���A�X�y�N�g�̑ΏۂƂȂ邩�ǂ���</returns>
        public override bool IsApplied(MethodBase method)
        {
            return Attribute.IsDefined(method.ReflectedType, attributeType);
        }
    }
}
