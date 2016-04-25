using System;
using JetBrains.Annotations;

namespace Either_Tests.NHibernate.Code
{
    public abstract class Database
    {
        protected Database([NotNull] string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));
            this.ConnectionString = connectionString;
        }

        [NotNull]
        public string ConnectionString { get; }

        public abstract void CreateDatabaseMedia();
    }
}