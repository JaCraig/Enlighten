using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer.Languages.English;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Languages.Interfaces;
using Enlighten.Tokenizer.Utils;
using Xunit;

namespace Enlighten.Tests.Tokenizer.Languages.English
{
    public class EnglishLanguageTests : TestBaseClass
    {
        [Fact]
        public void Detokenize()
        {
            var Tokenizer = new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() });
            var Result = Tokenizer.Tokenize(new TokenizableStream<char>("This is a test.".ToCharArray()));
            Assert.Equal("This is a test.", Tokenizer.Detokenize(Result));
        }

        [Fact]
        public void RemoveStopWords()
        {
            var Tokenizer = new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() });
            var Result = Tokenizer.RemoveStopWords(Tokenizer.Tokenize(new TokenizableStream<char>("This is a test.".ToCharArray())));
            Assert.Equal("This   test.", Tokenizer.Detokenize(Result));
        }

        [Fact]
        public void Tokenize()
        {
            var Result = new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }).Tokenize(new TokenizableStream<char>("This is a test.".ToCharArray()));
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
            var Result = new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }).Tokenize(new TokenizableStream<char>("".ToCharArray()));
            Assert.Single(Result);

            Assert.Equal(0, Result[0].EndPosition);
            Assert.Equal(0, Result[0].StartPosition);
            Assert.Equal(TokenType.EOF, Result[0].TokenType);
            Assert.Equal(string.Empty, Result[0].Value);
        }

        [Fact]
        public void TokenizeLongerText()
        {
            var Result = new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }).Tokenize(new TokenizableStream<char>(@"""I said, 'what're you? Crazy?'"" said Sandowsky. ""I can't afford to do that.""".ToCharArray()));

            Assert.Equal(37, Result.Length);
        }

        [Fact]
        public void TweetTokenize()
        {
            var Result = new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol(), new Ellipsis(), new Email(), new Emoji(), new HashTag(), new OtherTokenFinder(), new URL(), new Username() }).Tokenize(new TokenizableStream<char>("@SaintsRow: Check out new the trailer for GAT V http://youtu.be/FSX7niJVlNQ  #GATV".ToCharArray()));

            Assert.Equal(23, Result.Length);

            Assert.Equal(9, Result[0].EndPosition);
            Assert.Equal(0, Result[0].StartPosition);
            Assert.Equal(TokenType.Username, Result[0].TokenType);
            Assert.Equal("@SaintsRow", Result[0].Value);

            Assert.Equal(10, Result[1].EndPosition);
            Assert.Equal(10, Result[1].StartPosition);
            Assert.Equal(TokenType.Colon, Result[1].TokenType);
            Assert.Equal(":", Result[1].Value);

            Assert.Equal(74, Result[19].EndPosition);
            Assert.Equal(48, Result[19].StartPosition);
            Assert.Equal(TokenType.Url, Result[19].TokenType);
            Assert.Equal("http://youtu.be/FSX7niJVlNQ", Result[19].Value);

            Assert.Equal(81, Result[21].EndPosition);
            Assert.Equal(77, Result[21].StartPosition);
            Assert.Equal(TokenType.HashTag, Result[21].TokenType);
            Assert.Equal("#GATV", Result[21].Value);
        }
    }
}