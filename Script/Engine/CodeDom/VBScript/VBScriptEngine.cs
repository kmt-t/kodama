using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.IO;
using System.Reflection;
using Microsoft.VisualBasic;
using Kodama.Script.Engine;
using Kodama.Script.Engine.CodeDom;

namespace Kodama.Script.Engine.CodeDom.VBScript
{
    /// <summary>
    /// VBScript�G���W���ł�
    /// </summary>
    /// <remarks>
    /// ���̃N���X��CodeDom�𗘗p���Ă��܂��B
    /// �ɗ�Visual Studio for Application�𗘗p�������̂ł͂Ȃ��A������𗘗p���ĉ������B
    /// </remarks>
    public class VBScriptEngine : IScriptEngine
    {
        /// <summary>
        /// CodeDom�̃R���p�C�����
        /// </summary>
        private CodeDomProvider provider = new VBCodeProvider();

        /// <summary>
        /// �X�N���v�g�̃A�Z���u��
        /// </summary>
        private Assembly scriptAssembly = null;

        /// <summary>
        /// �f�o�b�O���𐶐����邩�ǂ���
        /// </summary>
        private bool generateDebugInfo = false;

        /// <summary>
        /// �X�N���v�g���Q�Ƃ���A�Z���u��
        /// </summary>
        private ArrayList referenceAssemblies = new ArrayList();

        /// <summary>
        /// �X�N���v�g�R�[�h
        /// </summary>
        private ArrayList scriptCode = new ArrayList();

        /// <summary>
        /// �X�N���v�g�̃A�Z���u�����擾����
        /// </summary>
        /// <remarks>
        /// ����������ăX�N���v�g�̃��^�����擾�ł���B
        /// </remarks>
        public Assembly ScriptAssembly
        {
            get { return scriptAssembly; }
        }

        /// <summary>
        /// �f�o�b�O���𐶐����邩�ǂ���
        /// </summary>
        public bool GenerateDebugInfo
        {
            get { return generateDebugInfo; }
            set { generateDebugInfo = value; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public VBScriptEngine()
        {
            AddReference("system.dll");
            AddReference("mscorlib.dll");
            AddReference(Assembly.GetExecutingAssembly().Location);
        }

        /// <summary>
        /// �X�N���v�g�ŎQ�Ƃ���A�Z���u����ǉ�����
        /// </summary>
        /// <remarks>
        /// �A�Z���u���̎Q�Ƃ̓R���p�C���O�ɒǉ����Ă��������B
        /// </remarks>
        /// <param name="assemblyName">�X�N���v�g�ŎQ�Ƃ���A�Z���u����</param>
        public void AddReference(string assemblyName)
        {
            referenceAssemblies.Add(assemblyName);
        }

        /// <summary>
        /// �X�N���v�g����݂���O���[�o���ȃC���X�^���X��ǉ�����
        /// </summary>
        /// <remarks>
        /// �O���[�o���ȃC���X�^���X�̓R���p�C���O�ɒǉ����Ă��������B
        /// </remarks>
        /// <param name="name">�X�N���v�g����Q�Ƃ���Ƃ��ɂ����C���X�^���X��</param>
        /// <param name="obj">�X�N���v�g�ɒǉ�����O���[�o���ȃC���X�^���X</param>
        /// <exception cref="NotSupportedException">
        /// CodeDom�ł̓O���[�o���ȃC���X�^���X���T�|�[�g���Ă��Ȃ��̂ŕK�����̗�O���������܂�
        /// </exception>
        public void AddGlobalIntstance(string name, object obj)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// �X�N���v�g���t�@�C������ǂݏo���A���ɂ���R�[�h�ɒǉ�����
        /// </summary>
        /// <param name="filename">�X�N���v�g�t�@�C����</param>
        public void AddScriptCodeFromFile(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            string code = reader.ReadToEnd();
            reader.Close();
            scriptCode.Add(code);
        }

        /// <summary>
        /// �e�L�X�g�����ɂ���R�[�h�ɒǉ�����
        /// </summary>
        /// <param name="name">�X�N���v�g��</param>
        /// <param name="code">�ǉ�����e�L�X�g</param>
        public void AddScriptCodeFromText(string name, string code)
        {
            scriptCode.Add(code);
        }

        /// <summary>
        /// �X�N���v�g���R���p�C������
        /// </summary>
        /// <exception cref="CompileErrorException">
        /// �R���p�C���G���[�����������ꍇ�͂��̗�O�𓊂���
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
            compilerParams.GenerateExecutable      = false;
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
        /// �X�N���v�g�����s����
        /// </summary>
        /// <exception cref="ScriptEntryPointNotFoundException">
        /// �G���g���[�|�C���g���݂���Ȃ��ꍇ�͂��̗�O�𓊂���
        /// </exception>
        /// <exception cref="NotCompiledException">
        /// �X�N���v�g���R���p�C������Ă��Ȃ��ꍇ�͂��̗�O�𓊂���
        /// </exception>
        /// <remarks>
        /// �X�N���v�g�����s����ɂ̓G���g���[�|�C���g���K�v�ł��B
        /// �G���g���[�|�C���g�Ƃ̓X�N���v�g�̈�ԍŏ��ɋN������
        /// ���\�b�h�̂��Ƃ������܂��B���̃G���W���ł�VBScript��
        /// �G���g���[�|�C���g�́A�֐�����"Main"�ň����Ȃ��̐ÓI��
        /// ���\�b�h�ŁAScriptEntryPointAttribute���������Ă�����̂Ƃ��܂��B
        /// </remarks>
        public void Run()
        {
            if (scriptAssembly == null)
            {
                throw new NotCompiledException();
            }

            MethodInfo mi = FindEntryPoint();
            mi.Invoke(null, null);
        }

        /// <summary>
        /// �X�N���v�g�̎��s���~����
        /// </summary>
        public void Stop()
        {
            // ���ɂȂ�
        }

        /// <summary>
        /// �X�N���v�g�̃��\�[�X���J�����A�X�N���v�g�G���W���𖳌��ɂ���
        /// </summary>
        /// <remarks>
        /// �����I������X�N���v�g�G���W���͂��̃��\�b�h���Ăяo���Ă��������B
        /// </remarks>
        public void Close()
        {
            // ���ɂȂ�
        }

        /// <summary>
        /// �X�N���v�g����G���g���[�|�C���g��T��
        /// </summary>
        /// <returns>���������G���g���[�|�C���g</returns>
        /// <exception cref="ScriptEntryPointNotFoundException">
        /// �G���g���[�|�C���g���݂���Ȃ��ꍇ�͂��̗�O�𓊂���
        /// </exception>
        /// <remarks>
        /// �G���g���[�|�C���g�Ƃ̓X�N���v�g�̈�ԍŏ��ɋN������
        /// ���\�b�h�̂��Ƃ������܂��B���̃G���W���ł�VBScript��
        /// �G���g���[�|�C���g�́A�֐�����"Main"�ň����Ȃ��̐ÓI��
        /// ���\�b�h�ŁAScriptEntryPointAttribute���������Ă�����̂Ƃ��܂��B
        /// </remarks>
        private MethodInfo FindEntryPoint()
        {
            foreach (Type type in scriptAssembly.GetTypes())
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
    }
}
