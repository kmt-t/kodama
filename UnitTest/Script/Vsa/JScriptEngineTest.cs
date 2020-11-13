using System;
using System.Runtime.Remoting.Messaging;
using Microsoft.Vsa;
using Kodama.Script.Factory;
using Kodama.Script.Engine;
using Kodama.Script.Engine.Vsa;
using Kodama.Script.Engine.Vsa.JScript;
using NUnit.Framework;

namespace Kodama.UnitTest.Script.Vsa
{
    [TestFixture]
    public class JScriptEngineTest
    {
        [Test]
        public void TestCompile()
        {
            IScriptEngineFactory factory = new JScriptEngineFactory();
            Assert.IsTrue(factory.CanCompile("test.js"));
            Assert.IsTrue(factory.CanCompile("test.JS"));
            Assert.IsFalse(factory.CanCompile("test.vbs"));
            IScriptEngine engine = null;
            try
            {
                engine = factory.Create();
                engine.AddGlobalIntstance("Test", "JScript TestOK");
                engine.AddScriptCodeFromText("TestCode", "import System; Console.WriteLine(Test);");
                engine.Compile();
                engine.Run();
            }
            catch (CompileErrorException e)
            {
                Console.WriteLine(e.GetType().Name);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                foreach (CompileErrorInfo cei in e.GetCompileErrorInfos())
                {
                    Console.WriteLine("Source   : " + cei.SourceName );
                    Console.WriteLine("Desc     : " + cei.Description);
                    Console.WriteLine("Line     : " + cei.ErrorLine  );
                    Console.WriteLine("LineText : " + cei.ErrorText  );
                }
                Assert.Fail();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Assert.Fail();
            }
            finally
            {
                if (engine != null)
                {
                    engine.Close();
                }
            }
        }

        [Test]
        [ExpectedException(typeof(VsaCompileErrorException))]
        public void TestCompileError()
        {
            IScriptEngineFactory factory = new JScriptEngineFactory();
            Assert.IsTrue(factory.CanCompile("test.js"));
            Assert.IsTrue(factory.CanCompile("test.JS"));
            Assert.IsFalse(factory.CanCompile("test.vbs"));
            IScriptEngine engine = null;
            try
            {
                engine = factory.Create();
                engine.AddGlobalIntstance("Test", "JScript TestOK");
                engine.AddScriptCodeFromText("TestCode", "Ç≈ÇΩÇÁÇﬂÇ»ÉRÅ[Éh");
                engine.Compile();
                engine.Run();
            }
            finally
            {
                if (engine != null)
                {
                    engine.Close();
                }
            }
        }
    }
}
