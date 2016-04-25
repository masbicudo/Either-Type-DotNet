using System;
using BCL;
using Either_Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Either_Tests
{
    [TestClass]
    public class EitherNewTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var either = new Either<int, string>(1);
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
            var either = new Either<int, string>(null);
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
            var either = new Either<int, string>("Miguel");
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
            // FREE TEST SLOT
        }

        [TestMethod]
        public void TestMethod5()
        {
            var either = new Either<int, DateTime>(1);
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
            var either = new Either<int, DateTime>(date);
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

            Exception exception = null;
            try
            {
                var value2 = either.Value2;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            Assert.IsInstanceOfType(exception, typeof(Exception));
        }
    }
}