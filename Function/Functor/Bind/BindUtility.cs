using System;
using System.Reflection;
using Kodama.Function.Functor;
using Kodama.Function.Functor.Bind;
using Kodama.Function.Functor.Member;

namespace Kodama.Function.Functor.Bind
{
    /// <summary>
    /// 引数がバインドされたコンストラクタ、メソッドを取得するためのユーティリティ
    /// </summary>
    public class BindUtility
    {
        /// <summary>
        /// 指定されたパラメータで引数がバインドされたコンストラクタを返す
        /// </summary>
        /// <param name="type">バインドするコンストラクタを持つ型</param>
        /// <param name="arguments">バインドするパラメータ</param>
        /// <returns>引数がバインドされたコンストラクタ</returns>
        /// <exception cref="UnmatchArgumentException">
        /// 指定されたパラメータでコンストラクタがバインドできなかった場合に発生する例外
        /// </exception>
        public static BindFunctor CreateBindConstructor(Type type, params object[] arguments)
        {
            foreach (ConstructorInfo ci in type.GetConstructors())
            {
                if (IsCompatibleArguments(ci, arguments))
                {
                    return new BindFunctor(new ConstructorFunctor(ci), arguments);
                }
            }

            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// 指定されたパラメータで引数がバインドされたメソッドを返す
        /// </summary>
        /// <param name="type">バインドするメソッドを持つ型</param>
        /// <param name="name">バインドするメソッド名</param>
        /// <param name="arguments">バインドするパラメータ</param>
        /// <returns>引数がバインドされたメソッド</returns>
        /// <exception cref="UnmatchArgumentException">
        /// 指定されたパラメータでメソッドがバインドできなかった場合に発生する例外
        /// </exception>
        public static BindFunctor CreateBindMember(Type type, string name, params object[] arguments)
        {
            object[] offsetArguments = new object[arguments.Length - 1];
            Array.Copy(arguments, 1, offsetArguments, 0, offsetArguments.Length);

            if (!type.IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }

            foreach (MethodInfo mi in type.GetMethods())
            {
                if (string.Compare(mi.Name, name) != 0)
                {
                    continue;
                }
                if (IsCompatibleArguments(mi, offsetArguments))
                {
                    return new BindFunctor(new MemberFunctor(mi), arguments);
                }
            }

            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// 指定されたメソッドが指定されたパラメータで引数をバインド可能かどうか判定する
        /// </summary>
        /// <param name="method">バインド可能かどうか判定するメソッド</param>
        /// <param name="arguments">バインドするパラメータ</param>
        /// <returns>指定されたメソッドが指定されたパラメータで引数をバインド可能かどうか</returns>
        private static bool IsCompatibleArguments(MethodBase method, object[] arguments)
        {
            ParameterInfo[] paramerterInfos = method.GetParameters();

            if (arguments.Length != paramerterInfos.Length)
            {
                return false;
            }

            for (int i = 0; i < paramerterInfos.Length; ++i)
            {
                if (arguments[i] is NotBoundArgument)
                {
                    continue;
                }
                if (arguments[i] is IArgumentProvider)
                {
                    continue;
                }
                if (!paramerterInfos[i].ParameterType.IsAssignableFrom(arguments[i].GetType()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
