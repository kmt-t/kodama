using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Kodama.Aop.Pointcut.Compose;

namespace Kodama.Aop.Pointcut.Compose.Method
{
    /// <summary>
    /// 正規表現とメソッドの名前でフィルタリングするPointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class MethodNamePointcut : ComposiblePointcut
    {
        /// <summary>
        /// フィルタリングするメソッド名の正規表現
        /// </summary>
        private Regex regex;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">フィルタリングするメソッド名の正規表現</param>
        public MethodNamePointcut(string name)
        {
            regex = new Regex(name);
        }

        /// <summary>
        /// 指定されたメソッドがアスペクトの対象となるかチェックします
        /// </summary>
        /// <param name="method">チェックするメソッド</param>
        /// <returns>指定されたメソッドがアスペクトの対象となるかどうか</returns>
        public override bool IsApplied(MethodBase method)
        {
            Match m = regex.Match(method.Name);
            return m.Success && (string.Compare(method.Name, m.Value) == 0);
        }
    }
}
