using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Utils;
using System;
using Xunit;

namespace Enlighten.Tests.Tokenizer.Languages.English.TokenFinders
{
    public class NumberTests : TestBaseClass
    {
        public static TheoryData<string, Token> Data = new TheoryData<string, Token>
        {
            { "200 100 0",new Token{EndPosition=2,StartPosition=0,TokenType=TokenType.Number,Value="200" } },
            { "\r\nThis has new line",null },
            { "2,023.23 1234",new Token{EndPosition=7,StartPosition=0,TokenType=TokenType.Number,Value="2,023.23" } },
            { "1000. 43",new Token{EndPosition=3,StartPosition=0,TokenType=TokenType.Number,Value="1000" } },
            { ".This. has. no. new line.",null },
            { "..This. has. no. new line.",null },
            { "\n\n\n\n\n\nThis has an new line.",null },
            { "\n This has an new line.",null },
            { "\r This has an new line.",null },
            { " ... This has no new line.",null },
            { "",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.EOF,Value=string.Empty } },
            { null,new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.EOF,Value=string.Empty } },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void IsMatch(string input, Token expectedValue)
        {
            var Result = new Number().IsMatch(new TokenizableStream<char>(input?.ToCharArray() ?? Array.Empty<char>()));
            Assert.Equal(expectedValue?.EndPosition, Result?.EndPosition);
            Assert.Equal(expectedValue?.StartPosition, Result?.StartPosition);
            Assert.Equal(expectedValue?.TokenType, Result?.TokenType);
            Assert.Equal(expectedValue?.Value, Result?.Value);
        }
    }
}