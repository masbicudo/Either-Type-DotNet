using BCL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Either_Tests
{
    [TestClass]
    public class EitherComparisonTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var either = new Either<int, string>(1);
            var cmp = either == 1;
            Assert.AreEqual(true, cmp);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var either = new Either<int, string>(1);
            var cmp = either.Equals(1);
            Assert.AreEqual(true, cmp);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var either = new Either<int, string>(1);
            var cmp = EqualityComparer<Either<int, string>>.Default.Equals(either, 1);
            Assert.AreEqual(true, cmp);
        }

        [TestMethod]
        public void TestMethod4()
        {
            object either = new Either<int, string>(1);
            var cmp = either.Equals(1);
            Assert.AreEqual(true, cmp);
        }

        [TestMethod]
        public void TestMethod5()
        {
            var either1 = new Either<int, string>(1);
            var either2 = new Either<string, int>(1);
            var cmp = either1 == either2;
            Assert.AreEqual(true, cmp);
        }

        [TestMethod]
        public void TestMethod6()
        {
            var either1 = new Either<int, string>(1);
            var either2 = new Either<string, int>(1);
            var cmp = either1.Equals(either2);
            Assert.AreEqual(true, cmp);
        }

        [TestMethod]
        public void TestMethod7()
        {
            object either1 = new Either<int, string>(1);
            var either2 = new Either<string, int>(1);
            var cmp = either1.Equals(either2);
            Assert.AreEqual(true, cmp);
        }

        [TestMethod]
        public void TestMethod8()
        {
            object either1 = new Either<int, string>(null);
            var cmp = either1.Equals(null);
            Assert.AreEqual(true, cmp);
        }

        [TestMethod]
        public void TestMetho9()
        {
            object either1 = new Either<int, string>(1);
            var cmp = either1.Equals(null);
            Assert.AreEqual(false, cmp);
        }
    }
}