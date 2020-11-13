using System;

namespace Kodama.DependencyInjection.Builder
{
    /// <summary>
    /// スクリプトからファクトリーメソッドがみつからない場合に発生する例外
    /// </summary>
    public class FactoryNotFoundException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FactoryNotFoundException() : base("FactoryNotFoundException")
        {
        }
    }
}
