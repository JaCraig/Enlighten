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
            { ".This. has. no. new line.",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Period,Value="." } },
            { "..This. has. no. new line.",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Period,Value="." } },
            { "\n\n\n\n\n\nThis has an new line.",null },
            { "\n This has an new line.",null },
            { "\r This has an new line.",null },
            { " ... This has no new line.",null },
            { "",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.EOF,Value=string.Empty } },
            { null,new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.EOF,Value=string.Empty } },
            { "&",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Ampersand,Value="&" } },
            { "*",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Asterisk,Value="*" } },
            { "@",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.AtSymbol,Value="@" } },
            { "\\",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Backslash,Value="\\" } },
            { "`",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Backtick,Value="`" } },
            { "^",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Caret,Value="^" } },
            { ":",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Colon,Value=":" } },
            { ",",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Comma,Value="," } },
            { "$",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.DollarSymbol,Value="$" } },
            { "\"",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.DoubleQuote,Value="\"" } },
            { "=",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Equals,Value="=" } },
            { "!",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.ExclamationMark,Value="!" } },
            { ">",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.GreaterThan,Value=">" } },
            { "#",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.HashSymbol,Value="#" } },
            { "-",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Hyphen,Value="-" } },
            { "{",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.LeftBrace,Value="{" } },
            { "[",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.LeftBracket,Value="[" } },
            { "(",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.LeftParen,Value="(" } },
            { "<",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.LessThan,Value="<" } },
            { "%",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.PercentSymbol,Value="%" } },
            { ".",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Period,Value="." } },
            { "|",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Pipe,Value="|" } },
            { "+",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Plus,Value="+" } },
            { "?",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.QuestionMark,Value="?" } },
            { "}",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.RightBrace,Value="}" } },
            { "]",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.RightBracket,Value="]" } },
            { ")",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.RightParen,Value=")" } },
            { ";",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.SemiColon,Value=";" } },
            { "'",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.SingleQuote,Value="'" } },
            { "/",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Slash,Value="/" } },
            { "~",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Tilde,Value="~" } },
            { "_",new Token{EndPosition=0,StartPosition=0,TokenType=TokenType.Underscore,Value="_" } },
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