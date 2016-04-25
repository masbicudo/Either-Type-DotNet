using BCL;

namespace Either_Tests.Models
{
    public class SomethingWithEither<T1, T2>
    {
        public Either<T1, T2> Either { get; set; }
    }
}