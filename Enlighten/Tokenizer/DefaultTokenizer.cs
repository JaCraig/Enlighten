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
using Enlighten.Tokenizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.Tokenizer
{
    /// <summary>
    /// Default tokenizer
    /// </summary>
    /// <seealso cref="ITokenizer"/>
    public class DefaultTokenizer : ITokenizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTokenizer"/> class.
        /// </summary>
        /// <param name="languages">The languages.</param>
        public DefaultTokenizer(IEnumerable<ITokenizerLanguage> languages)
        {
            Languages = languages.Where(x => x.GetType().Assembly != typeof(DefaultTokenizer).Assembly).ToDictionary(x => x.ISOCode);
            foreach (var Language in languages.Where(x => x.GetType().Assembly == typeof(DefaultTokenizer).Assembly
                && !Languages.ContainsKey(x.ISOCode)))
            {
                Languages.Add(Language.ISOCode, Language);
            }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>The languages.</value>
        public Dictionary<string, ITokenizerLanguage> Languages { get; }

        /// <summary>
        /// Detokenizes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The resulting text.</returns>
        public string Detokenize(Token[] tokens, TokenizerLanguage language)
        {
            if (!Languages.ContainsKey(language))
                return "";
            return Languages[language].Detokenize(tokens);
        }

        /// <summary>
        /// Tokenizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="language">The language.</param>
        /// <returns>The tokens found.</returns>
        public Token[] Tokenize(string text, TokenizerLanguage language)
        {
            if (!Languages.ContainsKey(language))
                return Array.Empty<Token>();
            var Language = Languages[language];
            var Stream = new TokenizableStream<char>(text?.ToCharArray() ?? Array.Empty<char>());
            return Language.Tokenize(Stream);
        }
    }
}