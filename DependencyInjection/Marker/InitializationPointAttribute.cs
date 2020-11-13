using System;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// ���������\�b�h�ɕt���鑮���ł�
    /// </summary>
    /// <remarks>
    /// ���������\�b�h�̓C���X�^���X������A�ˑ������������ꂽ���
    /// ���s����郁�\�b�h�ł��B
    /// </remarks>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    [AttributeUsage(AttributeTargets.Method)]
    public class InitializationPointAttribute : Attribute
    {
    }
}
