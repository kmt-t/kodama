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
    /// ���̃R���|�[�l���g���œo�^����Ă���R���|�[�l���g�́A
    /// IComponentContainer#GetComponent���ɏ�ɓ����C���X�^���X��Ԃ��B
    /// </remarks>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public class SingletonComponentEntry : IComponentEntry
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
        /// �ˑ����𒍓����邽�߂̃R���X�g���N�^���o�C���h�����t�@���N�^
        /// </summary>
        private BindFunctor injectionConstructorFunctor;

        /// <summary>
        /// �ˑ����𒍓����邽�߂̈������o�C���h�ς݂̊֐��I�u�W�F�N�g
        /// </summary>
        private ArrayList injectionFactors = new ArrayList();

        /// <summary>
        /// �R���|�[�l���g�����������邽�߂̈������o�C���h�ς݂̊֐��I�u�W�F�N�g
        /// </summary>
        private ArrayList initializationFactors = new ArrayList();

        /// <summary>
        /// �C���X�^���X�̐��������ǂ����̃t���O�B�z�Q�Ƃ����o����̂ɂ�����
        /// </summary>
        private bool instantiating = false;

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
        public BindFunctor InjectionConstructor
        {
            set { injectionConstructorFunctor = value; }
        }

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="container">�R���|�[�l���g���o�^����R���e�i</param>
        /// <param name="type">�o�^����R���|�[�l���g�̌^</param>
        public SingletonComponentEntry(IComponentContainer container, Type type)
        {
            componentType     = type;
            componentName     = type.FullName;
            componentInstance = null;

            injectionConstructorFunctor = null;
            foreach (ConstructorInfo ci in componentType.GetConstructors())
            {
                if (Attribute.IsDefined(ci, typeof(InjectionPointAttribute)))
                {
                    ArrayList arguments = new ArrayList();
                    foreach (ParameterInfo param in ci.GetParameters())
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
                    injectionConstructorFunctor = new BindFunctor(new ConstructorFunctor(ci), arguments.ToArray());
                }
            }

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
        /// <param name="type">�o�^����R���|�[�l���g�̌^</param>
        /// <param name="name">�o�^����R���|�[�l���g�̖��O</param>
        public SingletonComponentEntry(IComponentContainer container, Type type, string name)
        {
            componentType     = type;
            componentName     = name;
            componentInstance = null;

            injectionConstructorFunctor = null;
            foreach (ConstructorInfo ci in componentType.GetConstructors())
            {
                if (Attribute.IsDefined(ci, typeof(InjectionPointAttribute)))
                {
                    ArrayList arguments = new ArrayList();
                    foreach (ParameterInfo param in ci.GetParameters())
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
                    injectionConstructorFunctor = new BindFunctor(new ConstructorFunctor(ci), arguments.ToArray());
                }
            }

            foreach (MethodInfo mi in componentType.GetMethods())
            {
                if (Attribute.IsDefined(mi, typeof(InjectionPointAttribute)))
                {
                    ParameterInfo[] paramerters = mi.GetParameters();
                    ArrayList       arguments   = new ArrayList();
                    arguments.Add(new NotBoundArgument(0));
                    foreach (ParameterInfo param in paramerters)
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
            foreach (object arg in injectionConstructorFunctor.GetBoundArguments())
            {
                IArgumentComponentProvider provider = arg as IArgumentComponentProvider;
                if (provider != null)
                {
                    list.Add(provider.ComponentEntry);
                }
            }
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
        /// <exception cref="CyclicDependencyException">
        /// �R���|�[�l���g�̈ˑ��֌W���R���X�g���N�^�C���W�F�N�V������
        /// �z���Ă��Ă��A�z���Ă���R���|�[�l���g��Singleton���[�h�̏ꍇ�ɔ��������O
        /// </exception>
        /// <remarks>
        /// ���̃��\�b�h�ŐV�����C���X�^���X��Ԃ��������̃C���X�^���X��Ԃ�����
        /// �����őI�����܂��B
        /// </remarks>
        public object GetInstance()
        {
            if (componentInstance == null)
            {
                if (instantiating && (injectionConstructorFunctor != null))
                {
                    throw new CyclicDependencyException();
                }

                instantiating = true;

                if (injectionConstructorFunctor == null)
                {
                    componentInstance = Activator.CreateInstance(componentType);
                }
                else
                {
                    componentInstance = injectionConstructorFunctor.Invoke();
                }

                foreach (IFunctor functor in injectionFactors)
                {
                    functor.Invoke(componentInstance);
                }

                foreach (IFunctor functor in initializationFactors)
                {
                    functor.Invoke(componentInstance);
                }

                instantiating = false;
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
            IDisposable disposable = componentInstance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
                componentInstance = null;
            }
        }
    }
}
