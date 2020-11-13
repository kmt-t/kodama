using System;
using Kodama.Function.Functor;

namespace Kodama.Extension.Function.Functor.Generics
{
    /// <summary>
    /// Genericsを利用した汎用デリゲート
    /// </summary>
    /// <returns>戻り値</returns>
    public delegate R GenericsDelegate0<R>();

    /// <summary>
    /// Genericsを利用した汎用デリゲート
    /// </summary>
    /// <param name="arg1">1つめの引数</param>
    /// <returns>戻り値</returns>
    public delegate R GenericsDelegate1<R, A1>(A1 arg1);

    /// <summary>
    /// Genericsを利用した汎用デリゲート
    /// </summary>
    /// <param name="arg1">1つめの引数</param>
    /// <param name="arg2">2つめの引数</param>
    /// <returns>戻り値</returns>
    public delegate R GenericsDelegate2<R, A1, A2>(A1 arg1, A2 arg2);

    /// <summary>
    /// Genericsを利用した汎用デリゲート
    /// </summary>
    /// <param name="arg1">1つめの引数</param>
    /// <param name="arg2">2つめの引数</param>
    /// <param name="arg3">3つめの引数</param>
    /// <returns>戻り値</returns>
    public delegate R GenericsDelegate3<R, A1, A2, A3>(A1 arg1, A2 arg2, A3 arg3);

    /// <summary>
    /// Genericsを利用した汎用デリゲート
    /// </summary>
    /// <param name="arg1">1つめの引数</param>
    /// <param name="arg2">2つめの引数</param>
    /// <param name="arg3">3つめの引数</param>
    /// <param name="arg4">4つめの引数</param>
    /// <returns>戻り値</returns>
    public delegate R GenericsDelegate4<R, A1, A2, A3, A4>(A1 arg1, A2 arg2, A3 arg3, A4 arg4);

    /// <summary>
    /// Genericsを利用した汎用デリゲート
    /// </summary>
    /// <param name="arg1">1つめの引数</param>
    /// <param name="arg2">2つめの引数</param>
    /// <param name="arg3">3つめの引数</param>
    /// <param name="arg4">4つめの引数</param>
    /// <param name="arg5">5つめの引数</param>
    /// <returns>戻り値</returns>
    public delegate R GenericsDelegate5<R, A1, A2, A3, A4, A5>
        (A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5);

    /// <summary>
    /// Genericsを利用した汎用デリゲート
    /// </summary>
    /// <param name="arg1">1つめの引数</param>
    /// <param name="arg2">2つめの引数</param>
    /// <param name="arg3">3つめの引数</param>
    /// <param name="arg4">4つめの引数</param>
    /// <param name="arg5">5つめの引数</param>
    /// <param name="arg6">6つめの引数</param>
    /// <returns>戻り値</returns>
    public delegate R GenericsDelegate6<R, A1, A2, A3, A4, A5, A6>
        (A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6);

    /// <summary>
    /// Genericsを利用した汎用デリゲート
    /// </summary>
    /// <param name="arg1">1つめの引数</param>
    /// <param name="arg2">2つめの引数</param>
    /// <param name="arg3">3つめの引数</param>
    /// <param name="arg4">4つめの引数</param>
    /// <param name="arg5">5つめの引数</param>
    /// <param name="arg6">6つめの引数</param>
    /// <param name="arg7">7つめの引数</param>
    /// <returns>戻り値</returns>
    public delegate R GenericsDelegate7<R, A1, A2, A3, A4, A5, A6, A7>
        (A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7);

    /// <summary>
    /// Genericsを利用した汎用デリゲート
    /// </summary>
    /// <param name="arg1">1つめの引数</param>
    /// <param name="arg2">2つめの引数</param>
    /// <param name="arg3">3つめの引数</param>
    /// <param name="arg4">4つめの引数</param>
    /// <param name="arg5">5つめの引数</param>
    /// <param name="arg6">6つめの引数</param>
    /// <param name="arg7">7つめの引数</param>
    /// <param name="arg8">8つめの引数</param>
    /// <returns>戻り値</returns>
    public delegate R GenericsDelegate8<R, A1, A2, A3, A4, A5, A6, A7, A8>
        (A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7, A8 arg8);

    /// <summary>
    /// Genericsを利用した汎用デリゲート
    /// </summary>
    /// <param name="arg1">1つめの引数</param>
    /// <param name="arg2">2つめの引数</param>
    /// <param name="arg3">3つめの引数</param>
    /// <param name="arg4">4つめの引数</param>
    /// <param name="arg5">5つめの引数</param>
    /// <param name="arg6">6つめの引数</param>
    /// <param name="arg7">7つめの引数</param>
    /// <param name="arg8">8つめの引数</param>
    /// <param name="arg9">9つめの引数</param>
    /// <returns>戻り値</returns>
    public delegate R GenericsDelegate9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9>
        (A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7, A8 arg8, A9 arg9);

