using System;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// SetterInjectionに利用するSetterに付ける属性です
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    public class InjectionPointAttribute : Attribute
    {
    }
}
