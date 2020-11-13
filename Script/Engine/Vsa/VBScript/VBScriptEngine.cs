using System;
using System.Collections;
using System.IO;
using System.Reflection;
using Microsoft.Vsa;
using Kodama.Script.Engine;
using Kodama.Script.Engine.Vsa;

namespace Kodama.Script.Engine.Vsa.VBScript
{
    /// <summary>
    /// VBScriptエンジンです
    /// </summary>
    /// <remarks>
    /// このクラスはVisual Studio for Applicationを利用しています。
    /// これは.NET Framework2.0ではObsoleteになるので、極力CodeDomを
    /// 利用したものを利用してください。
    /// </remarks>
    public class VBScriptEngine : IScriptEngine
    {
        /// <summary>
        /// IVsaSiteを継承するクラス。内部的に使用する
        /// </summary>
        private class VBScriptVsaSite : IVsaSite
        {
            /// <summary>
            /// デフォルトルートモニカ
            /// </summary>
            private const string DEFAULT_ROOT_MONIKER = "Kodama://Script/";

            /// <summary>
            /// デフォルトルート名前空間
            /// </summary>
            private const string DEFAULT_ROOT_NAME_SPACE = "Kodama.Script";

            /// <summary>
            /// Vsaエンジン
            /// </summary>
            private IVsaEngine vsaEngine = new Microsoft.VisualBasic.Vsa.VsaEngine();

            /// <summary>
            /// コンパイルエラー
            /// </summary>
            private ArrayList errors = new ArrayList();

            /// <summary>
            /// スクリプトから参照できるグローバルオブジェクト
            /// </summary>
            private Hashtable globalObjectMap = new Hashtable();

            /// <summary>
            /// スクリプトのアセンブリ
            /// </summary>
            /// <remarks>
            /// これをつかってスクリプトのメタ情報を取得できる
            /// </remarks>
            public Assembly ScriptAssembly 
            {
                get { return vsaEngine.Assembly; }
            }

            /// <summary>
            /// デバッグ情報を生成するかどうか
            /// </summary>
            public bool GenerateDebugInfo
            {
                get { return vsaEngine.GenerateDebugInfo; }
                set { vsaEngine.GenerateDebugInfo = value; }
            }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            public VBScriptVsaSite()
            {
                // 以前にコンパイルしたスクリプトコードがゴミとして残ることがある
                // (IVsaEngineのバグ?)のでルートモニカは毎回ユニークな名前とする
                vsaEngine.RootMoniker = DEFAULT_ROOT_MONIKER + Guid.NewGuid().ToString() + "/";
                vsaEngine.Site        = this;
                vsaEngine.InitNew();
                vsaEngine.RootNamespace     = DEFAULT_ROOT_NAME_SPACE;
                vsaEngine.GenerateDebugInfo = true;
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
                IVsaReferenceItem refItem = (IVsaReferenceItem)vsaEngine.Items.CreateItem
                    (assemblyName, VsaItemType.Reference, VsaItemFlag.None);
                refItem.AssemblyName = assemblyName;
            }

            /// <summary>
            /// スクリプトからみえるグローバルなインスタンスを追加する
            /// </summary>
            /// <remarks>
            /// グローバルなインスタンスはコンパイル前に追加してください。
            /// </remarks>
            /// <param name="name">スクリプトから参照するときにつかうインスタンス名</param>
            /// <param name="globalObject">スクリプトに追加するグローバルなインスタンス</param>
            public void AddGlobalObject(string name, object globalObject)
            {
                IVsaGlobalItem  globalItem = (IVsaGlobalItem)vsaEngine.Items.CreateItem
                    (name, VsaItemType.AppGlobal, VsaItemFlag.None);
                globalItem.TypeString = globalObject.GetType().FullName;
                globalObjectMap.Add(name, globalObject);
            }

            /// <summary>
            /// スクリプトコードを追加する
            /// </summary>
            /// <param name="name">スクリプトコード名</param>
            /// <param name="code">追加するスクリプトコード</param>
            public void AddScriptCode(string name, string code)
            {
                IVsaCodeItem codeItem = (IVsaCodeItem)vsaEngine.Items.CreateItem
                    (name,  VsaItemType.Code, VsaItemFlag.None);
                codeItem.SourceText = code;
            }

