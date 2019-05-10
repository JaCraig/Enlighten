﻿using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Utils;
using System;
using Xunit;

namespace Enlighten.Tests.Tokenizer.Languages.English.TokenFinders
{
    public class UsernameTests : TestBaseClass
    {
        public static TheoryData<string, Token> Data = new TheoryData<string, Token>
        {
            { "This has no Username",null },
            { "@something This has Username",new Token{EndPosition=9,StartPosition=0,TokenType=TokenType.Username,Value="@something" } },
            { "This has no Username..",null },
            { "This. has. no. Username.",null },
            { ".This. has. no. Username.",null },
            { "..This. has. no. Username.",null },
            { "@another@a.tr . . . . . . .This has an Username.",new Token{EndPosition=7,StartPosition=0,TokenType=TokenType.Username,Value="@another" } },
            { "@boop... This has an Username.",new Token{EndPosition=4,StartPosition=0,TokenType=TokenType.Username,Value="@boop" } },
            { "@SOMETHING_Is_Here This has an Username.",new Token{EndPosition=17,StartPosition=0,TokenType=TokenType.Username,Value="@SOMETHING_Is_Here" } },
            { " ... This has no Username.",null },
            { "",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.EOF,Value=string.Empty } },
            { null,new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.EOF,Value=string.Empty } },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void IsMatch(string input, Token expectedValue)
        {
            var Result = new Username().IsMatch(new TokenizableStream<char>(input?.ToCharArray() ?? Array.Empty<char>()));
            Assert.Equal(expectedValue?.EndPosition, Result?.EndPosition);
            Assert.Equal(expectedValue?.StartPosition, Result?.StartPosition);
            Assert.Equal(expectedValue?.TokenType, Result?.TokenType);
            Assert.Equal(expectedValue?.Value, Result?.Value);
        }
    }
}