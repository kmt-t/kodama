using System;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// SetterInjection�ɗ��p����Setter�ɕt���鑮���ł�
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    public class InjectionPointAttribute : Attribute
    {
    }
}
