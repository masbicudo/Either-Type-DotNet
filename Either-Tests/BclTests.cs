using System;
using BCL;
using Either_Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Either_Tests
{
    [TestClass]
    public class BclTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var either = (Either<int, string>)Either.Factory.Create(1, typeof(int), typeof(string));
            Assert.AreEqual(1, either.Value);
            Assert.AreEqual(1, either.Value1);
            try
            {
                Assert.AreEqual(1, either.Value2);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var either = (Either<int, string>)Either.Factory.Create(null, typeof(int), typeof(string));
            Assert.AreEqual(null, either.Value);
            try
            {
                Assert.AreEqual(null, either.Value1);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
            Assert.AreEqual(null, either.Value2);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var either = (Either<int, string>)Either.Factory.Create("Miguel", typeof(int), typeof(string));
            Assert.AreEqual("Miguel", either.Value);
            try
            {
                Assert.AreEqual("Miguel", either.Value1);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
            Assert.AreEqual("Miguel", either.Value2);
        }

        [TestMethod]
        public void TestMethod4()
        {
            try
            {
                var either = (Either<int, DateTime>)Either.Factory.Create(null, typeof(int), typeof(DateTime));
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidCastException));
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            var either = (Either<int, DateTime>)Either.Factory.Create(1, typeof(int), typeof(DateTime));
            Assert.AreEqual(1, either.Value);
            Assert.AreEqual(1, either.Value1);
            try
            {
                Assert.AreEqual(1, either.Value2);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
        }

        [TestMethod]
        public void TestMethod6()
        {
            var date = DateTime.Now;
            var either = (Either<int, DateTime>)Either.Factory.Create(date, typeof(int), typeof(DateTime));
            Assert.AreEqual(date, either.Value);
            try
            {
                Assert.AreEqual(date, either.Value1);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
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
    }
}