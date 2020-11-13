using System;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// 同じインターフェイスを実装しているコンポーネントが複数ある場合に発生する例外
    /// </summary>
    public class TooManyRegistrationException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TooManyRegistrationException() : base("TooManyRegistrationException")
        {
        }
    }
}
