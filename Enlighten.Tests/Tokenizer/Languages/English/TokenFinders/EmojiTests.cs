using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Utils;
using System;
using Xunit;

namespace Enlighten.Tests.Tokenizer.Languages.English.TokenFinders
{
    public class EmojiTests : TestBaseClass<Emoji>
    {
        public static TheoryData<string, Token> Data = new TheoryData<string, Token>
        {
            { "⚾ 100 0",new Token(0,0,TokenType.Emoji,"⚾" ) },
            { "💕\r\nThis has new line",new Token(1,0,TokenType.Emoji,"💕" ) },
            { "💯 1234",new Token(1,0,TokenType.Emoji,"💯" ) },
            { "🏫. 43",new Token(1,0,TokenType.Emoji,"🏫" ) },
            { ".This. has. no. new line.",null },
            { "..This. has. no. new line.",null },
            { "\n\n\n\n\n\nThis has an new line.",null },
            { "\n This has an new line.",null },
            { "\r This has an new line.",null },
            { " ... This has no new line.",null },
            { "",new Token(0,0,TokenType.EOF,string.Empty ) },
            { null,new Token(0,0,TokenType.EOF,string.Empty ) },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void IsMatch(string input, Token expectedValue)
        {
            var Result = new Emoji().IsMatch(new TokenizableStream<char>(input?.ToCharArray() ?? Array.Empty<char>()));
            Assert.Equal(expectedValue?.EndPosition, Result?.EndPosition);
            Assert.Equal(expectedValue?.StartPosition, Result?.StartPosition);
            Assert.Equal(expectedValue?.TokenType, Result?.TokenType);
            Assert.Equal(expectedValue?.Value, Result?.Value);
        }
    }
}