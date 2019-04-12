using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Utils;
using System;
using Xunit;

namespace Enlighten.Tests.Tokenizer.Languages.English.TokenFinders
{
    public class NewLineTests : TestBaseClass
    {
        public static TheoryData<string, Token> Data = new TheoryData<string, Token>
        {
            { "This has no new line",null },
            { "\r\nThis has new line",new Token{EndPosition=1,StartPosition=0,TokenType=TokenType.NewLine,Value="\r\n" } },
            { "This has no new line..",null },
            { "This. has. no. new line.",null },
            { ".This. has. no. new line.",null },
            { "..This. has. no. new line.",null },
            { "\n\n\n\n\n\nThis has an new line.",new Token{EndPosition=13,StartPosition=0,TokenType=TokenType.NewLine,Value="\n\n\n\n\n\n" } },
            { "\n This has an new line.",new Token{EndPosition=1,StartPosition=0,TokenType=TokenType.NewLine,Value="\n" } },
            { "\r This has an new line.",new Token{EndPosition=1,StartPosition=0,TokenType=TokenType.NewLine,Value="\r" } },
            { " ... This has no new line.",null },
            { "",null },
            { null,null },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void IsMatch(string input, Token expectedValue)
        {
            var Result = new NewLine().IsMatch(new TokenizableStream<char>(input?.ToCharArray() ?? Array.Empty<char>()));
            Assert.Equal(expectedValue?.EndPosition, Result?.EndPosition);
            Assert.Equal(expectedValue?.StartPosition, Result?.StartPosition);
            Assert.Equal(expectedValue?.TokenType, Result?.TokenType);
            Assert.Equal(expectedValue?.Value, Result?.Value);
        }
    }
}