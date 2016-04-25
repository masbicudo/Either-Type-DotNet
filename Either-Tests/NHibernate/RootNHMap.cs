using BCL;
using Either_For_NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Either_Tests.NHibernate
{
    /// <summary>
    /// Maps the <see cref="Root"/> type.
    /// </summary>
    public class RootNHMap : ClassMapping<Root>
    {
        public RootNHMap()
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
