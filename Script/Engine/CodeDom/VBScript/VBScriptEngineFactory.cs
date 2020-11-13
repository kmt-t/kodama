using System;
using System.IO;
using Kodama.Script.Factory;

namespace Kodama.Script.Engine.CodeDom.VBScript
{
    /// <summary>
    /// VBScriptEngineを生成するファクトリークラスです
    /// </summary>
    public class VBScriptEngineFactory : IScriptEngineFactory
    {
        /// <summary>
        /// 指定されたファイルがコンパイル可能かチェックする
        /// </summary>
        /// <param name="filename">チェックするファイル名</param>
        /// <returns>ファイルがコンパイル可能かどうか</returns>
        public bool CanCompile(string filename)
        {
            return string.Compare(Path.GetExtension(filename), ".vb", true) == 0;
        }

        /// <summary>
        /// スクリプトエンジンのインスタンスを生成する
        /// </summary>
        /// <returns>生成されたインスタンス</returns>
        public IScriptEngine Create()
        {
            return new VBScriptEngine();
        }

        /// <summary>
        /// ファイルからスクリプトエンジンのインスタンスを生成する
        /// </summary>
        /// <param name="filename">スクリプトエンジンを生成するファイル名</param>
        /// <returns>生成されたインスタンス</returns>
        public IScriptEngine CreateFromFile(string filename)
        {
            VBScriptEngine engine = new VBScriptEngine();
            engine.AddScriptCodeFromFile(filename);
            return engine;
        }
    }
}
