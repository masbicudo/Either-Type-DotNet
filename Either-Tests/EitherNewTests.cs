using BCL;
using Either_Tests.BaseTestClasses;
using Either_Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Either_Tests
{
    [TestClass]
    public class EitherNewTests : TestsBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            var either = new Either<int, string>(1);
            Assert.AreEqual(1, either.Value);
            Assert.AreEqual(1, either.Value1);
            AssertThrows<InvalidOperationException>(() => either.Value2);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var either = new Either<int, string>(null);
            Assert.AreEqual(null, either.Value);
            AssertThrows<InvalidOperationException>(() => either.Value1);
            Assert.AreEqual(null, either.Value2);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var either = new Either<int, string>("Miguel");
            Assert.AreEqual("Miguel", either.Value);
            AssertThrows<InvalidOperationException>(() => either.Value1);
            Assert.AreEqual("Miguel", either.Value2);
        }

        [TestMethod]
        public void TestMethod4()
        {
            // FREE TEST SLOT
        }

        [TestMethod]
        public void TestMethod5()
        {
            var either = new Either<int, DateTime>(1);
            Assert.AreEqual(1, either.Value);
            Assert.AreEqual(1, either.Value1);
            AssertThrows<InvalidOperationException>(() => either.Value2);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var date = DateTime.Now;
            var either = new Either<int, DateTime>(date);
            Assert.AreEqual(date, either.Value);
            AssertThrows<InvalidOperationException>(() => either.Value1);
            Assert.AreEqual(date, either.Value2);
        }

        [TestMethod]
        public void TestMethod7()
        {
            var either = new Either<string, SomeClass>((string)null);

            // in this case, selector is 0, because the value is null
            // as both types are nullable, then reading from any of the alternatives shout return null
            Assert.AreEqual(0, either.GetSelectedAlternative());
            Assert.AreEqual(null, either.Value);
            Assert.AreEqual(null, either.Value1);
            Assert.AreEqual(null, either.Value2);
        }

        [TestMethod]
        public void TestMethod7_1()
        {
            var either = new Either<string, SomeClass>((SomeClass)null);

            // in this case, selector is 0, because the value is null
            // as both types are nullable, then reading from any of the alternatives shout return null
            Assert.AreEqual(0, either.GetSelectedAlternative());
            Assert.AreEqual(null, either.Value);
            Assert.AreEqual(null, either.Value1);
            Assert.AreEqual(null, either.Value2);
        }

        [TestMethod]
        public void TestMethod8()
        {
            var either = new Either<string, int>(null);

            // in this case, selector is 0, because the value is null
            // as both types are nullable, then reading from any of the alternatives shout return null
            Assert.AreEqual(0, either.GetSelectedAlternative());
            Assert.AreEqual(null, either.Value);
            Assert.AreEqual(null, either.Value1);
            AssertThrows<Exception>(() => either.Value2);
        }

        [TestMethod]
        public void TestMethod9()
        {
            var either = new Either<int?, DateTime>(null);

            // in this case, selector is 0, because the value is null
            // as both types are nullable, then reading from any of the alternatives shout return null
            Assert.AreEqual(0, either.GetSelectedAlternative());
            Assert.AreEqual(null, either.Value);
            Assert.AreEqual(null, either.Value1);
            AssertThrows<Exception>(() => either.Value2);
        }
    }
}