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
    /// AspectのWeave処理をするクラスです
    /// </summary>
    /// <remarks>
    /// <p>このクラスはグローバルにインスタンスの生成時に処理を行うために
    /// Singletonになっています。</p>
    /// <p>このクラスでWeaveするAspectを登録すると、クラス生成時にAspectが
    /// Weaveされます。また、既に生成された既存のインスタンスにAspectを
    /// Weaveすることも可能です。</p>
    /// <p>透過的プロクシとメッセージシンクを利用しているため、Weave対象と
    /// なるオブジェクトはContextBoundObjectを継承している必要があります。</p>
    /// <p>注意点は透過的プロクシとスタックビルダーシンクのオーバーヘッドが
    /// メソッド呼び出しなどにがかかることです。パフォーマンスが要求される場合は
    /// アスペクトを利用しない(ContextBoundObjectを継承せずAspectAttribute属性
    /// も指定しない)ことを推奨します。</p>
    /// <p>もうひとつの注意点は透過的プロクシはコンストラクタを直接インターセプト
    /// することができません。なので、コンストラクタをインターセプトしたい場合は、
    /// コンストラクタから初期化用のメソッドを呼び出すようにして、その初期化用の
    /// メソッドをインターセプトして下さい。</p>
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// 
    /// // IMethodInterceptorはメソッドの呼び出しをインターセプトします。
    /// // 派生クラスでIMethodInterceptor#Invokeをオーバーライドして
    /// // インターセプトの処理を書いてください。また、IMethodInterceptor#Invoke内
    /// // では必ず1回、MethodInvocation#Proceedを呼んでください。
    /// // MethodInvocation#Proceedが呼ばれると、インターセプトされたメソッドの
    /// // 本来の処理が実行されます。
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
    /// // AttributePointcutの指定で利用する属性を定義します
    /// [AttributeUsage(AttributeTargets.Method)]
    /// public class TestInterceptAttribute : Attribute
    /// {
    /// }
    ///
    /// // アスペクトをWeaveするクラスにはAspectTarget属性をつけます。
    /// // また、ContextBoundObjectを継承します。
    /// [AspectTarget]
    /// public class TestAspectWeavedClass : ContextBoundObject
    /// {
    ///     // このメソッドがInterceptされるメソッドです。
    ///     // AttributePointcutを利用する場合、インターセプトされるメソッドには
    ///     // 適当に定義した属性をつけて、AttributePointcutの作成時にその属性を指定します。
    ///     // SignaturePointcutを利用する場合は、メソッド名を正規表現で指定できる
    ///     // ので属性の指定はいりません。
    ///     [TestIntercept]
    ///     public void Hello()
    ///     {
    ///         Console.WriteLine("Hello");
    ///     }
    /// }
    ///
    /// // このメソッドを実行すると、コンソールに、
    /// // "Pre","Hello","Post"の順で3行、表示されます
    /// public void MethodAttrInterceptTest()
    /// {
    ///     // Aspectの生成
    ///     // InterceptorとPointcutを設定します
    ///     IAspect aspect = new AspectImpl(
    ///         new TestInterceptor(),
    ///         new MethodAttributePointcut(typeof(TestInterceptAttribute));
    ///
    ///     // Aspectの登録
    ///     AspectWeaver.Instance().Register(aspect);
    ///
    ///     // インスタンスの生成は普通のnew演算子でOKです
    ///     TestAspectWeavedClass wc = new TestAspectWeavedClass();
    ///
    ///     // このメソッドがTestInterceptorでインターセプトされます
    ///     wc.Hello();
    /// }
    /// </code>
    /// </example>
    public class AspectWeaver
    {
        /// <summary>
        /// Aspectを処理するメッセージシンク
        /// </summary>
        internal class AspectWeaveMessageSink : IMessageSink
        {
            /// <summary>
            /// 内部的に利用するコンストラクタ呼び出しに対する応答メッセージ
            /// </summary>
            /// <seealso href="http://www.mono-project.com/about/index.html">Mono Source Code</seealso>
            [Serializable]
            private class InternalConstructionResponse : ReturnMessage, IConstructionReturnMessage
            {
                /// <summary>
                /// コンストラクタ
                /// </summary>
                /// <param name="outArguments">コンストラクタのout引数に出力する値の配列</param>
                /// <param name="message">コンストラクタ呼び出しのIMethodCallMessage</param>
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
                /// コンストラクタ。コンストラクタ呼び出しに例外が発生した場合はこちらをつかいます
                /// </summary>
                /// <param name="e">コンストラクタで発生した例外</param>
                /// <param name="message">コンストラクタ呼び出しのIMethodCallMessage</param>
                public InternalConstructionResponse(Exception e, IMethodCallMessage message) :
                    base(e, message)
                {
                }
            }

            /// <summary>
            /// メッセージシンクで処理するオブジェクト
            /// </summary>
            private MarshalByRefObject target;

            /// <summary>
            /// 次に処理するIMessageSink
            /// </summary>
            private IMessageSink nextSink;

            /// <summary>
            /// 割り込みテーブル
            /// </summary>
            private Hashtable interceptorMap = new Hashtable();

            /// <summary>
            /// 次に処理するIMessageSink
            /// </summary>
            public IMessageSink NextSink
            {
                get { return nextSink; }
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="obj">メッセージシンクで処理するオブジェクト</param>
            /// <param name="sink">次に処理するメッセージシンク</param>
            public AspectWeaveMessageSink(MarshalByRefObject obj, IMessageSink sink)
            {
                target   = obj;
                nextSink = sink;
                Weave();
            }

            /// <summary>
            /// アスペクトをインスタンスにWeaveする
            /// </summary>
            /// <param name="aspect">Weaveするアスペクト</param>
            /// <exception cref="CanNotWeaveAspectException">
            /// 引数のインスタンスがWeave不可能なインスタンスの場合この例外を投げる
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
            /// 引数で渡されたインスタンスに登録されているアスペクトをWeaveする
            /// </summary>
            /// <exception cref="CanNotWeaveAspectException">
            /// 引数のインスタンスがWeave不可能なインスタンスの場合この例外を投げる
            /// </exception>
            public void Weave()
            {
                foreach (IAspect aspect in AspectWeaver.Instance().GetAspects())
                {
                    Weave(aspect);
                }
            }

            /// <summary>
            /// メッセージシンクの処理(同期処理)
            /// </summary>
            /// <param name="msg">メソッド呼び出しのIMessage</param>
            /// <returns>戻り値のIMessage</returns>
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
            /// メッセージシンクの処理(非同期処理)
            /// </summary>
            /// <param name="msg">メソッド呼び出しのIMessage</param>
            /// <param name="replySink">メソッドの応答を処理するメッセージシンク</param>
            /// <returns>戻り値のIMessage</returns>
            public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
            {
                return nextSink.AsyncProcessMessage(msg, replySink);
            }
        }

        /// <summary>
        /// Singletonのインスタンス
        /// </summary>
        private static AspectWeaver instance = null;

        /// <summary>
        /// 登録されているAspect
        /// </summary>
        private ArrayList aspects = new ArrayList();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// このクラスはSingletonなのでコンストラクタはprivateです。
        /// </remarks>
        private AspectWeaver()
        {
        }

        /// <summary>
        /// Singletonのインスタンス取得メソッド
        /// </summary>
        /// <returns>AspectWeaverのインスタンス</returns>
        public static AspectWeaver Instance()
        {
            if (instance == null)
            {
                instance = new AspectWeaver();
            }
            return instance;
        }

        /// <summary>
        /// Aspectを登録する
        /// </summary>
        /// <param name="aspect">登録するAspect</param>
        public void Register(IAspect aspect)
        {
            aspects.Add(aspect);
        }

        /// <summary>
        /// 登録されたアスペクトの取得
        /// </summary>
        /// <returns>登録されたアスペクト</returns>
        public IAspect[] GetAspects()
        {
            return (IAspect[])aspects.ToArray(typeof(IAspect));
        }
    }
}
