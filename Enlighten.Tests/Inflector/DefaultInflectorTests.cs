using Enlighten.Inflector;
using Enlighten.Inflector.Enums;
using Enlighten.Inflector.Interfaces;
using Enlighten.Tests.BaseClasses;
using Xunit;

namespace Enlighten.Tests.Inflector
{
    public class DefaultInflectorTests : TestBaseClass
    {
        [Fact]
        public void IsPlural()
        {
            var TestObject = new DefaultInflector(new IInflectorLanguage[] { new EnglishInflector() });
            Assert.True(TestObject.IsPlural("Tests", InflectorLanguage.English));
            Assert.True(TestObject.IsPlural("bees", InflectorLanguage.English));
            Assert.False(TestObject.IsPlural("book", InflectorLanguage.English));
        }

        [Fact]
        public void IsSingular()
        {
            var TestObject = new DefaultInflector(new IInflectorLanguage[] { new EnglishInflector() });
            Assert.False(TestObject.IsSingular("Tests", InflectorLanguage.English));
            Assert.False(TestObject.IsSingular("bees", InflectorLanguage.English));
            Assert.True(TestObject.IsSingular("book", InflectorLanguage.English));
        }

        [Fact]
        public void Pluralize()
        {
            var TestObject = new DefaultInflector(new IInflectorLanguage[] { new EnglishInflector() });
            Assert.Equal("Tests", TestObject.Pluralize("Tests", InflectorLanguage.English));
            Assert.Equal("bees", TestObject.Pluralize("bees", InflectorLanguage.English));
            Assert.Equal("books", TestObject.Pluralize("book", InflectorLanguage.English));
        }

        [Fact]
        public void Singularize()
        {
            var TestObject = new DefaultInflector(new IInflectorLanguage[] { new EnglishInflector() });
            Assert.Equal("Test", TestObject.Singularize("Tests", InflectorLanguage.English));
            Assert.Equal("bee", TestObject.Singularize("bees", InflectorLanguage.English));
            Assert.Equal("book", TestObject.Singularize("book", InflectorLanguage.English));
        }

        [Fact]
        public void ToGerund()
        {
            var TestObject = new DefaultInflector(new IInflectorLanguage[] { new EnglishInflector() });
            Assert.Equal("working", TestObject.ToGerund("work", InflectorLanguage.English));
        }

        [Fact]
        public void ToPast()
        {
            var TestObject = new DefaultInflector(new IInflectorLanguage[] { new EnglishInflector() });
            Assert.Equal("worked", TestObject.ToPast("work", InflectorLanguage.English));
        }

        [Fact]
        public void ToPresent()
        {
            var TestObject = new DefaultInflector(new IInflectorLanguage[] { new EnglishInflector() });
            Assert.Equal("works", TestObject.ToPresent("work", InflectorLanguage.English));
        }
    }
}