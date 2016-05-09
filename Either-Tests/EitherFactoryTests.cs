using BCL;
using Either_Tests.BaseTestClasses;
using Either_Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Either_Tests.BaseTestClasses.MyAssert;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Either_Tests
{
    [TestClass]
    public class EitherFactoryTests : TestsBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            var either = (Either<int, string>)Either.Factory.Create(1, typeof(int), typeof(string));
            AreEqual(1, either.Value);
            AreEqual(1, either.Value1);
            Throws<InvalidOperationException>(() => either.Value2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var either = (Either<int, string>)Either.Factory.Create(null, typeof(int), typeof(string));
            AreEqual(null, either.Value);
            Throws<InvalidOperationException>(() => either.Value1);
            AreEqual(null, either.Value2);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var either = (Either<int, string>)Either.Factory.Create("Miguel", typeof(int), typeof(string));
            AreEqual("Miguel", either.Value);
            Throws<InvalidOperationException>(() => either.Value1);
            AreEqual("Miguel", either.Value2);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Throws<InvalidCastException>(() => (Either<int, DateTime>)Either.Factory.Create(null, typeof(int), typeof(DateTime)));
        }

        [TestMethod]
        public void TestMethod5()
        {
            var either = (Either<int, DateTime>)Either.Factory.Create(1, typeof(int), typeof(DateTime));
            AreEqual(1, either.Value);
            AreEqual(1, either.Value1);
            Throws<InvalidOperationException>(() => either.Value2);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var date = DateTime.Now;
            var either = (Either<int, DateTime>)Either.Factory.Create(date, typeof(int), typeof(DateTime));
            AreEqual(date, either.Value);
            Throws<InvalidOperationException>(() => either.Value1);
            AreEqual(date, either.Value2);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var either = (Either<string, SomeClass>)Either.Factory.Create(null, typeof(string), typeof(SomeClass));

            // in this case, selector is 0, because the value is null
            // as both types are nullable, then reading from any of the alternatives shout return null
            AreEqual(null, either.Value);
            AreEqual(null, either.Value1);
            AreEqual(null, either.Value2);
        }

        [TestMethod]
        public void TestMethod8()
        {
            var either = (Either<string, int>)Either.Factory.Create(null, typeof(string), typeof(int));

            // in this case, selector is 0, because the value is null
            // as both types are nullable, then reading from any of the alternatives shout return null
            AreEqual(0, either.GetSelectedAlternative());
            AreEqual(null, either.Value);
            AreEqual(null, either.Value1);
            Throws(() => either.Value2);
        }

        [TestMethod]
        public void TestMethod9()
        {
            var either = (Either<int?, DateTime>)Either.Factory.Create(null, typeof(int?), typeof(DateTime));

            // in this case, selector is 0, because the value is null
            // as both types are nullable, then reading from any of the alternatives shout return null
            AreEqual(0, either.GetSelectedAlternative());
            AreEqual(null, either.Value);
            AreEqual(null, either.Value1);
            Throws(() => either.Value2);
        }
    }
}