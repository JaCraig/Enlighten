using Enlighten.Stemmer;
using Enlighten.Stemmer.Enums;
using Enlighten.Stemmer.Interfaces;
using Enlighten.Stemmer.Languages;
using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using Xunit;

namespace Enlighten.Tests.Stemmer
{
    public class DefaultStemmerTests : TestBaseClass
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
            var Result = new DefaultStemmer(new IStemmerLanguage[] { new EnglishLanguage() }).Stem(new DefaultTokenizer(new ITokenizerLanguage[] { new Enlighten.Tokenizer.Languages.English.EnglishLanguage() }).Tokenize("This is a test.", TokenizerLanguage.EnglishRuleBased), StemmerLanguage.EnglishPorter2);
            Assert.Equal("this", Result[0].Value);
            Assert.Equal(" ", Result[1].Value);
            Assert.Equal("is", Result[2].Value);
            Assert.Equal(" ", Result[3].Value);
            Assert.Equal("a", Result[4].Value);
            Assert.Equal(" ", Result[5].Value);
            Assert.Equal("test", Result[6].Value);
            Assert.Equal(".", Result[7].Value);

            Result = new DefaultStemmer(new IStemmerLanguage[] { new EnglishLanguage() }).Stem(new DefaultTokenizer(new ITokenizerLanguage[] { new Enlighten.Tokenizer.Languages.English.EnglishLanguage() }).Tokenize("These are some more tests.", TokenizerLanguage.EnglishRuleBased), StemmerLanguage.EnglishPorter2);
            Assert.Equal("these", Result[0].Value);
            Assert.Equal(" ", Result[1].Value);
            Assert.Equal("are", Result[2].Value);
            Assert.Equal(" ", Result[3].Value);
            Assert.Equal("some", Result[4].Value);
            Assert.Equal(" ", Result[5].Value);
            Assert.Equal("more", Result[6].Value);
            Assert.Equal(" ", Result[7].Value);
            Assert.Equal("test", Result[8].Value);
            Assert.Equal(".", Result[9].Value);
        }
    }
}