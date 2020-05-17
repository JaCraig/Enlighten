using Enlighten.Frequency;
using Enlighten.Tests.BaseClasses;
using Enlighten.TextSummarization.Languages;
using FileCurator;
using Xunit;

namespace Enlighten.Tests.TextSummarization.Summarizers
{
    public class EnglishDefaultTests : TestBaseClass
    {
        [Fact]
        public void NewsArticleTest()
        {
            string Text = new FileInfo("./Data/MotherJonesArticle.txt");
            var Pipeline = Canister.Builder.Bootstrapper.Resolve<Pipeline>();
            var TestObject = new EnglishDefault(Canister.Builder.Bootstrapper.Resolve<FrequencyAnalyzer>());
            var Result = TestObject.Summarize(Pipeline.Process(Text), 5);
            Assert.Equal("Earlier in the month, the mayor’s pilot program to close a mere mile and a half of streets failed because, according to de Blasio, there were not enough NYPD officers to enforce it. After Oakland launched a program to close nearly 10 percent of its streets to cars with little police enforcement, de Blasio insisted the approach would never work in New York, saying, “We are just profoundly different than those other cities. That same sense of New York exceptionalism is what caused the governor and mayor alike to throw up their hands and blame the city’s staggering coronavirus death toll on population density, rather than their own failure to shut things down sooner. The street closures, mostly located in and around parks, will allow much-needed space for outdoor recreation in a city where miles of sidewalks are too narrow for social distancing. Here’s hoping that the specific street closures, when announced, actually benefit the crowded and low-income neighborhoods that need them most.", Result.Detokenize());
        }

        [Fact]
        public void TheDoorTest()
        {
            string Text = new FileInfo("./Data/TheDoor.txt");
            var Pipeline = Canister.Builder.Bootstrapper.Resolve<Pipeline>();
            var TestObject = new EnglishDefault(Canister.Builder.Bootstrapper.Resolve<FrequencyAnalyzer>());
            var Result = TestObject.Summarize(Pipeline.Process(Text), 5);
            Assert.Equal("And what he had eaten not having agreed with him. This, too, has been tested, she said, pointing, but not at it, and found viable. And you better believe it will be. Or the one with the photostatic copy of the check for thirty-two dollars and fifty cents. There will be no not-jumping.", Result.Detokenize());
        }
    }
}