using System;
using System.Runtime.Remoting.Messaging;
using Microsoft.Vsa;
using Kodama.Script.Factory;
using Kodama.Script.Engine;
using Kodama.Script.Engine.CodeDom;
using Kodama.Script.Engine.CodeDom.VBScript;
using NUnit.Framework;

namespace Kodama.UnitTest.Script.CodeDom
{
    [TestFixture]
    public class VBScriptEngineTest
    {
        [Test]
        public void TestCompile()
        {
            IScriptEngineFactory factory = new VBScriptEngineFactory();
            Assert.IsTrue(factory.CanCompile("test.vb"));
            Assert.IsTrue(factory.CanCompile("test.VB"));
            Assert.IsFalse(factory.CanCompile("test.js"));
            IScriptEngine engine = null;
            try
            {
                engine = factory.Create();
//                engine.GenerateDebugInfo = true;
                engine.AddScriptCodeFromText(
                    "TestCode",
                    "imports System                                \n" +
                    "imports System.Diagnostics                    \n" + 
                    "imports Kodama.Script.Engine                  \n" +
                    "module TestModule                             \n" +
                    "    <ScriptEntryPoint> _                      \n" +
                    "    sub Main()                                \n" +
//                    "        Debugger.Launch()                     \n" +
//                    "        Debugger.Break()                      \n" +
                    "        Console.WriteLine(\"VBScript TestOK\")\n" +
                    "    end sub                                   \n" +
                    "end module                                    \n");
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
        [ExpectedException(typeof(CodeDomCompileErrorException))]
        public void TestCompileError()
        {
            IScriptEngineFactory factory = new VBScriptEngineFactory();
            Assert.IsTrue(factory.CanCompile("test.vb"));
            Assert.IsTrue(factory.CanCompile("test.VB"));
            Assert.IsFalse(factory.CanCompile("test.js"));
            IScriptEngine engine = null;
            try
            {
                engine = factory.Create();
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
