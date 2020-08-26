using Enlighten.Frequency;
using Enlighten.Tests.BaseClasses;
using Enlighten.TextSummarization;
using Enlighten.TextSummarization.Enum;
using Enlighten.TextSummarization.Interfaces;
using Enlighten.TextSummarization.Languages;
using FileCurator;
using Xunit;

namespace Enlighten.Tests.TextSummarization
{
    public class DefaultTextSummarizerTests : TestBaseClass
    {
        [Fact]
        public void NewsArticleTest()
        {
            string Text = new FileInfo("./Data/MotherJonesArticle.txt");
            var Pipeline = Canister.Builder.Bootstrapper.Resolve<Pipeline>();
            var TestObject = new DefaultTextSummarizer(new ITextSummarizerLanguage[] { new EnglishDefault(Canister.Builder.Bootstrapper.Resolve<FrequencyAnalyzer>()) });
            var Result = TestObject.Summarize(Pipeline.Process(Text), 5, TextSummarizationLanguage.EnglishDefault);
            Assert.Equal("Residents of New York City, the nation’s coronavirus hotspot, will soon be able to walk, run, skate, and bike on the city’s already vacant streets. Earlier in the month, the mayor’s pilot program to close a mere mile and a half of streets failed because, according to de Blasio, there were not enough NYPD officers to enforce it. After Oakland launched a program to close nearly 10 percent of its streets to cars with little police enforcement, de Blasio insisted the approach would never work in New York, saying, “We are just profoundly different than those other cities. That same sense of New York exceptionalism is what caused the governor and mayor alike to throw up their hands and blame the city’s staggering coronavirus death toll on population density, rather than their own failure to shut things down sooner. Here’s hoping that the specific street closures, when announced, actually benefit the crowded and low-income neighborhoods that need them most.", Result.Detokenize());
        }

        [Fact]
        public void TheDoor25PercentTest()
        {
            string Text = new FileInfo("./Data/TheDoor.txt");
            var Pipeline = Canister.Builder.Bootstrapper.Resolve<Pipeline>();
            var TestObject = new DefaultTextSummarizer(new ITextSummarizerLanguage[] { new EnglishDefault(Canister.Builder.Bootstrapper.Resolve<FrequencyAnalyzer>()) });
            var Result = TestObject.Summarize(Pipeline.Process(Text), .25f, TextSummarizationLanguage.EnglishDefault);
            Assert.Equal($"And everybody is always somewhere else. The names were tex and frequently koid. Or they were flex and oid or they were duroid sand or flexsan duro, but everything was glass but not quite glass and the thing that you touched the surface, washable, crease-resistant was rubber, only it wasn't quite rubber and you didn't quite touch it but almost. The wall, which was glass but turned out on being approached not to be a wall, it was something else, it was an opening or doorway--and the doorway through which he saw himself approaching turned out to be something else, it was a wall. It came from a place in the base of the wall or stat where the flue carrying the filterable air was, and not far from the Minipiano, which was made of the same material nailbrushes are made of, and which was under the stairs. This, too, has been tested, she said, pointing, but not at it, and found viable. And you better believe it will be. Or the one with the photostatic copy of the check for thirty-two dollars and fifty cents. It is not till later that the exhaustion sets in. There will be no not-jumping. First, of course, there were the preliminary bouts, the convulsions, and the calm and the willingness. I call it and it comes in sheets, something like insulating board, unattainable and ugli-proof. And there was the man out in Jersey, because I keep thinking about his terrible necessity and the passion and trouble he had gone to all those years in the indescribable abundance of a householder's detail, building the estate and the planting of the trees and in spring the lawn-dressing and in fall the bulbs for the spring burgeoning, and the watering of the grass on the long light evenings in summer and the gravel for the driveway all had to be thought out, planned and the decorative borders, probably, the perennials and the bug spray, and the building of the house from plans of the architect, first the sills, then the studs, then the full corn in the ear, the floors laid on the floor timbers, smoothed, and then the carpets upon the smooth floors and the curtains and the rods therefor. Is it something you read in the paper, perhaps? They can stand just so much, em, Doctor? And what is that, pray, that you have in your hand? Still, you never can tell, em, Madam?{System.Environment.NewLine}He crossed carefully the room, the thick carpet under him softly, and went toward the door carefully, which was glass and he could see himself in it, and which, at his approach, opened to allow him to pass through; and beyond he half expected to find one of the old doors that he had known, perhaps the one with the circle, the one with the girl her arms outstretched in loveliness and beauty before him. As he stepped off, the ground came up slightly, to meet his foot.", Result.Detokenize());
        }

        [Fact]
        public void TheDoorTest()
        {
            string Text = new FileInfo("./Data/TheDoor.txt");
            var Pipeline = Canister.Builder.Bootstrapper.Resolve<Pipeline>();
            var TestObject = new DefaultTextSummarizer(new ITextSummarizerLanguage[] { new EnglishDefault(Canister.Builder.Bootstrapper.Resolve<FrequencyAnalyzer>()) });
            var Result = TestObject.Summarize(Pipeline.Process(Text), 5, TextSummarizationLanguage.EnglishDefault);
            var Value = Result.Detokenize();
            Assert.Equal("It came from a place in the base of the wall or stat where the flue carrying the filterable air was, and not far from the Minipiano, which was made of the same material nailbrushes are made of, and which was under the stairs. And you better believe it will be. Or the one with the photostatic copy of the check for thirty-two dollars and fifty cents. There will be no not-jumping. And there was the man out in Jersey, because I keep thinking about his terrible necessity and the passion and trouble he had gone to all those years in the indescribable abundance of a householder's detail, building the estate and the planting of the trees and in spring the lawn-dressing and in fall the bulbs for the spring burgeoning, and the watering of the grass on the long light evenings in summer and the gravel for the driveway all had to be thought out, planned and the decorative borders, probably, the perennials and the bug spray, and the building of the house from plans of the architect, first the sills, then the studs, then the full corn in the ear, the floors laid on the floor timbers, smoothed, and then the carpets upon the smooth floors and the curtains and the rods therefor.", Result.Detokenize());
        }
    }
}