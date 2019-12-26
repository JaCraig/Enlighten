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
using Enlighten.POSTagger.Enum;
using Enlighten.POSTagger.Interfaces;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using FileCurator;
using System;
using System.Linq;

namespace Enlighten.POSTagger.Taggers
{
    /// <summary>
    /// Simple Brill tagger
    /// </summary>
    /// <seealso cref="IPOSTaggerLanguage"/>
    public class BrillTagger : IPOSTaggerLanguage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrillTagger"/> class.
        /// </summary>
        public BrillTagger()
        {
            Lexicon = new ListMapping<string, string>();
            BuildLexicon();
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string ISOCode { get; } = POSTaggerLanguage.BrillTagger;

        /// <summary>
        /// Gets the lexicon.
        /// </summary>
        /// <value>The lexicon.</value>
        public ListMapping<string, string> Lexicon { get; }

        /// <summary>
        /// Tags the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The tokens tagged.</returns>
        public Token[] Tag(Token[] tokens)
        {
            Token? PreviousToken = null;
            for (int i = 0, tokensLength = tokens.Length; i < tokensLength; i++)
            {
                var Token = tokens[i];
                if (Token.TokenType == TokenType.Word)
                {
                    if (Lexicon.TryGetValue(Token.Value, out var POS) || Lexicon.TryGetValue(Token.Value.ToLowerInvariant(), out POS))
                    {
                    }
                    else if (Token.Value.Length == 1)
                    {
                        POS = new string[] { Token.Value + "^" };
                    }
                    else
                    {
                        POS = new string[] { "NN" };
                    }

                    var Word = Token.PartOfSpeech = POS.First();

                    if (PreviousToken?.PartOfSpeech == "DT")
                    {
                        if (Word == "VBD" || Word == "VBP" || Word == "VB")
                        {
                            Token.PartOfSpeech = "NN";
                        }
                    }

                    if (Word.StartsWith("N", StringComparison.OrdinalIgnoreCase) && Token.Value.EndsWith("ED", StringComparison.OrdinalIgnoreCase))
                        Token.PartOfSpeech = "VBN";

                    if (Token.Value.EndsWith("LY", StringComparison.OrdinalIgnoreCase))
                        Token.PartOfSpeech = "RB";

                    if (Token.PartOfSpeech.StartsWith("NN", StringComparison.OrdinalIgnoreCase) && Token.Value.EndsWith("AL", StringComparison.OrdinalIgnoreCase))
                        Token.PartOfSpeech = "JJ";

                    if (Token.PartOfSpeech.StartsWith("NN", StringComparison.OrdinalIgnoreCase) && string.Equals(PreviousToken?.Value, "WOULD", StringComparison.OrdinalIgnoreCase))
                        Token.PartOfSpeech = "VB";

                    if (Token.PartOfSpeech == "NN" && Token.Value.EndsWith("S", StringComparison.OrdinalIgnoreCase))
                        Token.PartOfSpeech = "NNS";

                    if (Token.PartOfSpeech.StartsWith("NN", StringComparison.OrdinalIgnoreCase) && Token.Value.EndsWith("ING", StringComparison.OrdinalIgnoreCase))
                        Token.PartOfSpeech = "VBG";

                    PreviousToken = Token;
                }
                else if (Token.TokenType == TokenType.Number)
                {
                    Token.PartOfSpeech = "CD";
                }
                else if (Token.TokenType == TokenType.Email || Token.TokenType == TokenType.HashTag || Token.TokenType == TokenType.Username)
                {
                    Token.PartOfSpeech = "NN";
                }
                else if (Token.TokenType == TokenType.Emoji)
                {
                    Token.PartOfSpeech = "EM";
                }
            }
            return tokens;
        }

        /// <summary>
        /// Builds the lexicon.
        /// </summary>
        private void BuildLexicon()
        {
            var Data = new FileInfo("resource://Enlighten/Enlighten.POSTagger.Resources/lexicon.txt").Read();
            var Lines = Data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, LinesLength = Lines.Length; i < LinesLength; i++)
            {
                var Line = Lines[i];
                if (string.IsNullOrEmpty(Line))
                    continue;
                var LineData = Line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var Key = LineData[0];
                for (int j = 1, LineDataLength = LineData.Length; j < LineDataLength; j++)
                {
                    var Item = LineData[j];
                    Lexicon.Add(Key, Item);
                }
            }
        }
    }
}