using System;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// InjectionPointAttribute属性でマーキングされたメソッドの、自動
    /// インジェクションされる引数にこの属性がついていた場合、属性が持つ
    /// 名前のコンポーネントがインジェクションされます
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ExplicitComponentNameAttribute : ExplicitComponentAttribute
    {
        /// <summary>
        /// 自動インジェクションされるコンポーネントの名前
        /// </summary>
        private string componentName;

        /// <summary>
        /// 自動インジェクションされるコンポーネントの名前
        /// </summary>
        public string ComponentName
        {
            get { return componentName; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">
        /// 自動インジェクションされるコンポーネントの名前
        /// </param>
        public ExplicitComponentNameAttribute(string name)
        {
            componentName = name;
        }

        /// <summary>
        /// 依存性を注入するIArgumentComponentProviderを生成する
        /// </summary>
        /// <param name="container">依存性を注入するのにつかうIComponentContainerのインスタンス</param>
        /// <returns>依存性を注入するIArgumentComponentProviderのインスタンス</returns>
        public override IArgumentComponentProvider CreateProvider(IComponentContainer container)
        {
            return new NamedArgumentComponentProvider(container, componentName);
        }
    }
}
