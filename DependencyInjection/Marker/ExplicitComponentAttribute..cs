using System;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// InjectionPointAttribute属性でマーキングされたメソッドの、自動
    /// インジェクションされる引数にこの属性がついていた場合、属性が持つ
    /// IArgumentComponentProviderをつかって依存性が注入されます
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public abstract class ExplicitComponentAttribute : Attribute
    {
        /// <summary>
        /// 依存性を注入するIArgumentComponentProviderを生成する
        /// </summary>
        /// <param name="container">依存性を注入するのにつかうIComponentContainerのインスタンス</param>
        /// <returns>依存性を注入するIArgumentComponentProviderのインスタンス</returns>
        public abstract IArgumentComponentProvider CreateProvider(IComponentContainer container);
    }
}
