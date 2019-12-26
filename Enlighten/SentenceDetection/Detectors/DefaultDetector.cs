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
    /// Default detector
    /// </summary>
    /// <seealso cref="IDetector"/>
    public class DefaultDetector : IDetector
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name => SentenceDetectorLanguage.Default;

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
                if (ConsumeToken(Token, TempTokens))
                    TempTokens.Add(Token);
                if (IsSentenceStopper(Token))
                {
                    if (TempTokens.Count > 1)
                        ReturnValue.Add(new Sentence(i, CurrentStart, TempTokens.ToArray()));
                    TempTokens = new List<Token>();
                    CurrentStart = i + 1;
                }
            }
            return ReturnValue.ToArray();
        }

        /// <summary>
        /// Determines whether [is sentence stopper] [the specified token].
        /// </summary>
        /// <param name="Token">The token.</param>
        /// <returns><c>true</c> if [is sentence stopper] [the specified token]; otherwise, <c>false</c>.</returns>
        private static bool IsSentenceStopper(Token Token)
        {
            return Token.TokenType == TokenType.EOF
                || Token.TokenType == TokenType.ExclamationMark
                || Token.TokenType == TokenType.Period
                || Token.TokenType == TokenType.QuestionMark;
        }

        /// <summary>
        /// Determines if the token should be consumed.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="tempTokens">The temporary tokens.</param>
        /// <returns>True if the token is part of the sentence, false otherwise.</returns>
        private bool ConsumeToken(Token token, List<Token> tempTokens)
        {
            if (tempTokens.Count == 0 && token.TokenType != TokenType.Word)
                return false;
            if (token.TokenType == TokenType.DoubleQuote || token.TokenType == TokenType.SingleQuote)
                return false;
            return true;
        }
    }
}