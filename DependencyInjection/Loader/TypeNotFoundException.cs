using System;

namespace Kodama.DependencyInjection.Loader
{
    /// <summary>
    /// �w�肳�ꂽ�^���݂���Ȃ������ꍇ�ɔ��������Q
    /// </summary>
    public class TypeNotFoundException : ApplicationException
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public TypeNotFoundException() : base("TypeNotFoundException")
        {
        }
    }
}