            /// <summary>
            /// スクリプトをコンパイルする
            /// </summary>
            /// <exception cref="VsaCompileErrorException">
            /// コンパイルエラーが発生した場合はこの例外を投げる
            /// </exception>
            public void Compile()
            {
                errors.Clear();
                if (!vsaEngine.Compile())
                {
//                    IVsaError[] cei = (IVsaError[])errors.ToArray(typeof(IVsaError));   // 何故か例外が発生するときがある
                    int size = 0;
                    for (int i = 0; i < errors.Count; ++i)
                    {
                        // ありえないはずだけど、何故かエラーが発生するので念のため
                        if (errors[i] is IVsaError)
                        {
                            ++size;
                        }
                    }
                    IVsaError[] cei = new IVsaError[size];
                    for (int i = 0, j = 0; i < cei.Length; ++i, ++j)
                    {
                        // ありえないはずだけど、何故かエラーが発生するので念のため
                        if (errors[i] is IVsaError)
                        {
                            cei[j] = (IVsaError)errors[i];
                        }
                    }
                    throw new VsaCompileErrorException(cei);
                }
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
            /// <exception cref="ScriptEntryPointNotFoundException">
            /// エントリーポイントがみつからない場合はこの例外を投げる
            /// </exception>
            /// <remarks>
            /// スクリプトを実行するにはエントリーポイントが必要です。
            /// エントリーポイントとはスクリプトの一番最初に起動する
            /// メソッドのことをいいます。このエンジンでのVBScriptの
            /// エントリーポイントは、関数名が"Main"で引数なしの静的な
            /// メソッドで、ScriptEntryPointAttribute属性がついているものとします。
            /// </remarks>
            public void Run()
            {
                if (!vsaEngine.IsCompiled)
                {
                    throw new NotCompiledException();
                }
                vsaEngine.Run();

                MethodInfo entryPoint = FindEntryPoint();
                entryPoint.Invoke(null, null);
            }

            /// <summary>
            /// スクリプトの実行を停止する
            /// </summary>
            public void Stop()
            {
                vsaEngine.Reset();
            }

            /// <summary>
            /// スクリプトのリソースを開放し、スクリプトエンジンを無効にする
            /// </summary>
            /// <remarks>
            /// つかい終わったスクリプトエンジンはこのメソッドを呼び出してください。
            /// </remarks>
            public void Close()
            {
                if (vsaEngine.IsRunning)
                {
                    Stop();
                }
                vsaEngine.RevokeCache();
                vsaEngine.Close();
                globalObjectMap.Clear();
                errors.Clear();
            }

            /// <summary>
            /// スクリプトからエントリーポイントを探す
            /// </summary>
            /// <returns>見つかったエントリーポイント</returns>
            /// <exception cref="ScriptEntryPointNotFoundException">
            /// エントリーポイントがみつからない場合はこの例外を投げる
            /// </exception>
            /// <remarks>
            /// エントリーポイントとはスクリプトの一番最初に起動する
            /// メソッドのことをいいます。このエンジンでのVBScriptの
            /// エントリーポイントは、関数名が"Main"で引数なしの静的な
            /// メソッドで、ScriptEntryPointAttribute属性がついているものとします。
            /// </remarks>
            private MethodInfo FindEntryPoint()
            {
                foreach (Type type in vsaEngine.Assembly.GetTypes())
                {
                    MethodInfo mi = type.GetMethod("Main");
                    if (mi == null)
                    {
                        continue;
                    }
                    if (!mi.IsStatic)
                    {
                        continue;
                    }
                    if (mi.GetParameters().Length != 0)
                    {
                        continue;
                    }
                    if (!Attribute.IsDefined(mi, typeof(ScriptEntryPointAttribute)))
                    {
                        continue;
                    }
                    return mi;
                }
                throw new ScriptEntryPointNotFoundException();
            }

            /// <summary>
            /// スクリプトから見えるグローバルオブジェクトを取得する
            /// </summary>
            /// <param name="name">グローバルオブジェクトの名前</param>
            /// <returns>グローバルオブジェクト</returns>
            object IVsaSite.GetGlobalInstance(string name)
            {
                if (globalObjectMap.Contains(name))
                {
                    return globalObjectMap[name];
                }
                return null;
            }

            /// <summary>
            /// ???
            /// </summary>
            /// <param name="itemName">???</param>
            /// <param name="eventSourceName">???</param>
            /// <returns>???</returns>
            object IVsaSite.GetEventSourceInstance(string itemName, string eventSourceName)
            {
                if (globalObjectMap.Contains(eventSourceName))
                {
                    return globalObjectMap[eventSourceName];
                }
                return null;
            }

            /// <summary>
            /// ???
            /// </summary>
            /// <param name="notify">???</param>
            /// <param name="info">???</param>
            void IVsaSite.Notify(string notify, object info)
            {
            }

            /// <summary>
            /// ???
            /// </summary>
            /// <param name="pe">???</param>
            /// <param name="debugInfo">???</param>
            void IVsaSite.GetCompiledState(out Byte[] pe, out Byte[] debugInfo)
            {
                pe = debugInfo = null;
            }

            /// <summary>
            /// コンパイルエラー発生時に呼ばれるメソッド
            /// </summary>
            /// <param name="err">コンパイルエラー</param>
            /// <returns>コンパイルエラーを継続して報告するかどうか</returns>
            bool IVsaSite.OnCompilerError(IVsaError err)
            {
                errors.Add(err);
                return true;
            }
        }

