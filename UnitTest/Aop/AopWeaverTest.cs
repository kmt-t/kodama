using System;
using Kodama.Aop.Aspect;
using Kodama.Aop.Weaver;
using Kodama.Aop.Interceptor;
using Kodama.Aop.JoinPoint;
using Kodama.Aop.Pointcut;
using Kodama.Aop.Pointcut.Compose;
using Kodama.Aop.Pointcut.Compose.Class;
using Kodama.Aop.Pointcut.Compose.Method;
using Kodama.DbC.Constraint;
using NUnit.Framework;

namespace Kodama.UnitTest.Aop
{
    [TestFixture]
    public class AopWeaverTest
    {
        public class TestInterceptor : IMethodInterceptor
        {
            public object Invoke(MethodInvocation invocation, out object[] outArguments)
            {
                Console.WriteLine("Pre");
                object ret = invocation.Proceed(out outArguments);
                Console.WriteLine("Post");
                return ret;
            }
        }

        public class TestThrowExecptionInterceptor : IMethodInterceptor
        {
            public object Invoke(MethodInvocation invocation, out object[] outArguments)
            {
                throw new ApplicationException("Test Exception");
            }
        }

        [AttributeUsage(AttributeTargets.Method)]
        public class TestInterceptAttribute : Attribute
        {
        }

        [AspectTarget]
        public class TestAspectWeavedClass : ContextBoundObject
        {
            [TestIntercept]
            public void Hello()
            {
                Console.WriteLine("Hello");
            }
        }

        public class TestAspectWeavedClass2 : ContextBoundObject
        {
        }

        [AspectTarget]
        public class TestAspectWeavedClass3 : TestAspectWeavedClass2
        {
            [TestIntercept]
            public void Hello()
            {
                Console.WriteLine("Hello");
            }
        }

        [AspectTarget, InvariantCondition("target.y == 0")]
        public class DbCTest : ContextBoundObject
        {
            public int y = 0;

            [PreCondition("x < 0")]
            public int Test1(int x)
            {
                return x + y;
            }

            [PostCondition("ret < 0")]
            public int Test2(int x)
            {
                return x + y;
            }

            public void Test3(int x)
            {
                y++;
            }
        }

        [Test]
        public void TestSignatureIntercept()
        {
            try
            {
                IAspect aspect = new AspectImpl(
                    new TestInterceptor(),
                    new ClassNamePointcut(".*\\+TestAspectWeavedClass") &
                    new MethodNamePointcut("Hello"));
                AspectWeaver.Instance().Register(aspect);
                TestAspectWeavedClass wc = new TestAspectWeavedClass();
                Console.WriteLine("MethodSigInterceptTest Start");
                wc.Hello();
                Console.WriteLine("MethodSigInterceptTest End");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [Test]
        public void TestAttributeIntercept()
        {
            try
            {
                IAspect aspect = new AspectImpl(
                    new TestInterceptor(),
                    new ClassNamePointcut(".*") &
                    new MethodAttributePointcut(typeof(TestInterceptAttribute)));
                AspectWeaver.Instance().Register(aspect);
                TestAspectWeavedClass wc = new TestAspectWeavedClass();
                Console.WriteLine("MethodAttrInterceptTest Start");
                wc.Hello();
                Console.WriteLine("MethodAttrInterceptTest End");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [Test]
        public void TestInhelitAttributeIntercept()
        {
            try
            {
                IAspect aspect = new AspectImpl(
                    new TestInterceptor(),
                    new ClassNamePointcut(".*") &
                    new MethodAttributePointcut(typeof(TestInterceptAttribute)));
                AspectWeaver.Instance().Register(aspect);
                TestAspectWeavedClass3 wc = new TestAspectWeavedClass3();
                Console.WriteLine("MethodAttrInterceptTest Start");
                wc.Hello();
                Console.WriteLine("MethodAttrInterceptTest End");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Assert.Fail();
            }
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void TestThrowExceptionIntercept()
        {
            AspectImpl aspect = new AspectImpl(
                new TestThrowExecptionInterceptor(),
                new ClassNamePointcut(".*") &
                new MethodAttributePointcut(typeof(TestInterceptAttribute)));
            AspectWeaver.Instance().Register(aspect);
            TestAspectWeavedClass wc = new TestAspectWeavedClass();
            Console.WriteLine("MethodAttrInterceptTest Start");
            wc.Hello();
            Console.WriteLine("MethodAttrInterceptTest End");
        }

        [Test]
        public void TestDbCIntercept1()
        {
            AspectImpl aspect = new AspectImpl(
                new ConstraintInterceptor(),
                new ClassAttributePointcut(typeof(InvariantConditionAttribute)) |
                new MethodAttributePointcut(typeof(PreConditionAttribute)) |
                new MethodAttributePointcut(typeof(PostConditionAttribute)));
            AspectWeaver.Instance().Register(aspect);

            new DbCTest().Test1(-1);
            new DbCTest().Test2(-1);
        }

        [Test]
        public void TestDbCIntercept2()
        {
/*            AspectImpl aspect = new AspectImpl(
                new ConstraintInterceptor(),
                new ClassAttributePointcut(typeof(InvariantConditionAttribute)) |
                new MethodAttributePointcut(typeof(PreConditionAttribute)) |
                new MethodAttributePointcut(typeof(PostConditionAttribute)));
            AspectWeaver.Instance().Register(aspect);

            new DbCTest().Test1(1);
            new DbCTest().Test2(1);
            new DbCTest().Test3(-1);*/
        }
    }
}
