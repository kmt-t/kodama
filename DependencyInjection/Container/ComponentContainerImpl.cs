using System;
using System.Collections;
using Kodama.DependencyInjection.Component;

namespace Kodama.DependencyInjection.Container
{
    /// <summary>
    /// DependencyInjection�R���e�i�̎����N���X�ł�
    /// </summary>
    /// <example>
    /// <code lang="C#">
    /// using System;
    /// using Kodama.DependencyInjection.Component;
    /// using Kodama.DependencyInjection.Container;
    /// using Kodama.DependencyInjection.Factory;
    /// using Kodama.Function;
    /// using Kodama.Function.Bind;
    /// using Kodama.Function.Member;
    ///
    /// ....
    ///
    /// public interface IBar
    /// {
    ///     void Print();
    /// }
    ///
    /// public class BarImpl : IBar
    /// {
    ///     public void Print()
    ///     {
    ///         Console.WriteLine("BarImpl");
    ///     }
    /// }
    ///
    /// public class Foo
    /// {
    ///     private IBar dependency1;
    ///     private IBar dependency2;
    ///     private int  val;
    ///
    ///     // InjectionPoint�����̂������\�b�h�͎����I��
    ///     // �Z�b�^�[�C���W�F�N�V�����̃��\�b�h�ɐݒ肳���
    ///     [InjectionPoint]
    ///     public void SetDependency1(IBar dep)
    ///     {
    ///         dependency1 = dep;
    ///     }
    ///
    ///     public void SetDependency2(IBar dep, int v)
    ///     {
    ///         dependency2 = dep;
    ///         val         = v;
    ///     }
    ///
    ///     // InitializationPoint�����̂������\�b�h�͎����I��
    ///     // ���������\�b�h�ɐݒ肳���
    ///     [InitializationPoint]
    ///     public void Initialize1()
    ///     {
    ///         Console.WriteLine("Init1");
    ///     }
    ///
    ///     public void Initialize2(int v)
    ///     {
    ///         Console.WriteLine("Init2 val = " + v.ToString());
    ///     }
    /// }
    ///
    /// ...
    ///
    /// // �ʏ�̓R���e�i�ւ̃R���|�[�l���g�̓o�^�A�Z�b�^�[�C���W�F�N�V�����y��
    /// // ���������\�b�h�̐ݒ�̓X�N���v�g�ōs���B
    /// // �ڍׂ�Kodama.DependencyInjection.Factory.DefaultComponetContainerFactory#Create
    /// // ���\�b�h�̃I�[�o�[���[�h���Q�ƁB
    ///
    /// IMutableComponentContainer container = new ComponentContainerImpl();
    ///
    /// continer.Register(typeof(BarImpl));
    ///
    /// IComponentEntry entry = new PrototypeComponetEntry(container, typeof(Foo));
    /// 
    /// // �蓮�ɂ��Z�b�^�[�C���W�F�N�V�����̐ݒ�
    /// entry.AddInjectionFanctor(
    ///     new BindFunctor(
    ///         new MemberFunctor(typeof(Foo).GetMethod("SetDependency2")),
    ///         new NotBoundArgument(0),
    ///         new TypedArgumentComponentProvider(container, typeof(IBar)),
    ///         1));
    ///
    /// // �蓮�ɂ�鏉�������\�b�h�̐ݒ�
    /// entry.AddInitializationFactor(
    ///     new BindFunctor(
    ///         new MemberFunctor(typeof(Foo).GetMethod("Initialize2")),
    ///         new NotBoundArgument(0),
    ///         2));
    ///
    /// container.Register(entry);
    ///
    /// Foo foo = (Foo)continer.GetComponent(typeof(Foo));
    /// </code>
    /// </example>
    public class ComponentContainerImpl : IMutableComponentContainer
    {
        /// <summary>
        /// ����̃C���^�[�t�F�C�X�ɗD�悵�Ċ��蓖�Ă���R���|�[�l���g�̏��
        /// </summary>
        private Hashtable primaryComponentEntries = new Hashtable();

