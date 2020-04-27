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
using System.Linq;

namespace Enlighten.Tokenizer.Languages.English.TokenFinders
{
    /// <summary>
    /// Abbreviation finder.
    /// </summary>
    /// <seealso cref="TokenFinderBaseClass"/>
    public class Abbreviation : TokenFinderBaseClass
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public override int Order { get; } = 2;

        /// <summary>
        /// The common abbreviations
        /// </summary>
        private string[] CommonAbbreviations = new string[]
        {
            "LLC",
            "LP",
            "LLP",
            "LLLP",
            "PLLC",
            "CORP",
            "PC",
            "DBA",
            "CO",
            "MR",
            "MR.",
            "MRS",
            "MRS.",
            "APPROX",
            "APPT",
            "ASAP",
            "BYOB",
            "C/O",
            "DEPT",
            "DIY",
            "EST",
            "MIN",
            "MISC",
            "NUM",
            "RSVP",
            "TEL",
            "TEMP",
            "VET",
            "VS",
            "TSP",
            "TBS",
            "TBSP",
            "C",
            "GAL",
            "LB",
            "PT",
            "QT",
            "AVE",
            "BLVD",
            "CYN",
            "DR",
            "DR.",
            "LN",
            "RD",
            "ST",
            "E",
            "N",
            "NE",
            "NW",
            "S",
            "SE",
            "SW",
            "W",
            "BA",
            "BS",
            "MA",
            "MPHIL",
            "JD",
            "DC",
            "PA",
            "MD",
            "VP",
            "SVP",
            "EVP",
            "CMO",
            "CFO",
            "CEO",
            "ACE",
            "AD",
            "AFAIK",
            "AFK",
            "ANI",
            "BRB",
            "CUL",
            "CWYL",
            "IIRC",
            "IQ",
            "LOL",
            "NP",
            "ROFL",
            "TY",
            "WC",
            "AAA",
            "CCC",
            "CWA",
            "FDIC",
            "FHA",
            "NRA",
            "SSA",
            "PM",
            "EG",
            "ETC",
            "IE",
            "NB",
            "PS",
            "VIZ",
            "OK",
        };

        /// <summary>
        /// The actual implementation of the IsMatch done by the individual classes.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>The token.</returns>
        protected override Token? IsMatchImpl(TokenizableStream<char> tokenizer)
        {
            if (tokenizer.End() || !char.IsLetter(tokenizer.Current))
                return null;

            var StartPosition = tokenizer.Index;

            bool CharacterFound = true;
            int PeriodCount = 0;
            while (CharacterFound)
            {
                CharacterFound = false;
                while (!tokenizer.End() && (char.IsLetter(tokenizer.Current) || tokenizer.Current == '\'' || tokenizer.Current == '-'))
                {
                    CharacterFound = true;
                    tokenizer.Consume();
                }

                if (tokenizer.Current == '.' && CharacterFound)
                {
                    tokenizer.Consume();
                    ++PeriodCount;
                }
            }

            var EndPosition = tokenizer.Index - 1;

            var Result = new string(tokenizer.Slice(StartPosition, EndPosition).ToArray());

            if (PeriodCount > 1)
            {
                return new Token(
                    EndPosition,
                    StartPosition,
                    TokenType.Abbreviation,
                    Result
                );
            }

            var UpperResult = Result.ToUpperInvariant();

            if (Result == UpperResult && Result.Length <= 4 && Result.Length > 1)
            {
                return new Token(
                    EndPosition,
                    StartPosition,
                    TokenType.Abbreviation,
                    Result
                );
            }

            if (!CommonAbbreviations.Any(x => x == UpperResult))
                return null;

            return new Token(
                EndPosition,
                StartPosition,
                TokenType.Abbreviation,
                Result
            );
        }
    }
}