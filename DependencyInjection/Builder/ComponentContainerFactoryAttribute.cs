using System;

namespace Kodama.DependencyInjection.Builder
{
    /// <summary>
    /// DependencyInjectionコンテナを生成するファクトリメソッドに付ける属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ComponentContainerFactoryAttribute : Attribute
    {
    }
}
