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

using Enlighten.SentenceDetection;
using Enlighten.Tokenizer.Enums;

namespace Enlighten.Tokenizer.Interfaces
{
    /// <summary>
    /// Tokenizer interface
    /// </summary>
    public interface ITokenizer
    {
        /// <summary>
        /// Detokenizes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The resulting text.</returns>
        string Detokenize(Token[] tokens, TokenizerLanguage language);

        /// <summary>
        /// Detokenizes the specified sentences.
        /// </summary>
        /// <param name="sentences">The sentences.</param>
        /// <param name="language">The language.</param>
        /// <returns>The resulting text.</returns>
        string Detokenize(Sentence[] sentences, TokenizerLanguage language);

        /// <summary>
        /// Removes the stop words.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The tokens without the stop words.</returns>
        Token[] RemoveStopWords(Token[] tokens, TokenizerLanguage language);

        /// <summary>
        /// Tokenizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="language">The language.</param>
        /// <returns>The tokens found.</returns>
        Token[] Tokenize(string text, TokenizerLanguage language);
    }
}