    /// <summary>
    /// Generics対応版ファンクタ
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    public interface IGenericsFunctor0<R> : IFunctor
    {
        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        GenericsDelegate0<R> ConvertToDelegate0();

        /// <summary>
        /// ファンクタの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        R Invoke0();
    }

    /// <summary>
    /// Generics対応版ファンクタ
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    /// <typeparam name="A1">1つ目の引数</typeparam>
    public interface IGenericsFunctor1<R, A1> : IFunctor
    {
        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        GenericsDelegate1<R, A1> ConvertToDelegate1();

        /// <summary>
        /// ファンクタの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        R Invoke1(A1 arg1);
    }

    /// <summary>
    /// Generics対応版ファンクタ
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    /// <typeparam name="A1">1つ目の引数</typeparam>
    /// <typeparam name="A2">2つ目の引数</typeparam>
    public interface IGenericsFunctor2<R, A1, A2> : IFunctor
    {
        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        GenericsDelegate2<R, A1, A2> ConvertToDelegate2();

        /// <summary>
        /// ファンクタの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <returns>戻り値</returns>
        R Invoke2(A1 arg1, A2 arg2);
    }

    /// <summary>
    /// Generics対応版ファンクタ
    /// </summary>
    /// <typeparam name="R">デリゲートの戻り値</typeparam>
    /// <typeparam name="A1">デリゲートの1つめの引数</typeparam>
    /// <typeparam name="A2">デリゲートの2つめの引数</typeparam>
    /// <typeparam name="A3">デリゲートの3つめの引数</typeparam>
    public interface IGenericsFunctor3<R, A1, A2, A3> : IFunctor
    {
        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        GenericsDelegate3<R, A1, A2, A3> ConvertToDelegate3();

        /// <summary>
        /// ファンクタの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <returns>戻り値</returns>
        R Invoke3(A1 arg1, A2 arg2, A3 arg3);
    }

    /// <summary>
    /// Generics対応版ファンクタ
    /// </summary>
    /// <typeparam name="R">デリゲートの戻り値</typeparam>
    /// <typeparam name="A1">デリゲートの1つめの引数</typeparam>
    /// <typeparam name="A2">デリゲートの2つめの引数</typeparam>
    /// <typeparam name="A3">デリゲートの3つめの引数</typeparam>
    /// <typeparam name="A4">デリゲートの4つめの引数</typeparam>
    public interface IGenericsFunctor4<R, A1, A2, A3, A4> : IFunctor
    {
        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        GenericsDelegate4<R, A1, A2, A3, A4> ConvertToDelegate4();

        /// <summary>
        /// ファンクタの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <param name="arg4">4つめの引数</param>
        /// <returns>戻り値</returns>
        R Invoke4(A1 arg1, A2 arg2, A3 arg3, A4 arg4);
    }

    /// <summary>
    /// Generics対応版ファンクタ
    /// </summary>
    /// <typeparam name="R">デリゲートの戻り値</typeparam>
    /// <typeparam name="A1">デリゲートの1つめの引数</typeparam>
    /// <typeparam name="A2">デリゲートの2つめの引数</typeparam>
    /// <typeparam name="A3">デリゲートの3つめの引数</typeparam>
    /// <typeparam name="A4">デリゲートの4つめの引数</typeparam>
    /// <typeparam name="A5">デリゲートの5つめの引数</typeparam>
    public interface IGenericsFunctor5<R, A1, A2, A3, A4, A5> : IFunctor
    {
        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        GenericsDelegate5<R, A1, A2, A3, A4, A5> ConvertToDelegate5();

        /// <summary>
        /// ファンクタの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <param name="arg4">4つめの引数</param>
        /// <param name="arg5">5つめの引数</param>
        /// <returns>戻り値</returns>
        R Invoke5(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5);
    }

    /// <summary>
    /// Generics対応版ファンクタ
    /// </summary>
    /// <typeparam name="R">デリゲートの戻り値</typeparam>
    /// <typeparam name="A1">デリゲートの1つめの引数</typeparam>
    /// <typeparam name="A2">デリゲートの2つめの引数</typeparam>
    /// <typeparam name="A3">デリゲートの3つめの引数</typeparam>
    /// <typeparam name="A4">デリゲートの4つめの引数</typeparam>
    /// <typeparam name="A5">デリゲートの5つめの引数</typeparam>
    /// <typeparam name="A6">デリゲートの6つめの引数</typeparam>
    public interface IGenericsFunctor6<R, A1, A2, A3, A4, A5, A6> : IFunctor
    {
        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        GenericsDelegate6<R, A1, A2, A3, A4, A5, A6> ConvertToDelegate6();

