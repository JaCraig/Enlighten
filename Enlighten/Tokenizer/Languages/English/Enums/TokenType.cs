﻿/*
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
        public static TokenType Ampersand { get { return new TokenType("Ampersand"); } }

        /// <summary>
        /// Gets the asterisk.
        /// </summary>
        /// <value>The asterisk.</value>
        public static TokenType Asterisk { get { return new TokenType("Asterisk"); } }

        /// <summary>
        /// Gets at symbol.
        /// </summary>
        /// <value>At symbol.</value>
        public static TokenType AtSymbol { get { return new TokenType("AtSymbol"); } }

        /// <summary>
        /// Gets the backslash.
        /// </summary>
        /// <value>The backslash.</value>
        public static TokenType Backslash { get { return new TokenType("Backslash"); } }

        /// <summary>
        /// Gets the backtick.
        /// </summary>
        /// <value>The backtick.</value>
        public static TokenType Backtick { get { return new TokenType("Backtick"); } }

        /// <summary>
        /// Gets the caret.
        /// </summary>
        /// <value>The caret.</value>
        public static TokenType Caret { get { return new TokenType("Caret"); } }

        /// <summary>
        /// Gets the colon.
        /// </summary>
        /// <value>The colon.</value>
        public static TokenType Colon { get { return new TokenType("Colon"); } }

        /// <summary>
        /// Gets the comma.
        /// </summary>
        /// <value>The comma.</value>
        public static TokenType Comma { get { return new TokenType("Comma"); } }

        /// <summary>
        /// Gets the dollar symbol.
        /// </summary>
        /// <value>The dollar symbol.</value>
        public static TokenType DollarSymbol { get { return new TokenType("DollarSymbol"); } }

        /// <summary>
        /// Gets the double quote.
        /// </summary>
        /// <value>The double quote.</value>
        public static TokenType DoubleQuote { get { return new TokenType("DoubleQuote"); } }

        /// <summary>
        /// Gets the ellipsis.
        /// </summary>
        /// <value>The ellipsis.</value>
        public static TokenType Ellipsis { get { return new TokenType("Ellipsis"); } }

        /// <summary>
        /// Gets the EOF.
        /// </summary>
        /// <value>The EOF.</value>
        public static TokenType EOF { get { return new TokenType("EOF"); } }

        /// <summary>
        /// Gets the equals.
        /// </summary>
        /// <value>The equals.</value>
        public static TokenType Equals { get { return new TokenType("Equals"); } }

        /// <summary>
        /// Gets the exclamation mark.
        /// </summary>
        /// <value>The exclamation mark.</value>
        public static TokenType ExclamationMark { get { return new TokenType("ExclamationMark"); } }

        /// <summary>
        /// Gets the greater than.
        /// </summary>
        /// <value>The greater than.</value>
        public static TokenType GreaterThan { get { return new TokenType("GreaterThan"); } }

        /// <summary>
        /// Gets the hash symbol.
        /// </summary>
        /// <value>The hash symbol.</value>
        public static TokenType HashSymbol { get { return new TokenType("HashSymbol"); } }

        /// <summary>
        /// Gets the hyphen.
        /// </summary>
        /// <value>The hyphen.</value>
        public static TokenType Hyphen { get { return new TokenType("Hyphen"); } }

        /// <summary>
        /// Gets the left brace.
        /// </summary>
        /// <value>The left brace.</value>
        public static TokenType LeftBrace { get { return new TokenType("LeftBrace"); } }

        /// <summary>
        /// Gets the left bracket.
        /// </summary>
        /// <value>The left bracket.</value>
        public static TokenType LeftBracket { get { return new TokenType("LeftBracket"); } }

        /// <summary>
        /// Gets the left paren.
        /// </summary>
        /// <value>The left paren.</value>
        public static TokenType LeftParen { get { return new TokenType("LeftParen"); } }

        /// <summary>
        /// Gets the less than.
        /// </summary>
        /// <value>The less than.</value>
        public static TokenType LessThan { get { return new TokenType("LessThan"); } }

        /// <summary>
        /// Gets the new line.
        /// </summary>
        /// <value>The new line.</value>
        public static TokenType NewLine { get { return new TokenType("NewLine"); } }

        /// <summary>
        /// Gets the other.
        /// </summary>
        /// <value>The other.</value>
        public static TokenType Other { get { return new TokenType("Other"); } }

        /// <summary>
        /// Gets the percent symbol.
        /// </summary>
        /// <value>The percent symbol.</value>
        public static TokenType PercentSymbol { get { return new TokenType("PercentSymbol"); } }

        /// <summary>
        /// Gets the period.
        /// </summary>
        /// <value>The period.</value>
        public static TokenType Period { get { return new TokenType("Period"); } }

        /// <summary>
        /// Gets the pipe.
        /// </summary>
        /// <value>The pipe.</value>
        public static TokenType Pipe { get { return new TokenType("Pipe"); } }

        /// <summary>
        /// Gets the plus.
        /// </summary>
        /// <value>The plus.</value>
        public static TokenType Plus { get { return new TokenType("Plus"); } }

        /// <summary>
        /// Gets the question mark.
        /// </summary>
        /// <value>The question mark.</value>
        public static TokenType QuestionMark { get { return new TokenType("QuestionMark"); } }

        /// <summary>
        /// Gets the right brace.
        /// </summary>
        /// <value>The right brace.</value>
        public static TokenType RightBrace { get { return new TokenType("RightBrace"); } }

        /// <summary>
        /// Gets the right bracket.
        /// </summary>
        /// <value>The right bracket.</value>
        public static TokenType RightBracket { get { return new TokenType("RightBracket"); } }

        /// <summary>
        /// Gets the right paren.
        /// </summary>
        /// <value>The right paren.</value>
        public static TokenType RightParen { get { return new TokenType("RightParen"); } }

        /// <summary>
        /// Gets the semi colon.
        /// </summary>
        /// <value>The semi colon.</value>
        public static TokenType SemiColon { get { return new TokenType("SemiColon"); } }

        /// <summary>
        /// Gets the single quote.
        /// </summary>
        /// <value>The single quote.</value>
        public static TokenType SingleQuote { get { return new TokenType("SingleQuote"); } }

        /// <summary>
        /// Gets the slash.
        /// </summary>
        /// <value>The slash.</value>
        public static TokenType Slash { get { return new TokenType("Slash"); } }

        /// <summary>
        /// Gets the tilde.
        /// </summary>
        /// <value>The tilde.</value>
        public static TokenType Tilde { get { return new TokenType("Tilde"); } }

        /// <summary>
        /// Gets the underscore.
        /// </summary>
        /// <value>The underscore.</value>
        public static TokenType Underscore { get { return new TokenType("Underscore"); } }

        /// <summary>
        /// Gets the white space.
        /// </summary>
        /// <value>The white space.</value>
        public static TokenType WhiteSpace { get { return new TokenType("WhiteSpace"); } }

        /// <summary>
        /// Gets the word.
        /// </summary>
        /// <value>The word.</value>
        public static TokenType Word { get { return new TokenType("Word"); } }
    }
}