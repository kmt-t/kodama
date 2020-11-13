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
    /// �X�^���h�A���[���A�v���P�[�V�����̃u�[�g���[�_
    /// </summary>
    /// <remarks>
    /// <p>�u�[�g���[�_�͎��s�t�@�C���ł��B�ȉ��̂悤�ȓ�������܂��B</p>
    /// <p>1.���[�U���u�[�g���[�_���N������</p>
    /// <p>2.�u�[�g���[�_���R�}���h���C�������ɓn���ꂽ�X�N���v�g���Z�b�g�A�b�v����</p>
    /// <p>3.�X�N���v�g�̃G���g���[�|�C���g���Ăяo��</p>
    /// <p>4.�X�N���v�g���DependencyInjection�R���e�i�ɃR���|�[�l���g�̓o�^��A
    ///   �v���p�e�B�̃o�C���h���s��</p>
    /// <p>5.�R���e�i�ɓo�^���ꂽ�R���|�[�l���g�̃G���g���[�|�C���g(�N���p�̃C���^�[�t�F�C�X)
    ///   ���X�N���v�g����Ăяo��</p>
    /// <br/>
    /// <p>�u�[�g���[�_�̃R�}���h���C�������͈ȉ��̂悤�ɂȂ��Ă��܂��B</p>
    /// <li>-s �X�N���v�g�t�@�C���̎w��(�����w��s��)�B�X�N���v�g��VBScript�AJScript�AC#�����p�\</li>
    /// <li>-c �X�N���v�g�ŎQ�Ƃ���A�Z���u���������ꂽXML�t�@�C���̎w��(�����w��s��)</li>
    /// <p>-s�w��͕K�{�ł����A-c�̓I�v�V�����ł��B���������Έȉ��̂悤�ȋN���p�����[�^�ƂȂ�܂��B</p>
    /// <code>
    /// StandAloneBootLoader -s boot.vb -c ref.xml
    /// </code>
    /// <p>�X�N���v�g�ŎQ�Ƃ���A�Z���u���������ꂽXML�t�@�C���̕\�L�ł����A
    /// �ȉ��̂悤�ɂȂ��Ă��܂��B</p>
    /// <code>
    /// &lt;?xml version="1.0" encoding="shift_jis"?&gt;
    /// &lt;referenceAssembly&gt;
    ///     &lt;assembly name="foo.dll"/&gt;
    ///     &lt;assembly name="bar.dll"/&gt;
    /// &lt;/referenceAssembly&gt;
    /// </code>
    /// <p>�ڍׂ�"Kodama\BootLoader\Schema\ReferenceAssembly.xsd"�Q�ƁB</p>
    /// </remarks>
    public class StandAloneBootLoader
    {
        /// <summary>
        /// �X�^���h�A���[���A�v���P�[�V�����̃G���g���[�|�C���g�ł�
        /// </summary>
        /// <param name="args">�R�}���h���C������</param>
        [STAThread]
        private static void Main(string[] args)
        {
            // �R�}���h���C���������p�[�X����
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

            // �X�N���v�g�̎w�肪�Ȃ��ꍇ�̓G���[
            if (scriptPath == null)
            {
                MessageBox.Show("Please specify a script to set a command line argument.");
                return;
            }

            // XML�ݒ�t�@�C�����p�[�X����
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

            // �X�N���v�g�G���W�����쐬���A�X�N���v�g���Ăяo��
            IScriptEngineFactory[] factories = new IScriptEngineFactory[]
                {new CSharpScriptEngineFactory(), new JScriptEngineFactory(), new VBScriptEngineFactory()};
            foreach (IScriptEngineFactory factory in factories)
            {
                // �T�|�[�g���Ă���t�H�[�}�b�g���`�F�b�N����
                if (!factory.CanCompile(scriptPath))
                {
                    continue;
                }

                // �X�N���v�g�G���W���̍쐬
                IScriptEngine engine = factory.CreateFromFile(scriptPath);

                // XML�t�@�C���ɏ�����Ă����A�Z���u���̎Q�Ƃ�ݒ肷��
                if (reference != null)
                {
                    foreach (referenceAssemblyAssembly assembly in reference.Items)
                    {
                        engine.AddReference(assembly.name);
                    }
                }

                // �R���p�C������
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

                // �X�N���v�g�����s����
                try
                {
                    engine.Run();
                }
                catch (ScriptEntryPointNotFoundException)
                {
                    engine.Close();
                    MessageBox.Show("An entry point is not found in the script.");
                }

                // �G���W�������
                engine.Close();

                return;
            }

            MessageBox.Show("It is the format of the script which is not supported.");
        }
    }
}
