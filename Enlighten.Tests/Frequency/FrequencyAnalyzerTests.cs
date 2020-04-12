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
            var TempFreq = new FrequencyAnalyzer(Tokens, 100);
            var TopTen = TempFreq.WordCount.OrderByDescending(x => x.Value).Take(10).ToArray();
            Assert.Equal("the", TopTen[0].Key);
            Assert.Equal(196, TopTen[0].Value);

            Assert.Equal(1751, TempFreq.NumberOfWords);
            Assert.Equal(453, TempFreq.NumberOfTypes);
            Assert.Equal(0.2587093089663049685893774986m, TempFreq.TypeTokenRatio);
            Assert.Equal(0.620745003028467595396729255m, TempFreq.AverageTypeTokenRatio);
        }
    }
}