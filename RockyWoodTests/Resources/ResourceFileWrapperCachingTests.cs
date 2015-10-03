using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using NUnit.Framework;
using RockyWood.Resources;

namespace RockyWoodTests.Resources
{
    [TestFixture]
    public class ResourceFileWrapperCachingTests
    {
        private ResourceFileWrapper<DummyResource> _wrapper;

        [SetUp]
        public void Setup() => _wrapper = new ResourceFileWrapper<DummyResource>();

        [Test]
        public void GettingTheSameResourceTwice_ReturnsTheCachedValue()
        {
            var value1 = _wrapper.ResourceValue("x");
            var value2 = _wrapper.ResourceValue("x");
            Assert.AreEqual(value1, value2);
        }

        [Test]
        public void GettingTheSameSpecificCultureResourceTwice_ReturnsTheCachedValue()
        {
            var value1 = _wrapper.SpecificCultureResourceValue(CultureInfo.GetCultureInfo("FR-fr"), "x");
            var value2 = _wrapper.SpecificCultureResourceValue(CultureInfo.GetCultureInfo("FR-fr"), "x");
            Assert.AreEqual(value1, value2);
        }

        [Test]
        public void GettingGenericAndSpecificCultureResource_ReturnsDifferentValue()
        {
            var value1 = _wrapper.ResourceValue("x");
            var value2 = _wrapper.SpecificCultureResourceValue(CultureInfo.GetCultureInfo("FR-fr"), "x");
            Assert.AreEqual("12", $"{value1.Value}{value2.Value}");
        }

        private class DummyResource
        {
            public static ResourceManager ResourceManager => new DummyResourceManager();

            class DummyResourceManager : ResourceManager
            {
                private readonly List<string> _responses = new List<string> { "1", "2" };
                private int _responsesIndex = 0;

                public override string GetString(string name) => _responses[_responsesIndex++];

                public override string GetString(string name, CultureInfo culture) => _responses[_responsesIndex++];
            }
        }
    }
}
