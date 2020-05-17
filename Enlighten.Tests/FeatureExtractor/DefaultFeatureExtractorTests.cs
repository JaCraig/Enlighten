using Enlighten.FeatureExtraction;
using Enlighten.FeatureExtraction.Enum;
using Enlighten.FeatureExtraction.Extractors;
using Enlighten.FeatureExtraction.Interfaces;
using Enlighten.Frequency;
using Enlighten.Tests.BaseClasses;
using FileCurator;
using Xunit;

namespace Enlighten.Tests.FeatureExtractor
{
    public class DefaultFeatureExtractorTests : TestBaseClass
    {
        [Fact]
        public void NewsArticleTest()
        {
            var Pipeline = Canister.Builder.Bootstrapper.Resolve<Pipeline>();
            Document[] Docs = new Document[3];
            Docs[0] = Pipeline.Process(new FileInfo("./Data/TheDoor.txt"));
            Docs[1] = Pipeline.Process(new FileInfo("./Data/DailyMailArticle.txt"));
            Docs[2] = Pipeline.Process(new FileInfo("./Data/Birches.txt"));
            Document Text = Pipeline.Process(new FileInfo("./Data/MotherJonesArticle.txt"));
            var TestObject = new DefaultFeatureExtractor(new IFeatureExtractorLanguage[] { new EnglishDefault(Canister.Builder.Bootstrapper.Resolve<FrequencyAnalyzer>()) });
            var Results = TestObject.Extract(Text, Docs, 5, FeatureExtractionType.EnglishDefault);
            Assert.Equal("New", Results[0]);
            Assert.Equal("York", Results[1]);
            Assert.Equal("Mayor", Results[2]);
            Assert.Equal("de", Results[3]);
            Assert.Equal("Blasio", Results[4]);
        }
    }
}