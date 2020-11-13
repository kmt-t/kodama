using System;
using System.Reflection;
using Kodama.Script.Engine;
using Kodama.DependencyInjection.Component;
using Kodama.DependencyInjection.Container;
using Kodama.DependencyInjection.Marker;
using Kodama.DependencyInjection.Builder;
using NUnit.Framework;

namespace Kodama.UnitTest.DependencyInjection
{
    [TestFixture]
    public class DependencyInjectionTest
    {
        public interface TestIntarface1
        {
            void Print();
        }

        public interface TestIntarface2
        {
            void Print();
        }

        [AutoRegistratonComponent("Test", InstanceMode = ComponentInstanceMode.Singleton)]
        public class TestClass1 : TestIntarface1
        {
            public void Print()
            {
                Console.WriteLine("TestClass1");
            }
        }

        public class TestClass2 : TestIntarface1
        {
            public void Print()
            {
                Console.WriteLine("TestClass2");
            }
        }

        [AutoRegistratonComponent("Test")]
        public class TestClass3 : TestIntarface2
        {
            private TestIntarface1 dependency;

            public void Print()
            {
                dependency.Print();
                Console.WriteLine("TestClass3");
            }

            [InitializationPoint]
            public void Init()
            {
                Console.WriteLine("Init Class 3");
            }

            public TestIntarface1 Injection
            {
                [InjectionPoint]
                set { dependency = value; }
            }
        }

        public class TestClass4 : TestIntarface1
        {
            private TestIntarface2 dependency;

            public void Print()
            {
                dependency.Print();
                Console.WriteLine("TestClass4");
            }

            [InitializationPoint]
            public void Init()
            {
                Console.WriteLine("Init Class 4");
            }

            public TestIntarface2 Injection
            {
                [InjectionPoint]
                set { dependency = value; }
            }
        }

        public class TestClass5 : TestIntarface2
        {
            private TestIntarface1 dependency;

            public void Print()
            {
                dependency.Print();
                Console.WriteLine("TestClass3");
            }

            [InitializationPoint]
            public void Init()
            {
                Console.WriteLine("Init Class 3");
            }

            [InjectionPoint]
            public void Injection([ExplicitComponentName("TestClass1")]TestIntarface1 inter)
            {
                dependency = inter;
            }
        }

        [AutoRegistratonComponent("Test")]
        public class TestClass6 : TestIntarface2
        {
            private TestIntarface1 dependency;

            public void Print()
            {
                dependency.Print();
                Console.WriteLine("TestClass6");
            }

            [InitializationPoint]
            public void Init()
            {
                Console.WriteLine("Init Class 6");
            }

            [InjectionPoint]
            public TestClass6(TestIntarface1 dep)
            {
                dependency = dep;
            }
        }

