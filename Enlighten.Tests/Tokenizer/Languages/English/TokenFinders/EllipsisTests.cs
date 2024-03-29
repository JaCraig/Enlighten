﻿using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Utils;
using System;
using Xunit;

namespace Enlighten.Tests.Tokenizer.Languages.English.TokenFinders
{
    public class EllipsisTests : TestBaseClass<Ellipsis>
    {
        public static TheoryData<string, Token> Data = new TheoryData<string, Token>
        {
            { "This has no ellipsis",null },
            { "...This has ellipsis",new Token(2,0,TokenType.Ellipsis,"..."  ) },
            { "This has no ellipsis..",null },
            { "This. has. no. ellipsis.",null },
            { ".This. has. no. ellipsis.",null },
            { "..This. has. no. ellipsis.",null },
            { ". . . . . . .This has an ellipsis.",new Token(4,0,TokenType.Ellipsis,". . ." ) },
            { "... This has an ellipsis.",new Token(2,0,TokenType.Ellipsis,"..." ) },
            {"… This has an ellipsis.",new Token(0,0,TokenType.Ellipsis,"…" ) },
            { " ... This has no ellipsis.",null },
            { "",new Token(0,0,TokenType.EOF,string.Empty ) },
            { null,new Token(0,0,TokenType.EOF,string.Empty ) },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void IsMatch(string input, Token expectedValue)
        {
            var Result = new Ellipsis().IsMatch(new TokenizableStream<char>(input?.ToCharArray() ?? Array.Empty<char>()));
            Assert.Equal(expectedValue?.EndPosition, Result?.EndPosition);
            Assert.Equal(expectedValue?.StartPosition, Result?.StartPosition);
            Assert.Equal(expectedValue?.TokenType, Result?.TokenType);
            Assert.Equal(expectedValue?.Value, Result?.Value);
        }
    }
}