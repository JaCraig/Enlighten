using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Utils;
using System;
using Xunit;

namespace Enlighten.Tests.Tokenizer.Languages.English.TokenFinders
{
    public class WhitespaceTests : TestBaseClass
    {
        public static TheoryData<string, Token> Data = new TheoryData<string, Token>
        {
            { "This has no new line",null },
            { "\r\nThis has new line",null },
            { "This has no new line..",null },
            { "This. has. no. new line.",null },
            { ".This. has. no. new line.",null },
            { "..This. has. no. new line.",null },
            { "\n\n\n\n\n\nThis has an new line.",null },
            { "\n This has an new line.",null },
            { "\r This has an new line.",null },
            { " ... This has no new line.",new Token(0,0,TokenType.WhiteSpace," "," " ) },
            { "\t... This has no new line.",new Token(0,0,TokenType.WhiteSpace,"\t","\t" ) },
            { "\t \t... This has no new line.",new Token(2,0,TokenType.WhiteSpace,"\t \t","\t \t" ) },
            { "",new Token(0,0,TokenType.EOF,string.Empty,string.Empty ) },
            { null,new Token(0,0,TokenType.EOF,string.Empty,string.Empty ) },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void IsMatch(string input, Token expectedValue)
        {
            var Result = new Whitespace().IsMatch(new TokenizableStream<char>(input?.ToCharArray() ?? Array.Empty<char>()));
            Assert.Equal(expectedValue?.EndPosition, Result?.EndPosition);
            Assert.Equal(expectedValue?.StartPosition, Result?.StartPosition);
            Assert.Equal(expectedValue?.TokenType, Result?.TokenType);
            Assert.Equal(expectedValue?.Value, Result?.Value);
        }
    }
}