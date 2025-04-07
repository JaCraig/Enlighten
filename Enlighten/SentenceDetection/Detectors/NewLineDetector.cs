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

using Enlighten.SentenceDetection.Enum;
using Enlighten.SentenceDetection.Interfaces;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using System;
using System.Collections.Generic;

namespace Enlighten.SentenceDetection.Detectors
{
    /// <summary>
    /// New line detector
    /// </summary>
    /// <seealso cref="IDetector"/>
    public class NewLineDetector : IDetector
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name => SentenceDetectorLanguage.NewLine;

        /// <summary>
        /// Detects the sentences.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The sentences found based on the tokens.</returns>
        public Sentence[] DetectSentences(Token[] tokens)
        {
            if (tokens == null || tokens.Length == 0)
                return Array.Empty<Sentence>();

            List<Sentence> ReturnValue = new List<Sentence>();
            int CurrentStart = 0;
            List<Token> TempTokens = new List<Token>();
            for (int i = 0, tokensLength = tokens.Length; i < tokensLength; i++)
            {
                var Token = tokens[i];
                TempTokens.Add(Token);
                if (Token.TokenType == TokenType.EOF
                    || Token.TokenType == TokenType.NewLine)
                {
                    if (TempTokens.Count > 1)
                    {
                        ReturnValue.Add(new Sentence(i, CurrentStart, [.. TempTokens]));
                    }
                    CurrentStart = i + 1;
                }
            }
            return [.. ReturnValue];
        }
    }
}