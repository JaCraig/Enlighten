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

using Enlighten.Tokenizer.BaseClasses;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Utils;
using System.Collections.Generic;

namespace Enlighten.Tokenizer.Languages.English.TokenFinders
{
    public class Symbol : TokenFinderBaseClass
    {
        /// <summary>
        /// The symbols
        /// </summary>
        private readonly Dictionary<char, TokenType> Symbols = new Dictionary<char, TokenType>
        {
            ['&'] = TokenType.Ampersand,
            ['*'] = TokenType.Asterisk,
            ['@'] = TokenType.AtSymbol,
            ['\\'] = TokenType.Backslash,
            ['`'] = TokenType.Backtick,
            ['^'] = TokenType.Caret,
            [':'] = TokenType.Colon,
            [','] = TokenType.Comma,
            ['$'] = TokenType.DollarSymbol,
            ['"'] = TokenType.DoubleQuote,
            ['='] = TokenType.EqualsToken,
            ['!'] = TokenType.ExclamationMark,
            ['>'] = TokenType.GreaterThan,
            ['#'] = TokenType.HashSymbol,
            ['-'] = TokenType.Hyphen,
            ['{'] = TokenType.LeftBrace,
            ['['] = TokenType.LeftBracket,
            ['('] = TokenType.LeftParen,
            ['<'] = TokenType.LessThan,
            ['%'] = TokenType.PercentSymbol,
            ['.'] = TokenType.Period,
            ['|'] = TokenType.Pipe,
            ['+'] = TokenType.Plus,
            ['?'] = TokenType.QuestionMark,
            ['}'] = TokenType.RightBrace,
            [']'] = TokenType.RightBracket,
            [')'] = TokenType.RightParen,
            [';'] = TokenType.SemiColon,
            ['\''] = TokenType.SingleQuote,
            ['/'] = TokenType.Slash,
            ['~'] = TokenType.Tilde,
            ['_'] = TokenType.Underscore
        };

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public override int Order { get; } = 3;

        /// <summary>
        /// Determines whether [is match implementation] [the specified tokenizer].
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns></returns>
        protected override Token? IsMatchImpl(TokenizableStream<char> tokenizer)
        {
            if (tokenizer.End() || !Symbols.ContainsKey(tokenizer.Current))
                return null;

            var StartPosition = tokenizer.Index;
            var Value = tokenizer.Current;

            tokenizer.Consume();

            var EndPosition = tokenizer.Index - 1;

            var Result = new string(new char[] { Value });

            return new Token(
                EndPosition,
                StartPosition,
                Symbols[Value],
                Result
            )
            {
                ReplacementValue = "<SYM>"
            };
        }
    }
}