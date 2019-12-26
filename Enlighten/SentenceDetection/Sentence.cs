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
using Enlighten.Tokenizer;

namespace Enlighten.SentenceDetection
{
    /// <summary>
    /// Sentence data holder
    /// </summary>
    public class Sentence
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Sentence"/> class.
        /// </summary>
        /// <param name="endPosition">The end position.</param>
        /// <param name="startPosition">The start position.</param>
        /// <param name="tokens">The tokens.</param>
        public Sentence(int endPosition, int startPosition, Token[] tokens)
        {
            EndPosition = endPosition;
            StartPosition = startPosition;
            Tokens = tokens;
        }

        /// <summary>
        /// Gets or sets the end position. (inclusive)
        /// </summary>
        /// <value>The end position.</value>
        public int EndPosition { get; set; }

        /// <summary>
        /// Gets or sets the start position. (inclusive)
        /// </summary>
        /// <value>The start position.</value>
        public int StartPosition { get; set; }

        /// <summary>
        /// Gets or sets the tokens.
        /// </summary>
        /// <value>The tokens.</value>
        public Token[] Tokens { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return Tokens.ToString(x => x.Value, "");
        }
    }
}