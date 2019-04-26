using BigBook;
using Enlighten.POSTagger.Taggers;
using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Languages.English;
using Xunit;

namespace Enlighten.Tests.POSTagger.Taggers
{
    public class BrillTaggerTests : TestBaseClass
    {
        [Fact]
        public void Setup()
        {
            var TestObject = new BrillTagger();
            Assert.Equal(98823, TestObject.Lexicon.Keys.Count);
        }

        [Fact]
        public void Tag()
        {
            var TestObject = new BrillTagger();
            var Tokenizer = new DefaultTokenizer(new[] { new EnglishLanguage() });
            var Results = TestObject.Tag(Tokenizer.Tokenize("I would go buy a computer.", TokenizerLanguage.EnglishRuleBased));
            Assert.Equal("PRP MD VB VB DT NN", Results.ToString(x => x.PartOfSpeech, " "));
        }
    }
}