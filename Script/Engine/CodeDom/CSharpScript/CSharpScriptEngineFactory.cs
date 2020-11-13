using System;
using System.IO;
using Kodama.Script.Factory;

namespace Kodama.Script.Engine.CodeDom.CSharpScript
{
    /// <summary>
    /// CSharpScriptEngine�𐶐�����t�@�N�g���[�N���X�ł�
    /// </summary>
    public class CSharpScriptEngineFactory : IScriptEngineFactory
    {
        /// <summary>
        /// �w�肳�ꂽ�t�@�C�����R���p�C���\���`�F�b�N����
        /// </summary>
        /// <param name="filename">�`�F�b�N����t�@�C����</param>
        /// <returns>�t�@�C�����R���p�C���\���ǂ���</returns>
        public bool CanCompile(string filename)
        {
            return string.Compare(Path.GetExtension(filename), ".cs", true) == 0;
        }

        /// <summary>
        /// �X�N���v�g�G���W���̃C���X�^���X�𐶐�����
        /// </summary>
        /// <returns>�������ꂽ�C���X�^���X</returns>
        public IScriptEngine Create()
        {
            return new CSharpScriptEngine();
        }

        /// <summary>
        /// �t�@�C������X�N���v�g�G���W���̃C���X�^���X�𐶐�����
        /// </summary>
        /// <param name="filename">�X�N���v�g�G���W���𐶐�����t�@�C����</param>
        /// <returns>�������ꂽ�C���X�^���X</returns>
        public IScriptEngine CreateFromFile(string filename)
        {
            CSharpScriptEngine engine = new CSharpScriptEngine();
            engine.AddScriptCodeFromFile(filename);
            return engine;
        }
    }
}
