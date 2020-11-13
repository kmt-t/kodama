using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using Kodama.Aop.Aspect;
using Kodama.Aop.Interceptor;
using Kodama.Aop.JoinPoint;
using Kodama.Aop.Pointcut;

namespace Kodama.Aop.Weaver
{
    /// <summary>
    /// Aspect��Weave����������N���X�ł�
    /// </summary>
    /// <remarks>
    /// <p>���̃N���X�̓O���[�o���ɃC���X�^���X�̐������ɏ������s�����߂�
    /// Singleton�ɂȂ��Ă��܂��B</p>
    /// <p>���̃N���X��Weave����Aspect��o�^����ƁA�N���X��������Aspect��
    /// Weave����܂��B�܂��A���ɐ������ꂽ�����̃C���X�^���X��Aspect��
    /// Weave���邱�Ƃ��\�ł��B</p>
    /// <p>���ߓI�v���N�V�ƃ��b�Z�[�W�V���N�𗘗p���Ă��邽�߁AWeave�Ώۂ�
    /// �Ȃ�I�u�W�F�N�g��ContextBoundObject���p�����Ă���K�v������܂��B</p>
    /// <p>���ӓ_�͓��ߓI�v���N�V�ƃX�^�b�N�r���_�[�V���N�̃I�[�o�[�w�b�h��
    /// ���\�b�h�Ăяo���Ȃǂɂ������邱�Ƃł��B�p�t�H�[�}���X���v�������ꍇ��
    /// �A�X�y�N�g�𗘗p���Ȃ�(ContextBoundObject���p������AspectAttribute����
    /// ���w�肵�Ȃ�)���Ƃ𐄏����܂��B</p>
    /// <p>�����ЂƂ̒��ӓ_�͓��ߓI�v���N�V�̓R���X�g���N�^�𒼐ڃC���^�[�Z�v�g
    /// ���邱�Ƃ��ł��܂���B�Ȃ̂ŁA�R���X�g���N�^���C���^�[�Z�v�g�������ꍇ�́A
    /// �R���X�g���N�^���珉�����p�̃��\�b�h���Ăяo���悤�ɂ��āA���̏������p��
    /// ���\�b�h���C���^�[�Z�v�g���ĉ������B</p>
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// 
    /// // IMethodInterceptor�̓��\�b�h�̌Ăяo�����C���^�[�Z�v�g���܂��B
    /// // �h���N���X��IMethodInterceptor#Invoke���I�[�o�[���C�h����
    /// // �C���^�[�Z�v�g�̏����������Ă��������B�܂��AIMethodInterceptor#Invoke��
    /// // �ł͕K��1��AMethodInvocation#Proceed���Ă�ł��������B
    /// // MethodInvocation#Proceed���Ă΂��ƁA�C���^�[�Z�v�g���ꂽ���\�b�h��
    /// // �{���̏��������s����܂��B
    /// public class TestInterceptor : IMethodInterceptor
    /// {
    ///     public object Invoke(MethodInvocation invocation, out object[] outArguments)
    ///     {
    ///         Console.WriteLine("Pre");
    ///         object ret = invocation.Proceed(out outArguments);
    ///         Console.WriteLine("Post");
    ///         return ret;
    ///     }
    /// }
    ///
    /// // AttributePointcut�̎w��ŗ��p���鑮�����`���܂�
    /// [AttributeUsage(AttributeTargets.Method)]
    /// public class TestInterceptAttribute : Attribute
    /// {
    /// }
    ///
    /// // �A�X�y�N�g��Weave����N���X�ɂ�AspectTarget���������܂��B
    /// // �܂��AContextBoundObject���p�����܂��B
    /// [AspectTarget]
    /// public class TestAspectWeavedClass : ContextBoundObject
    /// {
    ///     // ���̃��\�b�h��Intercept����郁�\�b�h�ł��B
    ///     // AttributePointcut�𗘗p����ꍇ�A�C���^�[�Z�v�g����郁�\�b�h�ɂ�
    ///     // �K���ɒ�`�������������āAAttributePointcut�̍쐬���ɂ��̑������w�肵�܂��B
    ///     // SignaturePointcut�𗘗p����ꍇ�́A���\�b�h���𐳋K�\���Ŏw��ł���
    ///     // �̂ő����̎w��͂���܂���B
    ///     [TestIntercept]
    ///     public void Hello()
    ///     {
    ///         Console.WriteLine("Hello");
    ///     }
    /// }
    ///
    /// // ���̃��\�b�h�����s����ƁA�R���\�[���ɁA
    /// // "Pre","Hello","Post"�̏���3�s�A�\������܂�
    /// public void MethodAttrInterceptTest()
    /// {
    ///     // Aspect�̐���
    ///     // Interceptor��Pointcut��ݒ肵�܂�
    ///     IAspect aspect = new AspectImpl(
    ///         new TestInterceptor(),
    ///         new MethodAttributePointcut(typeof(TestInterceptAttribute));
    ///
    ///     // Aspect�̓o�^
    ///     AspectWeaver.Instance().Register(aspect);
    ///
    ///     // �C���X�^���X�̐����͕��ʂ�new���Z�q��OK�ł�
    ///     TestAspectWeavedClass wc = new TestAspectWeavedClass();
    ///
    ///     // ���̃��\�b�h��TestInterceptor�ŃC���^�[�Z�v�g����܂�
    ///     wc.Hello();
    /// }
    /// </code>
    /// </example>
    public class AspectWeaver
    {
        /// <summary>
        /// Aspect���������郁�b�Z�[�W�V���N
        /// </summary>
        internal class AspectWeaveMessageSink : IMessageSink
        {
            /// <summary>
            /// �����I�ɗ��p����R���X�g���N�^�Ăяo���ɑ΂��鉞�����b�Z�[�W
            /// </summary>
            /// <seealso href="http://www.mono-project.com/about/index.html">Mono Source Code</seealso>
            [Serializable]
            private class InternalConstructionResponse : ReturnMessage, IConstructionReturnMessage
            {
                /// <summary>
                /// �R���X�g���N�^
                /// </summary>
                /// <param name="outArguments">�R���X�g���N�^��out�����ɏo�͂���l�̔z��</param>
                /// <param name="message">�R���X�g���N�^�Ăяo����IMethodCallMessage</param>
                public InternalConstructionResponse(object[] outArguments, IMethodCallMessage message) :
                    base(
                        null,
                        outArguments,
                        outArguments == null ? 0 : outArguments.Length,
                        message.LogicalCallContext,
                        message)
                {
                }

