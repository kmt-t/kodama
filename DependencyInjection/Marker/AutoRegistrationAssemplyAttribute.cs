using System;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// �R���|�[�l���g�̎����o�^�ɑΉ������A�Z���u���ɕt���鑮��
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AutoRegistrationAssemblyAttribute : Attribute
    {
    }
}