        /// <summary>
        /// ファンクタの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <param name="arg4">4つめの引数</param>
        /// <param name="arg5">5つめの引数</param>
        /// <param name="arg6">6つめの引数</param>
        /// <returns>戻り値</returns>
        R Invoke6(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6);
    }

    /// <summary>
    /// Generics対応版ファンクタ
    /// </summary>
    /// <typeparam name="R">デリゲートの戻り値</typeparam>
    /// <typeparam name="A1">デリゲートの1つめの引数</typeparam>
    /// <typeparam name="A2">デリゲートの2つめの引数</typeparam>
    /// <typeparam name="A3">デリゲートの3つめの引数</typeparam>
    /// <typeparam name="A4">デリゲートの4つめの引数</typeparam>
    /// <typeparam name="A5">デリゲートの5つめの引数</typeparam>
    /// <typeparam name="A6">デリゲートの6つめの引数</typeparam>
    /// <typeparam name="A7">デリゲートの7つめの引数</typeparam>
    public interface IGenericsFunctor7<R, A1, A2, A3, A4, A5, A6, A7> : IFunctor
    {
        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        GenericsDelegate7<R, A1, A2, A3, A4, A5, A6, A7> ConvertToDelegate7();

        /// <summary>
        /// ファンクタの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <param name="arg4">4つめの引数</param>
        /// <param name="arg5">5つめの引数</param>
        /// <param name="arg6">6つめの引数</param>
        /// <param name="arg7">7つめの引数</param>
        /// <returns>戻り値</returns>
        R Invoke7(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7);
    }

    /// <summary>
    /// Generics対応版ファンクタ
    /// </summary>
    /// <typeparam name="R">デリゲートの戻り値</typeparam>
    /// <typeparam name="A1">デリゲートの1つめの引数</typeparam>
    /// <typeparam name="A2">デリゲートの2つめの引数</typeparam>
    /// <typeparam name="A3">デリゲートの3つめの引数</typeparam>
    /// <typeparam name="A4">デリゲートの4つめの引数</typeparam>
    /// <typeparam name="A5">デリゲートの5つめの引数</typeparam>
    /// <typeparam name="A6">デリゲートの6つめの引数</typeparam>
    /// <typeparam name="A7">デリゲートの7つめの引数</typeparam>
    /// <typeparam name="A8">デリゲートの8つめの引数</typeparam>
    public interface IGenericsFunctor8<R, A1, A2, A3, A4, A5, A6, A7, A8> : IFunctor
    {
        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        GenericsDelegate8<R, A1, A2, A3, A4, A5, A6, A7, A8> ConvertToDelegate8();

        /// <summary>
        /// ファンクタの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <param name="arg4">4つめの引数</param>
        /// <param name="arg5">5つめの引数</param>
        /// <param name="arg6">6つめの引数</param>
        /// <param name="arg7">7つめの引数</param>
        /// <param name="arg8">8つめの引数</param>
        /// <returns>戻り値</returns>
        R Invoke8(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7, A8 arg8);
    }

    /// <summary>
    /// Generics対応版ファンクタ
    /// </summary>
    /// <typeparam name="R">デリゲートの戻り値</typeparam>
    /// <typeparam name="A1">デリゲートの1つめの引数</typeparam>
    /// <typeparam name="A2">デリゲートの2つめの引数</typeparam>
    /// <typeparam name="A3">デリゲートの3つめの引数</typeparam>
    /// <typeparam name="A4">デリゲートの4つめの引数</typeparam>
    /// <typeparam name="A5">デリゲートの5つめの引数</typeparam>
    /// <typeparam name="A6">デリゲートの6つめの引数</typeparam>
    /// <typeparam name="A7">デリゲートの7つめの引数</typeparam>
    /// <typeparam name="A8">デリゲートの8つめの引数</typeparam>
    /// <typeparam name="A9">デリゲートの9つめの引数</typeparam>
    public interface IGenericsFunctor9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> : IFunctor
    {
        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        GenericsDelegate9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> ConvertToDelegate9();

        /// <summary>
        /// ファンクタの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <param name="arg4">4つめの引数</param>
        /// <param name="arg5">5つめの引数</param>
        /// <param name="arg6">6つめの引数</param>
        /// <param name="arg7">7つめの引数</param>
        /// <param name="arg8">8つめの引数</param>
        /// <param name="arg9">9つめの引数</param>
        /// <returns>戻り値</returns>
        R Invoke9(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7, A8 arg8, A9 arg9);
    }
}
