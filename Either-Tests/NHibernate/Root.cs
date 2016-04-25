using BCL;

namespace Either_Tests.NHibernate
{
    public class Root
    {
        public virtual int Id { get; set; }
        public Either<string, int> StringOrInt { get; set; }
    }
}