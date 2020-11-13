using System;
using System.Reflection;

namespace Kodama.Script.Engine
{
    /// <summary>
    /// スクリプトエンジンの基底インターフェイスです
    /// </summary>
    /// <remarks>
    /// 新たなスクリプト言語のサポートはこのインターフェイスを継承してください
    /// </remarks>
    public interface IScriptEngine
    {
        /// <summary>
        /// スクリプトのアセンブリ
        /// </summary>
        /// <remarks>
        /// これをつかってスクリプトのメタ情報を取得できる。
        /// </remarks>
        Assembly ScriptAssembly
        {
            get;
        }

        /// <summary>
        /// デバッグ情報を生成するかどうか
        /// </summary>
        bool GenerateDebugInfo
        {
            get;
            set;
        }

        /// <summary>
        /// スクリプトで参照するアセンブリを追加する
        /// </summary>
        /// <param name="assemblyName">スクリプトで参照するアセンブリ名</param>
        void AddReference(string assemblyName);

        /// <summary>
        /// スクリプトからみえるグローバルなインスタンスを追加する
        /// </summary>
        /// <param name="name">スクリプトから参照するときにつかうインスタンス名</param>
        /// <param name="obj">スクリプトに追加するグローバルなインスタンス</param>
        void AddGlobalIntstance(string name, object obj);

        /// <summary>
        /// スクリプトをファイルから読み出し、既にあるコードに追加する
        /// </summary>
        /// <param name="filename">スクリプトファイル名</param>
        void AddScriptCodeFromFile(string filename);

        /// <summary>
        /// テキストを既にあるコードに追加する
        /// </summary>
        /// <param name="name">スクリプト名</param>
        /// <param name="code">追加するテキスト</param>
        void AddScriptCodeFromText(string name, string code);

        /// <summary>
        /// スクリプトをコンパイルする
        /// </summary>
        void Compile();

        /// <summary>
        /// スクリプトを実行する
        /// </summary>
        void Run();

        /// <summary>
        /// スクリプトの実行を停止する
        /// </summary>
        void Stop();

        /// <summary>
        /// スクリプトのリソースを開放し、スクリプトエンジンを無効にする
        /// </summary>
        /// <remarks>
        /// つかい終わったスクリプトエンジンはこのメソッドを呼び出してください。
        /// </remarks>
        void Close();
    }
}
