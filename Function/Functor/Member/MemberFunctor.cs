using System;
using System.Collections;
using System.Reflection;
using Kodama.Function.Functor;

namespace Kodama.Function.Functor.Member
{
    /// <summary>
    /// インスタンスメソッドの呼び出しを関数オブジェクトにしたものです
    /// </summary>
    /// <remarks>
    /// C++でいうSTL/Boostのmem_funに相当します。
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// 
    /// // このクラスのメソッドをMemberFunctorをつかって呼び出します
    /// public class TestClass
    /// {
    ///     // このメソッドをMemberFunctorをつかって呼び出します
    ///     public void Print(string arg1, string arg2, string arg3, string arg4)
    ///     {
    ///         Console.WriteLine(arg1);
    ///         Console.WriteLine(arg2);
    ///         Console.WriteLine(arg3);
    ///         Console.WriteLine(arg4);
    ///     }
    /// }
    ///
    /// // この関数を呼ぶと、コンソールに"1"、"2"、"3"、"4"と4行表示されます
    /// public void MemberFunctorTest()
    /// {
    ///     // MemberFunctorを生成します
    ///     IFunctor functor = new MemberFunctor(typeof(TestClass).GetMethod("Print"));
    ///
    ///     // MemberFunctorを呼び出します
    ///     // この場合、以下のコードと同じ動作になります。
    ///     //    TestClass ts = new TestClass();
    ///     //    ts.Print("1", "2", "3", "4");
    ///     functor.Invoke(new TestClass(), "1", "2", "3", "4");
    /// }
    /// </code>
    /// </example>
    /// <seealso href="http://boost.cppll.jp/HEAD/">Boostリファレンス日本語訳</seealso>
    public class MemberFunctor : IFunctor
    {
        /// <summary>
        /// MemberFunctorで呼び出すインスタンスメソッド
        /// </summary>
        MethodBase internalMethod;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="method">MemberFunctorで呼び出すインスタンスメソッド</param>
        public MemberFunctor(MethodBase method)
        {
            internalMethod = method;
        }

        /// <summary>
        /// 関数オブジェクトをデリゲートに変換して返す
        /// </summary>
        /// <returns>関数オブジェクトから変換されたデリゲート</returns>
        public FunctorHandler ConvertToDelegate()
        {
            return new FunctorHandler(Invoke);
        }

        /// <summary>
        /// 関数オブジェクトの呼び出し
        /// </summary>
        /// <returns>関数オブジェクトの処理の戻り値</returns>
        /// <exception cref="UnmatchArgumentException">引数の数と型があわない場合はこの例外を投げる</exception>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// 関数オブジェクトの呼び出し
        /// </summary>
        /// <param name="arguments">関数オブジェクトの処理の引数</param>
        /// <returns>関数オブジェクトの処理の戻り値</returns>
        /// <exception cref="UnmatchArgumentException">引数の数と型があわない場合はこの例外を投げる</exception>
        public object Invoke(params object[] arguments)
        {
            ParameterInfo[] paramerterInfos = internalMethod.GetParameters();
            if (arguments.Length != paramerterInfos.Length + 1)
            {
                throw new UnmatchArgumentException();
            }
            for (int i = 0; i < paramerterInfos.Length; ++i)
            {
                if (!paramerterInfos[i].ParameterType.IsAssignableFrom(arguments[i + 1].GetType()))
                {
                    throw new UnmatchArgumentException();
                }
            }

            object[] memberMethodArgument = new object[arguments.Length - 1];
            for (int i = 0; i < memberMethodArgument.Length; ++i)
            {
                memberMethodArgument[i] = arguments[i + 1];
            }

            return internalMethod.Invoke(arguments[0], memberMethodArgument);
        }
    }
}
