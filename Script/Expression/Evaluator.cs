using System;

namespace Kodama.Script.Expression
{
    /// <summary>
    /// JScript�ŋL�q���ꂽ����]�����܂�
    /// </summary>
    /// <remarks>
    /// ���̃N���X���r���h����O�ɁA���Kodama.Script.Expression.InternalEvaluator��
    /// �r���h����K�v������܂��B
    /// </remarks>
    public class Evaluator
    {
        /// <summary>
        /// JScript�ŋL�q���ꂽ����]�����܂�
        /// </summary>
        /// <param name="expression">�]�����鎮</param>
        /// <param name="isUnsafe">�����t���̃Z�L�����e�B�R���e�L�X�g�Ŏ��s����Ȃ����ǂ���</param>
        public static object Eval(string expression, bool isUnsafe)
        {
            if (isUnsafe)
            {
                return InternalEvaluator.Eval(expression, isUnsafe);
            }
            else{
                return InternalEvaluator.Eval(expression);
            }
        }

        /// <summary>
        /// JScript�ŋL�q���ꂽ����]�����܂�
        /// </summary>
        /// <param name="expression">�]�����鎮</param>
        public static object Eval(string expression)
        {
            return InternalEvaluator.Eval(expression);
        }
    }
}
