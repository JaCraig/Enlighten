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

using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.Interfaces;
using Enlighten.Tokenizer.Utils;

namespace Enlighten.Tokenizer.BaseClasses
{
    /// <summary>
    /// Token finder base class
    /// </summary>
    /// <seealso cref="IEnglishTokenFinder"/>
    public abstract class TokenFinderBaseClass : IEnglishTokenFinder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenFinderBaseClass"/> class.
        /// </summary>
        protected TokenFinderBaseClass()
        {
        }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public abstract int Order { get; }

        /// <summary>
        /// Determines whether the next set of item on the stream matches this finder.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The token.</returns>
        public Token IsMatch(TokenizableStream<char> stream)
        {
            if (stream.End())
            {
                return new Token
                {
                    StartPosition = stream.Index,
                    EndPosition = stream.Index,
                    TokenType = TokenType.EOF,
                    Value = string.Empty
                };
            }

            stream.TakeSnapshot();

            var Match = IsMatchImpl(stream);

            if (Match == null)
            {
                stream.RollbackSnapshot();
            }
            else
            {
                stream.CommitSnapshot();
            }

            return Match;
        }

        /// <summary>
        /// The actual implementation of the IsMatch done by the individual classes.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>The token.</returns>
        protected abstract Token IsMatchImpl(TokenizableStream<char> tokenizer);
    }
}