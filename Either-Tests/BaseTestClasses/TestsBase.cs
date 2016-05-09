using System.Threading;

namespace Either_Tests.BaseTestClasses
{
    public class TestsBase
    {
        private static int globalInstanceId;
        private readonly int instanceId;

        public TestsBase()
        {
            this.instanceId = Interlocked.Increment(ref globalInstanceId);
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}({this.instanceId})";
        }
    }
}