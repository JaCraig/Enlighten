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

namespace Enlighten.Tokenizer
{
    /// <summary>
    /// Token data holder
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class.
        /// </summary>
        /// <param name="endPosition">The end position.</param>
        /// <param name="startPosition">The start position.</param>
        /// <param name="tokenType">Type of the token.</param>
        /// <param name="value">The value.</param>
        public Token(int endPosition, int startPosition, string tokenType, string value)
        {
            EndPosition = endPosition;
            StartPosition = startPosition;
            TokenType = tokenType;
            Value = value;
            NormalizedValue = value;
            ReplacementValue = value;
        }

        /// <summary>
        /// Gets or sets the end position. (inclusive)
        /// </summary>
        /// <value>The end position.</value>
        public int EndPosition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is entity.
        /// </summary>
        /// <value><c>true</c> if this instance is entity; otherwise, <c>false</c>.</value>
        public bool Entity { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity.
        /// </summary>
        /// <value>The type of the entity.</value>
        public string? EntityType { get; set; }

        /// <summary>
        /// Gets or sets the normalized value.
        /// </summary>
        /// <value>The normalized value.</value>
        public string NormalizedValue { get; set; }

        /// <summary>
        /// Gets or sets the part of speech.
        /// </summary>
        /// <value>The part of speech.</value>
        public string? PartOfSpeech { get; set; }

        /// <summary>
        /// Gets or sets the replacement value.
        /// </summary>
        /// <value>The replacement value.</value>
        public string? ReplacementValue { get; set; }

        /// <summary>
        /// Gets or sets the start position. (inclusive)
        /// </summary>
        /// <value>The start position.</value>
        public int StartPosition { get; set; }

        /// <summary>
        /// Gets or sets the stemmed value.
        /// </summary>
        /// <value>The stemmed value.</value>
        public string? StemmedValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [stop word].
        /// </summary>
        /// <value><c>true</c> if [stop word]; otherwise, <c>false</c>.</value>
        public bool StopWord { get; set; }

        /// <summary>
        /// Gets or sets the type of the token.
        /// </summary>
        /// <value>The type of the token.</value>
        public string TokenType { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }

        /// <summary>
        /// Copies this instance.
        /// </summary>
        /// <returns>A copy of this instance.</returns>
        public Token Copy()
        {
            return new Token(EndPosition, StartPosition, TokenType, Value)
            {
                PartOfSpeech = PartOfSpeech,
                StemmedValue = StemmedValue,
                StopWord = StopWord,
                NormalizedValue = NormalizedValue,
                ReplacementValue = ReplacementValue,
                Entity = Entity,
                EntityType = EntityType
            };
        }

        /// <summary>
        /// Joins the specified token with this one.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>A new instance of the token.</returns>
        public Token Join(Token token)
        {
            if (token.StartPosition < StartPosition)
            {
                return new Token(EndPosition, token.StartPosition, TokenType, token.Value + Value)
                {
                    PartOfSpeech = PartOfSpeech,
                    StemmedValue = token.StemmedValue + StemmedValue,
                    StopWord = StopWord,
                    NormalizedValue = token.NormalizedValue + NormalizedValue,
                    ReplacementValue = token.ReplacementValue + ReplacementValue,
                    Entity = Entity,
                    EntityType = EntityType
                };
            }
            return new Token(token.EndPosition, StartPosition, TokenType, Value + token.Value)
            {
                PartOfSpeech = PartOfSpeech,
                StemmedValue = StemmedValue + token.StemmedValue,
                StopWord = StopWord,
                NormalizedValue = NormalizedValue + token.NormalizedValue,
                ReplacementValue = ReplacementValue + token.ReplacementValue,
                Entity = Entity,
                EntityType = EntityType
            };
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return $"{TokenType}: {Value}";
        }
    }
}