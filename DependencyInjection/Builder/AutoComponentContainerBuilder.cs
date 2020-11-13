using System;
using System.Collections;
using System.Reflection;
using System.Runtime.Remoting;
using System.IO;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Marker;

namespace Kodama.DependencyInjection.Builder
{
    /// <summary>
    /// �����I�ɁADependencyInjection�R���e�i�𐶐�����
    /// �R���|�[�l���g��o�^���A���̃C���X�^���X��Ԃ��r���_
    /// </summary>
    public class AutoComponentContainerBuilder : IComponentContainerBuilder
    {
        /// <summary>
        /// �����o�^����R���|�[�l���g�̃J�e�S��
        /// </summary>
        private string componentCategory = "";

        /// <summary>
        /// �����o�^�����A�Z���u��
        /// </summary>
        private ArrayList assemblies = new ArrayList();

        /// <summary>
        /// �����o�^����R���|�[�l���g�̃J�e�S��
        /// </summary>
        public string ComponentCategory
        {
            get { return componentCategory; }
            set { componentCategory = value; }
        }

        /// <summary>
        /// �����o�^�����A�Z���u����ǉ�����
        /// </summary>
        /// <param name="assemblyName">�����o�^����A�Z���u���̊��S��</param>
        public void AddAssemblyFile(AssemblyName assemblyName)
        {
            assemblies.Add(Assembly.Load(assemblyName));
        }

        /// <summary>
        /// �����o�^�����A�Z���u����ǉ�����
        /// </summary>
        /// <param name="filename">�����o�^����A�Z���u���̃t�@�C����</param>
        public void AddAssemblyFile(string filename)
        {
            assemblies.Add(Assembly.LoadFile(filename));
        }

        /// <summary>
        /// �����o�^�����A�Z���u����ǉ�����
        /// </summary>
        /// <param name="folder">�����o�^����A�Z���u�����܂܂��t�H���_</param>
        public void AddAssemblyFolder(string folder)
        {
            foreach (string name in Directory.GetFiles(folder, "*.dll"))
            {
                assemblies.Add(Assembly.LoadFile(Path.Combine(folder, name)));
            }
        }

        /// <summary>
        /// �����o�^����郊���[�e�B���O�I�u�W�F�N�g���܂ރA�Z���u����ǉ�����
        /// </summary>
        /// <param name="filename">�����[�e�B���O�I�u�W�F�N�g�ɂ��ċL�q�����ݒ�t�@�C���̃p�X</param>
        /// <seealso cref="System.Runtime.Remoting.RemotingConfiguration">RemotingConfiguration�N���X</seealso>
        public static void RemotingConfigure(string filename)
        {
            RemotingConfiguration.Configure(filename);
        }

        /// <summary>
        /// �����I�ɁADependencyInjection�R���e�i�𐶐�����
        /// �R���|�[�l���g��o�^���A���̃C���X�^���X��Ԃ�
        /// </summary>
        /// <returns>�������ꂽDependencyInjection�R���e�i</returns>
        public IComponentContainer Build()
        {
            // �A�Z���u����AutoRegistrationAssembly�������ݒ肳��Ă���̂�I�ʂ���
            ArrayList filteredAssemblies = new ArrayList();
            foreach (Assembly assembly in assemblies)
            {
                if (Attribute.IsDefined(assembly, typeof(AutoRegistrationAssemblyAttribute)))
                {
                    filteredAssemblies.Add(assembly);
                }
            }

            // �R���e�i�̃C���X�^���X�̐���
            IMutableComponentContainer container = new ComponentContainerImpl();

            // �I�ʂ��ꂽ�A�Z���u������AutoRegistrationComponent�������ݒ肳��Ă�����̂����R���e�i�ɓo�^����
            foreach (Assembly assembly in filteredAssemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (Attribute.IsDefined(type, typeof(AutoRegistratonComponentAttribute)))
                    {
                        AutoRegistratonComponentAttribute aca =
                            (AutoRegistratonComponentAttribute)Attribute.GetCustomAttribute
                            (type, typeof(AutoRegistratonComponentAttribute));
                        if (string.Compare(aca.ComponentCategory, componentCategory, true) == 0)
                        {
                            aca.Register(container, type);
                        }
                    }
                }
            }

            // �����[�e�B���O�I�u�W�F�N�g�̓o�^
            foreach (ActivatedClientTypeEntry entry in RemotingConfiguration.GetRegisteredActivatedClientTypes())
            {
                if (Attribute.IsDefined(entry.ObjectType, typeof(AutoRegistratonComponentAttribute)))
                {
                    AutoRegistratonComponentAttribute aca =
                        (AutoRegistratonComponentAttribute)Attribute.GetCustomAttribute
                        (entry.ObjectType, typeof(AutoRegistratonComponentAttribute));
                    if (string.Compare(aca.ComponentCategory, componentCategory, true) == 0)
                    {
                        aca.Register(container, entry.ObjectType);
                    }
                }
            }
            foreach (ActivatedServiceTypeEntry entry in RemotingConfiguration.GetRegisteredActivatedServiceTypes())
            {
                if (Attribute.IsDefined(entry.ObjectType, typeof(AutoRegistratonComponentAttribute)))
                {
                    AutoRegistratonComponentAttribute aca =
                        (AutoRegistratonComponentAttribute)Attribute.GetCustomAttribute
                        (entry.ObjectType, typeof(AutoRegistratonComponentAttribute));
                    if (string.Compare(aca.ComponentCategory, componentCategory, true) == 0)
                    {
                        aca.Register(container, entry.ObjectType);
                    }
                }
            }
            foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
            {
                if (Attribute.IsDefined(entry.ObjectType, typeof(AutoRegistratonComponentAttribute)))
                {
                    AutoRegistratonComponentAttribute aca =
                        (AutoRegistratonComponentAttribute)Attribute.GetCustomAttribute
                        (entry.ObjectType, typeof(AutoRegistratonComponentAttribute));
                    if (string.Compare(aca.ComponentCategory, componentCategory, true) == 0)
                    {
                        aca.Register(container, entry.ObjectType);
                    }
                }
            }
            foreach (WellKnownServiceTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownServiceTypes())
            {
                if (Attribute.IsDefined(entry.ObjectType, typeof(AutoRegistratonComponentAttribute)))
                {
                    AutoRegistratonComponentAttribute aca =
                        (AutoRegistratonComponentAttribute)Attribute.GetCustomAttribute
                        (entry.ObjectType, typeof(AutoRegistratonComponentAttribute));
                    if (string.Compare(aca.ComponentCategory, componentCategory, true) == 0)
                    {
                        aca.Register(container, entry.ObjectType);
                    }
                }
            }

            return container;
        }
    }
}
