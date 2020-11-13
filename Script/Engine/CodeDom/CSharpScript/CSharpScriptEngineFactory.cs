using System;
using System.IO;
using Kodama.Script.Factory;

namespace Kodama.Script.Engine.CodeDom.CSharpScript
{
    /// <summary>
    /// CSharpScriptEngineを生成するファクトリークラスです
    /// </summary>
    public class CSharpScriptEngineFactory : IScriptEngineFactory
    {
        /// <summary>
        /// 指定されたファイルがコンパイル可能かチェックする
        /// </summary>
        /// <param name="filename">チェックするファイル名</param>
        /// <returns>ファイルがコンパイル可能かどうか</returns>
        public bool CanCompile(string filename)
        {
            return string.Compare(Path.GetExtension(filename), ".cs", true) == 0;
        }

        /// <summary>
        /// スクリプトエンジンのインスタンスを生成する
        /// </summary>
        /// <returns>生成されたインスタンス</returns>
        public IScriptEngine Create()
        {
            return new CSharpScriptEngine();
        }

        /// <summary>
        /// ファイルからスクリプトエンジンのインスタンスを生成する
        /// </summary>
        /// <param name="filename">スクリプトエンジンを生成するファイル名</param>
        /// <returns>生成されたインスタンス</returns>
        public IScriptEngine CreateFromFile(string filename)
        {
            CSharpScriptEngine engine = new CSharpScriptEngine();
            engine.AddScriptCodeFromFile(filename);
            return engine;
        }
    }
}
