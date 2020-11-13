using System;

namespace Kodama.Script.Engine
{
    /// <summary>
    /// �R���p�C���G���[���ł�
    /// </summary>
    public class CompileErrorInfo
    {
        /// <summary>
        /// �G���[�̔��������\�[�X��
        /// </summary>
        private string sourceName;

        /// <summary>
        /// �G���[�̐�����
        /// </summary>
        private string description;

        /// <summary>
        /// �G���[�̔��������s
        /// </summary>
        private int errorLine;

        /// <summary>
        /// �G���[�̔��������s�̃e�L�X�g
        /// </summary>
        private string errorText; 

        /// <summary>
        /// �G���[�̔��������\�[�X��
        /// </summary>
        public string SourceName
        {
            get { return sourceName; }
        }

        /// <summary>
        /// �G���[�̐�����
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// �G���[�̔��������s
        /// </summary>
        public int ErrorLine
        {
            get { return errorLine; }
        }

        /// <summary>
        /// �G���[�̔��������s�̃e�L�X�g
        /// </summary>
        public string ErrorText
        {
            get { return errorText; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="name">�\�[�X��</param>
        /// <param name="desc">�G���[�̐�����</param>
        /// <param name="text">�G���[�̔��������s�̃e�L�X�g</param>
        /// <param name="line">�G���[�̔��������s</param>
        public CompileErrorInfo(
            string name,
            string desc,
            int    line,
            string text )
        {
            sourceName  = name;
            description = desc;
            errorLine   = line;
            errorText   = text; 
        }
    }
}
