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
    /// 現状ではConstructorInvocationは利用できません。
    /// </remarks>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class ConstructorInvocation : IInvocation
    {
        /// <summary>
        /// メソッド呼び出しオブジェクト
        /// </summary>
        private object target;

        /// <summary>
        /// メソッド呼び出しメッセージ
        /// </summary>
        private IConstructionCallMessage message;

        /// <summary>
        /// 次に呼び出すべきIMessageSink
        /// </summary>
        private IMessageSink nextSink;

        /// <summary>
        /// メソッドに結び付けられたInterceptor
        /// </summary>
        private IConstructorInterceptor[] interceptors;

        /// <summary>
        /// Interceptorの再帰呼び出しレベル
        /// </summary>
        private int interceptIndex;

        /// <summary>
        /// メソッド呼び出しオブジェクト
        /// </summary>
        public object Target
        {
            get { return target; }
        }

        /// <summary>
        /// 呼び出しの引数
        /// </summary>
        public object[] Arguments
        {
            get { return message.Args; }
        }

        /// <summary>
        /// メソッド呼び出しメッセージ
        /// </summary>
        public IConstructionCallMessage Message
        {
            get { return message; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="obj">メソッド呼び出しオブジェクト</param>
        /// <param name="msg">メソッド呼び出しメッセージ</param>
        /// <param name="sink">次に呼び出すべきIMessageSink</param>
        /// <param name="invokeInterceptors">このMethodInvocationで呼び出すInterceptor</param>
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
        /// 最初のInterceptorのチェーンの呼び出しを行います
        /// </summary>
        /// <param name="outArgumemnts">メソッドのout引数</param>
        /// <returns>メソッドの戻り値</returns>
        /// <remarks>
        /// 複数のInterceptorが設定されている場合、Interceptorの数だけ
        /// ConstructorInvocation#Proceed→Interceptor#Invokeが再帰的に
        /// 繰り返し呼び出される。このメソッドはその最初の呼び出しを
        /// おこなう。基本的にメッセージシンクからしか呼ばれない。
        /// </remarks>
        public object FirstProceed(out object[] outArgumemnts)
        {
            interceptIndex = 0;
            return Proceed(out outArgumemnts);
        }

        /// <summary>
        /// Interceptorのチェーンの呼び出しを行います
        /// </summary>
        /// <param name="outArgumemnts">メソッドのout引数</param>
        /// <returns>メソッドの戻り値</returns>
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
        /// 呼び出しに特定の属性が結び付けられているかどうかチェックする
        /// </summary>
        /// <param name="attrType">結びつきをチェックする属性の型</param>
        /// <returns>結び付けられているかどうか</returns>
        public bool IsDefined(Type attrType)
        {
            return Attribute.IsDefined(message.MethodBase, attrType);
        }
    }
}