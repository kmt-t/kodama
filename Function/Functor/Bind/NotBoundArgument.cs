using System;

namespace Kodama.Function.Functor.Bind
{
    /// <summary>
    /// BindFunctorでバインドしないパラメータはこのクラスのインスタンスを渡します
    /// </summary>
    /// <remarks>
    /// C++でいうBoostのbindの_1、_2、_3と同一のものです。
    /// </remarks>
    /// <seealso cref="Kodama.Function.Functor.Bind.BindFunctor"/>
    /// <seealso href="http://boost.cppll.jp/HEAD/">Boostリファレンス日本語訳</seealso>
    public class NotBoundArgument
    {
        /// <summary>
        /// BoundFunctor#Invokeの何番目の引数にあたるか示すインデックス
        /// </summary>
        private int argumentIndex;

        /// <summary>
        /// BoundFunctor#Invokeの何番目の引数にあたるか示すインデックス
        /// </summary>
        public int Index
        {
            get { return argumentIndex; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="index">BoundFunctor#Invokeの何番目の引数にあたるか示すインデックス</param>
        public NotBoundArgument(int index)
        {
            argumentIndex = index;
        }
    }
}
