using BCL;

namespace Either_Tests.NHibernate.Models
{
    public class EitherRoot
    {
        public virtual int Id { get; set; }
        public virtual Either<string, int> StringOrInt { get; set; }
    }
}