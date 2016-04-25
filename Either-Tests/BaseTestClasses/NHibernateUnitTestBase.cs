using Either_Tests.NHibernate.Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;

namespace Either_Tests.BaseTestClasses
{
    public class NHibernateUnitTestBase : JsonNetTestsBase
    {
        public ISession Session { get; set; }
        public SessionProviderNH SessionProvider { get; private set; }

        [TestInitialize]
        public void TestInitializeNHibernate()
        {
            this.SessionProvider = new SessionProviderNH();
            this.Session = this.SessionProvider.SessionFactory.OpenSession();
        }

        [TestCleanup]
        public void TestCleanupNHibernate()
        {
            this.Session.Dispose();
        }
    }
}
