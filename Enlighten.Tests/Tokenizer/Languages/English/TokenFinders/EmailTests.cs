using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Utils;
using System;
using Xunit;

namespace Enlighten.Tests.Tokenizer.Languages.English.TokenFinders
{
    public class EmailTests : TestBaseClass
    {
        public static TheoryData<string, Token> Data = new TheoryData<string, Token>
        {
            { "This has no Email",null },
            { "someone@somewhere.com This has Email",new Token(20,0,TokenType.Email,"someone@somewhere.com" ) },
            { "This has no Email..",null },
            { "This. has. no. Email.",null },
            { ".This. has. no. Email.",null },
            { "..This. has. no. Email.",null },
            { "another@a.tr . . . . . . .This has an Email.",new Token(11,0,TokenType.Email,"another@a.tr" ) },
            { "boop@gmail.com... This has an Email.",new Token(13,0,TokenType.Email,"boop@gmail.com" ) },
            {"BLAH@GmAiL.fr.com This has an Email.",new Token(16,0,TokenType.Email,"BLAH@GmAiL.fr.com" ) },
            { " ... This has no Email.",null },
            { "",new Token(0,0,TokenType.EOF,string.Empty ) },
            { null,new Token(0,0,TokenType.EOF,string.Empty ) },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void IsMatch(string input, Token expectedValue)
        {
            var Result = new Email().IsMatch(new TokenizableStream<char>(input?.ToCharArray() ?? Array.Empty<char>()));
            Assert.Equal(expectedValue?.EndPosition, Result?.EndPosition);
            Assert.Equal(expectedValue?.StartPosition, Result?.StartPosition);
            Assert.Equal(expectedValue?.TokenType, Result?.TokenType);
            Assert.Equal(expectedValue?.Value, Result?.Value);
        }
    }
}