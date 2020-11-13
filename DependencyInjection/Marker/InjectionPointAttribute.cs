using System;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// SetterInjection‚É—˜—p‚·‚éSetter‚É•t‚¯‚é‘®«‚Å‚·
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
    public class InjectionPointAttribute : Attribute
    {
    }
}
