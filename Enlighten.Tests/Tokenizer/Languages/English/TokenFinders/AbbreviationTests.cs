using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Utils;
using System;
using Xunit;

namespace Enlighten.Tests.Tokenizer.Languages.English.TokenFinders
{
    public class AbbreviationTests : TestBaseClass
    {
        public static TheoryData<string, Token> Data = new TheoryData<string, Token>
        {
            { "This has no ellipsis",null },
            { "Mr. ... This has ellipsis",new Token(2,0,TokenType.Abbreviation,"Mr.","mr.") },
            { "This has no ellipsis..",null },
            { "This. has. no. ellipsis.",null },
            { ".This. has. no. ellipsis.",null },
            { "..This. has. no. ellipsis.",null },
            { "O.K. . . . . . . .This has an ellipsis.",new Token(3,0,TokenType.Abbreviation,"O.K.","o.k.") },
            { "Dr Strange is kind of cool... This has an ellipsis.",new Token(1,0,TokenType.Abbreviation,"Dr","dr" ) },
            {"appt… This has an ellipsis. My appt is in the morning",new Token(3,0,TokenType.Abbreviation,"appt","appt" ) },
            {"IBM… This has an ellipsis. My appt is in the morning",new Token(2,0,TokenType.Abbreviation,"IBM","ibm" ) },
            { " ... This has no ellipsis.",null },
            { "",new Token(0,0,TokenType.EOF,string.Empty,string.Empty ) },
            { null,new Token(0,0,TokenType.EOF,string.Empty,string.Empty ) },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void IsMatch(string input, Token expectedValue)
        {
            var Result = new Abbreviation().IsMatch(new TokenizableStream<char>(input?.ToCharArray() ?? Array.Empty<char>()));
            Assert.Equal(expectedValue?.EndPosition, Result?.EndPosition);
            Assert.Equal(expectedValue?.StartPosition, Result?.StartPosition);
            Assert.Equal(expectedValue?.TokenType, Result?.TokenType);
            Assert.Equal(expectedValue?.Value, Result?.Value);
        }
    }
}