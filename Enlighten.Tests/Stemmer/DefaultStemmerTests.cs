using Enlighten.Stemmer;
using Enlighten.Stemmer.Enums;
using Enlighten.Stemmer.Interfaces;
using Enlighten.Stemmer.Languages;
using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Languages.Interfaces;
using Xunit;

namespace Enlighten.Tests.Stemmer
{
    public class DefaultStemmerTests : TestBaseClass<DefaultStemmer>
    {
        [Fact]
        public void Stem()
        {
            var Result = new DefaultStemmer(new IStemmerLanguage[] { new EnglishLanguage() }).Stem(new string[] { "This", "is", "a", "test" }, StemmerLanguage.EnglishPorter2);
            Assert.Equal("this", Result[0]);
            Assert.Equal("is", Result[1]);
            Assert.Equal("a", Result[2]);
            Assert.Equal("test", Result[3]);

            Result = new DefaultStemmer(new IStemmerLanguage[] { new EnglishLanguage() }).Stem(new string[] { "These", "are", "some", "more", "tests" }, StemmerLanguage.EnglishPorter2);
            Assert.Equal("these", Result[0]);
            Assert.Equal("are", Result[1]);
            Assert.Equal("some", Result[2]);
            Assert.Equal("more", Result[3]);
            Assert.Equal("test", Result[4]);
        }

        [Fact]
        public void StemTokens()
        {
            var Result = new DefaultStemmer(new IStemmerLanguage[] { new EnglishLanguage() }).Stem(new DefaultTokenizer(new ITokenizerLanguage[] { new Enlighten.Tokenizer.Languages.English.EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool).Tokenize("This is a test.", TokenizerLanguage.EnglishRuleBased), StemmerLanguage.EnglishPorter2);
            Assert.Equal("this", Result[0].StemmedValue);
            Assert.Equal(" ", Result[1].StemmedValue);
            Assert.Equal("is", Result[2].StemmedValue);
            Assert.Equal(" ", Result[3].StemmedValue);
            Assert.Equal("a", Result[4].StemmedValue);
            Assert.Equal(" ", Result[5].StemmedValue);
            Assert.Equal("test", Result[6].StemmedValue);
            Assert.Equal(".", Result[7].StemmedValue);

            Result = new DefaultStemmer(new IStemmerLanguage[] { new EnglishLanguage() }).Stem(new DefaultTokenizer(new ITokenizerLanguage[] { new Enlighten.Tokenizer.Languages.English.EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool).Tokenize("These are some more tests.", TokenizerLanguage.EnglishRuleBased), StemmerLanguage.EnglishPorter2);
            Assert.Equal("these", Result[0].StemmedValue);
            Assert.Equal(" ", Result[1].StemmedValue);
            Assert.Equal("are", Result[2].StemmedValue);
            Assert.Equal(" ", Result[3].StemmedValue);
            Assert.Equal("some", Result[4].StemmedValue);
            Assert.Equal(" ", Result[5].StemmedValue);
            Assert.Equal("more", Result[6].StemmedValue);
            Assert.Equal(" ", Result[7].StemmedValue);
            Assert.Equal("test", Result[8].StemmedValue);
            Assert.Equal(".", Result[9].StemmedValue);
        }
    }
}