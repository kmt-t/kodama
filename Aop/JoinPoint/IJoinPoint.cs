using System;
using System.Runtime.Remoting.Messaging;

namespace Kodama.Aop.JoinPoint
{
    /// <summary>
    /// JoinPoin
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public interface IJoinPoint
    {
        /// <summary>
        /// �ŏ���Interceptor�̃`�F�[���̌Ăяo�����s���܂�
        /// </summary>
        /// <param name="outArgumemnts">���\�b�h��out����</param>
        /// <returns>���\�b�h�̖߂�l</returns>
        /// <remarks>
        /// ������Interceptor���ݒ肳��Ă���ꍇ�AInterceptor�̐�����
        /// IJoinPoint#Proceed��Interceptor#Invoke���ċA�I��
        /// �J��Ԃ��Ăяo�����B���̃��\�b�h�͂��̍ŏ��̌Ăяo����
        /// �����Ȃ��B��{�I�Ƀ��b�Z�[�W�V���N���炵���Ă΂�Ȃ��B
        /// </remarks>
        object FirstProceed(out object[] outArgumemnts);

        /// <summary>
        /// Intercepter�̃`�F�[���̌Ăяo�����s���܂�
        /// </summary>
        /// <param name="outArgumemnts">���\�b�h��out����</param>
        /// <returns>���\�b�h�̖߂�l</returns>
        object Proceed(out object[] outArgumemnts);
    }
}
