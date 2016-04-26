using BCL;
using Either_Tests.BaseTestClasses;
using Either_Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Either_Tests
{
    [TestClass]
    public class JsonNetTests : JsonNetTestsBase
    {
        [TestMethod]
        public void TestMethod1()
        {
            var either = (Either<int, string>)Either.Factory.Create(1, typeof(int), typeof(string));
            var jsonStr = JsonConvert.SerializeObject(either);
            Assert.AreEqual("1", jsonStr);
        }

        [TestMethod]
        public void TestMethos2()
        {
            var jsonStr = "1";
            var either = JsonConvert.DeserializeObject<Either<int, string>>(jsonStr);
            Assert.AreEqual(1, either.Value);
        }

        [TestMethod]
        public void TestMethos3()
        {
            var jsonStr = "1.1";
            var either = JsonConvert.DeserializeObject<Either<float, string>>(jsonStr);
            Assert.AreEqual(1.1f, either.Value);
        }

        [TestMethod]
        public void TestMethos4()
        {
            var jsonStr = "{Either:1.1}";
            var obj = JsonConvert.DeserializeObject<SomethingWithEither<float, string>>(jsonStr);
            Assert.AreEqual(1.1f, obj.Either.Value1);
        }

        [TestMethod]
        public void TestMethos5()
        {
            var jsonStr = @"{Either:{Name:""Miguel""}}";
            var obj = JsonConvert.DeserializeObject<SomethingWithEither<SomeClass, int>>(jsonStr);
            Assert.AreEqual("Miguel", obj.Either.Value1.Name);
        }

        [TestMethod]
        public void TestMethos6()
        {
            var obj = new SomethingWithEither<SomeClass, int>
            {
                Either = new Either<SomeClass, int>(new SomeClass { Name = "Miguel" })
            };

            var jsonStr = JsonConvert.SerializeObject(obj);
            Assert.AreEqual(@"{""Either"":{""Name"":""Miguel""}}", jsonStr);
        }

        [TestMethod]
        public void TestMethos7()
        {
            var obj = new SomethingWithEither<SomeClass, int>
            {
                Either = new Either<SomeClass, int>(new SomeClass { Name = "Miguel" })
            };

            var jsonStr = JsonConvert.SerializeObject(obj);
            Assert.AreEqual(@"{""Either"":{""Name"":""Miguel""}}", jsonStr);

            var obj2 = JsonConvert.DeserializeObject<SomethingWithEither<SomeClass, int>>(jsonStr);
            Assert.AreEqual("Miguel", obj2.Either.Value1.Name);
        }

        [TestMethod]
        public void TestMethos8()
        {
            var jsonStr = @"{""A"":""a""}";
            var obj = JsonConvert.DeserializeObject<Either<IList<string>, IDictionary<string, string>>>(jsonStr);
            Assert.AreEqual("a", obj.Value2["A"]);
        }

        [TestMethod]
        public void TestMethos9()
        {
            var jsonStr = @"[""A"",""B""]";
            var obj = JsonConvert.DeserializeObject<Either<IList<string>, IDictionary<string, string>>>(jsonStr);
            Assert.AreEqual("A", obj.Value1[0]);
            Assert.AreEqual("B", obj.Value1[1]);
        }

        [TestMethod]
        public void TestMethos10()
        {
            Either<IList<string>, IDictionary<string, string>> obj = new[] { "1", "2" };
            var jsonStr = JsonConvert.SerializeObject(obj);
            Assert.AreEqual(jsonStr, @"[""1"",""2""]");
        }

        [TestMethod]
        public void TestMethod_SerializeNullableOfEither()
        {
            var obj = new SomethingWithNullableEither<int, string> { NullableEither = "Miguel" };
            var jsonStr = JsonConvert.SerializeObject(obj);
            Assert.AreEqual(jsonStr, @"{""NullableEither"":""Miguel""}");
        }
    }
}