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
using System.Collections.Generic;

namespace Enlighten.Tokenizer.Languages.English.TokenFinders
{
    /// <summary>
    /// Number finder
    /// </summary>
    /// <seealso cref="TokenFinderBaseClass"/>
    public class RomanNumerals : TokenFinderBaseClass
    {
        /// <summary>
        /// The roman map
        /// </summary>
        private static Dictionary<char, int> RomanMap = new Dictionary<char, int>()
        {
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000}
        };

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public override int Order => 2;

        /// <summary>
        /// The actual implementation of the IsMatch done by the individual classes.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>The token.</returns>
        protected override Token? IsMatchImpl(TokenizableStream<char> tokenizer)
        {
            if (tokenizer.End() || !(tokenizer.Current == 'I'
                || tokenizer.Current == 'V'
                || tokenizer.Current == 'X'
                || tokenizer.Current == 'L'
                || tokenizer.Current == 'C'
                || tokenizer.Current == 'D'
                || tokenizer.Current == 'M'))
                return null;

            var StartPosition = tokenizer.Index;

            ConsumeNumbers(tokenizer);

            if (!tokenizer.End() && char.IsLetter(tokenizer.Current))
                return null;

            var EndPosition = tokenizer.Index - 1;

            var Result = new string(tokenizer.Slice(StartPosition, EndPosition).ToArray());

            if (Result == "I")
                return null;

            return new Token(
                EndPosition,
                StartPosition,
                TokenType.Number,
                Result,
                ConvertToNumber(Result)
            );
        }

        /// <summary>
        /// Consumes the numbers.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        private static void ConsumeNumbers(TokenizableStream<char> tokenizer)
        {
            while (!tokenizer.End() && (tokenizer.Current == 'I'
                || tokenizer.Current == 'V'
                || tokenizer.Current == 'X'
                || tokenizer.Current == 'L'
                || tokenizer.Current == 'C'
                || tokenizer.Current == 'D'
                || tokenizer.Current == 'M'))
            {
                tokenizer.Consume();
            }
        }

        /// <summary>
        /// Converts to number.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        private string ConvertToNumber(string result)
        {
            int number = 0;
            for (int i = 0; i < result.Length; i++)
            {
                if (i + 1 < result.Length && RomanMap[result[i]] < RomanMap[result[i + 1]])
                {
                    number -= RomanMap[result[i]];
                }
                else
                {
                    number += RomanMap[result[i]];
                }
            }
            return number.ToString();
        }
    }
}