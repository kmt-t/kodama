using System;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// コンポーネントの自動登録に対応したコンポーネントに付ける属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegistratonComponentAttribute : Attribute
    {
        /// <summary>
        /// 登録するコンポーネントのカテゴリ
        /// </summary>
        /// <remarks>
        /// これにより自動登録されるコンポーネントを選別することができる
        /// </remarks>
        private string componentCategory;

        /// <summary>
        /// 登録するコンポーネントのインスタンス属性
        /// </summary>
        private ComponentInstanceMode instanceMode;

        /// <summary>
        /// 優先して割り当てるインターフェイスの型
        /// </summary>
        private Type interfaceType;

        /// <summary>
        /// 登録するコンポーネントの名前
        /// </summary>
        private string componentName;

        /// <summary>
        /// 登録するコンポーネントのカテゴリ
        /// </summary>
        /// <remarks>
        /// これにより自動登録されるコンポーネントを選別することができる
        /// </remarks>
        public string ComponentCategory
        {
            get { return componentCategory; }
        }

        /// <summary>
        /// 登録するコンポーネントのインスタンス属性
        /// </summary>
        /// <remarks>
        /// この属性の名前付き引数として利用します
        /// </remarks>
        public ComponentInstanceMode InstanceMode
        {
            get { return instanceMode; }
            set { instanceMode = value; }
        }

        /// <summary>
        /// 優先して割り当てるインターフェイスの型
        /// </summary>
        /// <remarks>
        /// この属性の名前付き引数として利用します
        /// </remarks>
        public Type InterfaceType
        {
            get { return interfaceType; }
            set { interfaceType = value; }
        }

        /// <summary>
        /// 登録するコンポーネントの名前
        /// </summary>
        /// <remarks>
        /// この属性の名前付き引数として利用します
        /// </remarks>
        public string ComponentName
        {
            get { return componentName; }
            set { componentName = value; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="category">コンポーネントのカテゴリ</param>
        public AutoRegistratonComponentAttribute(string category)
        {
            componentCategory = category;
            instanceMode      = ComponentInstanceMode.Prototype;
            interfaceType     = null;
            componentName     = null;
        }

        /// <summary>
        /// コンポーネントをコンテナに登録する
        /// </summary>
        /// <param name="container">コンポーネントを登録するコンテナ</param>
        /// <param name="type">登録するコンポーネント</param>
        public void Register(IMutableComponentContainer container, Type type)
        {
            IComponentEntry entry = null;
            switch (instanceMode)
            {
                case ComponentInstanceMode.Prototype :
                    if (componentName == null)
                    {
                        entry = new PrototypeComponentEntry(container, type);
                    }
                    else
                    {
                        entry = new PrototypeComponentEntry(container, type, componentName);
                    }
                    break;
                case ComponentInstanceMode.Singleton :
                    if (componentName == null)
                    {
                        entry = new SingletonComponentEntry(container, type);
                    }
                    else
                    {
                        entry = new SingletonComponentEntry(container, type, componentName);
                    }
                    break;
                default :
                    throw new InvlidComponentInstanceModeException();
            }

            if (interfaceType == null)
            {
                container.Register(entry);
            }
            else
            {
                container.Register(interfaceType, entry);
            }
        }
    }
}
