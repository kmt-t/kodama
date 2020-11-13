using System;
using Kodama.Function.Functor;
using Kodama.Function.Functor.Bind;
using Kodama.Function.Functor.Member;
using NUnit.Framework;

namespace Kodama.UnitTest.Function
{
    [TestFixture]
    public class FunctionTest
    {
        public object GenericMethod(params object[] arguments)
        {
            foreach (object arg in arguments)
            {
                Console.WriteLine(arg.ToString());
            }
            return null;
        }

        public class TestClass
        {
            public void Print(string arg1, string arg2, object arg3, string arg4)
            {
                Console.WriteLine(arg1);
                Console.WriteLine(arg2);
                Console.WriteLine(arg3.ToString());
                Console.WriteLine(arg4);
            }

            public override string ToString()
            {
                return "2";
            }
        }

        [Test]
        public void TestGenericFunctor()
        {
            IFunctor functor = new DelegateFunctor(new FunctorHandler(GenericMethod));
            functor.Invoke("GenericFunctorTest Start", "1", "2", "3", "GenericFunctorTest End");
        }

        [Test]
        public void TestMemberFunctor()
        {
            IFunctor functor = new MemberFunctor(typeof(TestClass).GetMethod("Print"));
            functor.Invoke(new TestClass(), "MemberFunctorTest Start", "1", 2, "MemberFunctorTest End");
        }

        [Test]
        [ExpectedException(typeof(UnmatchArgumentException))]
        public void TestMemberFunctorError1()
        {
            IFunctor functor = new MemberFunctor(typeof(TestClass).GetMethod("Print"));
            functor.Invoke(new TestClass(), "MemberFunctorTest Start", 1, "2", "MemberFunctorTest End");
        }

        [Test]
        [ExpectedException(typeof(UnmatchArgumentException))]
        public void TestMemberFunctorError2()
        {
            IFunctor functor = new MemberFunctor(typeof(TestClass).GetMethod("Print"));
            functor.Invoke(new TestClass(), "MemberFunctorTest Start", "1", "MemberFunctorTest End");
        }

        [Test]
        public void TestBindFunctor()
        {
            IFunctor methodFunctor = new MemberFunctor(typeof(TestClass).GetMethod("Print"));
            IFunctor bindFunctor   = new BindFunctor(
                methodFunctor,
                new NotBoundArgument(0),
                "BindFunctorTest Start",
                "1",
                "2",
                "BindFunctorTest End");
            bindFunctor.Invoke(new TestClass());
        }
    }
}
