using System;
using Either_For_JsonNet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Either_Tests.BaseTestClasses
{
    public class JsonNetTestsBase : TestsBase
    {
        private Func<JsonSerializerSettings> originalSettings;

        [TestInitialize]
        public void InitJsonConvert()
        {
            this.originalSettings = JsonConvert.DefaultSettings;
            JsonConvert.DefaultSettings = () =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new EitherJsonConverter());
                return settings;
            };
        }

        [TestCleanup]
        public void TestCleanupJsonNet()
        {
            JsonConvert.DefaultSettings = this.originalSettings;
        }
    }
}