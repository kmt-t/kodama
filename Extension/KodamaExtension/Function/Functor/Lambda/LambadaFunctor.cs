using System;
using Kodama.Function.Functor;
using Kodama.Extension.Function.Functor.Generics;

namespace Kodama.Extension.Function.Functor.Lambda
{
    /// <summary>
    /// lambdaファンクタ。匿名デリゲートと組み合わせてつかうことを想定している
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    public class LambadaFunctor0<R> : IGenericsFunctor0<R>
    {
        /// <summary>
        /// ファンクタの処理を行うデリゲート
        /// </summary>
        private GenericsDelegate0<R> internalDelegate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LambadaFunctor0(GenericsDelegate0<R> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public GenericsDelegate0<R> ConvertToDelegate0()
        {
            return internalDelegate;
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        public object Invoke()
        {
            return Invoke0();
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arguments">引数</param>
        /// <returns>戻り値</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 0)
            {
                throw new UnmatchArgumentException();
            }
            return Invoke0();
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        public R Invoke0()
        {
            return internalDelegate();
        }
    }

    /// <summary>
    /// lambdaファンクタ。匿名デリゲートと組み合わせてつかうことを想定している
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    /// <typeparam name="A1">1つめの引数</typeparam>
    public class LambadaFunctor1<R, A1> : IGenericsFunctor1<R, A1>
    {
        /// <summary>
        /// ファンクタの処理を行うデリゲート
        /// </summary>
        private GenericsDelegate1<R, A1> internalDelegate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LambadaFunctor1(GenericsDelegate1<R, A1> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public GenericsDelegate1<R, A1> ConvertToDelegate1()
        {
            return internalDelegate;
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arguments">引数</param>
        /// <returns>戻り値</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 1)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke1((A1)arguments[0]);
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <returns>戻り値</returns>
        public R Invoke1(A1 arg1)
        {
            return internalDelegate(arg1);
        }
    }

    /// <summary>
    /// lambdaファンクタ。匿名デリゲートと組み合わせてつかうことを想定している
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    /// <typeparam name="A1">1つめの引数</typeparam>
    /// <typeparam name="A2">2つめの引数</typeparam>
    public class LambadaFunctor2<R, A1, A2> : IGenericsFunctor2<R, A1, A2>
    {
        /// <summary>
        /// ファンクタの処理を行うデリゲート
        /// </summary>
        private GenericsDelegate2<R, A1, A2> internalDelegate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LambadaFunctor2(GenericsDelegate2<R, A1, A2> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public GenericsDelegate2<R, A1, A2> ConvertToDelegate2()
        {
            return internalDelegate;
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arguments">引数</param>
        /// <returns>戻り値</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 2)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke2((A1)arguments[0], (A2)arguments[1]);
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <returns>戻り値</returns>
        public R Invoke2(A1 arg1, A2 arg2)
        {
            return internalDelegate(arg1, arg2);
        }
    }

    /// <summary>
    /// lambdaファンクタ。匿名デリゲートと組み合わせてつかうことを想定している
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    /// <typeparam name="A1">1つめの引数</typeparam>
    /// <typeparam name="A2">2つめの引数</typeparam>
    /// <typeparam name="A3">3つめの引数</typeparam>
    public class LambadaFunctor3<R, A1, A2, A3> : IGenericsFunctor3<R, A1, A2, A3>
    {
        /// <summary>
        /// ファンクタの処理を行うデリゲート
        /// </summary>
        private GenericsDelegate3<R, A1, A2, A3> internalDelegate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LambadaFunctor3(GenericsDelegate3<R, A1, A2, A3> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public GenericsDelegate3<R, A1, A2, A3> ConvertToDelegate3()
        {
            return internalDelegate;
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arguments">引数</param>
        /// <returns>戻り値</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 3)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke3((A1)arguments[0], (A2)arguments[1], (A3)arguments[2]);
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <returns>戻り値</returns>
        public R Invoke3(A1 arg1, A2 arg2, A3 arg3)
        {
            return internalDelegate(arg1, arg2, arg3);
        }
    }

    /// <summary>
    /// lambdaファンクタ。匿名デリゲートと組み合わせてつかうことを想定している
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    /// <typeparam name="A1">1つめの引数</typeparam>
    /// <typeparam name="A2">2つめの引数</typeparam>
    /// <typeparam name="A3">3つめの引数</typeparam>
    /// <typeparam name="A4">4つめの引数</typeparam>
    public class LambadaFunctor4<R, A1, A2, A3, A4> : IGenericsFunctor4<R, A1, A2, A3, A4>
    {
        /// <summary>
        /// ファンクタの処理を行うデリゲート
        /// </summary>
        private GenericsDelegate4<R, A1, A2, A3, A4> internalDelegate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LambadaFunctor4(GenericsDelegate4<R, A1, A2, A3, A4> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public GenericsDelegate4<R, A1, A2, A3, A4> ConvertToDelegate4()
        {
            return internalDelegate;
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arguments">引数</param>
        /// <returns>戻り値</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 4)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke4((A1)arguments[0], (A2)arguments[1], (A3)arguments[2], (A4)arguments[3]);
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <param name="arg4">4つめの引数</param>
        /// <returns>戻り値</returns>
        public R Invoke4(A1 arg1, A2 arg2, A3 arg3, A4 arg4)
        {
            return internalDelegate(arg1, arg2, arg3, arg4);
        }
    }

    /// <summary>
    /// lambdaファンクタ。匿名デリゲートと組み合わせてつかうことを想定している
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    /// <typeparam name="A1">1つめの引数</typeparam>
    /// <typeparam name="A2">2つめの引数</typeparam>
    /// <typeparam name="A3">3つめの引数</typeparam>
    /// <typeparam name="A4">4つめの引数</typeparam>
    /// <typeparam name="A5">5つめの引数</typeparam>
    public class LambadaFunctor5<R, A1, A2, A3, A4, A5> : IGenericsFunctor5<R, A1, A2, A3, A4, A5>
    {
        /// <summary>
        /// ファンクタの処理を行うデリゲート
        /// </summary>
        private GenericsDelegate5<R, A1, A2, A3, A4, A5> internalDelegate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LambadaFunctor5(GenericsDelegate5<R, A1, A2, A3, A4, A5> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public GenericsDelegate5<R, A1, A2, A3, A4, A5> ConvertToDelegate5()
        {
            return internalDelegate;
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arguments">引数</param>
        /// <returns>戻り値</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 5)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A5).IsAssignableFrom(arguments[4].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke5(
                (A1)arguments[0],
                (A2)arguments[1],
                (A3)arguments[2],
                (A4)arguments[3],
                (A5)arguments[4]);
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <param name="arg4">4つめの引数</param>
        /// <param name="arg5">5つめの引数</param>
        /// <returns>戻り値</returns>
        public R Invoke5(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5)
        {
            return internalDelegate(arg1, arg2, arg3, arg4, arg5);
        }
    }

    /// <summary>
    /// lambdaファンクタ。匿名デリゲートと組み合わせてつかうことを想定している
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    /// <typeparam name="A1">1つめの引数</typeparam>
    /// <typeparam name="A2">2つめの引数</typeparam>
    /// <typeparam name="A3">3つめの引数</typeparam>
    /// <typeparam name="A4">4つめの引数</typeparam>
    /// <typeparam name="A5">5つめの引数</typeparam>
    /// <typeparam name="A6">6つめの引数</typeparam>
    public class LambadaFunctor6<R, A1, A2, A3, A4, A5, A6> :
        IGenericsFunctor6<R, A1, A2, A3, A4, A5, A6>
    {
        /// <summary>
        /// ファンクタの処理を行うデリゲート
        /// </summary>
        private GenericsDelegate6<R, A1, A2, A3, A4, A5, A6> internalDelegate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LambadaFunctor6(GenericsDelegate6<R, A1, A2, A3, A4, A5, A6> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public GenericsDelegate6<R, A1, A2, A3, A4, A5, A6> ConvertToDelegate6()
        {
            return internalDelegate;
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arguments">引数</param>
        /// <returns>戻り値</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 6)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A5).IsAssignableFrom(arguments[4].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A6).IsAssignableFrom(arguments[5].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke6(
                (A1)arguments[0],
                (A2)arguments[1],
                (A3)arguments[2],
                (A4)arguments[3],
                (A5)arguments[4],
                (A6)arguments[5]);
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <param name="arg4">4つめの引数</param>
        /// <param name="arg5">5つめの引数</param>
        /// <param name="arg6">6つめの引数</param>
        /// <returns>戻り値</returns>
        public R Invoke6(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6)
        {
            return internalDelegate(arg1, arg2, arg3, arg4, arg5, arg6);
        }
    }

    /// <summary>
    /// lambdaファンクタ。匿名デリゲートと組み合わせてつかうことを想定している
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    /// <typeparam name="A1">1つめの引数</typeparam>
    /// <typeparam name="A2">2つめの引数</typeparam>
    /// <typeparam name="A3">3つめの引数</typeparam>
    /// <typeparam name="A4">4つめの引数</typeparam>
    /// <typeparam name="A5">5つめの引数</typeparam>
    /// <typeparam name="A6">6つめの引数</typeparam>
    /// <typeparam name="A7">7つめの引数</typeparam>
    public class LambadaFunctor7<R, A1, A2, A3, A4, A5, A6, A7> :
        IGenericsFunctor7<R, A1, A2, A3, A4, A5, A6, A7>
    {
        /// <summary>
        /// ファンクタの処理を行うデリゲート
        /// </summary>
        private GenericsDelegate7<R, A1, A2, A3, A4, A5, A6, A7> internalDelegate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LambadaFunctor7(GenericsDelegate7<R, A1, A2, A3, A4, A5, A6, A7> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public GenericsDelegate7<R, A1, A2, A3, A4, A5, A6, A7> ConvertToDelegate7()
        {
            return internalDelegate;
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arguments">引数</param>
        /// <returns>戻り値</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 7)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A5).IsAssignableFrom(arguments[4].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A6).IsAssignableFrom(arguments[5].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A7).IsAssignableFrom(arguments[6].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke7(
                (A1)arguments[0],
                (A2)arguments[1],
                (A3)arguments[2],
                (A4)arguments[3],
                (A5)arguments[4],
                (A6)arguments[5],
                (A7)arguments[6]);
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arg1">1つめの引数</param>
        /// <param name="arg2">2つめの引数</param>
        /// <param name="arg3">3つめの引数</param>
        /// <param name="arg4">4つめの引数</param>
        /// <param name="arg5">5つめの引数</param>
        /// <param name="arg6">6つめの引数</param>
        /// <param name="arg7">7つめの引数</param>
        /// <returns>戻り値</returns>
        public R Invoke7(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7)
        {
            return internalDelegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
    }

    /// <summary>
    /// lambdaファンクタ。匿名デリゲートと組み合わせてつかうことを想定している
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    /// <typeparam name="A1">1つめの引数</typeparam>
    /// <typeparam name="A2">2つめの引数</typeparam>
    /// <typeparam name="A3">3つめの引数</typeparam>
    /// <typeparam name="A4">4つめの引数</typeparam>
    /// <typeparam name="A5">5つめの引数</typeparam>
    /// <typeparam name="A6">6つめの引数</typeparam>
    /// <typeparam name="A7">7つめの引数</typeparam>
    /// <typeparam name="A8">8つめの引数</typeparam>
    public class LambadaFunctor8<R, A1, A2, A3, A4, A5, A6, A7, A8> :
        IGenericsFunctor8<R, A1, A2, A3, A4, A5, A6, A7, A8>
    {
        /// <summary>
        /// ファンクタの処理を行うデリゲート
        /// </summary>
        private GenericsDelegate8<R, A1, A2, A3, A4, A5, A6, A7, A8> internalDelegate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LambadaFunctor8(GenericsDelegate8<R, A1, A2, A3, A4, A5, A6, A7, A8> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public GenericsDelegate8<R, A1, A2, A3, A4, A5, A6, A7, A8> ConvertToDelegate8()
        {
            return internalDelegate;
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arguments">引数</param>
        /// <returns>戻り値</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 8)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A5).IsAssignableFrom(arguments[4].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A6).IsAssignableFrom(arguments[5].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A7).IsAssignableFrom(arguments[6].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A8).IsAssignableFrom(arguments[7].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke8(
                (A1)arguments[0],
                (A2)arguments[1],
                (A3)arguments[2],
                (A4)arguments[3],
                (A5)arguments[4],
                (A6)arguments[5],
                (A7)arguments[6],
                (A8)arguments[7]);
        }

        /// <summary>
        /// メソッドの呼び出し
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
        public R Invoke8(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7, A8 arg8)
        {
            return internalDelegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
    }

    /// <summary>
    /// lambdaファンクタ。匿名デリゲートと組み合わせてつかうことを想定している
    /// </summary>
    /// <typeparam name="R">戻り値の型</typeparam>
    /// <typeparam name="A1">1つめの引数</typeparam>
    /// <typeparam name="A2">2つめの引数</typeparam>
    /// <typeparam name="A3">3つめの引数</typeparam>
    /// <typeparam name="A4">4つめの引数</typeparam>
    /// <typeparam name="A5">5つめの引数</typeparam>
    /// <typeparam name="A6">6つめの引数</typeparam>
    /// <typeparam name="A7">7つめの引数</typeparam>
    /// <typeparam name="A8">8つめの引数</typeparam>
    /// <typeparam name="A9">9つめの引数</typeparam>
    public class LambadaFunctor9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> :
        IGenericsFunctor9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9>
    {
        /// <summary>
        /// ファンクタの処理を行うデリゲート
        /// </summary>
        private GenericsDelegate9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> internalDelegate;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LambadaFunctor9(GenericsDelegate9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// デリゲートへの変換
        /// </summary>
        /// <returns>変換されたデリゲート</returns>
        public GenericsDelegate9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> ConvertToDelegate9()
        {
            return internalDelegate;
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <returns>戻り値</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// メソッドの呼び出し
        /// </summary>
        /// <param name="arguments">引数</param>
        /// <returns>戻り値</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 9)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A5).IsAssignableFrom(arguments[4].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A6).IsAssignableFrom(arguments[5].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A7).IsAssignableFrom(arguments[6].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A8).IsAssignableFrom(arguments[7].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A9).IsAssignableFrom(arguments[8].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke9(
                (A1)arguments[0],
                (A2)arguments[1],
                (A3)arguments[2],
                (A4)arguments[3],
                (A5)arguments[4],
                (A6)arguments[5],
                (A7)arguments[6],
                (A8)arguments[7],
                (A9)arguments[8]);
        }

        /// <summary>
        /// メソッドの呼び出し
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
        public R Invoke9(
            A1 arg1,
            A2 arg2,
            A3 arg3,
            A4 arg4,
            A5 arg5,
            A6 arg6,
            A7 arg7,
            A8 arg8,
            A9 arg9)
        {
            return internalDelegate(
                arg1,
                arg2,
                arg3,
                arg4,
                arg5,
                arg6,
                arg7,
                arg8,
                arg9);
        }
    }
}
