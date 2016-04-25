using BCL;
using Either_For_NHibernate;
using Either_Tests.NHibernate.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Either_Tests.NHibernate.Maps
{
    /// <summary>
    /// Maps the <see cref="EitherRoot"/> type.
    /// </summary>
    public class EitherRootNHMap : ClassMapping<EitherRoot>
    {
        public EitherRootNHMap()
        {
            this.Id(x => x.Id, m => m.Generator(new IdentityGeneratorDef()));

            this.Property(x => x.StringOrInt,
                m =>
                {
                    m.Type<EitherUserType<Either<string, int>>>();
                });
        }
    }
}
