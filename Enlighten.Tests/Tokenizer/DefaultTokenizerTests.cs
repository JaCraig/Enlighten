using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using Enlighten.Tokenizer.Languages.English;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Languages.Interfaces;
using Xunit;

namespace Enlighten.Tests.Tokenizer
{
    public class DefaultTokenizerTests : TestBaseClass<DefaultTokenizer>
    {
        [Fact]
        public void Detokenize()
        {
            var Tokenizer = new DefaultTokenizer(new ITokenizerLanguage[] { new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool);
            var Result = Tokenizer.Tokenize("This is a test.", TokenizerLanguage.EnglishRuleBased);
            Assert.Equal("This is a test.", Tokenizer.Detokenize(Result, TokenizerLanguage.EnglishRuleBased));
        }

        [Fact]
        public void Tokenize()
        {
            var Result = new DefaultTokenizer(new ITokenizerLanguage[] { new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool).Tokenize("This is a test.", TokenizerLanguage.EnglishRuleBased);
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
            var Result = new DefaultTokenizer(new ITokenizerLanguage[] { new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool).Tokenize("", TokenizerLanguage.EnglishRuleBased);
            Assert.Single(Result);

            Assert.Equal(0, Result[0].EndPosition);
            Assert.Equal(0, Result[0].StartPosition);
            Assert.Equal(TokenType.EOF, Result[0].TokenType);
            Assert.Equal(string.Empty, Result[0].Value);
        }

        [Fact]
        public void TokenizeLongerText()
        {
            var Result = new DefaultTokenizer(new ITokenizerLanguage[] { new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool).Tokenize(@"""I said, 'what're you? Crazy?'"" said Sandowsky. ""I can't afford to do that.""", TokenizerLanguage.EnglishRuleBased);

            Assert.Equal(37, Result.Length);
        }

        [Fact]
        public void TokenizeNull()
        {
            var Result = new DefaultTokenizer(new ITokenizerLanguage[] { new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool).Tokenize(null, TokenizerLanguage.EnglishRuleBased);
            Assert.Single(Result);

            Assert.Equal(0, Result[0].EndPosition);
            Assert.Equal(0, Result[0].StartPosition);
            Assert.Equal(TokenType.EOF, Result[0].TokenType);
            Assert.Equal(string.Empty, Result[0].Value);
        }
    }
}