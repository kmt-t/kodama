using System;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// コンポーネントの自動登録に対応したアセンブリに付ける属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AutoRegistrationAssemblyAttribute : Attribute
    {
    }
}
