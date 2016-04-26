using BCL;

namespace Either_Tests.Models
{
    public class SomethingWithNullableEither<T1, T2>
    {
        public Either<T1, T2>? NullableEither { get; set; }
    }
}