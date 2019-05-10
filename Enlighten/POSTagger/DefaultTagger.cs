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

using Enlighten.POSTagger.Enum;
using Enlighten.POSTagger.Interfaces;
using Enlighten.Tokenizer;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.POSTagger
{
    /// <summary>
    /// Default part of speech tagger
    /// </summary>
    /// <seealso cref="IPOSTagger"/>
    public class DefaultTagger : IPOSTagger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTagger"/> class.
        /// </summary>
        /// <param name="languages">The languages.</param>
        public DefaultTagger(IEnumerable<IPOSTaggerLanguage> languages)
        {
            Languages = languages.Where(x => x.GetType().Assembly != typeof(DefaultTagger).Assembly).ToDictionary(x => x.ISOCode);
            foreach (var Language in languages.Where(x => x.GetType().Assembly == typeof(DefaultTagger).Assembly
                && !Languages.ContainsKey(x.ISOCode)))
            {
                Languages.Add(Language.ISOCode, Language);
            }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>The languages.</value>
        private Dictionary<string, IPOSTaggerLanguage> Languages { get; }

        /// <summary>
        /// Stems the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The resulting stemmed tokens.</returns>
        public Token[] Tag(Token[] tokens, POSTaggerLanguage language)
        {
            if (!Languages.ContainsKey(language))
                return tokens;
            return Languages[language].Tag(tokens);
        }
    }
}