                /// <summary>
                /// �R���X�g���N�^�B�R���X�g���N�^�Ăяo���ɗ�O�����������ꍇ�͂�����������܂�
                /// </summary>
                /// <param name="e">�R���X�g���N�^�Ŕ���������O</param>
                /// <param name="message">�R���X�g���N�^�Ăяo����IMethodCallMessage</param>
                public InternalConstructionResponse(Exception e, IMethodCallMessage message) :
                    base(e, message)
                {
                }
            }

            /// <summary>
            /// ���b�Z�[�W�V���N�ŏ�������I�u�W�F�N�g
            /// </summary>
            private MarshalByRefObject target;

            /// <summary>
            /// ���ɏ�������IMessageSink
            /// </summary>
            private IMessageSink nextSink;

            /// <summary>
            /// ���荞�݃e�[�u��
            /// </summary>
            private Hashtable interceptorMap = new Hashtable();

            /// <summary>
            /// ���ɏ�������IMessageSink
            /// </summary>
            public IMessageSink NextSink
            {
                get { return nextSink; }
            }

            /// <summary>
            /// �R���X�g���N�^
            /// </summary>
            /// <param name="obj">���b�Z�[�W�V���N�ŏ�������I�u�W�F�N�g</param>
            /// <param name="sink">���ɏ������郁�b�Z�[�W�V���N</param>
            public AspectWeaveMessageSink(MarshalByRefObject obj, IMessageSink sink)
            {
                target   = obj;
                nextSink = sink;
                Weave();
            }

            /// <summary>
            /// �A�X�y�N�g���C���X�^���X��Weave����
            /// </summary>
            /// <param name="aspect">Weave����A�X�y�N�g</param>
            /// <exception cref="CanNotWeaveAspectException">
            /// �����̃C���X�^���X��Weave�s�\�ȃC���X�^���X�̏ꍇ���̗�O�𓊂���
            /// </exception>
            public void Weave(IAspect aspect)
            {
                if (!RemotingServices.IsTransparentProxy(target))
                {
                    throw new CanNotWeaveAspectException();
                }
                foreach (ConstructorInfo ci in target.GetType().GetConstructors())
                {
                    if (aspect.Pointcut.IsApplied(ci))
                    {
                        if (interceptorMap.Contains(ci))
                        {
                            ArrayList list = (ArrayList)interceptorMap[ci];
                            list.Add(aspect.Interceptor);
                        }
                        else
                        {
                            ArrayList list = new ArrayList();
                            list.Add(aspect.Interceptor);
                            interceptorMap.Add(ci, list);
                        }
                    }
                }
                foreach (MethodInfo mi in target.GetType().GetMethods())
                {
                    if (aspect.Pointcut.IsApplied(mi))
                    {
                        if (interceptorMap.Contains(mi))
                        {
                            ArrayList list = (ArrayList)interceptorMap[mi];
                            list.Add(aspect.Interceptor);
                        }
                        else
                        {
                            ArrayList list = new ArrayList();
                            list.Add(aspect.Interceptor);
                            interceptorMap.Add(mi, list);
                        }
                    }
                }
            }

