using System;
using System.Collections;
using System.Reflection;
using Kodama.Function.Functor;

namespace Kodama.Function.Functor.Member
{
    /// <summary>
    /// �C���X�^���X���\�b�h�̌Ăяo�����֐��I�u�W�F�N�g�ɂ������̂ł�
    /// </summary>
    /// <remarks>
    /// C++�ł���STL/Boost��mem_fun�ɑ������܂��B
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// 
    /// // ���̃N���X�̃��\�b�h��MemberFunctor�������ČĂяo���܂�
    /// public class TestClass
    /// {
    ///     // ���̃��\�b�h��MemberFunctor�������ČĂяo���܂�
    ///     public void Print(string arg1, string arg2, string arg3, string arg4)
    ///     {
    ///         Console.WriteLine(arg1);
    ///         Console.WriteLine(arg2);
    ///         Console.WriteLine(arg3);
    ///         Console.WriteLine(arg4);
    ///     }
    /// }
    ///
    /// // ���̊֐����ĂԂƁA�R���\�[����"1"�A"2"�A"3"�A"4"��4�s�\������܂�
    /// public void MemberFunctorTest()
    /// {
    ///     // MemberFunctor�𐶐����܂�
    ///     IFunctor functor = new MemberFunctor(typeof(TestClass).GetMethod("Print"));
    ///
    ///     // MemberFunctor���Ăяo���܂�
    ///     // ���̏ꍇ�A�ȉ��̃R�[�h�Ɠ�������ɂȂ�܂��B
    ///     //    TestClass ts = new TestClass();
    ///     //    ts.Print("1", "2", "3", "4");
    ///     functor.Invoke(new TestClass(), "1", "2", "3", "4");
    /// }
    /// </code>
    /// </example>
    /// <seealso href="http://boost.cppll.jp/HEAD/">Boost���t�@�����X���{���</seealso>
    public class MemberFunctor : IFunctor
    {
        /// <summary>
        /// MemberFunctor�ŌĂяo���C���X�^���X���\�b�h
        /// </summary>
        MethodBase internalMethod;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="method">MemberFunctor�ŌĂяo���C���X�^���X���\�b�h</param>
        public MemberFunctor(MethodBase method)
        {
            internalMethod = method;
        }

        /// <summary>
        /// �֐��I�u�W�F�N�g���f���Q�[�g�ɕϊ����ĕԂ�
        /// </summary>
        /// <returns>�֐��I�u�W�F�N�g����ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorHandler ConvertToDelegate()
        {
            return new FunctorHandler(Invoke);
        }

        /// <summary>
        /// �֐��I�u�W�F�N�g�̌Ăяo��
        /// </summary>
        /// <returns>�֐��I�u�W�F�N�g�̏����̖߂�l</returns>
        /// <exception cref="UnmatchArgumentException">�����̐��ƌ^������Ȃ��ꍇ�͂��̗�O�𓊂���</exception>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// �֐��I�u�W�F�N�g�̌Ăяo��
        /// </summary>
        /// <param name="arguments">�֐��I�u�W�F�N�g�̏����̈���</param>
        /// <returns>�֐��I�u�W�F�N�g�̏����̖߂�l</returns>
        /// <exception cref="UnmatchArgumentException">�����̐��ƌ^������Ȃ��ꍇ�͂��̗�O�𓊂���</exception>
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
