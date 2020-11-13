using System;
using Kodama.Function.Functor;

namespace Kodama.Extension.Function.Functor.Generics
{
    /// <summary>
    /// Generics�𗘗p�����ėp�f���Q�[�g
    /// </summary>
    /// <returns>�߂�l</returns>
    public delegate R GenericsDelegate0<R>();

    /// <summary>
    /// Generics�𗘗p�����ėp�f���Q�[�g
    /// </summary>
    /// <param name="arg1">1�߂̈���</param>
    /// <returns>�߂�l</returns>
    public delegate R GenericsDelegate1<R, A1>(A1 arg1);

    /// <summary>
    /// Generics�𗘗p�����ėp�f���Q�[�g
    /// </summary>
    /// <param name="arg1">1�߂̈���</param>
    /// <param name="arg2">2�߂̈���</param>
    /// <returns>�߂�l</returns>
    public delegate R GenericsDelegate2<R, A1, A2>(A1 arg1, A2 arg2);

    /// <summary>
    /// Generics�𗘗p�����ėp�f���Q�[�g
    /// </summary>
    /// <param name="arg1">1�߂̈���</param>
    /// <param name="arg2">2�߂̈���</param>
    /// <param name="arg3">3�߂̈���</param>
    /// <returns>�߂�l</returns>
    public delegate R GenericsDelegate3<R, A1, A2, A3>(A1 arg1, A2 arg2, A3 arg3);

    /// <summary>
    /// Generics�𗘗p�����ėp�f���Q�[�g
    /// </summary>
    /// <param name="arg1">1�߂̈���</param>
    /// <param name="arg2">2�߂̈���</param>
    /// <param name="arg3">3�߂̈���</param>
    /// <param name="arg4">4�߂̈���</param>
    /// <returns>�߂�l</returns>
    public delegate R GenericsDelegate4<R, A1, A2, A3, A4>(A1 arg1, A2 arg2, A3 arg3, A4 arg4);

    /// <summary>
    /// Generics�𗘗p�����ėp�f���Q�[�g
    /// </summary>
    /// <param name="arg1">1�߂̈���</param>
    /// <param name="arg2">2�߂̈���</param>
    /// <param name="arg3">3�߂̈���</param>
    /// <param name="arg4">4�߂̈���</param>
    /// <param name="arg5">5�߂̈���</param>
    /// <returns>�߂�l</returns>
    public delegate R GenericsDelegate5<R, A1, A2, A3, A4, A5>
        (A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5);

    /// <summary>
    /// Generics�𗘗p�����ėp�f���Q�[�g
    /// </summary>
    /// <param name="arg1">1�߂̈���</param>
    /// <param name="arg2">2�߂̈���</param>
    /// <param name="arg3">3�߂̈���</param>
    /// <param name="arg4">4�߂̈���</param>
    /// <param name="arg5">5�߂̈���</param>
    /// <param name="arg6">6�߂̈���</param>
    /// <returns>�߂�l</returns>
    public delegate R GenericsDelegate6<R, A1, A2, A3, A4, A5, A6>
        (A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6);

    /// <summary>
    /// Generics�𗘗p�����ėp�f���Q�[�g
    /// </summary>
    /// <param name="arg1">1�߂̈���</param>
    /// <param name="arg2">2�߂̈���</param>
    /// <param name="arg3">3�߂̈���</param>
    /// <param name="arg4">4�߂̈���</param>
    /// <param name="arg5">5�߂̈���</param>
    /// <param name="arg6">6�߂̈���</param>
    /// <param name="arg7">7�߂̈���</param>
    /// <returns>�߂�l</returns>
    public delegate R GenericsDelegate7<R, A1, A2, A3, A4, A5, A6, A7>
        (A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7);

    /// <summary>
    /// Generics�𗘗p�����ėp�f���Q�[�g
    /// </summary>
    /// <param name="arg1">1�߂̈���</param>
    /// <param name="arg2">2�߂̈���</param>
    /// <param name="arg3">3�߂̈���</param>
    /// <param name="arg4">4�߂̈���</param>
    /// <param name="arg5">5�߂̈���</param>
    /// <param name="arg6">6�߂̈���</param>
    /// <param name="arg7">7�߂̈���</param>
    /// <param name="arg8">8�߂̈���</param>
    /// <returns>�߂�l</returns>
    public delegate R GenericsDelegate8<R, A1, A2, A3, A4, A5, A6, A7, A8>
        (A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7, A8 arg8);

    /// <summary>
    /// Generics�𗘗p�����ėp�f���Q�[�g
    /// </summary>
    /// <param name="arg1">1�߂̈���</param>
    /// <param name="arg2">2�߂̈���</param>
    /// <param name="arg3">3�߂̈���</param>
    /// <param name="arg4">4�߂̈���</param>
    /// <param name="arg5">5�߂̈���</param>
    /// <param name="arg6">6�߂̈���</param>
    /// <param name="arg7">7�߂̈���</param>
    /// <param name="arg8">8�߂̈���</param>
    /// <param name="arg9">9�߂̈���</param>
    /// <returns>�߂�l</returns>
    public delegate R GenericsDelegate9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9>
        (A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7, A8 arg8, A9 arg9);

