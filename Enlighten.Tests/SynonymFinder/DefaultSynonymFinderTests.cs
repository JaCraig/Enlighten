using Enlighten.SynonymFinder;
using Enlighten.SynonymFinder.Enum;
using Enlighten.SynonymFinder.Finders;
using Enlighten.SynonymFinder.Interfaces;
using Enlighten.Tests.BaseClasses;
using Xunit;

namespace Enlighten.Tests.SynonymFinder
{
    public class DefaultSynonymFinderTests : TestBaseClass
    {
        [Fact]
        public void BasicTest()
        {
            var TestObject = new DefaultSynonymFinder(new ISynonymFinderLanguage[] { new EnglishFinder() });
            Assert.Equal("love", TestObject.FindSynonym("<3", SynonymFinderLanguage.English));
            Assert.Equal("hello", TestObject.FindSynonym("hi", SynonymFinderLanguage.English));
            Assert.Equal("yes", TestObject.FindSynonym("aye", SynonymFinderLanguage.English));
        }
    }
}