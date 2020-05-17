using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Utils;
using System;
using Xunit;

namespace Enlighten.Tests.Tokenizer.Languages.English.TokenFinders
{
    public class SymbolTests : TestBaseClass
    {
        public static TheoryData<string, Token> Data = new TheoryData<string, Token>
        {
            { "This has no new line",null },
            { "\r\nThis has new line",null },
            { "This has no new line..",null },
            { "This. has. no. new line.",null },
            { ".This. has. no. new line.",new Token(0,0,TokenType.Period,"." ) },
            { "..This. has. no. new line.",new Token(0,0,TokenType.Period,"." ) },
            { "\n\n\n\n\n\nThis has an new line.",null },
            { "\n This has an new line.",null },
            { "\r This has an new line.",null },
            { " ... This has no new line.",null },
            { "",new Token(0,0,TokenType.EOF,string.Empty ) },
            { null,new Token(0,0,TokenType.EOF,string.Empty ) },
            { "&",new Token(0,0,TokenType.Ampersand,"&") },
            { "*",new Token(0,0,TokenType.Asterisk,"*" ) },
            { "@",new Token(0,0,TokenType.AtSymbol,"@" ) },
            { "\\",new Token(0,0,TokenType.Backslash,"\\" ) },
            { "`",new Token(0,0,TokenType.Backtick,"`" ) },
            { "^",new Token(0,0,TokenType.Caret,"^" ) },
            { ":",new Token(0,0,TokenType.Colon,":" ) },
            { ",",new Token(0,0,TokenType.Comma,"," ) },
            { "$",new Token(0,0,TokenType.DollarSymbol,"$" ) },
            { "\"",new Token(0,0,TokenType.DoubleQuote,"\"" ) },
            { "=",new Token(0,0,TokenType.EqualsToken,"=" ) },
            { "!",new Token(0,0,TokenType.ExclamationMark,"!" ) },
            { ">",new Token(0,0,TokenType.GreaterThan,">" ) },
            { "#",new Token(0,0,TokenType.HashSymbol,"#" ) },
            { "-",new Token(0,0,TokenType.Hyphen,"-" ) },
            { "{",new Token(0,0,TokenType.LeftBrace,"{" ) },
            { "[",new Token(0,0,TokenType.LeftBracket,"[" ) },
            { "(",new Token(0,0,TokenType.LeftParen,"(" ) },
            { "<",new Token(0,0,TokenType.LessThan,"<" ) },
            { "%",new Token(0,0,TokenType.PercentSymbol,"%" ) },
            { ".",new Token(0,0,TokenType.Period,"." ) },
            { "|",new Token(0,0,TokenType.Pipe,"|" ) },
            { "+",new Token(0,0,TokenType.Plus,"+" ) },
            { "?",new Token(0,0,TokenType.QuestionMark,"?" ) },
            { "}",new Token(0,0,TokenType.RightBrace,"}" ) },
            { "]",new Token(0,0,TokenType.RightBracket,"]" ) },
            { ")",new Token(0,0,TokenType.RightParen,")" ) },
            { ";",new Token(0,0,TokenType.SemiColon,";" ) },
            { "'",new Token(0,0,TokenType.SingleQuote,"'" ) },
            { "/",new Token(0,0,TokenType.Slash,"/" ) },
            { "~",new Token(0,0,TokenType.Tilde,"~" ) },
            { "_",new Token(0,0,TokenType.Underscore,"_" ) },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void IsMatch(string input, Token expectedValue)
        {
            var Result = new Symbol().IsMatch(new TokenizableStream<char>(input?.ToCharArray() ?? Array.Empty<char>()));
            Assert.Equal(expectedValue?.EndPosition, Result?.EndPosition);
            Assert.Equal(expectedValue?.StartPosition, Result?.StartPosition);
            Assert.Equal(expectedValue?.TokenType, Result?.TokenType);
            Assert.Equal(expectedValue?.Value, Result?.Value);
        }
    }
}