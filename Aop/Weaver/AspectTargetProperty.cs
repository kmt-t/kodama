using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace Kodama.Aop.Weaver
{
    /// <summary>
    /// Aspect�ΏۃR���e�L�X�g�v���p�e�B
    /// </summary>
    internal class AspectTargetProperty : IContextProperty, IContributeObjectSink
    {
        /// <summary>
        /// �R���e�L�X�g�v���p�e�B��
        /// </summary>
        public string Name
        {
            get { return "AspectTargetProperty"; }
        }

        /// <summary>
        /// ���b�Z�[�W�V���N�̎擾
        /// </summary>
        /// <param name="obj">���b�Z�[�W�V���N�̃^�[�Q�b�g�ƂȂ�I�u�W�F�N�g</param>
        /// <param name="nextSink">���ɏ������ׂ����b�Z�[�W�V���N</param>
        /// <returns></returns>
        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return new AspectWeaver.AspectWeaveMessageSink(obj, nextSink);
        }

        /// <summary>
        /// �V�����R���e�L�X�g���K�؂��ǂ����m�F����
        /// </summary>
        /// <param name="newContext">�R���e�L�X�g</param>
        /// <returns>�V�����R���e�L�X�g�̃v���p�e�B���̊m�F����</returns>
        public bool IsNewContextOK(Context newContext)
        {
            return true;
        }

        /// <summary>
        /// ???
        /// </summary>
        /// <param name="newContext">???</param>
        public void Freeze(Context newContext)
        {
        }
    }
}