            /// <summary>
            /// �����œn���ꂽ�C���X�^���X�ɓo�^����Ă���A�X�y�N�g��Weave����
            /// </summary>
            /// <exception cref="CanNotWeaveAspectException">
            /// �����̃C���X�^���X��Weave�s�\�ȃC���X�^���X�̏ꍇ���̗�O�𓊂���
            /// </exception>
            public void Weave()
            {
                foreach (IAspect aspect in AspectWeaver.Instance().GetAspects())
                {
                    Weave(aspect);
                }
            }

            /// <summary>
            /// ���b�Z�[�W�V���N�̏���(��������)
            /// </summary>
            /// <param name="msg">���\�b�h�Ăяo����IMessage</param>
            /// <returns>�߂�l��IMessage</returns>
            public IMessage SyncProcessMessage(IMessage msg)
            {
                IConstructionCallMessage ccm = msg as IConstructionCallMessage;
                if (ccm != null)
                {
                    if (interceptorMap.Contains(ccm.MethodBase))
                    {
                        ArrayList list = (ArrayList)interceptorMap[ccm.MethodBase];
                        IConstructorInterceptor[] intercepters =
                            (IConstructorInterceptor[])list.ToArray(typeof(IConstructorInterceptor));
                        IJoinPoint jointpoint = new ConstructorInvocation(target, ccm, nextSink, intercepters);
                        try
                        {
                            object[] outAuguments = null;
                            jointpoint.FirstProceed(out outAuguments);
                            return new InternalConstructionResponse(outAuguments, ccm);
                        }
                        catch (Exception e)
                        {
                            return new InternalConstructionResponse(e, ccm);
                        }
                    }
                    else
                    {
                        return nextSink.SyncProcessMessage(msg);
                    }
                }
                IMethodCallMessage mcm = msg as IMethodCallMessage;
                if (mcm != null)
                {
                    if (interceptorMap.Contains(mcm.MethodBase))
                    {
                        ArrayList list = (ArrayList)interceptorMap[mcm.MethodBase];
                        IMethodInterceptor[] intercepters =
                            (IMethodInterceptor[])list.ToArray(typeof(IMethodInterceptor));
                        IJoinPoint jointpoint = new MethodInvocation(target, mcm, nextSink, intercepters);
                        try
                        {
                            object[] outAuguments = null;
                            object returnValue = jointpoint.FirstProceed(out outAuguments);
                            if (outAuguments == null)
                            {
                                return new ReturnMessage(
                                    returnValue,
                                    null,
                                    0,
                                    mcm.LogicalCallContext,
                                    mcm);
                            }
                            else
                            {
                                return new ReturnMessage(
                                    returnValue,
                                    outAuguments,
                                    outAuguments.Length,
                                    mcm.LogicalCallContext,
                                    mcm);
                            }
                        }
                        catch (Exception e)
                        {
                            return new ReturnMessage(e, mcm);
                        }
                    }
                    else
                    {
                        return nextSink.SyncProcessMessage(msg);
                    }
                }
                return nextSink.SyncProcessMessage(msg);
            }

            /// <summary>
            /// ���b�Z�[�W�V���N�̏���(�񓯊�����)
            /// </summary>
            /// <param name="msg">���\�b�h�Ăяo����IMessage</param>
            /// <param name="replySink">���\�b�h�̉������������郁�b�Z�[�W�V���N</param>
            /// <returns>�߂�l��IMessage</returns>
            public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
            {
                return nextSink.AsyncProcessMessage(msg, replySink);
            }
        }

        /// <summary>
        /// Singleton�̃C���X�^���X
        /// </summary>
        private static AspectWeaver instance = null;

        /// <summary>
        /// �o�^����Ă���Aspect
        /// </summary>
        private ArrayList aspects = new ArrayList();

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <remarks>
        /// ���̃N���X��Singleton�Ȃ̂ŃR���X�g���N�^��private�ł��B
        /// </remarks>
        private AspectWeaver()
        {
        }

        /// <summary>
        /// Singleton�̃C���X�^���X�擾���\�b�h
        /// </summary>
        /// <returns>AspectWeaver�̃C���X�^���X</returns>
        public static AspectWeaver Instance()
        {
            if (instance == null)
            {
                instance = new AspectWeaver();
            }
            return instance;
        }

        /// <summary>
        /// Aspect��o�^����
        /// </summary>
        /// <param name="aspect">�o�^����Aspect</param>
        public void Register(IAspect aspect)
        {
            aspects.Add(aspect);
        }

        /// <summary>
        /// �o�^���ꂽ�A�X�y�N�g�̎擾
        /// </summary>
        /// <returns>�o�^���ꂽ�A�X�y�N�g</returns>
        public IAspect[] GetAspects()
        {
            return (IAspect[])aspects.ToArray(typeof(IAspect));
        }
    }
}
