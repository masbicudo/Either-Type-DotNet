using Either_Tests.BaseTestClasses;
using Either_Tests.NHibernate.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Either_Tests
{
    [TestClass]
    public class NHibernateTests : NHibernateUnitTestBase
    {
        [TestMethod]
        public void TestMethod_SaveEither_String()
        {
            var obj = new EitherRoot
            {
                StringOrInt = "Miguel",
            };

            this.Session.Save(obj);
        }

        [TestMethod]
        public void TestMethod_SaveEither_Int32()
        {
            var obj = new EitherRoot
            {
                StringOrInt = 120,
            };

            this.Session.Save(obj);
        }
    }
}