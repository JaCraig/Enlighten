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

using BigBook;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.Interfaces;
using Enlighten.Tokenizer.Utils;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.Tokenizer.Languages.English
{
    /// <summary>
    /// English language
    /// </summary>
    /// <seealso cref="ITokenizerLanguage"/>
    public class EnglishLanguage : ITokenizerLanguage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishLanguage"/> class.
        /// </summary>
        public EnglishLanguage(IEnumerable<IEnglishTokenFinder> tokenFinders)
        {
            TokenFinders = tokenFinders.ToArray();
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string ISOCode { get; } = TokenizerLanguage.EnglishRuleBased;

        /// <summary>
        /// Gets the token finders.
        /// </summary>
        /// <value>The token finders.</value>
        public IEnglishTokenFinder[] TokenFinders { get; }

        /// <summary>
        /// The stop words
        /// </summary>
        private string[] StopWords = new string[]
        {
            "a",
"an",
"and",
"are",
"as",
"at",
"be",
"but",
"by",
"for",
"if",
"in",
"into",
"is",
"it",
"no",
"not",
"of",
"on",
"or",
"s",
"such",
"t",
"that",
"the",
"their",
"then",
"there",
"these",
"they",
"this",
"to",
"was",
"will",
"with"
        };

        /// <summary>
        /// Detokenizes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>Converts the tokens into the equivalent string.</returns>
        public string Detokenize(Token[] tokens)
        {
            return tokens.ToString(x => x.Value, string.Empty);
        }

        /// <summary>
        /// Removes the stop words.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The tokens with the stop words removed.</returns>
        public Token[] MarkStopWords(Token[] tokens)
        {
            foreach (var Token in tokens.Where(x => StopWords.Contains(x.Value)))
            {
                Token.StopWord = true;
            }
            return tokens;
        }

        /// <summary>
        /// Tokenizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The tokenized version of the text.</returns>
        public Token[] Tokenize(TokenizableStream<char> text)
        {
            return GetTokens(text, TokenFinders.OrderBy(x => x.Order).ToArray()).ToArray();
        }

        /// <summary>
        /// Gets the next token or null if their isn't one.
        /// </summary>
        /// <param name="tokenizableStream">The tokenizable stream.</param>
        /// <param name="tokenFinders">The token finders.</param>
        /// <returns>The next token.</returns>
        private static Token? Next(TokenizableStream<char> tokenizableStream, IEnglishTokenFinder[] tokenFinders)
        {
            if (tokenizableStream.End())
            {
                return null;
            }

            return tokenFinders.Select(x => x.IsMatch(tokenizableStream)).FirstOrDefault(x => x != null);
        }

        /// <summary>
        /// Gets the tokens.
        /// </summary>
        /// <param name="tokenizableStream">The tokenizable stream.</param>
        /// <param name="tokenFinders">The token finders.</param>
        /// <returns>The tokens.</returns>
        private IEnumerable<Token> GetTokens(TokenizableStream<char> tokenizableStream, IEnglishTokenFinder[] tokenFinders)
        {
            var CurrentToken = Next(tokenizableStream, tokenFinders);
            while (CurrentToken != null)
            {
                yield return CurrentToken;
                CurrentToken = Next(tokenizableStream, tokenFinders);
            }
            yield return new Token(
                tokenizableStream.Index,
                tokenizableStream.Index,
                TokenType.EOF,
                string.Empty
            );
        }
    }
}