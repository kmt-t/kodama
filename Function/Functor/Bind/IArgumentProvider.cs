using System;

namespace Kodama.Function.Functor.Bind
{
    /// <summary>
    /// バインドする引数をBindFunctor#Invoke呼び出し時に毎に遅延して提供するインターフェイス
    /// </summary>
    /// <remarks>
    /// BindFunctorのバインドする引数としてこれを渡すと、
    /// BindFunctor#Invokeの呼び出しごとにバインドされる引数が
    /// IArgumentProvider#Provideで返される値になる。
    /// C++のbindには同じような機能は存在しません。
    /// </remarks>
    /// <seealso cref="Kodama.Function.Functor.Bind.BindFunctor"/>
    public interface IArgumentProvider
    {
        /// <summary>
        /// 遅延してバインドする引数を取得する
        /// </summary>
        /// <returns>遅延してバインドする引数</returns>
        object Provide();
    }
}
