using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Either_Tests
{
    [TestClass]
    public class DotNetBehaviorTests
    {
        [TestMethod]
        public void NullableNullToString()
        {
            // This test proves that the Nullable struct
            // does not throw when calling ToString with
            // a null value.
            int? x = null;
            var s = x.ToString();
            Assert.AreEqual("", s);
        }
    }
}