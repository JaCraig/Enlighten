using Enlighten.Frequency;
using Enlighten.SentenceDetection.Interfaces;
using Enlighten.Stemmer.Interfaces;
using Enlighten.Tests.BaseClasses;
using Enlighten.TextSummarization.Languages;
using Enlighten.Tokenizer.Interfaces;
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
            var TestObject = new EnglishDefault(Canister.Builder.Bootstrapper.Resolve<ITokenizer>(), Canister.Builder.Bootstrapper.Resolve<IStemmer>(), Canister.Builder.Bootstrapper.Resolve<ISentenceDetector>(), Canister.Builder.Bootstrapper.Resolve<FrequencyAnalyzer>());
            var Result = TestObject.Summarize(Text, 5);
            Assert.Equal("Mayor Bill de Blasio announced Monday that he was committed to closing 40 miles of streets to cars in May, with the goal of 100 total miles of street closures, to allow New Yorkers more space to socially distance outside. Earlier in the month, the mayor’s pilot program to close a mere mile and a half of streets failed because, according to de Blasio, there were not enough NYPD officers to enforce it. After Oakland launched a program to close nearly 10 percent of its streets to cars with little police enforcement, de Blasio insisted the approach would never work in New York, saying, “We are just profoundly different than those other cities. That same sense of New York exceptionalism is what caused the governor and mayor alike to throw up their hands and blame the city’s staggering coronavirus death toll on population density, rather than their own failure to shut things down sooner. The street closures, mostly located in and around parks, will allow much-needed space for outdoor recreation in a city where miles of sidewalks are too narrow for social distancing.", Result);
        }

        [Fact]
        public void TheDoorTest()
        {
            string Text = new FileInfo("./Data/TheDoor.txt");
            var TestObject = new EnglishDefault(Canister.Builder.Bootstrapper.Resolve<ITokenizer>(), Canister.Builder.Bootstrapper.Resolve<IStemmer>(), Canister.Builder.Bootstrapper.Resolve<ISentenceDetector>(), Canister.Builder.Bootstrapper.Resolve<FrequencyAnalyzer>());
            var Result = TestObject.Summarize(Text, 5);
            Assert.Equal("He meant the rats that had been trained to jump at the square card with the circle in the middle, and the card because it was something it wasn't would give way and let the rat into a place where the food was, but then one day it would be a trick played on the rat, and the card would be changed, and the rat would jump but the card wouldn't give way, and it was an impossible situation for a rat and the rat would go insane and into its eyes would come the unspeakably bright imploring look of the frustrated, and after the convulsions were over and the frantic racing around, then the passive stage would set in and the willingness to let anything be done to it, even if it was something else. And it wouldn't be so bad if only you could read a sentence all the way through without jumping your eye to something else on the same page; and then he kept thinking there was that man out in Jersey, the one who started to chop his trees down, one by one, the man who began talking about how he would take his house to pieces, brick by brick, because he faced a problem incapable of solution, probably, so he began to hack at the trees in the yard, began to pluck with trembling fingers at the bricks in the house. I remember the door with the picture of the girl on it only it was spring, her arms outstretched in loveliness, her dress it was the one with the circle on it uncaught, beginning the slow, clear, blinding cascade-and I guess we would all like to try that door again, for it seemed like the way and for a while it was the way, the door would open and you would go through winged and exalted like any rat and the food would be there, the way the Professor had it arranged, everything O.K., and you had chosen the right door for the world was young. And there was the man out in Jersey, because I keep thinking about his terrible necessity and the passion and trouble he had gone to all those years in the indescribable abundance of a householder's detail, building the estate and the planting of the trees and in spring the lawn-dressing and in fall the bulbs for the spring burgeoning, and the watering of the grass on the long light evenings in summer and the gravel for the driveway all had to be thought out, planned and the decorative borders, probably, the perennials and the bug spray, and the building of the house from plans of the architect, first the sills, then the studs, then the full corn in the ear, the floors laid on the floor timbers, smoothed, and then the carpets upon the smooth floors and the curtains and the rods therefor. He crossed carefully the room, the thick carpet under him softly, and went toward the door carefully, which was glass and he could see himself in it, and which, at his approach, opened to allow him to pass through; and beyond he half expected to find one of the old doors that he had known, perhaps the one with the circle, the one with the girl her arms outstretched in loveliness and beauty before him.", Result);
        }
    }
}