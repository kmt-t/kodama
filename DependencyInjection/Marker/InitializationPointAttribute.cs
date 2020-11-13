using System;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// 初期化メソッドに付ける属性です
    /// </summary>
    /// <remarks>
    /// 初期化メソッドはインスタンス生成後、依存性が注入された後に
    /// 実行されるメソッドです。
    /// </remarks>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    [AttributeUsage(AttributeTargets.Method)]
    public class InitializationPointAttribute : Attribute
    {
    }
}
