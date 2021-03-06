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

using Enlighten.Stemmer.Enums;
using Enlighten.Tokenizer;

namespace Enlighten.Stemmer.Interfaces
{
    /// <summary>
    /// Stemmer interface
    /// </summary>
    public interface IStemmer
    {
        /// <summary>
        /// Stems the words.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <returns>The resulting stemmed words.</returns>
        string[] Stem(string[] words, StemmerLanguage language);

        /// <summary>
        /// Stems the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The resulting stemmed tokens.</returns>
        Token[] Stem(Token[] tokens, StemmerLanguage language);
    }
}