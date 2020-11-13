using System;
using System.Collections;
using System.Reflection;
using System.IO;

namespace Kodama.DependencyInjection.Loader
{
    /// <summary>
    /// �w�肳�ꂽ�p�X�̃A�Z���u������^�����[�h���܂�
    /// </summary>
    public class TypeLoader : IDisposable
    {
        /// <summary>
        /// �q���[�_
        /// </summary>
        private ArrayList children = new ArrayList();

        /// <summary>
        /// �^�����[�h����A�Z���u��
        /// </summary>
        private ArrayList assemblies = new ArrayList();

        /// <summary>
        /// �f�t�H���g�R���X�g���N�^
        /// </summary>
        public TypeLoader()
        {
            // �������Ȃ�
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="parent">�e���[�_</param>
        public TypeLoader(TypeLoader parent)
        {
            parent.AddChild(this);
        }

        /// <summary>
        /// �q���[�_��ǉ�����
        /// </summary>
        /// <param name="child">�ǉ�����q���[�_</param>
        public void AddChild(TypeLoader child)
        {
            children.Add(child);
        }

        /// <summary>
        /// �^��ǂݏo���A�Z���u����ǉ�����
        /// </summary>
        /// <param name="filename">�^��ǂݏo���A�Z���u���̃t�@�C����</param>
        public void AddAssemblyFile(string filename)
        {
            assemblies.Add(Assembly.LoadFile(filename));
        }

        /// <summary>
        /// �^��ǂݏo���A�Z���u����ǉ�����
        /// </summary>
        /// <param name="folder">�^��ǂݏo���A�Z���u�����܂܂��t�H���_</param>
        public void AddAssemblyFolder(string folder)
        {
            foreach (string name in Directory.GetFiles(folder, "*.dll"))
            {
                assemblies.Add(Assembly.LoadFile(Path.Combine(folder, name)));
            }
        }

        /// <summary>
        /// �w�肳�ꂽ���O�̌^���A�Z���u�����烍�[�h���܂�
        /// </summary>
        /// <param name="fullTypeName">���[�h����^�̖��O</param>
        /// <returns>���[�h���ꂽ�^</returns>
        /// <exception cref="TypeNotFoundException">
        /// �w�肳�ꂽ���O�̌^���݂���Ȃ������ꍇ�ɔ��������O
        /// </exception>
        public Type LoadType(string fullTypeName)
        {
            foreach (Assembly assembly in assemblies)
            {
                Type type = assembly.GetType(fullTypeName);
                if (type != null)
                {
                    return type;
                }
            }
            foreach (TypeLoader child in children)
            {
                try
                {
                    return child.LoadType(fullTypeName);
                }
                catch (TypeNotFoundException)
                {
                }
            }
            throw new TypeNotFoundException();
        }

        /// <summary>
        /// ���[�h�������\�[�X��j������
        /// </summary>
        public void Dispose()
        {
            assemblies.Clear();

            foreach (TypeLoader child in children)
            {
                child.Dispose();
            }
        }
    }
}
