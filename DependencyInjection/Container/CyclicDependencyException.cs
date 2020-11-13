using System;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// 依存関係が循環しているときに発生する例外
    /// </summary>
    public class CyclicDependencyException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CyclicDependencyException() : base("CyclicDependencyException")
        {
        }
    }
}
