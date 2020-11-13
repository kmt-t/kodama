using System;
using System.Collections;
using System.Reflection;
using Kodama.Function.Functor;

namespace Kodama.Function.Functor.Member
{
    /// <summary>
    /// コンストラクタの呼び出しを関数オブジェクトにしたものです
    /// </summary>
    public class ConstructorFunctor : IFunctor
    {
        /// <summary>
        /// ConstructorFunctorで呼び出すコンストラクタ
        /// </summary>
        ConstructorInfo internalConstructor;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="constructor">ConstructorFunctorで呼び出すコンストラクタ</param>
        public ConstructorFunctor(ConstructorInfo constructor)
        {
            internalConstructor = constructor;
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
            return Invoke(new object[] {});
        }

        /// <summary>
        /// 関数オブジェクトの呼び出し
        /// </summary>
        /// <param name="arguments">関数オブジェクトの処理の引数</param>
        /// <returns>関数オブジェクトの処理の戻り値</returns>
        /// <exception cref="UnmatchArgumentException">引数の数と型があわない場合はこの例外を投げる</exception>
        public object Invoke(params object[] arguments)
        {
            ParameterInfo[] paramerterInfos = internalConstructor.GetParameters();
            if (arguments.Length != paramerterInfos.Length)
            {
                throw new UnmatchArgumentException();
            }
            for (int i = 0; i < paramerterInfos.Length; ++i)
            {
                if (!paramerterInfos[i].ParameterType.IsAssignableFrom(arguments[i].GetType()))
                {
                    throw new UnmatchArgumentException();
                }
            }

            return internalConstructor.Invoke(arguments);
        }
    }
}
