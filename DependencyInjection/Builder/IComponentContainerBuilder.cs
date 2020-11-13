using System;
using Kodama.DependencyInjection.Container;

namespace Kodama.DependencyInjection.Builder
{
    /// <summary>
    /// DependencyInjectionコンテナを生成し、それにコンポーネントを登録し、そのインスタンスを返すビルダ
    /// </summary>
    public interface IComponentContainerBuilder
    {
        /// <summary>
        /// DependencyInjectionコンテナを生成し、それにコンポーネントを登録し、そのインスタンスを返す
        /// </summary>
        /// <returns>生成されたDependencyInjectionコンテナ</returns>
        IComponentContainer Build();
    }
}
