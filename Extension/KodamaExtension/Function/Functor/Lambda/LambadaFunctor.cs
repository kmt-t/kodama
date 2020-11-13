using System;
using Kodama.Function.Functor;
using Kodama.Extension.Function.Functor.Generics;

namespace Kodama.Extension.Function.Functor.Lambda
{
    /// <summary>
    /// lambda�t�@���N�^�B�����f���Q�[�g�Ƒg�ݍ��킹�Ă������Ƃ�z�肵�Ă���
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    public class LambadaFunctor0<R> : IGenericsFunctor0<R>
    {
        /// <summary>
        /// �t�@���N�^�̏������s���f���Q�[�g
        /// </summary>
        private GenericsDelegate0<R> internalDelegate;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LambadaFunctor0(GenericsDelegate0<R> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public GenericsDelegate0<R> ConvertToDelegate0()
        {
            return internalDelegate;
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        public object Invoke()
        {
            return Invoke0();
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arguments">����</param>
        /// <returns>�߂�l</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 0)
            {
                throw new UnmatchArgumentException();
            }
            return Invoke0();
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        public R Invoke0()
        {
            return internalDelegate();
        }
    }

    /// <summary>
    /// lambda�t�@���N�^�B�����f���Q�[�g�Ƒg�ݍ��킹�Ă������Ƃ�z�肵�Ă���
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    /// <typeparam name="A1">1�߂̈���</typeparam>
    public class LambadaFunctor1<R, A1> : IGenericsFunctor1<R, A1>
    {
        /// <summary>
        /// �t�@���N�^�̏������s���f���Q�[�g
        /// </summary>
        private GenericsDelegate1<R, A1> internalDelegate;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LambadaFunctor1(GenericsDelegate1<R, A1> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public GenericsDelegate1<R, A1> ConvertToDelegate1()
        {
            return internalDelegate;
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arguments">����</param>
        /// <returns>�߂�l</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 1)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke1((A1)arguments[0]);
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <returns>�߂�l</returns>
        public R Invoke1(A1 arg1)
        {
            return internalDelegate(arg1);
        }
    }

    /// <summary>
    /// lambda�t�@���N�^�B�����f���Q�[�g�Ƒg�ݍ��킹�Ă������Ƃ�z�肵�Ă���
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    /// <typeparam name="A1">1�߂̈���</typeparam>
    /// <typeparam name="A2">2�߂̈���</typeparam>
    public class LambadaFunctor2<R, A1, A2> : IGenericsFunctor2<R, A1, A2>
    {
        /// <summary>
        /// �t�@���N�^�̏������s���f���Q�[�g
        /// </summary>
        private GenericsDelegate2<R, A1, A2> internalDelegate;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LambadaFunctor2(GenericsDelegate2<R, A1, A2> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public GenericsDelegate2<R, A1, A2> ConvertToDelegate2()
        {
            return internalDelegate;
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arguments">����</param>
        /// <returns>�߂�l</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 2)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke2((A1)arguments[0], (A2)arguments[1]);
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <returns>�߂�l</returns>
        public R Invoke2(A1 arg1, A2 arg2)
        {
            return internalDelegate(arg1, arg2);
        }
    }

    /// <summary>
    /// lambda�t�@���N�^�B�����f���Q�[�g�Ƒg�ݍ��킹�Ă������Ƃ�z�肵�Ă���
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    /// <typeparam name="A1">1�߂̈���</typeparam>
    /// <typeparam name="A2">2�߂̈���</typeparam>
    /// <typeparam name="A3">3�߂̈���</typeparam>
    public class LambadaFunctor3<R, A1, A2, A3> : IGenericsFunctor3<R, A1, A2, A3>
    {
        /// <summary>
        /// �t�@���N�^�̏������s���f���Q�[�g
        /// </summary>
        private GenericsDelegate3<R, A1, A2, A3> internalDelegate;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LambadaFunctor3(GenericsDelegate3<R, A1, A2, A3> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public GenericsDelegate3<R, A1, A2, A3> ConvertToDelegate3()
        {
            return internalDelegate;
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arguments">����</param>
        /// <returns>�߂�l</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 3)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke3((A1)arguments[0], (A2)arguments[1], (A3)arguments[2]);
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <returns>�߂�l</returns>
        public R Invoke3(A1 arg1, A2 arg2, A3 arg3)
        {
            return internalDelegate(arg1, arg2, arg3);
        }
    }

    /// <summary>
    /// lambda�t�@���N�^�B�����f���Q�[�g�Ƒg�ݍ��킹�Ă������Ƃ�z�肵�Ă���
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    /// <typeparam name="A1">1�߂̈���</typeparam>
    /// <typeparam name="A2">2�߂̈���</typeparam>
    /// <typeparam name="A3">3�߂̈���</typeparam>
    /// <typeparam name="A4">4�߂̈���</typeparam>
    public class LambadaFunctor4<R, A1, A2, A3, A4> : IGenericsFunctor4<R, A1, A2, A3, A4>
    {
        /// <summary>
        /// �t�@���N�^�̏������s���f���Q�[�g
        /// </summary>
        private GenericsDelegate4<R, A1, A2, A3, A4> internalDelegate;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LambadaFunctor4(GenericsDelegate4<R, A1, A2, A3, A4> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public GenericsDelegate4<R, A1, A2, A3, A4> ConvertToDelegate4()
        {
            return internalDelegate;
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arguments">����</param>
        /// <returns>�߂�l</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 4)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke4((A1)arguments[0], (A2)arguments[1], (A3)arguments[2], (A4)arguments[3]);
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <returns>�߂�l</returns>
        public R Invoke4(A1 arg1, A2 arg2, A3 arg3, A4 arg4)
        {
            return internalDelegate(arg1, arg2, arg3, arg4);
        }
    }

    /// <summary>
    /// lambda�t�@���N�^�B�����f���Q�[�g�Ƒg�ݍ��킹�Ă������Ƃ�z�肵�Ă���
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    /// <typeparam name="A1">1�߂̈���</typeparam>
    /// <typeparam name="A2">2�߂̈���</typeparam>
    /// <typeparam name="A3">3�߂̈���</typeparam>
    /// <typeparam name="A4">4�߂̈���</typeparam>
    /// <typeparam name="A5">5�߂̈���</typeparam>
    public class LambadaFunctor5<R, A1, A2, A3, A4, A5> : IGenericsFunctor5<R, A1, A2, A3, A4, A5>
    {
        /// <summary>
        /// �t�@���N�^�̏������s���f���Q�[�g
        /// </summary>
        private GenericsDelegate5<R, A1, A2, A3, A4, A5> internalDelegate;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LambadaFunctor5(GenericsDelegate5<R, A1, A2, A3, A4, A5> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public GenericsDelegate5<R, A1, A2, A3, A4, A5> ConvertToDelegate5()
        {
            return internalDelegate;
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arguments">����</param>
        /// <returns>�߂�l</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 5)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A5).IsAssignableFrom(arguments[4].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke5(
                (A1)arguments[0],
                (A2)arguments[1],
                (A3)arguments[2],
                (A4)arguments[3],
                (A5)arguments[4]);
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <param name="arg5">5�߂̈���</param>
        /// <returns>�߂�l</returns>
        public R Invoke5(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5)
        {
            return internalDelegate(arg1, arg2, arg3, arg4, arg5);
        }
    }

    /// <summary>
    /// lambda�t�@���N�^�B�����f���Q�[�g�Ƒg�ݍ��킹�Ă������Ƃ�z�肵�Ă���
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    /// <typeparam name="A1">1�߂̈���</typeparam>
    /// <typeparam name="A2">2�߂̈���</typeparam>
    /// <typeparam name="A3">3�߂̈���</typeparam>
    /// <typeparam name="A4">4�߂̈���</typeparam>
    /// <typeparam name="A5">5�߂̈���</typeparam>
    /// <typeparam name="A6">6�߂̈���</typeparam>
    public class LambadaFunctor6<R, A1, A2, A3, A4, A5, A6> :
        IGenericsFunctor6<R, A1, A2, A3, A4, A5, A6>
    {
        /// <summary>
        /// �t�@���N�^�̏������s���f���Q�[�g
        /// </summary>
        private GenericsDelegate6<R, A1, A2, A3, A4, A5, A6> internalDelegate;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LambadaFunctor6(GenericsDelegate6<R, A1, A2, A3, A4, A5, A6> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public GenericsDelegate6<R, A1, A2, A3, A4, A5, A6> ConvertToDelegate6()
        {
            return internalDelegate;
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arguments">����</param>
        /// <returns>�߂�l</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 6)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A5).IsAssignableFrom(arguments[4].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A6).IsAssignableFrom(arguments[5].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke6(
                (A1)arguments[0],
                (A2)arguments[1],
                (A3)arguments[2],
                (A4)arguments[3],
                (A5)arguments[4],
                (A6)arguments[5]);
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <param name="arg5">5�߂̈���</param>
        /// <param name="arg6">6�߂̈���</param>
        /// <returns>�߂�l</returns>
        public R Invoke6(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6)
        {
            return internalDelegate(arg1, arg2, arg3, arg4, arg5, arg6);
        }
    }

    /// <summary>
    /// lambda�t�@���N�^�B�����f���Q�[�g�Ƒg�ݍ��킹�Ă������Ƃ�z�肵�Ă���
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    /// <typeparam name="A1">1�߂̈���</typeparam>
    /// <typeparam name="A2">2�߂̈���</typeparam>
    /// <typeparam name="A3">3�߂̈���</typeparam>
    /// <typeparam name="A4">4�߂̈���</typeparam>
    /// <typeparam name="A5">5�߂̈���</typeparam>
    /// <typeparam name="A6">6�߂̈���</typeparam>
    /// <typeparam name="A7">7�߂̈���</typeparam>
    public class LambadaFunctor7<R, A1, A2, A3, A4, A5, A6, A7> :
        IGenericsFunctor7<R, A1, A2, A3, A4, A5, A6, A7>
    {
        /// <summary>
        /// �t�@���N�^�̏������s���f���Q�[�g
        /// </summary>
        private GenericsDelegate7<R, A1, A2, A3, A4, A5, A6, A7> internalDelegate;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LambadaFunctor7(GenericsDelegate7<R, A1, A2, A3, A4, A5, A6, A7> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public GenericsDelegate7<R, A1, A2, A3, A4, A5, A6, A7> ConvertToDelegate7()
        {
            return internalDelegate;
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arguments">����</param>
        /// <returns>�߂�l</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 7)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A5).IsAssignableFrom(arguments[4].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A6).IsAssignableFrom(arguments[5].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A7).IsAssignableFrom(arguments[6].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke7(
                (A1)arguments[0],
                (A2)arguments[1],
                (A3)arguments[2],
                (A4)arguments[3],
                (A5)arguments[4],
                (A6)arguments[5],
                (A7)arguments[6]);
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <param name="arg5">5�߂̈���</param>
        /// <param name="arg6">6�߂̈���</param>
        /// <param name="arg7">7�߂̈���</param>
        /// <returns>�߂�l</returns>
        public R Invoke7(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7)
        {
            return internalDelegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
    }

    /// <summary>
    /// lambda�t�@���N�^�B�����f���Q�[�g�Ƒg�ݍ��킹�Ă������Ƃ�z�肵�Ă���
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    /// <typeparam name="A1">1�߂̈���</typeparam>
    /// <typeparam name="A2">2�߂̈���</typeparam>
    /// <typeparam name="A3">3�߂̈���</typeparam>
    /// <typeparam name="A4">4�߂̈���</typeparam>
    /// <typeparam name="A5">5�߂̈���</typeparam>
    /// <typeparam name="A6">6�߂̈���</typeparam>
    /// <typeparam name="A7">7�߂̈���</typeparam>
    /// <typeparam name="A8">8�߂̈���</typeparam>
    public class LambadaFunctor8<R, A1, A2, A3, A4, A5, A6, A7, A8> :
        IGenericsFunctor8<R, A1, A2, A3, A4, A5, A6, A7, A8>
    {
        /// <summary>
        /// �t�@���N�^�̏������s���f���Q�[�g
        /// </summary>
        private GenericsDelegate8<R, A1, A2, A3, A4, A5, A6, A7, A8> internalDelegate;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LambadaFunctor8(GenericsDelegate8<R, A1, A2, A3, A4, A5, A6, A7, A8> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public GenericsDelegate8<R, A1, A2, A3, A4, A5, A6, A7, A8> ConvertToDelegate8()
        {
            return internalDelegate;
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arguments">����</param>
        /// <returns>�߂�l</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 8)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A5).IsAssignableFrom(arguments[4].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A6).IsAssignableFrom(arguments[5].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A7).IsAssignableFrom(arguments[6].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A8).IsAssignableFrom(arguments[7].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke8(
                (A1)arguments[0],
                (A2)arguments[1],
                (A3)arguments[2],
                (A4)arguments[3],
                (A5)arguments[4],
                (A6)arguments[5],
                (A7)arguments[6],
                (A8)arguments[7]);
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <param name="arg5">5�߂̈���</param>
        /// <param name="arg6">6�߂̈���</param>
        /// <param name="arg7">7�߂̈���</param>
        /// <param name="arg8">8�߂̈���</param>
        /// <returns>�߂�l</returns>
        public R Invoke8(A1 arg1, A2 arg2, A3 arg3, A4 arg4, A5 arg5, A6 arg6, A7 arg7, A8 arg8)
        {
            return internalDelegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
        }
    }

    /// <summary>
    /// lambda�t�@���N�^�B�����f���Q�[�g�Ƒg�ݍ��킹�Ă������Ƃ�z�肵�Ă���
    /// </summary>
    /// <typeparam name="R">�߂�l�̌^</typeparam>
    /// <typeparam name="A1">1�߂̈���</typeparam>
    /// <typeparam name="A2">2�߂̈���</typeparam>
    /// <typeparam name="A3">3�߂̈���</typeparam>
    /// <typeparam name="A4">4�߂̈���</typeparam>
    /// <typeparam name="A5">5�߂̈���</typeparam>
    /// <typeparam name="A6">6�߂̈���</typeparam>
    /// <typeparam name="A7">7�߂̈���</typeparam>
    /// <typeparam name="A8">8�߂̈���</typeparam>
    /// <typeparam name="A9">9�߂̈���</typeparam>
    public class LambadaFunctor9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> :
        IGenericsFunctor9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9>
    {
        /// <summary>
        /// �t�@���N�^�̏������s���f���Q�[�g
        /// </summary>
        private GenericsDelegate9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> internalDelegate;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public LambadaFunctor9(GenericsDelegate9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> id)
        {
            internalDelegate = id;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public FunctorDelegate ConvertToDelegate()
        {
            return Invoke;
        }

        /// <summary>
        /// �f���Q�[�g�ւ̕ϊ�
        /// </summary>
        /// <returns>�ϊ����ꂽ�f���Q�[�g</returns>
        public GenericsDelegate9<R, A1, A2, A3, A4, A5, A6, A7, A8, A9> ConvertToDelegate9()
        {
            return internalDelegate;
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <returns>�߂�l</returns>
        public object Invoke()
        {
            throw new UnmatchArgumentException();
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arguments">����</param>
        /// <returns>�߂�l</returns>
        public object Invoke(params object[] arguments)
        {
            if (arguments.Length != 9)
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A1).IsAssignableFrom(arguments[0].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A2).IsAssignableFrom(arguments[1].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A3).IsAssignableFrom(arguments[2].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A4).IsAssignableFrom(arguments[3].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A5).IsAssignableFrom(arguments[4].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A6).IsAssignableFrom(arguments[5].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A7).IsAssignableFrom(arguments[6].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A8).IsAssignableFrom(arguments[7].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            if (!typeof(A9).IsAssignableFrom(arguments[8].GetType()))
            {
                throw new UnmatchArgumentException();
            }
            return Invoke9(
                (A1)arguments[0],
                (A2)arguments[1],
                (A3)arguments[2],
                (A4)arguments[3],
                (A5)arguments[4],
                (A6)arguments[5],
                (A7)arguments[6],
                (A8)arguments[7],
                (A9)arguments[8]);
        }

        /// <summary>
        /// ���\�b�h�̌Ăяo��
        /// </summary>
        /// <param name="arg1">1�߂̈���</param>
        /// <param name="arg2">2�߂̈���</param>
        /// <param name="arg3">3�߂̈���</param>
        /// <param name="arg4">4�߂̈���</param>
        /// <param name="arg5">5�߂̈���</param>
        /// <param name="arg6">6�߂̈���</param>
        /// <param name="arg7">7�߂̈���</param>
        /// <param name="arg8">8�߂̈���</param>
        /// <param name="arg9">9�߂̈���</param>
        /// <returns>�߂�l</returns>
        public R Invoke9(
            A1 arg1,
            A2 arg2,
            A3 arg3,
            A4 arg4,
            A5 arg5,
            A6 arg6,
            A7 arg7,
            A8 arg8,
            A9 arg9)
        {
            return internalDelegate(
                arg1,
                arg2,
                arg3,
                arg4,
                arg5,
                arg6,
                arg7,
                arg8,
                arg9);
        }
    }
}
