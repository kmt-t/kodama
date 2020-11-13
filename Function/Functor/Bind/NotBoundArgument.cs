using System;

namespace Kodama.Function.Functor.Bind
{
    /// <summary>
    /// BindFunctor�Ńo�C���h���Ȃ��p�����[�^�͂��̃N���X�̃C���X�^���X��n���܂�
    /// </summary>
    /// <remarks>
    /// C++�ł���Boost��bind��_1�A_2�A_3�Ɠ���̂��̂ł��B
    /// </remarks>
    /// <seealso cref="Kodama.Function.Functor.Bind.BindFunctor"/>
    /// <seealso href="http://boost.cppll.jp/HEAD/">Boost���t�@�����X���{���</seealso>
    public class NotBoundArgument
    {
        /// <summary>
        /// BoundFunctor#Invoke�̉��Ԗڂ̈����ɂ����邩�����C���f�b�N�X
        /// </summary>
        private int argumentIndex;

        /// <summary>
        /// BoundFunctor#Invoke�̉��Ԗڂ̈����ɂ����邩�����C���f�b�N�X
        /// </summary>
        public int Index
        {
            get { return argumentIndex; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="index">BoundFunctor#Invoke�̉��Ԗڂ̈����ɂ����邩�����C���f�b�N�X</param>
        public NotBoundArgument(int index)
        {
            argumentIndex = index;
        }
    }
}
