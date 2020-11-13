using System;
using System.Collections;
using Kodama.Function.Functor;

namespace Kodama.Function.Functor.Bind
{
    /// <summary>
    /// 他の関数オブジェクトの引数をバインドする関数オブジェクトです
    /// </summary>
    /// <remarks>
    /// C++でいうSTL/Boostのbindに相当します。
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// 
    /// public class TestClass1
    /// {
    ///     public void Print(string arg1, string arg2, object arg3, string arg4)
    ///     {
    ///         Console.WriteLine(arg1);
    ///         Console.WriteLine(arg2);
    ///         Console.WriteLine(arg3.ToString());
    ///         Console.WriteLine(arg4);
    ///     }
    /// }
    ///
    /// public class TestClass2
    /// {
    ///     public override string ToString()
    ///     {
    ///         return "3";
    ///     }
    /// }
    ///
    /// // この関数を呼ぶと、コンソールに"1"、"2"、"3"、"4"と4行表示されます
    /// public void BindFunctorTest()
    /// {
    ///     // 引数をバインドする対象のMemberFunctorを生成する
    ///     IFunctor memberFunctor = new MemberFunctor(typeof(TestClass1).GetMethod("Print"));
    ///
    ///     // BindFunctorを生成する。このとき引数のバインドを行う
    ///     IFunctor bindFunctor = new BindFunctor(
    ///         // 引数をバインドするMemberFunctor
    ///         memberFunctor,
    ///         // methodFunctorの第1引数はバインドしない引数の指定
    ///         new NotBoundArgument(0),
    ///         // methodFunctorの第2引数をバインド
    ///         "1",
    ///         // methodFunctorの第3引数はバインドしない引数の指定
    ///         new NotBoundArgument(1),
    ///         // BindFunctor#Invoke毎に新しいTestClass2のインスタンスがバインドされる
    ///         BindUtility.Create(typeof(TestClass2), null), 
    ///         // methodFunctorの第5引数をバインド
    ///         "4"});
    ///
    ///     // 関数オブジェクトの呼び出し
    ///     // ここでバインドしてない引数を渡します
    ///     bindFunctor.Invoke(new TestClass1(), "2");
    /// }
    /// </code>
    /// </example>
    /// <seealso href="http://boost.cppll.jp/HEAD/">Boostリファレンス日本語訳</seealso>
    public class BindFunctor : IFunctor
    {
        /// <summary>
        /// 引数をバインドする関数オブジェクト
        /// </summary>
        private IFunctor internalFunctor;

        /// <summary>
        /// バインドされた引数
        /// </summary>
        private object[] boundArguments;

        /// <summary>
        /// 関数オブジェクトをデリゲートに変換して返す
        /// </summary>
        /// <returns>関数オブジェクトから変換されたデリゲート</returns>
        public FunctorHandler ConvertToDelegate()
        {
            return new FunctorHandler(Invoke);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="functor">バインドする関数オブジェクト</param>
        /// <param name="arguments">バインドする引数</param>
        public BindFunctor(IFunctor functor, params object[] arguments)
        {
            internalFunctor = functor;
            boundArguments  = arguments;
        }

        /// <summary>
        /// バインドされた引数を返します
        /// </summary>
        /// <returns>バインドされた引数</returns>
        public object[] GetBoundArguments()
        {
            return boundArguments;
        }

        /// <summary>
        /// 関数オブジェクトの呼び出し
        /// </summary>
        /// <returns>関数オブジェクトの処理の戻り値</returns>
        /// <exception cref="UnmatchArgumentException">引数の数と型があわない場合はこの例外を投げる</exception>
        public object Invoke()
        {
            ArrayList list = new ArrayList();
            foreach (object arg in boundArguments)
            {
                if (arg is NotBoundArgument)
                {
                    throw new UnmatchArgumentException();
                }

                IArgumentProvider provider = arg as IArgumentProvider;
                if (provider != null)
                {
                    list.Add(provider.Provide());
                    continue;
                }

                list.Add(arg);
            }
            return internalFunctor.Invoke(list.ToArray());
        }

        /// <summary>
        /// 関数オブジェクトの呼び出し
        /// </summary>
        /// <param name="arguments">関数オブジェクトの処理の引数</param>
        /// <returns>関数オブジェクトの処理の戻り値</returns>
        /// <exception cref="UnmatchArgumentException">引数の数と型があわない場合はこの例外を投げる</exception>
        public object Invoke(params object[] arguments)
        {
            ArrayList list = new ArrayList();
            foreach (object arg in boundArguments)
            {
                NotBoundArgument notBoundArgument = arg as NotBoundArgument;
                if (notBoundArgument != null)
                {
                    if (arguments.Length <= notBoundArgument.Index)
                    {
                        throw new UnmatchArgumentException();
                    }
                    list.Add(arguments[notBoundArgument.Index]);
                    continue;
                }

                IArgumentProvider provider = arg as IArgumentProvider;
                if (provider != null)
                {
                    list.Add(provider.Provide());
                    continue;
                }

                list.Add(arg);
            }
            return internalFunctor.Invoke(list.ToArray());
        }
    }
}