        /// <summary>
        /// �o�^����Ă���R���|�[�l���g�̏��
        /// </summary>
        private ArrayList secondaryComponentEntries = new ArrayList();

        /// <summary>
        /// �q�R���e�i
        /// </summary>
        private ArrayList children = new ArrayList();

        /// <summary>
        /// �R���|�[�l���g���擾����
        /// </summary>
        /// <param name="componentType">�擾����R���|�[�l���g�̌^</param>
        /// <returns>�����ɂ킽���ꂽ�^�̃R���|�[�l���g</returns>
        /// <exception cref="CyclicDependencyException">
        /// �ˑ��֌W���z���Ă���Ƃ��ɔ��������O
        /// </exception>
        /// <exception cref="TooManyRegistrationException">
        /// �w�肳�ꂽ�C���^�[�t�F�C�X����������R���|�[�l���g�������������ꍇ�ɔ��������O
        /// </exception>
        /// <exception cref="ComponentNotFoundException">
        /// �w�肳�ꂽ�C���^�[�t�F�C�X����������R���|�[�l���g���Ȃ������ꍇ�ɔ��������O
        /// </exception>
        /// <exception cref="CyclicDependencyException">
        /// �R���|�[�l���g�̈ˑ��֌W���z���Ă��Ă��A�z���Ă���R���|�[�l���g��
        /// Prototype���[�h�̏ꍇ�ɔ��������O
        /// </exception>
        public object GetComponent(Type componentType)
        {
            return GetComponentEntry(componentType).GetInstance();
        }

        /// <summary>
        /// �R���|�[�l���g���擾����
        /// </summary>
        /// <param name="componentName">�擾����R���|�[�l���g�̖��O</param>
        /// <returns>�����ɂ킽���ꂽ���O�̃R���|�[�l���g</returns>
        /// <exception cref="TooManyRegistrationException">
        /// �w�肳�ꂽ���O�̃R���|�[�l���g�������������ꍇ�ɔ��������O
        /// </exception>
        /// <exception cref="ComponentNotFoundException">
        /// �w�肳�ꂽ���O�̃R���|�[�l���g���Ȃ������ꍇ�ɔ��������O
        /// </exception>
        /// <exception cref="CyclicDependencyException">
        /// �R���|�[�l���g�̈ˑ��֌W���z���Ă��Ă��A�z���Ă���R���|�[�l���g��
        /// Prototype���[�h�̏ꍇ�ɔ��������O
        /// </exception>
        public object GetComponent(string componentName)
        {
            return GetComponentEntry(componentName).GetInstance();
        }

