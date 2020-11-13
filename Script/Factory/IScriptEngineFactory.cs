using System;
using Kodama.Script.Engine;

namespace Kodama.Script.Factory
{
    /// <summary>
    /// スクリプトエンジンのファクトリーインターフェイス
    /// </summary>
    public interface IScriptEngineFactory
    {
        /// <summary>
        /// 指定されたファイルがコンパイル可能かチェックする
        /// </summary>
        /// <param name="filename">チェックするファイル名</param>
        /// <returns>ファイルがコンパイル可能かどうか</returns>
        bool CanCompile(string filename);

        /// <summary>
        /// スクリプトエンジンのインスタンスを生成する
        /// </summary>
        /// <returns>生成されたインスタンス</returns>
        IScriptEngine Create();

        /// <summary>
        /// ファイルからスクリプトエンジンのインスタンスを生成する
        /// </summary>
        /// <param name="filename">スクリプトエンジンを生成するファイル名</param>
        /// <returns>生成されたインスタンス</returns>
        IScriptEngine CreateFromFile(string filename);
    }
}
