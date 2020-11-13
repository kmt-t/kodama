using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using Kodama.Aop.Interceptor;

namespace Kodama.Aop.JoinPoint
{
    /// <summary>
    /// ConstructorInvocation
    /// </summary>
    /// <remarks>
    /// ����ł�ConstructorInvocation�͗��p�ł��܂���B
    /// </remarks>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class ConstructorInvocation : IInvocation
    {
        /// <summary>
        /// ���\�b�h�Ăяo���I�u�W�F�N�g
        /// </summary>
        private object target;

        /// <summary>
        /// ���\�b�h�Ăяo�����b�Z�[�W
        /// </summary>
        private IConstructionCallMessage message;

        /// <summary>
        /// ���ɌĂяo���ׂ�IMessageSink
        /// </summary>
        private IMessageSink nextSink;

        /// <summary>
        /// ���\�b�h�Ɍ��ѕt����ꂽInterceptor
        /// </summary>
        private IConstructorInterceptor[] interceptors;

        /// <summary>
        /// Interceptor�̍ċA�Ăяo�����x��
        /// </summary>
        private int interceptIndex;

        /// <summary>
        /// ���\�b�h�Ăяo���I�u�W�F�N�g
        /// </summary>
        public object Target
        {
            get { return target; }
        }

        /// <summary>
        /// �Ăяo���̈���
        /// </summary>
        public object[] Arguments
        {
            get { return message.Args; }
        }

        /// <summary>
        /// ���\�b�h�Ăяo�����b�Z�[�W
        /// </summary>
        public IConstructionCallMessage Message
        {
            get { return message; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="obj">���\�b�h�Ăяo���I�u�W�F�N�g</param>
        /// <param name="msg">���\�b�h�Ăяo�����b�Z�[�W</param>
        /// <param name="sink">���ɌĂяo���ׂ�IMessageSink</param>
        /// <param name="invokeInterceptors">����MethodInvocation�ŌĂяo��Interceptor</param>
        internal ConstructorInvocation(
            object                    obj,
            IConstructionCallMessage  msg,
            IMessageSink              sink,
            IConstructorInterceptor[] invokeInterceptors)
        {
            target         = obj;
            message        = msg;
            nextSink       = sink;
            interceptors   = invokeInterceptors;
            interceptIndex = 0;
        }

        /// <summary>
        /// �ŏ���Interceptor�̃`�F�[���̌Ăяo�����s���܂�
        /// </summary>
        /// <param name="outArgumemnts">���\�b�h��out����</param>
        /// <returns>���\�b�h�̖߂�l</returns>
        /// <remarks>
        /// ������Interceptor���ݒ肳��Ă���ꍇ�AInterceptor�̐�����
        /// ConstructorInvocation#Proceed��Interceptor#Invoke���ċA�I��
        /// �J��Ԃ��Ăяo�����B���̃��\�b�h�͂��̍ŏ��̌Ăяo����
        /// �����Ȃ��B��{�I�Ƀ��b�Z�[�W�V���N���炵���Ă΂�Ȃ��B
        /// </remarks>
        public object FirstProceed(out object[] outArgumemnts)
        {
            interceptIndex = 0;
            return Proceed(out outArgumemnts);
        }

        /// <summary>
        /// Interceptor�̃`�F�[���̌Ăяo�����s���܂�
        /// </summary>
        /// <param name="outArgumemnts">���\�b�h��out����</param>
        /// <returns>���\�b�h�̖߂�l</returns>
        public object Proceed(out object[] outArgumemnts)
        {
            while (interceptIndex < interceptors.Length)
            {
                return interceptors[interceptIndex++].Construct(this, out outArgumemnts);
            }

            IMethodReturnMessage ret = (IMethodReturnMessage)nextSink.SyncProcessMessage(message);

            outArgumemnts = ret.OutArgs;

            if (ret.Exception != null)
            {
                throw ret.Exception;
            }

            return ret.ReturnValue;
        }

        /// <summary>
        /// �Ăяo���ɓ���̑��������ѕt�����Ă��邩�ǂ����`�F�b�N����
        /// </summary>
        /// <param name="attrType">���т����`�F�b�N���鑮���̌^</param>
        /// <returns>���ѕt�����Ă��邩�ǂ���</returns>
        public bool IsDefined(Type attrType)
        {
            return Attribute.IsDefined(message.MethodBase, attrType);
        }
    }
}