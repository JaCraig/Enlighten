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

            Assert.Equal(1750, Result.NumberOfWords);
            Assert.Equal(452, Result.NumberOfTypes);
            Assert.Equal(0.2582857142857142857142857143m, Result.TypeTokenRatio);
            Assert.Equal(0.6204121212121212121212121212m, Result.AverageTypeTokenRatio);
        }
    }
}