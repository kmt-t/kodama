using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Windows.Forms;
using Kodama.BootLoader.Schema;
using Kodama.Script.Engine;
using Kodama.Script.Factory;
using Kodama.Script.Engine.CodeDom.CSharpScript;
using Kodama.Script.Engine.CodeDom.JScript;
using Kodama.Script.Engine.CodeDom.VBScript;

namespace Kodama.BootLoader
{
    /// <summary>
    /// スタンドアローンアプリケーションのブートローダ
    /// </summary>
    /// <remarks>
    /// <p>ブートローダは実行ファイルです。以下のような動作をします。</p>
    /// <p>1.ユーザがブートローダを起動する</p>
    /// <p>2.ブートローダがコマンドライン引数に渡されたスクリプトをセットアップする</p>
    /// <p>3.スクリプトのエントリーポイントを呼び出す</p>
    /// <p>4.スクリプト上でDependencyInjectionコンテナにコンポーネントの登録や、
    ///   プロパティのバインドを行う</p>
    /// <p>5.コンテナに登録されたコンポーネントのエントリーポイント(起動用のインターフェイス)
    ///   をスクリプトから呼び出す</p>
    /// <br/>
    /// <p>ブートローダのコマンドライン引数は以下のようになっています。</p>
    /// <li>-s スクリプトファイルの指定(複数指定不可)。スクリプトはVBScript、JScript、C#が利用可能</li>
    /// <li>-c スクリプトで参照するアセンブリが書かれたXMLファイルの指定(複数指定不可)</li>
    /// <p>-s指定は必須ですが、-cはオプションです。例をあげれば以下のような起動パラメータとなります。</p>
    /// <code>
    /// StandAloneBootLoader -s boot.vb -c ref.xml
    /// </code>
    /// <p>スクリプトで参照するアセンブリが書かれたXMLファイルの表記ですが、
    /// 以下のようになっています。</p>
    /// <code>
    /// &lt;?xml version="1.0" encoding="shift_jis"?&gt;
    /// &lt;referenceAssembly&gt;
    ///     &lt;assembly name="foo.dll"/&gt;
    ///     &lt;assembly name="bar.dll"/&gt;
    /// &lt;/referenceAssembly&gt;
    /// </code>
    /// <p>詳細は"Kodama\BootLoader\Schema\ReferenceAssembly.xsd"参照。</p>
    /// </remarks>
    public class StandAloneBootLoader
    {
        /// <summary>
        /// スタンドアローンアプリケーションのエントリーポイントです
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        [STAThread]
        private static void Main(string[] args)
        {
            // コマンドライン引数をパースする
            string scriptPath = null;
            string configPath = null;
            for (int i = 0; i < args.Length; ++i)
            {
                if ((string.Compare(args[i], "-s", true) == 0) && (i + 1 < args.Length))
                {
                    scriptPath = args[i + 1];
                }
                else if ((string.Compare(args[i], "-c", true) == 0) && (i + 1 < args.Length))
                {
                    configPath = args[i + 1];
                }
            }

            // スクリプトの指定がない場合はエラー
            if (scriptPath == null)
            {
                MessageBox.Show("Please specify a script to set a command line argument.");
                return;
            }

            // XML設定ファイルをパースする
            referenceAssembly reference = null;
            if (configPath != null)
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream
                    ("Kodama.BootLoader.Schema.AssemblyConfig.xsd");
                XmlSchemaCollection schema = new XmlSchemaCollection();
                schema.Add(null, new XmlTextReader(stream));

                XmlValidatingReader reader = new XmlValidatingReader(new XmlTextReader(configPath));
                reader.ValidationType = ValidationType.Schema;
                reader.Schemas.Add(schema);

                XmlSerializer serializer = new XmlSerializer(typeof(referenceAssembly));

                try
                {
                    reference = (referenceAssembly)serializer.Deserialize(reader);
                }
                finally
                {
                    reader.Close();
                }
            }

            // スクリプトエンジンを作成し、スクリプトを呼び出す
            IScriptEngineFactory[] factories = new IScriptEngineFactory[]
                {new CSharpScriptEngineFactory(), new JScriptEngineFactory(), new VBScriptEngineFactory()};
            foreach (IScriptEngineFactory factory in factories)
            {
                // サポートしているフォーマットかチェックする
                if (!factory.CanCompile(scriptPath))
                {
                    continue;
                }

                // スクリプトエンジンの作成
                IScriptEngine engine = factory.CreateFromFile(scriptPath);

                // XMLファイルに書かれていたアセンブリの参照を設定する
                if (reference != null)
                {
                    foreach (referenceAssemblyAssembly assembly in reference.Items)
                    {
                        engine.AddReference(assembly.name);
                    }
                }

                // コンパイルする
                try
                {
                    engine.Compile();
                }
                catch (CompileErrorException e)
                {
                    StringBuilder errors = new StringBuilder(10000);
                    foreach (CompileErrorInfo cei in e.GetCompileErrorInfos())
                    {
                        errors.Append("--------------------------------" + "\n");
                        errors.Append("SourceName  : " + cei.SourceName  + "\n");
                        errors.Append("Description : " + cei.Description + "\n");
                        errors.Append("Line        : " + cei.ErrorLine   + "\n");
                        errors.Append("Code        : " + cei.ErrorText   + "\n");
                        errors.Append("--------------------------------" + "\n");
                    }
                    engine.Close();
                    MessageBox.Show("The error occurred in compile of the script.\n" + errors.ToString());
                    return;
                }

                // スクリプトを実行する
                try
                {
                    engine.Run();
                }
                catch (ScriptEntryPointNotFoundException)
                {
                    engine.Close();
                    MessageBox.Show("An entry point is not found in the script.");
                }

                // エンジンを閉じる
                engine.Close();

                return;
            }

            MessageBox.Show("It is the format of the script which is not supported.");
        }
    }
}
