using System;
using Kodama.Script.Engine;

namespace Kodama.Script.Factory
{
    /// <summary>
    /// �X�N���v�g�G���W���̃t�@�N�g���[�C���^�[�t�F�C�X
    /// </summary>
    public interface IScriptEngineFactory
    {
        /// <summary>
        /// �w�肳�ꂽ�t�@�C�����R���p�C���\���`�F�b�N����
        /// </summary>
        /// <param name="filename">�`�F�b�N����t�@�C����</param>
        /// <returns>�t�@�C�����R���p�C���\���ǂ���</returns>
        bool CanCompile(string filename);

        /// <summary>
        /// �X�N���v�g�G���W���̃C���X�^���X�𐶐�����
        /// </summary>
        /// <returns>�������ꂽ�C���X�^���X</returns>
        IScriptEngine Create();

        /// <summary>
        /// �t�@�C������X�N���v�g�G���W���̃C���X�^���X�𐶐�����
        /// </summary>
        /// <param name="filename">�X�N���v�g�G���W���𐶐�����t�@�C����</param>
        /// <returns>�������ꂽ�C���X�^���X</returns>
        IScriptEngine CreateFromFile(string filename);
    }
}
