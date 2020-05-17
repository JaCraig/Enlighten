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
using System.Text.RegularExpressions;

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
        private static readonly Dictionary<char, int> RomanMap = new Dictionary<char, int>()
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
        /// The roman numerals
        /// </summary>
        private readonly HashSet<char> RomanNumeralCharacters = new HashSet<char>(new char[] { 'I', 'V', 'X', 'L', 'C', 'D', 'M' });

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public override int Order => 2;

        /// <summary>
        /// Converts to number.
        /// </summary>
        /// <param name="value">The result.</param>
        /// <returns>The number value.</returns>
        public string ConvertToNumber(string value)
        {
            int number = 0;
            for (int i = 0; i < value.Length; i++)
            {
                if (i + 1 < value.Length && RomanMap[value[i]] < RomanMap[value[i + 1]])
                {
                    number -= RomanMap[value[i]];
                }
                else
                {
                    number += RomanMap[value[i]];
                }
            }
            return number.ToString();
        }

        /// <summary>
        /// The actual implementation of the IsMatch done by the individual classes.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>The token.</returns>
        protected override Token? IsMatchImpl(TokenizableStream<char> tokenizer)
        {
            if (tokenizer.End() || !RomanNumeralCharacters.Contains(tokenizer.Current))
                return null;

            var StartPosition = tokenizer.Index;

            ConsumeNumbers(tokenizer, RomanNumeralCharacters);

            if (!tokenizer.End() && char.IsLetter(tokenizer.Current))
                return null;

            var EndPosition = tokenizer.Index - 1;

            var Result = new string(tokenizer.Slice(StartPosition, EndPosition).ToArray());

            if (Result == "I" || !Validate(Result))
                return null;

            return new Token(
                EndPosition,
                StartPosition,
                TokenType.Number,
                Result
            )
            {
                NormalizedValue = ConvertToNumber(Result),
                ReplacementValue = "<NUMBER>"
            };
        }

        /// <summary>
        /// Consumes the numbers.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <param name="romanNumeralChars">The roman numeral chars.</param>
        private static void ConsumeNumbers(TokenizableStream<char> tokenizer, HashSet<char> romanNumeralChars)
        {
            while (!tokenizer.End() && romanNumeralChars.Contains(tokenizer.Current))
            {
                tokenizer.Consume();
            }
        }

        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True if it</returns>
        private static bool Validate(string value)
        {
            return ValidateSameSymbolUpToThreeTimesInARow(value)
                && ValidateSubtractionRelatedRules(value);
        }

        /// <summary>
        /// Validates at most one number in a row is subtracted from another.
        /// </summary>
        /// <param name="clusterLength">Length of the cluster.</param>
        /// <returns>True if it is correct, false otherwise.</returns>
        private static bool ValidateAtMostOneNumberInARowIsSubtractedFromAnother(int clusterLength)
        {
            return clusterLength <= 1;
        }

        /// <summary>
        /// Validates the numbers smaller than ten times cannot be subtracted.
        /// </summary>
        /// <param name="previousSymbolValue">The previous symbol value.</param>
        /// <param name="currentSymbolValue">The current symbol value.</param>
        /// <returns>True if it is correct, false otherwise.</returns>
        private static bool ValidateNumbersSmallerThanTenTimesCannotBeSubtracted(int previousSymbolValue, int currentSymbolValue)
        {
            var currentDecreasedTenTimes = currentSymbolValue / 10;
            var currentIsBiggerThanTenTimes = currentDecreasedTenTimes > previousSymbolValue;

            if (currentIsBiggerThanTenTimes)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates the powers other than ten cannot be subtracted.
        /// </summary>
        /// <param name="previous">The previous symbol value.</param>
        /// <returns>True if it is correct, false otherwise.</returns>
        private static bool ValidatePowersOtherThanTenCannotBeSubtracted(
            int previous)
        {
            return previous == 1 || previous == 10 || previous == 100;
        }

        /// <summary>
        /// Validates the same symbol up to three times in a row.
        /// </summary>
        /// <param name="value">The string representation.</param>
        /// <returns>True if it is correct, false otherwise.</returns>
        private static bool ValidateSameSymbolUpToThreeTimesInARow(string value)
        {
            return !Regex.IsMatch(value, @"(.)\1{3,}");
        }

        /// <summary>
        /// Validates the subtraction related rules.
        /// </summary>
        /// <param name="value">The string representation.</param>
        /// <returns>True if it is valid, false otherwise.</returns>
        private static bool ValidateSubtractionRelatedRules(string value)
        {
            int previous = int.MaxValue;
            int clusterLength = 0;

            foreach (char currentSymbol in value)
            {
                RomanMap.TryGetValue(currentSymbol, out var CurrentSymbolValue);
                if (CurrentSymbolValue < previous)
                {
                    //new cluster
                    previous = CurrentSymbolValue;
                    clusterLength = 1;
                }
                else if (CurrentSymbolValue == previous)
                {
                    //old cluster grows
                    clusterLength++;
                }
                else if (CurrentSymbolValue > previous)
                {
                    //subtraction attempted

                    if (!ValidateAtMostOneNumberInARowIsSubtractedFromAnother(clusterLength))
                        return false;

                    if (!ValidatePowersOtherThanTenCannotBeSubtracted(previous))
                        return false;

                    if (!ValidateNumbersSmallerThanTenTimesCannotBeSubtracted(previous, CurrentSymbolValue))
                        return false;

                    clusterLength = 1;
                    previous = CurrentSymbolValue;
                }
            }
            return true;
        }
    }
}