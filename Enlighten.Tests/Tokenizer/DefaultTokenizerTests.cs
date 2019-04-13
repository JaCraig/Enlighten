using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using Enlighten.Tokenizer.Languages.English;
using Enlighten.Tokenizer.Languages.English.Enums;
using Xunit;

namespace Enlighten.Tests.Tokenizer
{
    public class DefaultTokenizerTests : TestBaseClass
    {
        [Fact]
        public void Tokenize()
        {
            var Result = new DefaultTokenizer(new ILanguage[] { new EnglishLanguage() }).Tokenize("This is a test.", Language.EnglishRuleBased);
            Assert.Equal(9, Result.Length);

            Assert.Equal(3, Result[0].EndPosition);
            Assert.Equal(0, Result[0].StartPosition);
            Assert.Equal(TokenType.Word, Result[0].TokenType);
            Assert.Equal("This", Result[0].Value);

            Assert.Equal(4, Result[1].EndPosition);
            Assert.Equal(4, Result[1].StartPosition);
            Assert.Equal(TokenType.WhiteSpace, Result[1].TokenType);
            Assert.Equal(" ", Result[1].Value);

            Assert.Equal(6, Result[2].EndPosition);
            Assert.Equal(5, Result[2].StartPosition);
            Assert.Equal(TokenType.Word, Result[2].TokenType);
            Assert.Equal("is", Result[2].Value);

            Assert.Equal(7, Result[3].EndPosition);
            Assert.Equal(7, Result[3].StartPosition);
            Assert.Equal(TokenType.WhiteSpace, Result[3].TokenType);
            Assert.Equal(" ", Result[3].Value);

            Assert.Equal(8, Result[4].EndPosition);
            Assert.Equal(8, Result[4].StartPosition);
            Assert.Equal(TokenType.Word, Result[4].TokenType);
            Assert.Equal("a", Result[4].Value);

            Assert.Equal(9, Result[5].EndPosition);
            Assert.Equal(9, Result[5].StartPosition);
            Assert.Equal(TokenType.WhiteSpace, Result[5].TokenType);
            Assert.Equal(" ", Result[5].Value);

            Assert.Equal(13, Result[6].EndPosition);
            Assert.Equal(10, Result[6].StartPosition);
            Assert.Equal(TokenType.Word, Result[6].TokenType);
            Assert.Equal("test", Result[6].Value);

            Assert.Equal(14, Result[7].EndPosition);
            Assert.Equal(14, Result[7].StartPosition);
            Assert.Equal(TokenType.Period, Result[7].TokenType);
            Assert.Equal(".", Result[7].Value);

            Assert.Equal(15, Result[8].EndPosition);
            Assert.Equal(15, Result[8].StartPosition);
            Assert.Equal(TokenType.EOF, Result[8].TokenType);
            Assert.Equal(string.Empty, Result[8].Value);
        }

        [Fact]
        public void TokenizeEmptyString()
        {
            var Result = new DefaultTokenizer(new ILanguage[] { new EnglishLanguage() }).Tokenize("", Language.EnglishRuleBased);
            Assert.Single(Result);

            Assert.Equal(0, Result[0].EndPosition);
            Assert.Equal(0, Result[0].StartPosition);
            Assert.Equal(TokenType.EOF, Result[0].TokenType);
            Assert.Equal(string.Empty, Result[0].Value);
        }

        [Fact]
        public void TokenizeNull()
        {
            var Result = new DefaultTokenizer(new ILanguage[] { new EnglishLanguage() }).Tokenize(null, Language.EnglishRuleBased);
            Assert.Single(Result);

            Assert.Equal(0, Result[0].EndPosition);
            Assert.Equal(0, Result[0].StartPosition);
            Assert.Equal(TokenType.EOF, Result[0].TokenType);
            Assert.Equal(string.Empty, Result[0].Value);
        }
    }
}