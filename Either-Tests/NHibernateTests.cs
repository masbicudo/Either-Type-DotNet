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
            var obj = new EitherRoot { StringOrInt = "Miguel", };
            this.Session.Save(obj);
        }

        [TestMethod]
        public void TestMethod_SaveEither_Int32()
        {
            var obj = new EitherRoot { StringOrInt = 120, };
            this.Session.Save(obj);
        }

        [TestMethod]
        public void TestMethod_LoadEither_String()
        {
            // SETUP - first we need to save to the database
            var obj = new EitherRoot { StringOrInt = "Miguel", };
            using (var session = this.SessionProvider.SessionFactory.OpenSession())
                session.Save(obj);

            // TEST - load the previously saved entity
            var obj2 = this.Session.Get<EitherRoot>(obj.Id);

            // ASSERT
            Assert.AreEqual(obj2.StringOrInt, "Miguel");
        }

        [TestMethod]
        public void TestMethod_LoadEither_Int32()
        {
            // SETUP - first we need to save to the database
            var obj = new EitherRoot { StringOrInt = 120, };
            using (var session = this.SessionProvider.SessionFactory.OpenSession())
                session.Save(obj);

            // TEST - load the previously saved entity
            var obj2 = this.Session.Get<EitherRoot>(obj.Id);

            // ASSERT
            Assert.AreEqual(obj2.StringOrInt, 120);
        }
    }
}