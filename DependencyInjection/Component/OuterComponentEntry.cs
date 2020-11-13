using System;
using System.Collections;
using System.Reflection;
using Kodama.Function.Functor;
using Kodama.Function.Functor.Bind;
using Kodama.Function.Functor.Member;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Marker;

namespace Kodama.DependencyInjection.Component
{
    /// <summary>
    /// DependencyInjection�R���e�i�ɓo�^����Ă���R���|�[�l���g���
    /// </summary>
    /// <remarks>
    /// ���ɂ���R���|�[�l���g�̃C���X�^���X��DependencyInjection�R���e�i��
    /// �o�^����̂Ɏg�p���܂��B
    /// ���̃R���|�[�l���g���œo�^����Ă���R���|�[�l���g�́A
    /// IComponentContainer#GetComponent���ɏ�ɓ����C���X�^���X��Ԃ��B
    /// </remarks>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public class OuterComponentEntry : IComponentEntry
    {
        /// <summary>
        /// �o�^����Ă���R���|�[�l���g�̃C���X�^���X
        /// </summary>
        private object componentInstance;

        /// <summary>
        /// �o�^����Ă���R���|�[�l���g�̌^
        /// </summary>
        private Type componentType;

        /// <summary>
        /// �o�^����Ă���R���|�[�l���g�̖��O
        /// </summary>
        private string componentName;

        /// <summary>
        /// ���Ɉˑ�������������Ă��邩�ǂ���
        /// </summary>
        private bool isSolvedDependency;

        /// <summary>
        /// �ˑ����𒍓����邽�߂̈������o�C���h�ς݂̊֐��I�u�W�F�N�g
        /// </summary>
        private ArrayList injectionFactors = new ArrayList();

        /// <summary>
        /// �R���|�[�l���g�����������邽�߂̈������o�C���h�ς݂̊֐��I�u�W�F�N�g
        /// </summary>
        private ArrayList initializationFactors = new ArrayList();

        /// <summary>
        /// �o�^����Ă���R���|�[�l���g�̌^
        /// </summary>
        public Type ComponentType
        {
            get { return componentType; }
        }

        /// <summary>
        /// �o�^����Ă���R���|�[�l���g�̖��O
        /// </summary>
        public string ComponentName
        {
            get { return componentName; }
        }

        /// <summary>
        /// �ˑ����𒍓����邽�߂̃R���X�g���N�^�̈������o�C���h�����t�@���N�^
        /// </summary>
        /// <exception cref="NotSupportedException">���̃��\�b�h�̓T�|�[�g���Ă��Ȃ��̂ŕK�����̗�O�𓊂���</exception>
        public BindFunctor InjectionConstructor
        {
            set { throw new NotSupportedException(); }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="container">�R���|�[�l���g���o�^����R���e�i</param>
        /// <param name="instance">�o�^����R���|�[�l���g�̃C���X�^���X</param>
        public OuterComponentEntry(IComponentContainer container, object instance)
        {
            componentType      = instance.GetType();
            componentName      = instance.GetType().FullName;
            componentInstance  = instance;
            isSolvedDependency = false;

            foreach (MethodInfo mi in componentType.GetMethods())
            {
                if (Attribute.IsDefined(mi, typeof(InjectionPointAttribute)))
                {
                    ArrayList arguments = new ArrayList();
                    arguments.Add(new NotBoundArgument(0));
                    foreach (ParameterInfo param in mi.GetParameters())
                    {
                        if (Attribute.IsDefined(param, typeof(ExplicitComponentAttribute)))
                        {
                            ExplicitComponentAttribute eca =
                                (ExplicitComponentAttribute)Attribute.GetCustomAttribute
                                (param, typeof(ExplicitComponentAttribute));
                            arguments.Add(eca.CreateProvider(container));
                        }
                        else
                        {
                            arguments.Add(new TypedArgumentComponentProvider(container, param.ParameterType));
                        }    
                    }
                    injectionFactors.Add(new BindFunctor(new MemberFunctor(mi), arguments.ToArray()));
                }
            }

            foreach (MethodInfo mi in componentType.GetMethods())
            {
                if (Attribute.IsDefined(mi, typeof(InitializationPointAttribute)))
                {
                    initializationFactors.Add(new MemberFunctor(mi));
                }
            }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="container">�R���|�[�l���g��o�^����R���e�i</param>
        /// <param name="instance">�o�^����R���|�[�l���g�̃C���X�^���X</param>
        /// <param name="name">�o�^����R���|�[�l���g�̖��O</param>
        public OuterComponentEntry(IComponentContainer container, object instance, string name)
        {
            componentType      = instance.GetType();
            componentName      = name;
            componentInstance  = instance;
            isSolvedDependency = false;

            foreach (MethodInfo mi in componentType.GetMethods())
            {
                if (Attribute.IsDefined(mi, typeof(InjectionPointAttribute)))
                {
                    ArrayList arguments = new ArrayList();
                    arguments.Add(new NotBoundArgument(0));
                    foreach (ParameterInfo param in mi.GetParameters())
                    {
                        arguments.Add(new TypedArgumentComponentProvider(container, param.ParameterType));
                    }
                    injectionFactors.Add(new BindFunctor(new MemberFunctor(mi), arguments.ToArray()));
                }
            }

            foreach (MethodInfo mi in componentType.GetMethods())
            {
                if (Attribute.IsDefined(mi, typeof(InitializationPointAttribute)))
                {
                    initializationFactors.Add(new MemberFunctor(mi));
                }
            }
        }

        /// <summary>
        /// �ˑ����𒍓����邽�߂̈������o�C���h�ς݂̊֐��I�u�W�F�N�g��ǉ�����
        /// </summary>
        /// <param name="functor">�ǉ�����֐��I�u�W�F�N�g</param>
        public void AddInjectionFanctor(BindFunctor functor)
        {
            injectionFactors.Add(functor);
        }

        /// <summary>
        /// �R���|�[�l���g�����������邽�߂̈������o�C���h�ς݂̊֐��I�u�W�F�N�g��ǉ�����
        /// </summary>
        /// <param name="functor">�ǉ�����֐��I�u�W�F�N�g</param>
        public void AddInitializationFactor(IFunctor functor)
        {
            initializationFactors.Add(functor);
        }

        /// <summary>
        /// �R���|�[�l���g���ˑ����Ă���R���|�[�l���g�̓o�^����Ԃ�
        /// </summary>
        /// <returns>�R���|�[�l���g���ˑ����Ă���R���|�[�l���g�̓o�^���</returns>
        public IComponentEntry[] GetDependencies()
        {
            ArrayList list = new ArrayList();
            foreach (BindFunctor functor in injectionFactors)
            {
                foreach (object arg in functor.GetBoundArguments())
                {
                    IArgumentComponentProvider provider = arg as IArgumentComponentProvider;
                    if (provider != null)
                    {
                        list.Add(provider.ComponentEntry);
                    }
                }
            }
            return (IComponentEntry[])list.ToArray(typeof(IComponentEntry));
        }

        /// <summary>
        /// �R���|�[�l���g�̃C���X�^���X���擾����
        /// </summary>
        /// <returns>�R���|�[�l���g�̃C���X�^���X</returns>
        /// <remarks>
        /// ���̃��\�b�h�ŐV�����C���X�^���X��Ԃ��������̃C���X�^���X��Ԃ�����
        /// �����őI�����܂��B
        /// </remarks>
        public object GetInstance()
        {
            if (!isSolvedDependency)
            {
                foreach (IFunctor functor in injectionFactors)
                {
                    functor.Invoke(componentInstance);
                }

                foreach (IFunctor functor in initializationFactors)
                {
                    functor.Invoke(componentInstance);
                }

                isSolvedDependency = true;
            }

            return componentInstance;
        }

        /// <summary>
        /// �R���|�[�l���g�̃C���X�^���X��j������
        /// </summary>
        /// <remarks>
        /// �j������R���|�[�l���g��IDisposable���������Ă��邱�ƁB
        /// </remarks>
        public void Discard()
        {
/*            IDisposable disposable = componentInstance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
                componentInstance = null;
                isSolvedDependency = false;
            }*/
        }
    }
}
