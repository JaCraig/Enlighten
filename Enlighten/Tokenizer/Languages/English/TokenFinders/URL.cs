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

using BigBook.ExtensionMethods;
using Enlighten.Tokenizer.BaseClasses;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Utils;
using System;

namespace Enlighten.Tokenizer.Languages.English.TokenFinders
{
    /// <summary>
    /// URL finder
    /// </summary>
    /// <seealso cref="TokenFinderBaseClass"/>
    public class URL : TokenFinderBaseClass
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
        protected override Token? IsMatchImpl(TokenizableStream<char> tokenizer)
        {
            if (tokenizer.End() || tokenizer.Index + 8 > tokenizer.Length)
                return null;

            var StartPosition = tokenizer.Index;

            Span<char> TempSlice = tokenizer.Slice(StartPosition, StartPosition + 7);

            if (TempSlice[0] is not 'f' and not 'h' and not 's' and not 'w')
                return null;

            while (!tokenizer.End() && !char.IsWhiteSpace(tokenizer.Current))
            {
                tokenizer.Consume();
            }

            var EndPosition = tokenizer.Index - 1;

            var Result = new string(tokenizer.Slice(StartPosition, EndPosition).ToArray());

            Result = Result.StripRight(".");

            tokenizer.Index = StartPosition + Result.Length;

            if (!Uri.IsWellFormedUriString(Result, UriKind.RelativeOrAbsolute)
                || (!Result.StartsWith("ftp:", StringComparison.OrdinalIgnoreCase) &&
                !Result.StartsWith("sftp:", StringComparison.OrdinalIgnoreCase) &&
                !Result.StartsWith("http:", StringComparison.OrdinalIgnoreCase) &&
                !Result.StartsWith("https:", StringComparison.OrdinalIgnoreCase) &&
                !Result.StartsWith("www.", StringComparison.OrdinalIgnoreCase)))
            {
                return null;
            }

            EndPosition = tokenizer.Index - 1;

            var TempResult = new string(tokenizer.Slice(StartPosition, EndPosition).ToArray());

            return new Token(
                EndPosition,
                StartPosition,
                TokenType.Url,
                TempResult
            );
        }
    }
}