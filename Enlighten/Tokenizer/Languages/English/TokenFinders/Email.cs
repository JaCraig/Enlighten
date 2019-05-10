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

using Enlighten.Tokenizer.BaseClasses;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Utils;

namespace Enlighten.Tokenizer.Languages.English.TokenFinders
{
    /// <summary>
    /// Email token finder
    /// </summary>
    /// <seealso cref="TokenFinderBaseClass"/>
    public class Email : TokenFinderBaseClass
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public override int Order { get; } = 2;

        /// <summary>
        /// The actual implementation of the IsMatch done by the individual classes.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>The token.</returns>
        protected override Token IsMatchImpl(TokenizableStream<char> tokenizer)
        {
            if (tokenizer.End() || !char.IsLetter(tokenizer.Current))
                return null;

            var StartPosition = tokenizer.Index;

            while (!tokenizer.End() && (char.IsLetter(tokenizer.Current) || char.IsNumber(tokenizer.Current) || tokenizer.Current == '-' || tokenizer.Current == '.'))
            {
                tokenizer.Consume();
            }

            if (tokenizer.Current != '@')
                return null;

            tokenizer.Consume();

            bool EmailFound = false;

            while (!tokenizer.End() && (char.IsLetter(tokenizer.Current) || char.IsNumber(tokenizer.Current) || tokenizer.Current == '-' || tokenizer.Current == '.'))
            {
                if (tokenizer.Current == '.' && !char.IsLetter(tokenizer.Peek(1)))
                    break;
                EmailFound |= tokenizer.Current == '.';
                tokenizer.Consume();
            }

            var EndPosition = tokenizer.Index - 1;

            return new Token
            {
                EndPosition = EndPosition,
                StartPosition = StartPosition,
                TokenType = TokenType.Email,
                Value = new string(tokenizer.Slice(StartPosition, EndPosition).ToArray())
            };
        }
    }
}