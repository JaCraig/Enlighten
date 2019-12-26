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

namespace Enlighten.Tokenizer.BaseClasses
{
    /// <summary>
    /// Single character token finder base class
    /// </summary>
    /// <seealso cref="TokenFinderBaseClass"/>
    public abstract class SingleCharacterTokenFinderBaseClass : TokenFinderBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleCharacterTokenFinderBaseClass"/> class.
        /// </summary>
        protected SingleCharacterTokenFinderBaseClass()
        {
        }

        /// <summary>
        /// Gets the character.
        /// </summary>
        /// <value>The character.</value>
        protected abstract char Character { get; }

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        /// <value>The type of the token.</value>
        protected abstract string TokenType { get; }

        /// <summary>
        /// The actual implementation of the IsMatch done by the individual classes.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>The token.</returns>
        protected override Token? IsMatchImpl(TokenizableStream<char> tokenizer)
        {
            if (!tokenizer.End() && tokenizer.Current == Character)
            {
                var StartPos = tokenizer.Index;
                tokenizer.Consume();
                var EndPos = tokenizer.Index;
                return new Token(
                    EndPos,
                    StartPos,
                    TokenType,
                    Character.ToString()
                );
            }
            return null;
        }
    }
}