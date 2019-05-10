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
            { "someone@somewhere.com This has Email",new Token{EndPosition=20,StartPosition=0,TokenType=TokenType.Email,Value="someone@somewhere.com" } },
            { "This has no Email..",null },
            { "This. has. no. Email.",null },
            { ".This. has. no. Email.",null },
            { "..This. has. no. Email.",null },
            { "another@a.tr . . . . . . .This has an Email.",new Token{EndPosition=11,StartPosition=0,TokenType=TokenType.Email,Value="another@a.tr" } },
            { "boop@gmail.com... This has an Email.",new Token{EndPosition=13,StartPosition=0,TokenType=TokenType.Email,Value="boop@gmail.com" } },
            {"BLAH@GmAiL.fr.com This has an Email.",new Token{EndPosition=16,StartPosition=0,TokenType=TokenType.Email,Value="BLAH@GmAiL.fr.com" } },
            { " ... This has no Email.",null },
            { "",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.EOF,Value=string.Empty } },
            { null,new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.EOF,Value=string.Empty } },
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