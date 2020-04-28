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

using Enlighten.Stemmer.Enums;
using Enlighten.Stemmer.Interfaces;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.Stemmer
{
    /// <summary>
    /// Default stemmer
    /// </summary>
    /// <seealso cref="IStemmer"/>
    public class DefaultStemmer : IStemmer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultStemmer"/> class.
        /// </summary>
        /// <param name="languages">The languages.</param>
        public DefaultStemmer(IEnumerable<IStemmerLanguage> languages)
        {
            Languages = languages.Where(x => x.GetType().Assembly != typeof(DefaultStemmer).Assembly).ToDictionary(x => x.ISOCode);
            foreach (var Language in languages.Where(x => x.GetType().Assembly == typeof(DefaultStemmer).Assembly
                && !Languages.ContainsKey(x.ISOCode)))
            {
                Languages.Add(Language.ISOCode, Language);
            }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>The languages.</value>
        public Dictionary<string, IStemmerLanguage> Languages { get; }

        /// <summary>
        /// Stems the words.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <param name="language"></param>
        /// <returns>The resulting stemmed words.</returns>
        public string[] Stem(string[] words, StemmerLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Stemmer))
                return words;
            return Stemmer.StemWords(words);
        }

        /// <summary>
        /// Stems the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The resulting stemmed tokens.</returns>
        public Token[] Stem(Token[] tokens, StemmerLanguage language)
        {
            Token[] FinalTokens = tokens.Where(x => x.TokenType == TokenType.Word).ToArray();
            var Results = Stem(FinalTokens.Select(x => x.Value).ToArray(), language);
            for (int x = 0; x < Results.Length; ++x)
            {
                FinalTokens[x].StemmedValue = Results[x];
            }
            foreach (var Token in tokens.Where(x => x.TokenType != TokenType.Word))
            {
                Token.StemmedValue = Token.Value;
            }
            return tokens;
        }
    }
}