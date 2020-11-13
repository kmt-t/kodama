using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;
using Kodama.Aop.Weaver;

namespace Kodama.Aop.Weaver
{
    /// <summary>
    /// ���̑����̂����N���X�̓C���X�^���X��������Aspect�p���ߓI�v���N�V�𐶐����܂��B
    /// ����ɂ����ʂȃt�@�N�g���[��K�v�Ƃ��܂���B
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    [AttributeUsage(AttributeTargets.Class)]
    public class AspectTargetAttribute : ContextAttribute
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public AspectTargetAttribute() : base("AspectTargetAttribute")
        {
        }

        /// <summary>
        /// �V�����R���e�L�X�g�쐬���Ƀv���p�e�B��ݒ肷��Ƃ��ɌĂ΂��B
        /// �g�҃V���N�ł̓C���X�^���X�̐����̓t�b�N�ł��Ȃ����߁A������
        /// Aspect��Wave���s���Ă���B
        /// </summary>
        /// <param name="constructionCallMessage">�R���X�g���N�^�Ăяo��IConstructionCallMessage</param>
        public override void GetPropertiesForNewContext(IConstructionCallMessage constructionCallMessage)
        {
            constructionCallMessage.ContextProperties.Add(new AspectTargetProperty());
        }
    }
}
