using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Utils;
using System;
using Xunit;

namespace Enlighten.Tests.Tokenizer.Languages.English.TokenFinders
{
    public class HashTagTests : TestBaseClass
    {
        public static TheoryData<string, Token> Data = new TheoryData<string, Token>
        {
            { "This has no HashTag",null },
            { "#something This has HashTag",new Token(9,0,TokenType.HashTag,"#something" ) },
            { "This has no HashTag..",null },
            { "This. has. no. HashTag.",null },
            { ".This. has. no. HashTag.",null },
            { "..This. has. no. HashTag.",null },
            { "#another@a.tr . . . . . . .This has an HashTag.",new Token(7,0,TokenType.HashTag,"#another" ) },
            { "#boop... This has an HashTag.",new Token(4,0,TokenType.HashTag,"#boop" ) },
            {"#SOMETHING_Is_Here This has an HashTag.",new Token(17,0,TokenType.HashTag,"#SOMETHING_Is_Here" ) },
            { " ... This has no HashTag.",null },
            { "",new Token(0,0,TokenType.EOF,string.Empty ) },
            { null,new Token(0,0,TokenType.EOF,string.Empty ) },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void IsMatch(string input, Token expectedValue)
        {
            var Result = new HashTag().IsMatch(new TokenizableStream<char>(input?.ToCharArray() ?? Array.Empty<char>()));
            Assert.Equal(expectedValue?.EndPosition, Result?.EndPosition);
            Assert.Equal(expectedValue?.StartPosition, Result?.StartPosition);
            Assert.Equal(expectedValue?.TokenType, Result?.TokenType);
            Assert.Equal(expectedValue?.Value, Result?.Value);
        }
    }
}