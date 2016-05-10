using BCL;
using Either_Tests.BaseTestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Either_Tests.BaseTestClasses.MyAssert;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
            AreEqual(typeof(int), type);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var either = new Either<int, string>(1);
            Throws(() => either.GetAlternativeType(0));
        }

        [TestMethod]
        public void TestMethod3()
        {
            var either = new Either<int, DateTime>(1);
            Throws(() => either.GetAlternativeType(0));
        }

        [TestMethod]
        public void TestMethod4()
        {
            var either = new Either<int, DateTime>();
            Throws(() => either.GetUnderlyingType());
        }

        [TestMethod]
        public void TestMethod5()
        {
            var either = new Either<int, string>();
            Throws(() => either.GetUnderlyingType());
        }

        [TestMethod]
        public void TestMethod6()
        {
            var either = new Either<int, string>();
            AreEqual(true, either.IsValid());
        }

        [TestMethod]
        public void TestMethod7()
        {
            var either = new Either<int, DateTime>();
            AreEqual(false, either.IsValid());
        }

        [TestMethod]
        public void TestMethod8()
        {
            var either = new Either<int, string>();
            AreEqual(0, either.GetSelectedAlternative());
        }

        [TestMethod]
        public void TestMethod9()
        {
            var either = new Either<int, DateTime>();
            Throws(() => AreEqual(0, either.GetSelectedAlternative()));
        }
    }
}