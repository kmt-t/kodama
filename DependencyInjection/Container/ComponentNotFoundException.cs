using System;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// コンポーネントがコンテナに登録されていないときに発生する例外
    /// </summary>
    public class ComponentNotFoundException : ApplicationException
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ComponentNotFoundException() : base("ComponentNotFoundException")
        {
        }
    }
}
