using BCL;
using Either_For_JsonNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Either_Tests
{
    [TestClass]
    public class JsonNetTests
    {
        private static void InitJsonConvert()
        {
            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new EitherJsonConverter());
                return settings;
            };
        }

        [TestMethod]
        public void TestMethod1()
        {
            InitJsonConvert();

            var either = (Either<int, string>)Either.Factory.Create(1, typeof(int), typeof(string));

            var jsonStr = JsonConvert.SerializeObject(either);

            Assert.AreEqual("1", jsonStr);
        }

        [TestMethod]
        public void TestMethos2()
        {
            InitJsonConvert();

            var jsonStr = "1";

            var either = JsonConvert.DeserializeObject<Either<int, string>>(jsonStr);

            Assert.AreEqual(1, either.Value);
        }

        [TestMethod]
        public void TestMethos3()
        {
            InitJsonConvert();

            var jsonStr = "1.1";

            var either = JsonConvert.DeserializeObject<Either<float, string>>(jsonStr);

            Assert.AreEqual(1.1f, either.Value);
        }

        [TestMethod]
        public void TestMethos4()
        {
            InitJsonConvert();

            var jsonStr = "{Either:1.1}";

            var obj = JsonConvert.DeserializeObject<SomethingWithEither<float, string>>(jsonStr);

            Assert.AreEqual(1.1f, obj.Either.Value1);
        }

        [TestMethod]
        public void TestMethos5()
        {
            InitJsonConvert();

            var jsonStr = @"{Either:{Name:""Miguel""}}";

            var obj = JsonConvert.DeserializeObject<SomethingWithEither<SomeClass, int>>(jsonStr);

            Assert.AreEqual("Miguel", obj.Either.Value1.Name);
        }

        [TestMethod]
        public void TestMethos6()
        {
            InitJsonConvert();

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
            InitJsonConvert();

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
            InitJsonConvert();

            var jsonStr = @"{""A"":""a""}";

            var obj = JsonConvert.DeserializeObject<Either<IList<string>, IDictionary<string, string>>>(jsonStr);

            Assert.AreEqual("a", obj.Value2["A"]);
        }

        [TestMethod]
        public void TestMethos9()
        {
            InitJsonConvert();

            var jsonStr = @"[""A"",""B""]";

            var obj = JsonConvert.DeserializeObject<Either<IList<string>, IDictionary<string, string>>>(jsonStr);

            Assert.AreEqual("A", obj.Value1[0]);
            Assert.AreEqual("B", obj.Value1[1]);
        }
    }

    public class SomethingWithEither<T1, T2>
    {
        public Either<T1, T2> Either { get; set; }
    }
}