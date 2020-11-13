using System;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// InjectionPointAttribute属性でマーキングされたメソッドの、自動
    /// インジェクションされる引数にこの属性がついていた場合、属性が持つ
    /// 型のコンポーネントがインジェクションされます
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ExplicitComponentTypeAttribute : ExplicitComponentAttribute
    {
        /// <summary>
        /// 自動インジェクションされるコンポーネントの型
        /// </summary>
        private Type componentType;

        /// <summary>
        /// 自動インジェクションされるコンポーネントの型
        /// </summary>
        public Type ComponentType
        {
            get { return componentType; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="type">
        /// 自動インジェクションされるコンポーネントの型
        /// </param>
        public ExplicitComponentTypeAttribute(Type type)
        {
            componentType = type;
        }

        /// <summary>
        /// 依存性を注入するIArgumentComponentProviderを生成する
        /// </summary>
        /// <param name="container">依存性を注入するのにつかうIComponentContainerのインスタンス</param>
        /// <returns>依存性を注入するIArgumentComponentProviderのインスタンス</returns>
        public override IArgumentComponentProvider CreateProvider(IComponentContainer container)
        {
            return new TypedArgumentComponentProvider(container, componentType);
        }
    }
}
