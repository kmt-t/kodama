using System;

namespace Kodama.Function.Functor.Bind
{
    /// <summary>
    /// �o�C���h���������BindFunctor#Invoke�Ăяo�����ɖ��ɒx�����Ē񋟂���C���^�[�t�F�C�X
    /// </summary>
    /// <remarks>
    /// BindFunctor�̃o�C���h��������Ƃ��Ă����n���ƁA
    /// BindFunctor#Invoke�̌Ăяo�����ƂɃo�C���h����������
    /// IArgumentProvider#Provide�ŕԂ����l�ɂȂ�B
    /// C++��bind�ɂ͓����悤�ȋ@�\�͑��݂��܂���B
    /// </remarks>
    /// <seealso cref="Kodama.Function.Functor.Bind.BindFunctor"/>
    public interface IArgumentProvider
    {
        /// <summary>
        /// �x�����ăo�C���h����������擾����
        /// </summary>
        /// <returns>�x�����ăo�C���h�������</returns>
        object Provide();
    }
}
