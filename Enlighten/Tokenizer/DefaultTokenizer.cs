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
using Enlighten.SentenceDetection;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using Enlighten.Tokenizer.Utils;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        /// <param name="objectPool">The object pool.</param>
        public DefaultTokenizer(IEnumerable<ITokenizerLanguage> languages, ObjectPool<StringBuilder> objectPool)
        {
            Languages = languages.Where(x => x.GetType().Assembly != typeof(DefaultTokenizer).Assembly).ToDictionary(x => x.ISOCode);
            foreach (var Language in languages.Where(x => x.GetType().Assembly == typeof(DefaultTokenizer).Assembly
                && !Languages.ContainsKey(x.ISOCode)))
            {
                Languages.Add(Language.ISOCode, Language);
            }
            ObjectPool = objectPool;
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>The languages.</value>
        public Dictionary<string, ITokenizerLanguage> Languages { get; }

        /// <summary>
        /// Gets the object pool.
        /// </summary>
        /// <value>The object pool.</value>
        private ObjectPool<StringBuilder> ObjectPool { get; }

        /// <summary>
        /// Detokenizes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The resulting text.</returns>
        public string Detokenize(Token[] tokens, TokenizerLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Tokenizer) || tokens is null || tokens.Length == 0)
                return string.Empty;
            return Tokenizer.Detokenize(tokens);
        }

        /// <summary>
        /// Detokenizes the specified sentences.
        /// </summary>
        /// <param name="sentences">The sentences.</param>
        /// <param name="language">The language.</param>
        /// <returns>The resulting text.</returns>
        public string Detokenize(Sentence[] sentences, TokenizerLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Tokenizer) || sentences is null || sentences.Length == 0)
                return string.Empty;
            var Builder = ObjectPool.Get();
            Builder.Append(Tokenizer.Detokenize(sentences[0].Tokens));
            for (int x = 1; x < sentences.Length; ++x)
            {
                Builder.Append(" ").Append(Tokenizer.Detokenize(sentences[x].Tokens));
            }
            var ReturnValue = Builder.ToString();
            ObjectPool.Return(Builder);
            return ReturnValue;
        }

        /// <summary>
        /// Removes the stop words.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The tokens without the stop words.</returns>
        public Token[] RemoveStopWords(Token[] tokens, TokenizerLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Tokenizer) || tokens is null || tokens.Length == 0)
                return Array.Empty<Token>();
            return Tokenizer.RemoveStopWords(tokens);
        }

        /// <summary>
        /// Tokenizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="language">The language.</param>
        /// <returns>The tokens found.</returns>
        public Token[] Tokenize(string text, TokenizerLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Tokenizer))
                return Array.Empty<Token>();
            var Stream = new TokenizableStream<char>(text?.ToCharArray() ?? Array.Empty<char>());
            return Tokenizer.Tokenize(Stream);
        }
    }
}