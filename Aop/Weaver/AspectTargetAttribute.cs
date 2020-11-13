using System;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Activation;
using Kodama.Aop.Weaver;

namespace Kodama.Aop.Weaver
{
    /// <summary>
    /// この属性のついたクラスはインスタンス生成時にAspect用透過的プロクシを生成します。
    /// これにより特別なファクトリーを必要としません。
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    [AttributeUsage(AttributeTargets.Class)]
    public class AspectTargetAttribute : ContextAttribute
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AspectTargetAttribute() : base("AspectTargetAttribute")
        {
        }

        /// <summary>
        /// 新しいコンテキスト作成時にプロパティを設定するときに呼ばれる。
        /// 使者シンクではインスタンスの生成はフックできないため、ここで
        /// AspectのWaveを行っている。
        /// </summary>
        /// <param name="constructionCallMessage">コンストラクタ呼び出しIConstructionCallMessage</param>
        public override void GetPropertiesForNewContext(IConstructionCallMessage constructionCallMessage)
        {
            constructionCallMessage.ContextProperties.Add(new AspectTargetProperty());
        }
    }
}