        /// <summary>
        /// IVsaSiteを実装する内部エンジン
        /// </summary>
        VBScriptVsaSite site = new VBScriptVsaSite();

        /// <summary>
        /// スクリプトのアセンブリを取得する
        /// </summary>
        /// <remarks>
        /// これをつかってスクリプトのメタ情報を取得できる。
        /// </remarks>
        public Assembly ScriptAssembly
        {
            get { return site.ScriptAssembly; }
        }

        /// <summary>
        /// デバッグ情報を生成するかどうか
        /// </summary>
        public bool GenerateDebugInfo
        {
            get { return site.GenerateDebugInfo; }
            set { site.GenerateDebugInfo = value; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public VBScriptEngine()
        {
            site.AddReference("system.dll");
            site.AddReference("mscorlib.dll");
            site.AddReference(Assembly.GetExecutingAssembly().Location);
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
            site.AddReference(assemblyName);
        }

        /// <summary>
        /// スクリプトからみえるグローバルなインスタンスを追加する
        /// </summary>
        /// <remarks>
        /// グローバルなインスタンスはコンパイル前に追加してください。
        /// </remarks>
        /// <param name="name">スクリプトから参照するときにつかうインスタンス名</param>
        /// <param name="obj">スクリプトに追加するグローバルなインスタンス</param>
        public void AddGlobalIntstance(string name, object obj)
        {
            site.AddGlobalObject(name, obj);
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
            site.AddScriptCode(Path.GetFileNameWithoutExtension(filename), code);
        }

        /// <summary>
        /// テキストを既にあるコードに追加する
        /// </summary>
        /// <param name="name">スクリプト名</param>
        /// <param name="code">追加するテキスト</param>
        public void AddScriptCodeFromText(string name, string code)
        {
            site.AddScriptCode(name, code);
        }

        /// <summary>
        /// スクリプトをコンパイルする
        /// </summary>
        /// <exception cref="VsaCompileErrorException">
        /// コンパイルエラーが発生した場合はこの例外を投げる
        /// </exception>
        public void Compile()
        {
            site.Compile();
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
        /// <remarks>
        /// スクリプトを実行するにはエントリーポイントが必要です。
        /// エントリーポイントとはスクリプトの一番最初に起動する
        /// メソッドのことをいいます。このエンジンでのVBScriptの
        /// エントリーポイントは、関数名が"Main"で引数なしの静的な
        /// メソッドで、ScriptEntryPointAttribute属性がついているものとします。
        /// </remarks>
        public void Run()
        {
            site.Run();
        }

        /// <summary>
        /// スクリプトの実行を停止する
        /// </summary>
        public void Stop()
        {
            site.Stop();
        }

        /// <summary>
        /// スクリプトのリソースを開放し、スクリプトエンジンを無効にする
        /// </summary>
        /// <remarks>
        /// つかい終わったスクリプトエンジンはこのメソッドを呼び出してください。
        /// </remarks>
        public void Close()
        {
            site.Close();
        }
    }
}
