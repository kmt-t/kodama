using System;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// 自動登録するコンポーネントのインスタンス属性をあらわす列挙型
    /// </summary>
    public enum ComponentInstanceMode
    {
        /// <summary>
        /// PrototypeComponentEntryによるコンポーネントの登録
        /// </summary>
        Prototype,

        /// <summary>
        /// SingletonComponentEntryによるコンポーネントの登録
        /// </summary>
        Singleton,

        /// <summary>
        /// OuterComponentEntryによるコンポーネントの登録
        /// </summary>
        /// <remarks>
        /// 実際にはこの値がつかわれることはない
        /// </remarks>
        Outer,
    }
}
