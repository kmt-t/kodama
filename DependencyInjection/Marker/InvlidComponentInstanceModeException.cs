using System;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// 不正な自動登録するコンポーネントのインスタンス属性が設定されたときに発生する例外
    /// </summary>
    public class InvlidComponentInstanceModeException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InvlidComponentInstanceModeException() :
            base("InvlidComponentInstanceModeException")
        {
        }
    }
}
