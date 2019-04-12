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
        /// Gets or sets the end position. (inclusive)
        /// </summary>
        /// <value>The end position.</value>
        public int EndPosition { get; set; }

        /// <summary>
        /// Gets or sets the start position. (inclusive)
        /// </summary>
        /// <value>The start position.</value>
        public int StartPosition { get; set; }

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
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            return $"{TokenType}: {Value}";
        }
    }
}