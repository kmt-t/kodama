using System;
using System.Collections;
using System.Reflection;
using Kodama.Script.Engine;
using Kodama.Script.Factory;
using Kodama.Script.Engine.CodeDom.CSharpScript;
using Kodama.Script.Engine.CodeDom.JScript;
using Kodama.Script.Engine.CodeDom.VBScript;

using Kodama.DependencyInjection.Container;

namespace Kodama.DependencyInjection.Builder
{
    /// <summary>
    /// �X�N���v�g�𗘗p���āADependencyInjection�R���e�i�𐶐�����
    /// �R���|�[�l���g��o�^���A���̃C���X�^���X��Ԃ��r���_
    /// </summary>
    /// <seealso cref="Kodama.DependencyInjection.Container.ComponentContainerImpl"/>
    public class ScriptComponentContainerBuilder : IComponentContainerBuilder
    {
        /// <summary>
        /// �X�N���v�g�t�@�C���̃p�X
        /// </summary>
        private string scriptPath = null;

        /// <summary>
        /// �X�N���v�g����Q�Ƃ���A�Z���u���̖��O
        /// </summary>
        private ArrayList assemblyNames = new ArrayList();

        /// <summary>
        /// �X�N���v�g�t�@�C���̃p�X
        /// </summary>
        public string Script
        {
            set { scriptPath = value; }
        }

        /// <summary>
        /// �X�N���v�g����Q�Ƃ���A�Z���u���̒ǉ�
        /// </summary>
        /// <param name="assemblyName">�X�N���v�g����Q�Ƃ���A�Z���u��</param>
        public void AddReference(string assemblyName)
        {
            assemblyNames.Add(assemblyName);
        }

        /// <summary>
        /// �X�N���v�g�𗘗p���āADependencyInjection�R���e�i�𐶐�����
        /// �R���|�[�l���g��o�^���A���̃C���X�^���X��Ԃ�
        /// </summary>
        /// <returns>�������ꂽDependencyInjection�R���e�i</returns>
        /// <exception cref="CompileErrorException">
        /// �X�N���v�g�̃R���p�C���G���[�����������ꍇ�͂��̗�O�𓊂���
        /// </exception>
        /// <exception cref="FactoryNotFoundException">
        /// DependencyInjection�R���e�i�̃t�@�N�g���[���\�b�h���X�N���v�g����
        /// �݂���Ȃ��ꍇ�͂��̗�O�𓊂���
        /// </exception>
        /// <remarks>
        /// <p>�X�N���v�g�ɂ�C#�AVBScript��JScript�����p�ł��܂��B�X�N���v�g���
        /// ComponentContainerFactoryAttribute����������IComponentContainer�C���^�[�t�F�C�X
        /// �ƌ݊����̂���l��Ԃ�Static�Ȋ֐����ADependencyInjection�R���e�i�𐶐���
        /// �ˑ��֌W���ݒ肵�����̂�Ԃ��t�@�N�g���[���\�b�h�ƂȂ�܂��B�X�N���v�g���
        /// ���̃t�@�N�g���[���\�b�h�����݂��Ȃ��ꍇ��FactoryNotFoundException��O��
        /// �����܂��B�ȉ��ɃX�N���v�g�̗�������܂��B</p>
        /// <code lang="C#">
        /// using System;
        /// using Kodama.DependencyInjection.Container;
        /// using Kodama.DependencyInjection.Factory;
        /// using HogeHoge;
        ///
        /// public class ComponentContainerBuilder
        /// {
        ///     [ComponentContainerFactory]
        ///     public IMutableComponentContainer Build()
        ///     {
        ///         IMutableComponentContainer container = new ComponentContainerImpl();
        ///         container.Register(typeof(ClassA));
        ///         container.Register(typeof(ClassB));
        ///         return container;
        ///     }
        /// }
        /// </code>
        /// </remarks>
        public IComponentContainer Build()
        {
            IScriptEngineFactory[] factories = new IScriptEngineFactory[]
                {new CSharpScriptEngineFactory(), new JScriptEngineFactory(), new VBScriptEngineFactory()};
            foreach (IScriptEngineFactory factory in factories)
            {
                if (!factory.CanCompile(scriptPath))
                {
                    continue;
                }

                IScriptEngine engine = factory.CreateFromFile(scriptPath);

                engine.AddReference(Assembly.GetExecutingAssembly().Location);
                foreach (string assemblyName in assemblyNames)
                {
                    engine.AddReference(assemblyName);
                }

                try 
                {
                    engine.Compile();
                }
                finally
                {
                    engine.Close();
                }

                foreach (Type type in engine.ScriptAssembly.GetTypes())
                {
                    foreach (MethodInfo mi in type.GetMethods())
                    {
                        if (!mi.IsStatic)
                        {
                            continue;
                        }
                        if (mi.GetParameters().Length != 0)
                        {
                            continue;
                        }
                        if (!typeof(IComponentContainer).IsAssignableFrom(mi.ReturnType))
                        {
                            continue;
                        }
                        if (!Attribute.IsDefined(mi, typeof(ComponentContainerFactoryAttribute)))
                        {
                            continue;
                        }

                        IComponentContainer container = (IComponentContainer)mi.Invoke(null, null);

                        engine.Close();

                        return container;
                    }
                }

                engine.Close();

                throw new FactoryNotFoundException();
            }

            throw new NotSupportedScriptException();
        }
    }
}
