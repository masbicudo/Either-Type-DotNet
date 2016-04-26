using BCL;
using Either_Tests.BaseTestClasses;
using Either_Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Either_Tests
{
    [TestClass]
    public class EitherFactoryTests : TestsBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            var either = (Either<int, string>)Either.Factory.Create(1, typeof(int), typeof(string));
            Assert.AreEqual(1, either.Value);
            Assert.AreEqual(1, either.Value1);
            AssertThrows<InvalidOperationException>(() => either.Value2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var either = (Either<int, string>)Either.Factory.Create(null, typeof(int), typeof(string));
            Assert.AreEqual(null, either.Value);
            AssertThrows<InvalidOperationException>(() => either.Value1);
            Assert.AreEqual(null, either.Value2);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var either = (Either<int, string>)Either.Factory.Create("Miguel", typeof(int), typeof(string));
            Assert.AreEqual("Miguel", either.Value);
            AssertThrows<InvalidOperationException>(() => either.Value1);
            Assert.AreEqual("Miguel", either.Value2);
        }

        [TestMethod]
        public void TestMethod4()
        {
            AssertThrows<InvalidCastException>(() => (Either<int, DateTime>)Either.Factory.Create(null, typeof(int), typeof(DateTime)));
        }

        [TestMethod]
        public void TestMethod5()
        {
            var either = (Either<int, DateTime>)Either.Factory.Create(1, typeof(int), typeof(DateTime));
            Assert.AreEqual(1, either.Value);
            Assert.AreEqual(1, either.Value1);
            AssertThrows<InvalidOperationException>(() => either.Value2);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var date = DateTime.Now;
            var either = (Either<int, DateTime>)Either.Factory.Create(date, typeof(int), typeof(DateTime));
            Assert.AreEqual(date, either.Value);
            AssertThrows<InvalidOperationException>(() => either.Value1);
            Assert.AreEqual(date, either.Value2);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var either = (Either<string, SomeClass>)Either.Factory.Create(null, typeof(string), typeof(SomeClass));

            // in this case, selector is 0, because the value is null
            // as both types are nullable, then reading from any of the alternatives shout return null
            Assert.AreEqual(null, either.Value);
            Assert.AreEqual(null, either.Value1);
            Assert.AreEqual(null, either.Value2);
        }

        [TestMethod]
        public void TestMethod8()
        {
            var either = (Either<string, int>)Either.Factory.Create(null, typeof(string), typeof(int));

            // in this case, selector is 0, because the value is null
            // as both types are nullable, then reading from any of the alternatives shout return null
            Assert.AreEqual(0, either.GetSelectedAlternative());
            Assert.AreEqual(null, either.Value);
            Assert.AreEqual(null, either.Value1);
            AssertThrows(() => either.Value2);
        }

        [TestMethod]
        public void TestMethod9()
        {
            var either = (Either<int?, DateTime>)Either.Factory.Create(null, typeof(int?), typeof(DateTime));

            // in this case, selector is 0, because the value is null
            // as both types are nullable, then reading from any of the alternatives shout return null
            Assert.AreEqual(0, either.GetSelectedAlternative());
            Assert.AreEqual(null, either.Value);
            Assert.AreEqual(null, either.Value1);
            AssertThrows(() => either.Value2);
        }
    }
}