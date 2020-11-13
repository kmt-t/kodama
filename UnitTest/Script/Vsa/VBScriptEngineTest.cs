using System;
using System.Runtime.Remoting.Messaging;
using Microsoft.Vsa;
using Kodama.Script.Factory;
using Kodama.Script.Engine;
using Kodama.Script.Engine.Vsa;
using Kodama.Script.Engine.Vsa.VBScript;
using NUnit.Framework;
 
namespace Kodama.UnitTest.Script.Vsa
{
    [TestFixture]
    public class VBScriptEngineTest
    {
        [Test]
        public void TestCompile()
        {
            IScriptEngineFactory factory = new VBScriptEngineFactory();
            Assert.IsTrue(factory.CanCompile("test.vbs"));
            Assert.IsTrue(factory.CanCompile("test.VBS"));
            Assert.IsFalse(factory.CanCompile("test.js"));
            IScriptEngine engine = null;
            try
            {
                engine = factory.Create();
                engine.AddGlobalIntstance("Test", "VBScript TestOK");
//                engine.GenerateDebugInfo = true;
                engine.AddScriptCodeFromText(
                    "TestCode",
                    "imports System                 \n" +
                    "imports Kodama.Script.Engine   \n" +
                    "imports System.Diagnostics     \n" +
                    "module TestModule              \n" +
                    "    <ScriptEntryPoint> _       \n" +
                    "    sub Main()                 \n" +
//                    "        Debugger.Launch()      \n" +
//                    "        Debugger.Break()       \n" +
                    "        Console.WriteLine(Test)\n" +
                    "    end sub                    \n" +
                    "end module                     \n");
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
                Console.WriteLine(e.InnerException.GetType().Name);
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine(e.InnerException.StackTrace);

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
            IScriptEngineFactory factory = new VBScriptEngineFactory();
            Assert.IsTrue(factory.CanCompile("test.vbs"));
            Assert.IsTrue(factory.CanCompile("test.VBS"));
            Assert.IsFalse(factory.CanCompile("test.js"));
            IScriptEngine engine = null;
            try
            {
                engine = factory.Create();
                engine.AddGlobalIntstance("Test", "VBScript TestOK");
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