    /// <summary>
    /// Generics�Ή��Ńt�@���N�^
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    public interface IGenericsFunctor0<R> : IFunctor
    {
        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        GenericsDelegate0<R> ConvertToDelegate0();

        /// <summary>
        /// �t�@���N�^�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        R Invoke0();
    }

    /// <summary>
    /// Generics�Ή��Ńt�@���N�^
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    /// <typeparam name="A1">1�ڂ̈���</typeparam>
    public interface IGenericsFunctor1<R, A1> : IFunctor
    {
        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        GenericsDelegate1<R, A1> ConvertToDelegate1();

        /// <summary>
        /// �t�@���N�^�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        R Invoke1(A1 arg1);
    }

    /// <summary>
    /// Generics�Ή��Ńt�@���N�^
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    /// <typeparam name="A1">1�ڂ̈���</typeparam>
    /// <typeparam name="A2">2�ڂ̈���</typeparam>
    public interface IGenericsFunctor2<R, A1, A2> : IFunctor
    {
        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        GenericsDelegate2<R, A1, A2> ConvertToDelegate2();

        /// <summary>
        /// �t�@���N�^�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <returns>�߂�l</returns>
        R Invoke2(A1 arg1, A2 arg2);
    }

    /// <summary>
    /// Generics�Ή��Ńt�@���N�^
    /// </summary>
    /// <typeparam name="R">�f���Q�[�g�̖߂�l</typeparam>
    /// <typeparam name="A1">�f���Q�[�g��1�߂̈���</typeparam>
    /// <typeparam name="A2">�f���Q�[�g��2�߂̈���</typeparam>
    /// <typeparam name="A3">�f���Q�[�g��3�߂̈���</typeparam>
    public interface IGenericsFunctor3<R, A1, A2, A3> : IFunctor
    {
        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        GenericsDelegate3<R, A1, A2, A3> ConvertToDelegate3();

        /// <summary>
        /// �t�@���N�^�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <returns>�߂�l</returns>
        R Invoke3(A1 arg1, A2 arg2, A3 arg3);
    }

    /// <summary>
    /// Generics�Ή��Ńt�@���N�^
    /// </summary>
    /// <typeparam name="R">�f���Q�[�g�̖߂�l</typeparam>
    /// <typeparam name="A1">�f���Q�[�g��1�߂̈���</typeparam>
    /// <typeparam name="A2">�f���Q�[�g��2�߂̈���</typeparam>
    /// <typeparam name="A3">�f���Q�[�g��3�߂̈���</typeparam>
    /// <typeparam name="A4">�f���Q�[�g��4�߂̈���</typeparam>
    public interface IGenericsFunctor4<R, A1, A2, A3, A4> : IFunctor
    {
        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        GenericsDelegate4<R, A1, A2, A3, A4> ConvertToDelegate4();

        /// <summary>
        /// �t�@���N�^�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <returns>�߂�l</returns>
        R Invoke4(A1 arg1, A2 arg2, A3 arg3, A4 arg4);
    }

    /// <summary>
    /// Generics�Ή��Ńt�@���N�^
    /// </summary>
    /// <typeparam name="R">�f���Q�[�g�̖߂�l</typeparam>
    /// <typeparam name="A1">�f���Q�[�g��1�߂̈���</typeparam>
    /// <typeparam name="A2">�f���Q�[�g��2�߂̈���</typeparam>
    /// <typeparam name="A3">�f���Q�[�g��3�߂̈���</typeparam>
    /// <typeparam name="A4">�f���Q�[�g��4�߂̈���</typeparam>
    /// <typeparam name="A5">�f���Q�[�g��5�߂̈���</typeparam>
    public interface IGenericsFunctor5<R, A1, A2, A3, A4, A5> : IFunctor
    {
        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        GenericsDelegate5<R, A1, A2, A3, A4, A5> ConvertToDelegate5();

        /// <summary>
        /// �t�@���N�^�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <param name="arg5">5�߂̈���</param>
        /// <returns>�߂�l</returns>
        R Invoke5(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5);
    }

    /// <summary>
    /// Generics�Ή��Ńt�@���N�^
    /// </summary>
    /// <typeparam name="R">�f���Q�[�g�̖߂�l</typeparam>
    /// <typeparam name="A1">�f���Q�[�g��1�߂̈���</typeparam>
    /// <typeparam name="A2">�f���Q�[�g��2�߂̈���</typeparam>
    /// <typeparam name="A3">�f���Q�[�g��3�߂̈���</typeparam>
    /// <typeparam name="A4">�f���Q�[�g��4�߂̈���</typeparam>
    /// <typeparam name="A5">�f���Q�[�g��5�߂̈���</typeparam>
    /// <typeparam name="A6">�f���Q�[�g��6�߂̈���</typeparam>
    public interface IGenericsFunctor6<R, A1, A2, A3, A4, A5, A6> : IFunctor
    {
        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        GenericsDelegate6<R, A1, A2, A3, A4, A5, A6> ConvertToDelegate6();

