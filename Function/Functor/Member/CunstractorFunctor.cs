using System;
using System.Collections;
using System.Reflection;
using Kodama.Function.Functor;

namespace Kodama.Function.Functor.Member
{
    /// <summary>
    /// �R���X�g���N�^�̌Ăяo�����֐��I�u�W�F�N�g�ɂ������̂ł�
    /// </summary>
    public class ConstructorFunctor : IFunctor
    {
        /// <summary>
        /// ConstructorFunctor�ŌĂяo���R���X�g���N�^
        /// </summary>
        ConstructorInfo internalConstructor;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="constructor">ConstructorFunctor�ŌĂяo���R���X�g���N�^</param>
        public ConstructorFunctor(ConstructorInfo constructor)
        {
            internalConstructor = constructor;
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
            return Invoke(new object[] {});
        }

        /// <summary>
        /// �֐��I�u�W�F�N�g�̌Ăяo��
        /// </summary>
        /// <param name="arguments">�֐��I�u�W�F�N�g�̏����̈���</param>
        /// <returns>�֐��I�u�W�F�N�g�̏����̖߂�l</returns>
        /// <exception cref="UnmatchArgumentException">�����̐��ƌ^������Ȃ��ꍇ�͂��̗�O�𓊂���</exception>
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
