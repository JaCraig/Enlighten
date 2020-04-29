using Enlighten.FeatureExtraction;
using Enlighten.FeatureExtraction.Enum;
using Enlighten.FeatureExtraction.Extractors;
using Enlighten.FeatureExtraction.Interfaces;
using Enlighten.Frequency;
using Enlighten.Stemmer.Interfaces;
using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer.Interfaces;
using FileCurator;
using Xunit;

namespace Enlighten.Tests.FeatureExtractor
{
    public class DefaultFeatureExtractorTests : TestBaseClass
    {
        [Fact]
        public void NewsArticleTest()
        {
            string[] Docs = new string[3];
            Docs[0] = new FileInfo("./Data/TheDoor.txt");
            Docs[1] = new FileInfo("./Data/DailyMailArticle.txt");
            Docs[2] = new FileInfo("./Data/Birches.txt");
            string Text = new FileInfo("./Data/MotherJonesArticle.txt");
            var TestObject = new DefaultFeatureExtractor(new IFeatureExtractorLanguage[] { new EnglishDefault(Canister.Builder.Bootstrapper.Resolve<ITokenizer>(), Canister.Builder.Bootstrapper.Resolve<IStemmer>(), Canister.Builder.Bootstrapper.Resolve<FrequencyAnalyzer>()) });
            var Results = TestObject.Extract(Text, Docs, 5, FeatureExtractionType.EnglishDefault);
            Assert.Equal("A", Results[0]);
            Assert.Equal("A", Results[1]);
            Assert.Equal("A", Results[2]);
            Assert.Equal("A", Results[3]);
            Assert.Equal("A", Results[4]);
            Assert.Equal("A", Results[5]);
        }
    }
}