        /// <summary>
        /// �t�@���N�^�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <param name="arg5">5�߂̈���</param>
        /// <param name="arg6">6�߂̈���</param>
        /// <returns>�߂�l</returns>
        R Invoke6(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6);
    }

    /// <summary>
    /// Generics�Ή��Ńt�@���N�^
    /// </summary>
    /// <typeparam name="R">�f���Q�[�g�̖߂�l</typeparam>
    /// <typeparam name="A1">�f���Q�[�g��1�߂̈���</typeparam>
    /// <typeparam name="A2">�f���Q�[�g��2�߂̈���</typeparam>
    /// <typeparam name="A3">�f���Q�[�g��3�߂̈���</typeparam>
    /// <typeparam name="A4">�f���Q�[�g��4�߂̈���</typeparam>
    /// <typeparam name="A5">�f���Q�[�g��5�߂̈���</typeparam>
    /// <typeparam name="A6">�f���Q�[�g��6�߂̈���</typeparam>
    /// <typeparam name="A7">�f���Q�[�g��7�߂̈���</typeparam>
    public interface IGenericsFunctor7<R, A1, A2, A3, A4, A5, A6, A7> : IFunctor
    {
        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        GenericsDelegate7<R, A1, A2, A3, A4, A5, A6, A7> ConvertToDelegate7();

        /// <summary>
        /// �t�@���N�^�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <param name="arg5">5�߂̈���</param>
        /// <param name="arg6">6�߂̈���</param>
        /// <param name="arg7">7�߂̈���</param>
        /// <returns>�߂�l</returns>
        R Invoke7(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7);
    }

    /// <summary>
    /// Generics�Ή��Ńt�@���N�^
    /// </summary>
    /// <typeparam name="R">�f���Q�[�g�̖߂�l</typeparam>
    /// <typeparam name="A1">�f���Q�[�g��1�߂̈���</typeparam>
    /// <typeparam name="A2">�f���Q�[�g��2�߂̈���</typeparam>
    /// <typeparam name="A3">�f���Q�[�g��3�߂̈���</typeparam>
    /// <typeparam name="A4">�f���Q�[�g��4�߂̈���</typeparam>
    /// <typeparam name="A5">�f���Q�[�g��5�߂̈���</typeparam>
    /// <typeparam name="A6">�f���Q�[�g��6�߂̈���</typeparam>
    /// <typeparam name="A7">�f���Q�[�g��7�߂̈���</typeparam>
    /// <typeparam name="A8">�f���Q�[�g��8�߂̈���</typeparam>
    public interface IGenericsFunctor8<R, A1, A2, A3, A4, A5, A6, A7, A8> : IFunctor
    {
        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        GenericsDelegate8<R, A1, A2, A3, A4, A5, A6, A7, A8> ConvertToDelegate8();

        /// <summary>
        /// �t�@���N�^�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <param name="arg5">5�߂̈���</param>
        /// <param name="arg6">6�߂̈���</param>
        /// <param name="arg7">7�߂̈���</param>
        /// <param name="arg8">8�߂̈���</param>
        /// <returns>�߂�l</returns>
        R Invoke8(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7, A8 arg8);
    }

    /// <summary>
    /// Generics�Ή��Ńt�@���N�^
    /// </summary>
    /// <typeparam name="R">�f���Q�[�g�̖߂�l</typeparam>
    /// <typeparam name="A1">�f���Q�[�g��1�߂̈���</typeparam>
    /// <typeparam name="A2">�f���Q�[�g��2�߂̈���</typeparam>
    /// <typeparam name="A3">�f���Q�[�g��3�߂̈���</typeparam>
    /// <typeparam name="A4">�f���Q�[�g��4�߂̈���</typeparam>
    /// <typeparam name="A5">�f���Q�[�g��5�߂̈���</typeparam>
    /// <typeparam name="A6">�f���Q�[�g��6�߂̈���</typeparam>
    /// <typeparam name="A7">�f���Q�[�g��7�߂̈���</typeparam>
    /// <typeparam name="A8">�f���Q�[�g��8�߂̈���</typeparam>
    /// <typeparam name="A9">�f���Q�[�g��9�߂̈���</typeparam>
    public interface IGenericsFunctor9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> : IFunctor
    {
        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        GenericsDelegate9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> ConvertToDelegate9();

        /// <summary>
        /// �t�@���N�^�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <param name="arg5">5�߂̈���</param>
        /// <param name="arg6">6�߂̈���</param>
        /// <param name="arg7">7�߂̈���</param>
        /// <param name="arg8">8�߂̈���</param>
        /// <param name="arg9">9�߂̈���</param>
        /// <returns>�߂�l</returns>
        R Invoke9(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7, A8 arg8, A9 arg9);
    }
}
