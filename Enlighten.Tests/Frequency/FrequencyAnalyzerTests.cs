using Enlighten.Frequency;
using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using FileCurator;
using System.Linq;
using Xunit;

namespace Enlighten.Tests.Frequency
{
    public class FrequencyAnalyzerTests : TestBaseClass
    {
        [Fact]
        public void TheDoorTest()
        {
            string Text = new FileInfo("./Data/TheDoor.txt");
            var Tokenizer = Canister.Builder.Bootstrapper.Resolve<ITokenizer>();
            var Tokens = Tokenizer.Tokenize(Text, TokenizerLanguage.EnglishRuleBased);
            var TempFreq = new FrequencyAnalyzer();
            var Result = TempFreq.Analyze(Tokens, 100);
            var TopTen = Result.WordCount.OrderByDescending(x => x.Value).Take(10).ToArray();
            Assert.Equal("the", TopTen[0].Key);
            Assert.Equal(196, TopTen[0].Value);

            Assert.Equal(2040, Result.NumberOfWords);
            Assert.Equal(565, Result.NumberOfTypes);
            Assert.Equal(0.2769607843137254901960784314m, Result.TypeTokenRatio);
            Assert.Equal(0.6612371134020618556701030928m, Result.AverageTypeTokenRatio);
        }
    }
}