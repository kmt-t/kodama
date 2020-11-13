using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Kodama.Aop.Pointcut.Compose;

namespace Kodama.Aop.Pointcut.Compose.Class
{
    /// <summary>
    /// 正規表現とクラスの名前でフィルタリングするPointcut
    /// </summary>
    /// <seealso cref="Kodama.Aop.Weaver.AspectWeaver"/>
    /// <seealso href="http://aopalliance.sourceforge.net/doc/index.html">AOP Alliance</seealso>
    public class ClassNamePointcut : ComposiblePointcut
    {
        /// <summary>
        /// フィルタリングするクラス名の正規表現
        /// </summary>
        private Regex regex;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">フィルタリングするクラス名の正規表現</param>
        public ClassNamePointcut(string name)
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
            Match m = regex.Match(method.ReflectedType.FullName);
            return m.Success && (string.Compare(method.ReflectedType.FullName, m.Value) == 0);
        }
    }
}
