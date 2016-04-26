using BCL;
using Either_Tests.BaseTestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Either_Tests
{
    [TestClass]
    public class EitherTypeTests : TestsBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            var either = new Either<int, string>(1);
            var type = either.GetUnderlyingType();
            Assert.AreEqual(typeof(int), type);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var either = new Either<int, string>(1);
            AssertThrows(() => either.GetAlternativeType(0));
        }

        [TestMethod]
        public void TestMethod3()
        {
            var either = new Either<int, DateTime>(1);
            AssertThrows(() => either.GetAlternativeType(0));
        }

        [TestMethod]
        public void TestMethod4()
        {
            var either = new Either<int, DateTime>();
            AssertThrows(() => either.GetUnderlyingType());
        }

        [TestMethod]
        public void TestMethod5()
        {
            var either = new Either<int, string>();
            AssertThrows(() => either.GetUnderlyingType());
        }

        [TestMethod]
        public void TestMethod6()
        {
            var either = new Either<int, string>();
            Assert.AreEqual(true, either.IsValid());
        }

        [TestMethod]
        public void TestMethod7()
        {
            var either = new Either<int, DateTime>();
            Assert.AreEqual(false, either.IsValid());
        }

        [TestMethod]
        public void TestMethod8()
        {
            var either = new Either<int, string>();
            Assert.AreEqual(0, either.GetSelectedAlternative());
        }

        [TestMethod]
        public void TestMethod9()
        {
            var either = new Either<int, DateTime>();
            AssertThrows(() => Assert.AreEqual(0, either.GetSelectedAlternative()));
        }
    }
}