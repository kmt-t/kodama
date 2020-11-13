using System;

namespace Kodama.DependencyInjection.Marker
{
    /// <summary>
    /// �����o�^����R���|�[�l���g�̃C���X�^���X����������킷�񋓌^
    /// </summary>
    public enum ComponentInstanceMode
    {
        /// <summary>
        /// PrototypeComponentEntry�ɂ��R���|�[�l���g�̓o�^
        /// </summary>
        Prototype,

        /// <summary>
        /// SingletonComponentEntry�ɂ��R���|�[�l���g�̓o�^
        /// </summary>
        Singleton,

        /// <summary>
        /// OuterComponentEntry�ɂ��R���|�[�l���g�̓o�^
        /// </summary>
        /// <remarks>
        /// ���ۂɂ͂��̒l�������邱�Ƃ͂Ȃ�
        /// </remarks>
        Outer,
    }
}
