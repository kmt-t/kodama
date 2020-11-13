using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Reflection;
using Microsoft.JScript;
using Kodama.Script.Engine;
using Kodama.Script.Engine.CodeDom;

namespace Kodama.Script.Engine.CodeDom.JScript
{
    /// <summary>
    /// JScriptエンジンです
    /// </summary>
    /// <remarks>
    /// このクラスはCodeDomを利用しています。
    /// 極力Visual Studio for Applicationを利用したものではなく、こちらを利用して下さい。
    /// </remarks>
    public class JScriptEngine : IScriptEngine
    {
        /// <summary>
        /// CodeDomのコンパイラを提供
        /// </summary>
        private CodeDomProvider provider = new JScriptCodeProvider();

        /// <summary>
        /// スクリプトのアセンブリ
        /// </summary>
        private Assembly scriptAssembly = null;

        /// <summary>
        /// デバッグ情報を生成するかどうか
        /// </summary>
        private bool generateDebugInfo = false;

        /// <summary>
        /// スクリプトが参照するアセンブリ
        /// </summary>
        private ArrayList referenceAssemblies = new ArrayList();

        /// <summary>
        /// スクリプトコード
        /// </summary>
        private ArrayList scriptCode = new ArrayList();

        /// <summary>
        /// スクリプトのアセンブリを取得する
        /// </summary>
        /// <remarks>
        /// これをつかってスクリプトのメタ情報を取得できる。
        /// </remarks>
        public Assembly ScriptAssembly
        {
            get { return scriptAssembly; }
        }

        /// <summary>
        /// デバッグ情報を生成するかどうか
        /// </summary>
        public bool GenerateDebugInfo
        {
            get { return generateDebugInfo; }
            set { generateDebugInfo = value; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public JScriptEngine()
        {
            AddReference("system.dll");
            AddReference("mscorlib.dll");
            AddReference(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// スクリプトで参照するアセンブリを追加する
        /// </summary>
        /// <remarks>
        /// アセンブリの参照はコンパイル前に追加してください。
        /// </remarks>
        /// <param name="assemblyName">スクリプトで参照するアセンブリ名</param>
        public void AddReference(string assemblyName)
        {
            referenceAssemblies.Add(assemblyName);
        }

        /// <summary>
        /// スクリプトからみえるグローバルなインスタンスを追加する
        /// </summary>
        /// <remarks>
        /// グローバルなインスタンスはコンパイル前に追加してください。
        /// </remarks>
        /// <param name="name">スクリプトから参照するときにつかうインスタンス名</param>
        /// <param name="obj">スクリプトに追加するグローバルなインスタンス</param>
        /// <exception cref="NotSupportedException">
        /// CodeDomではグローバルなインスタンスをサポートしていないので必ずこの例外が発生します
        /// </exception>
        public void AddGlobalIntstance(string name, object obj)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// スクリプトをファイルから読み出し、既にあるコードに追加する
        /// </summary>
        /// <param name="filename">スクリプトファイル名</param>
        public void AddScriptCodeFromFile(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            string code = reader.ReadToEnd();
            reader.Close();
            scriptCode.Add(code);
        }

        /// <summary>
        /// テキストを既にあるコードに追加する
        /// </summary>
        /// <param name="name">スクリプト名</param>
        /// <param name="code">追加するテキスト</param>
        public void AddScriptCodeFromText(string name, string code)
        {
            scriptCode.Add(code);
        }

        /// <summary>
        /// スクリプトをコンパイルする
        /// </summary>
        /// <exception cref="CompileErrorException">
        /// コンパイルエラーが発生した場合はこの例外を投げる
        /// </exception>
        public void Compile()
        {
            ICodeCompiler compiler = provider.CreateCompiler();

            CompilerParameters compilerParams = new CompilerParameters();
            foreach (string assembly in referenceAssemblies)
            {
                compilerParams.ReferencedAssemblies.Add(assembly);
            }
            compilerParams.GenerateInMemory        = true;
            compilerParams.GenerateExecutable      = true;
            compilerParams.IncludeDebugInformation = generateDebugInfo;

            CompilerResults result = compiler.CompileAssemblyFromSourceBatch
                (compilerParams, (string[])scriptCode.ToArray(typeof(string)));   
            foreach (CompilerError error in result.Errors)
            {
                if (!error.IsWarning)
                {
                    throw new CodeDomCompileErrorException(result);
                }
            }

            scriptAssembly = result.CompiledAssembly;
        }

        /// <summary>
        /// スクリプトを実行する
        /// </summary>
        /// <exception cref="ScriptEntryPointNotFoundException">
        /// エントリーポイントがみつからない場合はこの例外を投げる
        /// </exception>
        /// <exception cref="NotCompiledException">
        /// スクリプトがコンパイルされていない場合はこの例外を投げる
        /// </exception>
        public void Run()
        {
            if (scriptAssembly == null)
            {
                throw new NotCompiledException();
            }

            MethodInfo mi = scriptAssembly.EntryPoint;
            if (mi == null)
            {
                throw new ScriptEntryPointNotFoundException();
            }
            mi.Invoke(null, new object[] {new string[] {}});
        }

        /// <summary>
        /// スクリプトの実行を停止する
        /// </summary>
        public void Stop()
        {
            // 特になし
        }

        /// <summary>
        /// スクリプトのリソースを開放し、スクリプトエンジンを無効にする
        /// </summary>
        /// <remarks>
        /// つかい終わったスクリプトエンジンはこのメソッドを呼び出してください。
        /// </remarks>
        public void Close()
        {
            // 特になし
        }
    }
}
