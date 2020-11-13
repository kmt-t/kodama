using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace Kodama.Aop.Weaver
{
    /// <summary>
    /// Aspect対象コンテキストプロパティ
    /// </summary>
    internal class AspectTargetProperty : IContextProperty, IContributeObjectSink
    {
        /// <summary>
        /// コンテキストプロパティ名
        /// </summary>
        public string Name
        {
            get { return "AspectTargetProperty"; }
        }

        /// <summary>
        /// メッセージシンクの取得
        /// </summary>
        /// <param name="obj">メッセージシンクのターゲットとなるオブジェクト</param>
        /// <param name="nextSink">次に処理すべきメッセージシンク</param>
        /// <returns></returns>
        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return new AspectWeaver.AspectWeaveMessageSink(obj, nextSink);
        }

        /// <summary>
        /// 新しいコンテキストが適切かどうか確認する
        /// </summary>
        /// <param name="newContext">コンテキスト</param>
        /// <returns>新しいコンテキストのプロパティ等の確認結果</returns>
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