        /// <summary>
        /// �R���|�[�l���g���o�^����Ă��邩�`�F�b�N����
        /// </summary>
        /// <param name="componentType">�o�^����Ă��邩�m�F����R���|�[�l���g�̌^</param>
        /// <returns>�R���|�[�l���g���o�^����Ă��邩�ǂ���</returns>
        public bool Contains(Type componentType)
        {
            foreach (IComponentEntry entry in primaryComponentEntries.Values)
            {
                if (object.Equals(componentType, entry.ComponentType))
                {
                    return true;
                }
            }
            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (object.Equals(componentType, entry.ComponentType))
                {
                    return true;
                }
            }
            foreach (IComponentContainer child in children)
            {
                if (child.Contains(componentType))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// �R���|�[�l���g���o�^����Ă��邩�`�F�b�N����
        /// </summary>
        /// <param name="componentName">�o�^����Ă��邩�m�F����R���|�[�l���g�̖��O</param>
        /// <returns>�R���|�[�l���g���o�^����Ă��邩�ǂ���</returns>
        public bool Contains(string componentName)
        {
            foreach (IComponentEntry entry in primaryComponentEntries.Values)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0) 
                {
                    return true;
                }
            }
            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0) 
                {
                    return true;
                }
            }
            foreach (IComponentContainer child in children)
            {
                if (child.Contains(componentName))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <param name="componentType">�o�^����R���|�[�l���g�̌^</param>
        public void Register(Type componentType)
        {
            Register(new PrototypeComponentEntry(this, componentType));
        }

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <param name="componentInstance">�o�^����R���|�[�l���g�̃C���X�^���X</param>
        public void RegisterInstance(object componentInstance)
        {
            Register(new OuterComponentEntry(this, componentInstance));
        }

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <param name="componentType">�o�^����R���|�[�l���g�̌^</param>
        /// <param name="componentName">�o�^����R���|�[�l���g�̖��O</param>
        public void Register(Type componentType, string componentName)
        {
            Register(new PrototypeComponentEntry(this, componentType, componentName));
        }

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <param name="componentInstance">�o�^����R���|�[�l���g�̃C���X�^���X</param>
        /// <param name="componentName">�o�^����R���|�[�l���g�̖��O</param>
        public void RegisterInstance(object componentInstance, string componentName)
        {
            Register(new OuterComponentEntry(this, componentInstance, componentName));
        }

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <param name="componentEntry">�o�^����R���|�[�l���g�̏��</param>
        public void Register(IComponentEntry componentEntry)
        {
            secondaryComponentEntries.Add(componentEntry);
        }

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <remarks>
        /// ����̃C���^�[�t�F�C�X�ɗD�悵�Ċ��蓖�Ă�R���|�[�l���g���w�肵�܂��B
        /// </remarks>
        /// <param name="interfaceType">�D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X</param>
        /// <param name="implementComponentType">�D�悵�Ċ��蓖�Ă�R���|�[�l���g</param>
        /// <exception cref="TooManyRegistrationException">
        /// �w�肳�ꂽ�C���^�[�t�F�C�X����������R���|�[�l���g�������������ꍇ�ɔ��������O
        /// </exception>
        public void Register(Type interfaceType, Type implementComponentType)
        {
            Register(interfaceType, new PrototypeComponentEntry(this, implementComponentType));
        }

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <remarks>
        /// ����̃C���^�[�t�F�C�X�ɗD�悵�Ċ��蓖�Ă�R���|�[�l���g���w�肵�܂��B
        /// </remarks>
        /// <param name="interfaceType">�D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X</param>
        /// <param name="implementComponentInstance">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̃C���X�^���X</param>
        public void RegisterInstance(Type interfaceType, object implementComponentInstance)
        {
            Register(interfaceType, new OuterComponentEntry(this, implementComponentInstance));
        }

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <remarks>
        /// ����̃C���^�[�t�F�C�X�ɗD�悵�Ċ��蓖�Ă�R���|�[�l���g���w�肵�܂��B
        /// </remarks>
        /// <param name="interfaceType">�D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X</param>
        /// <param name="implementComponentType">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̌^</param>
        /// <param name="componentName">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̖��O</param>
        /// <exception cref="TooManyRegistrationException">
        /// �w�肳�ꂽ�C���^�[�t�F�C�X����������R���|�[�l���g�������������ꍇ�ɔ��������O
        /// </exception>
        public void Register(Type interfaceType, Type implementComponentType, string componentName)
        {
            Register(interfaceType, new PrototypeComponentEntry(this, implementComponentType, componentName));
        }

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <remarks>
        /// ����̃C���^�[�t�F�C�X�ɗD�悵�Ċ��蓖�Ă�R���|�[�l���g���w�肵�܂��B
        /// </remarks>
        /// <param name="interfaceType">�D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X</param>
        /// <param name="implementComponentInstance">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̃C���X�^���X</param>
        /// <param name="componentName">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̖��O</param>
        public void RegisterInstance(Type interfaceType, object implementComponentInstance, string componentName)
        {
            Register(interfaceType, new OuterComponentEntry(this, implementComponentInstance, componentName));
        }

        /// <summary>
        /// �R���|�[�l���g��o�^����
        /// </summary>
        /// <remarks>
        /// ����̃C���^�[�t�F�C�X�ɗD�悵�Ċ��蓖�Ă�R���|�[�l���g���w�肵�܂��B
        /// </remarks>
        /// <param name="interfaceType">�D�悵�Ċ��蓖�Ă�C���^�[�t�F�C�X</param>
        /// <param name="implementComponentEntry">�D�悵�Ċ��蓖�Ă�R���|�[�l���g�̏��</param>
        /// <exception cref="TooManyRegistrationException">
        /// �w�肳�ꂽ�C���^�[�t�F�C�X����������R���|�[�l���g�������������ꍇ�ɔ��������O
        /// </exception>
        public void Register(Type interfaceType, IComponentEntry implementComponentEntry)
        {
            if (primaryComponentEntries.Contains(interfaceType))
            {
                throw new TooManyRegistrationException();
            }

            primaryComponentEntries.Add(interfaceType, implementComponentEntry);
        }

        /// <summary>
        /// �w�肳�ꂽ�C���^�[�t�F�C�X����������R���|�[�l���g�̓o�^�����擾����
        /// </summary>
        /// <param name="interfaceType">�擾��������R���|�[�l���g�̃C���^�[�t�F�C�X</param>
        /// <returns>�w�肳�ꂽ�C���^�[�t�F�C�X����������R���|�[�l���g�̓o�^���</returns>
        /// <exception cref="TooManyRegistrationException">
        /// �w�肳�ꂽ�C���^�[�t�F�C�X����������R���|�[�l���g�������������ꍇ�ɔ��������O
        /// </exception>
        /// <exception cref="ComponentNotFoundException">
        /// �w�肳�ꂽ�C���^�[�t�F�C�X����������R���|�[�l���g���Ȃ������ꍇ�ɔ��������O
        /// </exception>
        public IComponentEntry GetComponentEntry(Type interfaceType)
        {
            if (primaryComponentEntries.Contains(interfaceType))
            {
                return (IComponentEntry)primaryComponentEntries[interfaceType];
            }

            if (CheckTooManyRegistration(interfaceType))
            {
                throw new TooManyRegistrationException();
            }

            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (interfaceType.IsAssignableFrom(entry.ComponentType))
                {
                    return entry;
                }
            }

            foreach (IComponentContainer child in children)
            {
                try
                {
                    return child.GetComponentEntry(interfaceType);
                }
                catch (ComponentNotFoundException)
                {
                    // �����Ȃ�
                }
            }

            throw new ComponentNotFoundException();
        }

        /// <summary>
        /// �w�肳�ꂽ���O�����R���|�[�l���g�̓o�^�����擾����
        /// </summary>
        /// <param name="componentName">�擾����R���|�[�l���g�̖��O</param>
        /// <returns>�w�肳�ꂽ���O�����R���|�[�l���g�̓o�^���</returns>
        /// <exception cref="TooManyRegistrationException">
        /// �w�肳�ꂽ���O�����R���|�[�l���g�������������ꍇ�ɔ��������O
        /// </exception>
        /// <exception cref="ComponentNotFoundException">
        /// �w�肳�ꂽ���O�����R���|�[�l���g���Ȃ������ꍇ�ɔ��������O
        /// </exception>
        public IComponentEntry GetComponentEntry(string componentName)
        {
            if (CheckTooManyRegistration(componentName))
            {
                throw new TooManyRegistrationException();
            }

            foreach (IComponentEntry entry in primaryComponentEntries.Values)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0)
                {
                    return entry;
                }
            }

            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0)
                {
                    return entry;
                }
            }

            foreach (IComponentContainer child in children)
            {
                try
                {
                    return child.GetComponentEntry(componentName);
                }
                catch (ComponentNotFoundException)
                {
                    // �����Ȃ�
                }
            }

            throw new ComponentNotFoundException();
        }

        /// <summary>
        /// �w�肳�ꂽ�C���^�[�t�F�C�X�����R���|�[�l���g�������o�^����ĂȂ����`�F�b�N����
        /// </summary>
        /// <param name="interfaceType">�`�F�b�N����C���^�[�t�F�C�X</param>
        /// <returns>�w�肳�ꂽ�C���^�[�t�F�C�X�����R���|�[�l���g�����łɕ����o�^����Ă��Ȃ����ǂ���</returns>
        private bool CheckTooManyRegistration(Type interfaceType)
        {
            int count = 0;
            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (interfaceType.IsAssignableFrom(entry.ComponentType)) 
                {
                    ++count;
                }
            }

            // ���g���b�L�[�ȃJ�E���g�̎d��������...
            // �q��IComponentContainer#GetComponentEntry���Ăяo�����Ƃ�
            // �q��IComponentContainerImpl#CheckTooManyRegistration��
            // �d���e�X�g�����A�d��������Η�O�𓊂���
            foreach (IComponentContainer child in children)
            {
                try
                {
                    child.GetComponentEntry(interfaceType);
                    ++count;
                }
                catch (ComponentNotFoundException)
                {
                    // �����Ȃ�
                }
                catch (TooManyRegistrationException)
                {
                    return true;
                }
            }

            return count > 1;
        }

        /// <summary>
        /// ���O�����R���|�[�l���g�������o�^����ĂȂ����`�F�b�N����
        /// </summary>
        /// <param name="componentName">�R���|�[�l���g�̖��O</param>
        /// <returns>���O�����R���|�[�l���g�����łɕ����o�^����Ă��Ȃ����ǂ���</returns>
        private bool CheckTooManyRegistration(string componentName)
        {
            int count = 0;
            foreach (IComponentEntry entry in primaryComponentEntries.Values)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0) 
                {
                    ++count;
                }
            }
            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                if (string.Compare(componentName, entry.ComponentName) == 0) 
                {
                    ++count;
                }
            }

            // ���g���b�L�[�ȃJ�E���g�̎d��������...
            // �q��IComponentContainer#GetComponentEntry���Ăяo�����Ƃ�
            // �q��IComponentContainerImpl#CheckTooManyRegistration��
            // �d���e�X�g�����A�d��������Η�O�𓊂���
            foreach (IComponentContainer child in children)
            {
                try
                {
                    child.GetComponentEntry(componentName);
                    ++count;
                }
                catch (ComponentNotFoundException)
                {
                    // �����Ȃ�
                }
                catch (TooManyRegistrationException)
                {
                    return true;
                }
            }

            return count > 1;
        }

        /// <summary>
        /// �o�^����Ă���R���|�[�l���g�̃C���X�^���X��j������
        /// </summary>
        /// <remarks>
        /// �o�^����Ă���R���|�[�l���g�̂���Singleton�ŃC���X�^���X��
        /// �Ǘ�����Ă�����̂̃C���X�^���X��j������B�j������R���|�[�l���g��
        /// IDisposable���������Ă��邱�ƁB
        /// </remarks>
        public void Discard()
        {
            foreach (IComponentEntry entry in primaryComponentEntries.Values)
            {
                entry.Discard();
            }
            foreach (IComponentEntry entry in secondaryComponentEntries)
            {
                entry.Discard();
            }
        }

        /// <summary>
        /// ���ׂĂ̎q�R���e�i���擾����
        /// </summary>
        /// <returns>�q�R���e�i�̔z��</returns>
        public IComponentContainer[] GetChildren()
        {
            return (IComponentContainer[])children.ToArray(typeof(IComponentContainer));
        }

        /// <summary>
        /// �q�R���e�i��ǉ�����
        /// </summary>
        /// <param name="child">�ǉ�����q�R���e�i</param>
        public void AddChild(IComponentContainer child)
        {
            children.Add(child);
        }
    }
}
