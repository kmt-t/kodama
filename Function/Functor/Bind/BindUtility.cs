using System;
using System.Reflection;
using Kodama.Function.Functor;
using Kodama.Function.Functor.Bind;
using Kodama.Function.Functor.Member;

namespace Kodama.Function.Functor.Bind
{
    /// <summary>
    /// �������o�C���h���ꂽ�R���X�g���N�^�A���\�b�h���擾���邽�߂̃��[�e�B���e�B
    /// </summary>
    public class BindUtility
    {
        /// <summary>
        /// �w�肳�ꂽ�p�����[�^�ň������o�C���h���ꂽ�R���X�g���N�^��Ԃ�
        /// </summary>
        /// <param name="type">�o�C���h����R���X�g���N�^�����^</param>
        /// <param name="arguments">�o�C���h����p�����[�^</param>
        /// <returns>�������o�C���h���ꂽ�R���X�g���N�^</returns>
        /// <exception cref="UnmatchArgumentException">
        /// �w�肳�ꂽ�p�����[�^�ŃR���X�g���N�^���o�C���h�ł��Ȃ������ꍇ�ɔ��������O
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
        /// �w�肳�ꂽ�p�����[�^�ň������o�C���h���ꂽ���\�b�h��Ԃ�
        /// </summary>
        /// <param name="type">�o�C���h���郁�\�b�h�����^</param>
        /// <param name="name">�o�C���h���郁�\�b�h��</param>
        /// <param name="arguments">�o�C���h����p�����[�^</param>
        /// <returns>�������o�C���h���ꂽ���\�b�h</returns>
        /// <exception cref="UnmatchArgumentException">
        /// �w�肳�ꂽ�p�����[�^�Ń��\�b�h���o�C���h�ł��Ȃ������ꍇ�ɔ��������O
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
        /// �w�肳�ꂽ���\�b�h���w�肳�ꂽ�p�����[�^�ň������o�C���h�\���ǂ������肷��
        /// </summary>
        /// <param name="method">�o�C���h�\���ǂ������肷�郁�\�b�h</param>
        /// <param name="arguments">�o�C���h����p�����[�^</param>
        /// <returns>�w�肳�ꂽ���\�b�h���w�肳�ꂽ�p�����[�^�ň������o�C���h�\���ǂ���</returns>
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
