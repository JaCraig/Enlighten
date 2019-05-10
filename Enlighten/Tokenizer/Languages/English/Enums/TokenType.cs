/*
Copyright 2019 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using BigBook.Patterns.BaseClasses;

namespace Enlighten.Tokenizer.Languages.English.Enums
{
    /// <summary>
    /// Token type
    /// </summary>
    /// <seealso cref="StringEnumBaseClass{TokenType}"/>
    public class TokenType : StringEnumBaseClass<TokenType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenType"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public TokenType(string value) : base(value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenType"/> class.
        /// </summary>
        public TokenType()
            : this("")
        {
        }

        /// <summary>
        /// Gets the ampersand.
        /// </summary>
        /// <value>The ampersand.</value>
        public static TokenType Ampersand { get; } = new TokenType("Ampersand");

        /// <summary>
        /// Gets the asterisk.
        /// </summary>
        /// <value>The asterisk.</value>
        public static TokenType Asterisk { get; } = new TokenType("Asterisk");

        /// <summary>
        /// Gets at symbol.
        /// </summary>
        /// <value>At symbol.</value>
        public static TokenType AtSymbol { get; } = new TokenType("AtSymbol");

        /// <summary>
        /// Gets the backslash.
        /// </summary>
        /// <value>The backslash.</value>
        public static TokenType Backslash { get; } = new TokenType("Backslash");

        /// <summary>
        /// Gets the backtick.
        /// </summary>
        /// <value>The backtick.</value>
        public static TokenType Backtick { get; } = new TokenType("Backtick");

        /// <summary>
        /// Gets the caret.
        /// </summary>
        /// <value>The caret.</value>
        public static TokenType Caret { get; } = new TokenType("Caret");

        /// <summary>
        /// Gets the colon.
        /// </summary>
        /// <value>The colon.</value>
        public static TokenType Colon { get; } = new TokenType("Colon");

        /// <summary>
        /// Gets the comma.
        /// </summary>
        /// <value>The comma.</value>
        public static TokenType Comma { get; } = new TokenType("Comma");

        /// <summary>
        /// Gets the dollar symbol.
        /// </summary>
        /// <value>The dollar symbol.</value>
        public static TokenType DollarSymbol { get; } = new TokenType("DollarSymbol");

        /// <summary>
        /// Gets the double quote.
        /// </summary>
        /// <value>The double quote.</value>
        public static TokenType DoubleQuote { get; } = new TokenType("DoubleQuote");

        /// <summary>
        /// Gets the ellipsis.
        /// </summary>
        /// <value>The ellipsis.</value>
        public static TokenType Ellipsis { get; } = new TokenType("Ellipsis");

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <value>The email.</value>
        public static TokenType Email { get; } = new TokenType("Email");

        /// <summary>
        /// Gets the emoji.
        /// </summary>
        /// <value>The emoji.</value>
        public static TokenType Emoji { get; } = new TokenType("Emoji");

        /// <summary>
        /// Gets the EOF.
        /// </summary>
        /// <value>The EOF.</value>
        public static TokenType EOF { get; } = new TokenType("EOF");

        /// <summary>
        /// Gets the equals.
        /// </summary>
        /// <value>The equals.</value>
        public static TokenType EqualsToken { get; } = new TokenType("Equals");

        /// <summary>
        /// Gets the exclamation mark.
        /// </summary>
        /// <value>The exclamation mark.</value>
        public static TokenType ExclamationMark { get; } = new TokenType("ExclamationMark");

        /// <summary>
        /// Gets the greater than.
        /// </summary>
        /// <value>The greater than.</value>
        public static TokenType GreaterThan { get; } = new TokenType("GreaterThan");

        /// <summary>
        /// Gets the hash symbol.
        /// </summary>
        /// <value>The hash symbol.</value>
        public static TokenType HashSymbol { get; } = new TokenType("HashSymbol");

        /// <summary>
        /// Gets the hash tag.
        /// </summary>
        /// <value>The hash tag.</value>
        public static TokenType HashTag { get; } = new TokenType("HashTag");

        /// <summary>
        /// Gets the hyphen.
        /// </summary>
        /// <value>The hyphen.</value>
        public static TokenType Hyphen { get; } = new TokenType("Hyphen");

        /// <summary>
        /// Gets the left brace.
        /// </summary>
        /// <value>The left brace.</value>
        public static TokenType LeftBrace { get; } = new TokenType("LeftBrace");

        /// <summary>
        /// Gets the left bracket.
        /// </summary>
        /// <value>The left bracket.</value>
        public static TokenType LeftBracket { get; } = new TokenType("LeftBracket");

        /// <summary>
        /// Gets the left paren.
        /// </summary>
        /// <value>The left paren.</value>
        public static TokenType LeftParen { get; } = new TokenType("LeftParen");

        /// <summary>
        /// Gets the less than.
        /// </summary>
        /// <value>The less than.</value>
        public static TokenType LessThan { get; } = new TokenType("LessThan");

        /// <summary>
        /// Gets the new line.
        /// </summary>
        /// <value>The new line.</value>
        public static TokenType NewLine { get; } = new TokenType("NewLine");

        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <value>The number.</value>
        public static TokenType Number { get; } = new TokenType("Number");

        /// <summary>
        /// Gets the other.
        /// </summary>
        /// <value>The other.</value>
        public static TokenType Other { get; } = new TokenType("Other");

        /// <summary>
        /// Gets the percent symbol.
        /// </summary>
        /// <value>The percent symbol.</value>
        public static TokenType PercentSymbol { get; } = new TokenType("PercentSymbol");

        /// <summary>
        /// Gets the period.
        /// </summary>
        /// <value>The period.</value>
        public static TokenType Period { get; } = new TokenType("Period");

        /// <summary>
        /// Gets the pipe.
        /// </summary>
        /// <value>The pipe.</value>
        public static TokenType Pipe { get; } = new TokenType("Pipe");

        /// <summary>
        /// Gets the plus.
        /// </summary>
        /// <value>The plus.</value>
        public static TokenType Plus { get; } = new TokenType("Plus");

        /// <summary>
        /// Gets the question mark.
        /// </summary>
        /// <value>The question mark.</value>
        public static TokenType QuestionMark { get; } = new TokenType("QuestionMark");

        /// <summary>
        /// Gets the right brace.
        /// </summary>
        /// <value>The right brace.</value>
        public static TokenType RightBrace { get; } = new TokenType("RightBrace");

        /// <summary>
        /// Gets the right bracket.
        /// </summary>
        /// <value>The right bracket.</value>
        public static TokenType RightBracket { get; } = new TokenType("RightBracket");

        /// <summary>
        /// Gets the right paren.
        /// </summary>
        /// <value>The right paren.</value>
        public static TokenType RightParen { get; } = new TokenType("RightParen");

        /// <summary>
        /// Gets the semi colon.
        /// </summary>
        /// <value>The semi colon.</value>
        public static TokenType SemiColon { get; } = new TokenType("SemiColon");

        /// <summary>
        /// Gets the single quote.
        /// </summary>
        /// <value>The single quote.</value>
        public static TokenType SingleQuote { get; } = new TokenType("SingleQuote");

        /// <summary>
        /// Gets the slash.
        /// </summary>
        /// <value>The slash.</value>
        public static TokenType Slash { get; } = new TokenType("Slash");

        /// <summary>
        /// Gets the tilde.
        /// </summary>
        /// <value>The tilde.</value>
        public static TokenType Tilde { get; } = new TokenType("Tilde");

        /// <summary>
        /// Gets the underscore.
        /// </summary>
        /// <value>The underscore.</value>
        public static TokenType Underscore { get; } = new TokenType("Underscore");

        /// <summary>
        /// Gets the URL.
        /// </summary>
        /// <value>The URL.</value>
        public static TokenType Url { get; } = new TokenType("Url");

        /// <summary>
        /// Gets the username.
        /// </summary>
        /// <value>The username.</value>
        public static TokenType Username { get; } = new TokenType("Username");

        /// <summary>
        /// Gets the white space.
        /// </summary>
        /// <value>The white space.</value>
        public static TokenType WhiteSpace { get; } = new TokenType("WhiteSpace");

        /// <summary>
        /// Gets the word.
        /// </summary>
        /// <value>The word.</value>
        public static TokenType Word { get; } = new TokenType("Word");
    }
}