        [Test]
        public void TestGetComponent1()
        {
            IMutableComponentContainer container = new ComponentContainerImpl();
            container.Register(typeof(TestClass1));
            try
            {
                Console.WriteLine("Test1 Start");
                TestClass1 test = (TestClass1)container.GetComponent(typeof(TestClass1));
                test.Print();
                Console.WriteLine("Test1 End");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestGetComponent2()
        {
            IMutableComponentContainer container = new ComponentContainerImpl();
            container.Register(typeof(TestClass1));
            container.Register(typeof(TestClass3));
            try
            {
                Console.WriteLine("Test2 Start");
                TestClass3 test = (TestClass3)container.GetComponent(typeof(TestClass3));
                test.Print();
                Console.WriteLine("Test2 End");
            }
            catch (Exception e)
            {
                Assert.Fail(e.StackTrace);
            }
        }

        [Test]
        public void TestGetComponent3()
        {
            IMutableComponentContainer container = new ComponentContainerImpl();
            container.Register(typeof(TestClass1));
            container.Register(typeof(TestIntarface2), typeof(TestClass2));
            container.Register(typeof(TestClass3));
            try
            {
                Console.WriteLine("Test3 Start");
                TestClass3 test = (TestClass3)container.GetComponent(typeof(TestClass3));
                test.Print();
                Console.WriteLine("Test3 End");
            }
            catch (Exception e)
            {
                Assert.Fail(e.StackTrace);
            }
        }

        [Test]
        [ExpectedException(typeof(TooManyRegistrationException))]
        public void TestGetComponent4()
        {
            IMutableComponentContainer container = new ComponentContainerImpl();
            container.Register(typeof(TestClass1));
            container.Register(typeof(TestClass2));
            container.Register(typeof(TestClass3));
            container.GetComponent(typeof(TestClass3));
        }

        [Test]
        [ExpectedException(typeof(CyclicDependencyException))]
        public void TestGetComponent5()
        {
            IMutableComponentContainer container = new ComponentContainerImpl();
            container.Register(typeof(TestClass3));
            container.Register(typeof(TestClass4));
            container.GetComponent(typeof(TestClass3));
        }

        [Test]
        public void TestGetComponent6()
        {
            IMutableComponentContainer container = new ComponentContainerImpl();
            container.Register(new SingletonComponentEntry(container, typeof(TestClass3)));
            container.Register(new SingletonComponentEntry(container, typeof(TestClass4)));
            container.GetComponent(typeof(TestClass3));
        }

        [Test]
        public void TestGetComponent7()
        {
            try
            {
                ScriptComponentContainerBuilder builder = new ScriptComponentContainerBuilder();
                builder.Script = "injection.vb";
                builder.AddReference(Assembly.GetExecutingAssembly().Location);
                IMutableComponentContainer container = (IMutableComponentContainer)builder.Build();
                container.GetComponent(typeof(TestClass3));
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
        }

        [Test]
        public void TestGetComponent8()
        {
            IMutableComponentContainer container = new ComponentContainerImpl();
            container.Register(typeof(TestClass1), "TestComponent");
            try
            {
                Console.WriteLine("Test8 Start");
                TestClass1 test = (TestClass1)container.GetComponent("TestComponent");
                test.Print();
                Console.WriteLine("Test8 End");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestGetComponent9()
        {
            IMutableComponentContainer parent = new ComponentContainerImpl();
            IMutableComponentContainer child = new ComponentContainerImpl();
            parent.AddChild(child);
            child.Register(typeof(TestClass1));
            try
            {
                Console.WriteLine("Test9 Start");
                TestClass1 test = (TestClass1)parent.GetComponent(typeof(TestClass1));
                test.Print();
                Console.WriteLine("Test9 End");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestGetComponent10()
        {
            IMutableComponentContainer parent = new ComponentContainerImpl();
            IMutableComponentContainer child = new ComponentContainerImpl();
            parent.AddChild(child);
            child.Register(typeof(TestClass1), "Test1");
            try
            {
                Console.WriteLine("Test10 Start");
                TestClass1 test = (TestClass1)parent.GetComponent("Test1");
                test.Print();
                Console.WriteLine("Test10 End");
            }
            catch
            {
                Assert.Fail();
            }
        }

        [Test]
        [ExpectedException(typeof(TooManyRegistrationException))]
        public void TestGetComponent11()
        {
            IMutableComponentContainer parent = new ComponentContainerImpl();
            IMutableComponentContainer child = new ComponentContainerImpl();
            parent.AddChild(child);
            child.Register(typeof(TestClass1));
            parent.Register(typeof(TestClass2));
            parent.Register(typeof(TestClass3));
            parent.GetComponent(typeof(TestClass3));
        }

        [Test]
        public void TestGetComponent12()
        {
            IMutableComponentContainer container = new ComponentContainerImpl();
            container.Register(typeof(TestClass1), "TestClass1");
            container.Register(typeof(TestClass5));

            Console.WriteLine("Test12 Start");
            TestClass5 test = (TestClass5)container.GetComponent(typeof(TestClass5));
            test.Print();
            Console.WriteLine("Test12 End");
        }

        [Test]
        public void TestGetComponent13()
        {
            AutoComponentContainerBuilder builder = new AutoComponentContainerBuilder();
            builder.ComponentCategory = "Test";
            builder.AddAssemblyFile(Assembly.GetExecutingAssembly().Location);
            IComponentContainer container = builder.Build();
            try
            {
                Console.WriteLine("Test13 Start");
                TestClass3 test = (TestClass3)container.GetComponent(typeof(TestClass3));
                test.Print();
                Console.WriteLine("Test13 End");
            }
            catch (Exception e)
            {
                Assert.Fail(e.StackTrace);
            }
        }

        [Test]
        public void TestGetComponent15()
        {
            IMutableComponentContainer container = new ComponentContainerImpl();
            container.Register(typeof(TestClass1));
            container.Register(typeof(TestClass6));
            try
            {
                Console.WriteLine("Test14 Start");
                TestClass6 test = (TestClass6)container.GetComponent(typeof(TestClass6));
                test.Print();
                Console.WriteLine("Test14 End");
            }
            catch (Exception e)
            {
                Assert.Fail(e.StackTrace);
            }
        }
    }
}
