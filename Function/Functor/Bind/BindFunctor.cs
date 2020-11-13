using System;
using System.Collections;
using Kodama.Function.Functor;

namespace Kodama.Function.Functor.Bind
{
    /// <summary>
    /// ���̊֐��I�u�W�F�N�g�̈������o�C���h����֐��I�u�W�F�N�g�ł�
    /// </summary>
    /// <remarks>
    /// C++�ł���STL/Boost��bind�ɑ������܂��B
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
    /// // ���̊֐����ĂԂƁA�R���\�[����"1"�A"2"�A"3"�A"4"��4�s�\������܂�
    /// public void BindFunctorTest()
    /// {
    ///     // �������o�C���h����Ώۂ�MemberFunctor�𐶐�����
    ///     IFunctor memberFunctor = new MemberFunctor(typeof(TestClass1).GetMethod("Print"));
    ///
    ///     // BindFunctor�𐶐�����B���̂Ƃ������̃o�C���h���s��
    ///     IFunctor bindFunctor = new BindFunctor(
    ///         // �������o�C���h����MemberFunctor
    ///         memberFunctor,
    ///         // methodFunctor�̑�1�����̓o�C���h���Ȃ������̎w��
    ///         new NotBoundArgument(0),
    ///         // methodFunctor�̑�2�������o�C���h
    ///         "1",
    ///         // methodFunctor�̑�3�����̓o�C���h���Ȃ������̎w��
    ///         new NotBoundArgument(1),
    ///         // BindFunctor#Invoke���ɐV����TestClass2�̃C���X�^���X���o�C���h�����
    ///         BindUtility.Create(typeof(TestClass2), null), 
    ///         // methodFunctor�̑�5�������o�C���h
    ///         "4"});
    ///
    ///     // �֐��I�u�W�F�N�g�̌Ăяo��
    ///     // �����Ńo�C���h���ĂȂ�������n���܂�
    ///     bindFunctor.Invoke(new TestClass1(), "2");
    /// }
    /// </code>
    /// </example>
    /// <seealso href="http://boost.cppll.jp/HEAD/">Boost���t�@�����X���{���</seealso>
    public class BindFunctor : IFunctor
    {
        /// <summary>
        /// �������o�C���h����֐��I�u�W�F�N�g
        /// </summary>
        private IFunctor internalFunctor;

        /// <summary>
        /// �o�C���h���ꂽ����
        /// </summary>
        private object[] boundArguments;

        /// <summary>
        /// �֐��I�u�W�F�N�g���f���Q�[�g�ɕϊ����ĕԂ�
        /// </summary>
        /// <returns>�֐��I�u�W�F�N�g����ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorHandler ConvertToDelegate()
        {
            return new FunctorHandler(Invoke);
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="functor">�o�C���h����֐��I�u�W�F�N�g</param>
        /// <param name="arguments">�o�C���h�������</param>
        public BindFunctor(IFunctor functor, params object[] arguments)
        {
            internalFunctor = functor;
            boundArguments  = arguments;
        }

        /// <summary>
        /// �o�C���h���ꂽ������Ԃ��܂�
        /// </summary>
        /// <returns>�o�C���h���ꂽ����</returns>
        public object[] GetBoundArguments()
        {
            return boundArguments;
        }

        /// <summary>
        /// �֐��I�u�W�F�N�g�̌Ăяo��
        /// </summary>
        /// <returns>�֐��I�u�W�F�N�g�̏����̖߂�l</returns>
        /// <exception cref="UnmatchArgumentException">�����̐��ƌ^������Ȃ��ꍇ�͂��̗�O�𓊂���</exception>
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
        /// �֐��I�u�W�F�N�g�̌Ăяo��
        /// </summary>
        /// <param name="arguments">�֐��I�u�W�F�N�g�̏����̈���</param>
        /// <returns>�֐��I�u�W�F�N�g�̏����̖߂�l</returns>
        /// <exception cref="UnmatchArgumentException">�����̐��ƌ^������Ȃ��ꍇ�͂��̗�O�𓊂���</exception>
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
