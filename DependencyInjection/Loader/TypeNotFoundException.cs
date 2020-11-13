using System;

namespace Kodama.DependencyInjection.Loader
{
    /// <summary>
    /// 指定された型がみつからなかった場合に発生する冷害
    /// </summary>
    public class TypeNotFoundException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TypeNotFoundException() : base("TypeNotFoundException")
        {
        }
    }
}
