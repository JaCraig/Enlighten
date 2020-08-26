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
            Assert.Equal("Residents of New York City, the nation’s coronavirus hotspot, will soon be able to walk, run, skate, and bike on the city’s already vacant streets. Earlier in the month, the mayor’s pilot program to close a mere mile and a half of streets failed because, according to de Blasio, there were not enough NYPD officers to enforce it. After Oakland launched a program to close nearly 10 percent of its streets to cars with little police enforcement, de Blasio insisted the approach would never work in New York, saying, “We are just profoundly different than those other cities. That same sense of New York exceptionalism is what caused the governor and mayor alike to throw up their hands and blame the city’s staggering coronavirus death toll on population density, rather than their own failure to shut things down sooner. Here’s hoping that the specific street closures, when announced, actually benefit the crowded and low-income neighborhoods that need them most.", Result.Detokenize());
        }

        [Fact]
        public void TheDoorTest()
        {
            string Text = new FileInfo("./Data/TheDoor.txt");
            var Pipeline = Canister.Builder.Bootstrapper.Resolve<Pipeline>();
            var TestObject = new EnglishDefault(Canister.Builder.Bootstrapper.Resolve<FrequencyAnalyzer>());
            var Result = TestObject.Summarize(Pipeline.Process(Text), 5);
            Assert.Equal("It came from a place in the base of the wall or stat where the flue carrying the filterable air was, and not far from the Minipiano, which was made of the same material nailbrushes are made of, and which was under the stairs. And you better believe it will be. Or the one with the photostatic copy of the check for thirty-two dollars and fifty cents. There will be no not-jumping. And there was the man out in Jersey, because I keep thinking about his terrible necessity and the passion and trouble he had gone to all those years in the indescribable abundance of a householder's detail, building the estate and the planting of the trees and in spring the lawn-dressing and in fall the bulbs for the spring burgeoning, and the watering of the grass on the long light evenings in summer and the gravel for the driveway all had to be thought out, planned and the decorative borders, probably, the perennials and the bug spray, and the building of the house from plans of the architect, first the sills, then the studs, then the full corn in the ear, the floors laid on the floor timbers, smoothed, and then the carpets upon the smooth floors and the curtains and the rods therefor.", Result.Detokenize());
        }
    }
}