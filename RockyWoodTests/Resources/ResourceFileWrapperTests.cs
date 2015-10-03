using System.Globalization;
using NUnit.Framework;
using RockyWood.Resources;

namespace RockyWoodTests.Resources
{
    [TestFixture]
    public class ResourceFileWrapperTests
    {
        private ResourceFileWrapper<Resource1> _wrapper;

        [SetUp]
        public void Setup() => _wrapper = new ResourceFileWrapper<Resource1>();

        [Test]
        public void String1CanBeReadViaWrapper()
        {
            var value = _wrapper.ResourceValue("String1");
            Assert.AreEqual("123", value.Value);
        }

        [Test]
        public void CurrentCultureString1CanBeReadViaWrapper()
        {
            var value = _wrapper.SpecificCultureResourceValue(CultureInfo.CurrentCulture, "String1");
            Assert.AreEqual("123", value.Value);
        }

        [Test]
        public void SpecificCultureString1CanBeReadViaWrapper()
        {
            var value = _wrapper.SpecificCultureResourceValue(CultureInfo.GetCultureInfo("FR-fr"), "String1");
            Assert.AreEqual("un deux trois", value.Value);
        }

        [Test]
        public void NonSupportedCultureString1_ReturnsGenericString()
        {
            var value = _wrapper.SpecificCultureResourceValue(CultureInfo.GetCultureInfo("nl-NL"), "String1");
            Assert.AreEqual("123", value.Value);
        }

        [Test]
        public void MissingSpecificCultureString2_ReturnsGenericString()
        {
            var value = _wrapper.SpecificCultureResourceValue(CultureInfo.GetCultureInfo("FR-fr"), "String2");
            Assert.AreEqual("456", value.Value);
        }

        [Test]
        public void NonExistentString_ReturnsNone()
        {
            var value = _wrapper.ResourceValue("No such string");
            Assert.IsFalse(value.HasValue);
        }
    }
}
