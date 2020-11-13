using System;
using System.Reflection;

namespace Kodama.Script.Engine
{
    /// <summary>
    /// �X�N���v�g�G���W���̊��C���^�[�t�F�C�X�ł�
    /// </summary>
    /// <remarks>
    /// �V���ȃX�N���v�g����̃T�|�[�g�͂��̃C���^�[�t�F�C�X���p�����Ă�������
    /// </remarks>
    public interface IScriptEngine
    {
        /// <summary>
        /// �X�N���v�g�̃A�Z���u��
        /// </summary>
        /// <remarks>
        /// ����������ăX�N���v�g�̃��^�����擾�ł���B
        /// </remarks>
        Assembly ScriptAssembly
        {
            get;
        }

        /// <summary>
        /// �f�o�b�O���𐶐����邩�ǂ���
        /// </summary>
        bool GenerateDebugInfo
        {
            get;
            set;
        }

        /// <summary>
        /// �X�N���v�g�ŎQ�Ƃ���A�Z���u����ǉ�����
        /// </summary>
        /// <param name="assemblyName">�X�N���v�g�ŎQ�Ƃ���A�Z���u����</param>
        void AddReference(string assemblyName);

        /// <summary>
        /// �X�N���v�g����݂���O���[�o���ȃC���X�^���X��ǉ�����
        /// </summary>
        /// <param name="name">�X�N���v�g����Q�Ƃ���Ƃ��ɂ����C���X�^���X��</param>
        /// <param name="obj">�X�N���v�g�ɒǉ�����O���[�o���ȃC���X�^���X</param>
        void AddGlobalIntstance(string name, object obj);

        /// <summary>
        /// �X�N���v�g���t�@�C������ǂݏo���A���ɂ���R�[�h�ɒǉ�����
        /// </summary>
        /// <param name="filename">�X�N���v�g�t�@�C����</param>
        void AddScriptCodeFromFile(string filename);

        /// <summary>
        /// �e�L�X�g�����ɂ���R�[�h�ɒǉ�����
        /// </summary>
        /// <param name="name">�X�N���v�g��</param>
        /// <param name="code">�ǉ�����e�L�X�g</param>
        void AddScriptCodeFromText(string name, string code);

        /// <summary>
        /// �X�N���v�g���R���p�C������
        /// </summary>
        void Compile();

        /// <summary>
        /// �X�N���v�g�����s����
        /// </summary>
        void Run();

        /// <summary>
        /// �X�N���v�g�̎��s���~����
        /// </summary>
        void Stop();

        /// <summary>
        /// �X�N���v�g�̃��\�[�X���J�����A�X�N���v�g�G���W���𖳌��ɂ���
        /// </summary>
        /// <remarks>
        /// �����I������X�N���v�g�G���W���͂��̃��\�b�h���Ăяo���Ă��������B
        /// </remarks>
        void Close();
    }
}
