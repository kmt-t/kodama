using System;

namespace Kodama.Script.Expression
{
    /// <summary>
    /// JScriptで記述された式を評価します
    /// </summary>
    /// <remarks>
    /// このクラスをビルドする前に、先にKodama.Script.Expression.InternalEvaluatorを
    /// ビルドする必要があります。
    /// </remarks>
    public class Evaluator
    {
        /// <summary>
        /// JScriptで記述された式を評価します
        /// </summary>
        /// <param name="expression">評価する式</param>
        /// <param name="isUnsafe">制限付きのセキュリティコンテキストで実行されないかどうか</param>
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
        /// JScriptで記述された式を評価します
        /// </summary>
        /// <param name="expression">評価する式</param>
        public static object Eval(string expression)
        {
            return InternalEvaluator.Eval(expression);
        }
    }
}
