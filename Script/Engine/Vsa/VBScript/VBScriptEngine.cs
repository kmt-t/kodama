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
    /// VBScript�G���W���ł�
    /// </summary>
    /// <remarks>
    /// ���̃N���X��Visual Studio for Application�𗘗p���Ă��܂��B
    /// �����.NET Framework2.0�ł�Obsolete�ɂȂ�̂ŁA�ɗ�CodeDom��
    /// ���p�������̂𗘗p���Ă��������B
    /// </remarks>
    public class VBScriptEngine : IScriptEngine
    {
        /// <summary>
        /// IVsaSite���p������N���X�B�����I�Ɏg�p����
        /// </summary>
        private class VBScriptVsaSite : IVsaSite
        {
            /// <summary>
            /// �f�t�H���g���[�g���j�J
            /// </summary>
            private const string DEFAULT_ROOT_MONIKER = "Kodama://Script/";

            /// <summary>
            /// �f�t�H���g���[�g���O���
            /// </summary>
            private const string DEFAULT_ROOT_NAME_SPACE = "Kodama.Script";

            /// <summary>
            /// Vsa�G���W��
            /// </summary>
            private IVsaEngine vsaEngine = new Microsoft.VisualBasic.Vsa.VsaEngine();

            /// <summary>
            /// �R���p�C���G���[
            /// </summary>
            private ArrayList errors = new ArrayList();

            /// <summary>
            /// �X�N���v�g����Q�Ƃł���O���[�o���I�u�W�F�N�g
            /// </summary>
            private Hashtable globalObjectMap = new Hashtable();

            /// <summary>
            /// �X�N���v�g�̃A�Z���u��
            /// </summary>
            /// <remarks>
            /// ����������ăX�N���v�g�̃��^�����擾�ł���
            /// </remarks>
            public Assembly ScriptAssembly 
            {
                get { return vsaEngine.Assembly; }
            }

            /// <summary>
            /// �f�o�b�O���𐶐����邩�ǂ���
            /// </summary>
            public bool GenerateDebugInfo
            {
                get { return vsaEngine.GenerateDebugInfo; }
                set { vsaEngine.GenerateDebugInfo = value; }
            }

            /// <summary>
            /// �R���X�g���N�^
            /// </summary>
            public VBScriptVsaSite()
            {
                // �ȑO�ɃR���p�C�������X�N���v�g�R�[�h���S�~�Ƃ��Ďc�邱�Ƃ�����
                // (IVsaEngine�̃o�O?)�̂Ń��[�g���j�J�͖��񃆃j�[�N�Ȗ��O�Ƃ���
                vsaEngine.RootMoniker = DEFAULT_ROOT_MONIKER + Guid.NewGuid().ToString() + "/";
                vsaEngine.Site        = this;
                vsaEngine.InitNew();
                vsaEngine.RootNamespace     = DEFAULT_ROOT_NAME_SPACE;
                vsaEngine.GenerateDebugInfo = true;
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
                IVsaReferenceItem refItem = (IVsaReferenceItem)vsaEngine.Items.CreateItem
                    (assemblyName, VsaItemType.Reference, VsaItemFlag.None);
                refItem.AssemblyName = assemblyName;
            }

            /// <summary>
            /// �X�N���v�g����݂���O���[�o���ȃC���X�^���X��ǉ�����
            /// </summary>
            /// <remarks>
            /// �O���[�o���ȃC���X�^���X�̓R���p�C���O�ɒǉ����Ă��������B
            /// </remarks>
            /// <param name="name">�X�N���v�g����Q�Ƃ���Ƃ��ɂ����C���X�^���X��</param>
            /// <param name="globalObject">�X�N���v�g�ɒǉ�����O���[�o���ȃC���X�^���X</param>
            public void AddGlobalObject(string name, object globalObject)
            {
                IVsaGlobalItem  globalItem = (IVsaGlobalItem)vsaEngine.Items.CreateItem
                    (name, VsaItemType.AppGlobal, VsaItemFlag.None);
                globalItem.TypeString = globalObject.GetType().FullName;
                globalObjectMap.Add(name, globalObject);
            }

            /// <summary>
            /// �X�N���v�g�R�[�h��ǉ�����
            /// </summary>
            /// <param name="name">�X�N���v�g�R�[�h��</param>
            /// <param name="code">�ǉ�����X�N���v�g�R�[�h</param>
            public void AddScriptCode(string name, string code)
            {
                IVsaCodeItem codeItem = (IVsaCodeItem)vsaEngine.Items.CreateItem
                    (name,  VsaItemType.Code, VsaItemFlag.None);
                codeItem.SourceText = code;
            }

            /// <summary>
            /// �X�N���v�g���R���p�C������
            /// </summary>
            /// <exception cref="VsaCompileErrorException">
            /// �R���p�C���G���[�����������ꍇ�͂��̗�O�𓊂���
            /// </exception>
            public void Compile()
            {
                errors.Clear();
                if (!vsaEngine.Compile())
                {
//                    IVsaError[] cei = (IVsaError[])errors.ToArray(typeof(IVsaError));   // ���̂���O����������Ƃ�������
                    int size = 0;
                    for (int i = 0; i < errors.Count; ++i)
                    {
                        // ���肦�Ȃ��͂������ǁA���̂��G���[����������̂ŔO�̂���
                        if (errors[i] is IVsaError)
                        {
                            ++size;
                        }
                    }
                    IVsaError[] cei = new IVsaError[size];
                    for (int i = 0, j = 0; i < cei.Length; ++i, ++j)
                    {
                        // ���肦�Ȃ��͂������ǁA���̂��G���[����������̂ŔO�̂���
                        if (errors[i] is IVsaError)
                        {
                            cei[j] = (IVsaError)errors[i];
                        }
                    }
                    throw new VsaCompileErrorException(cei);
                }
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
            /// <exception cref="ScriptEntryPointNotFoundException">
            /// �G���g���[�|�C���g���݂���Ȃ��ꍇ�͂��̗�O�𓊂���
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
                if (!vsaEngine.IsCompiled)
                {
                    throw new NotCompiledException();
                }
                vsaEngine.Run();

                MethodInfo entryPoint = FindEntryPoint();
                entryPoint.Invoke(null, null);
            }

            /// <summary>
            /// �X�N���v�g�̎��s���~����
            /// </summary>
            public void Stop()
            {
                vsaEngine.Reset();
            }

            /// <summary>
            /// �X�N���v�g�̃��\�[�X���J�����A�X�N���v�g�G���W���𖳌��ɂ���
            /// </summary>
            /// <remarks>
            /// �����I������X�N���v�g�G���W���͂��̃��\�b�h���Ăяo���Ă��������B
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
            /// �X�N���v�g���猩����O���[�o���I�u�W�F�N�g���擾����
            /// </summary>
            /// <param name="name">�O���[�o���I�u�W�F�N�g�̖��O</param>
            /// <returns>�O���[�o���I�u�W�F�N�g</returns>
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
            /// �R���p�C���G���[�������ɌĂ΂�郁�\�b�h
            /// </summary>
            /// <param name="err">�R���p�C���G���[</param>
            /// <returns>�R���p�C���G���[���p�����ĕ񍐂��邩�ǂ���</returns>
            bool IVsaSite.OnCompilerError(IVsaError err)
            {
                errors.Add(err);
                return true;
            }
        }

        /// <summary>
        /// IVsaSite��������������G���W��
        /// </summary>
        VBScriptVsaSite site = new VBScriptVsaSite();

        /// <summary>
        /// �X�N���v�g�̃A�Z���u�����擾����
        /// </summary>
        /// <remarks>
        /// ����������ăX�N���v�g�̃��^�����擾�ł���B
        /// </remarks>
        public Assembly ScriptAssembly
        {
            get { return site.ScriptAssembly; }
        }

        /// <summary>
        /// �f�o�b�O���𐶐����邩�ǂ���
        /// </summary>
        public bool GenerateDebugInfo
        {
            get { return site.GenerateDebugInfo; }
            set { site.GenerateDebugInfo = value; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public VBScriptEngine()
        {
            site.AddReference("system.dll");
            site.AddReference("mscorlib.dll");
            site.AddReference(Assembly.GetExecutingAssembly().Location);
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
            site.AddReference(assemblyName);
        }

        /// <summary>
        /// �X�N���v�g����݂���O���[�o���ȃC���X�^���X��ǉ�����
        /// </summary>
        /// <remarks>
        /// �O���[�o���ȃC���X�^���X�̓R���p�C���O�ɒǉ����Ă��������B
        /// </remarks>
        /// <param name="name">�X�N���v�g����Q�Ƃ���Ƃ��ɂ����C���X�^���X��</param>
        /// <param name="obj">�X�N���v�g�ɒǉ�����O���[�o���ȃC���X�^���X</param>
        public void AddGlobalIntstance(string name, object obj)
        {
            site.AddGlobalObject(name, obj);
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
            site.AddScriptCode(Path.GetFileNameWithoutExtension(filename), code);
        }

        /// <summary>
        /// �e�L�X�g�����ɂ���R�[�h�ɒǉ�����
        /// </summary>
        /// <param name="name">�X�N���v�g��</param>
        /// <param name="code">�ǉ�����e�L�X�g</param>
        public void AddScriptCodeFromText(string name, string code)
        {
            site.AddScriptCode(name, code);
        }

        /// <summary>
        /// �X�N���v�g���R���p�C������
        /// </summary>
        /// <exception cref="VsaCompileErrorException">
        /// �R���p�C���G���[�����������ꍇ�͂��̗�O�𓊂���
        /// </exception>
        public void Compile()
        {
            site.Compile();
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
            site.Run();
        }

        /// <summary>
        /// �X�N���v�g�̎��s���~����
        /// </summary>
        public void Stop()
        {
            site.Stop();
        }

        /// <summary>
        /// �X�N���v�g�̃��\�[�X���J�����A�X�N���v�g�G���W���𖳌��ɂ���
        /// </summary>
        /// <remarks>
        /// �����I������X�N���v�g�G���W���͂��̃��\�b�h���Ăяo���Ă��������B
        /// </remarks>
        public void Close()
        {
            site.Close();
        }
    }
}
