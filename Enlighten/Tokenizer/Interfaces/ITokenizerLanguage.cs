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

using Enlighten.Tokenizer.Utils;

namespace Enlighten.Tokenizer.Interfaces
{
    /// <summary>
    /// Language interface
    /// </summary>
    public interface ITokenizerLanguage
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string ISOCode { get; }

        /// <summary>
        /// Detokenizes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>Converts the tokens into the equivalent string.</returns>
        string Detokenize(Token[] tokens);

        /// <summary>
        /// Removes the stop words.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The tokens with the stop words removed.</returns>
        Token[] RemoveStopWords(Token[] tokens);

        /// <summary>
        /// Tokenizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The tokenized version of the text.</returns>
        Token[] Tokenize(TokenizableStream<char> text);
    }
}