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

using Enlighten.Tokenizer.Utils;

namespace Enlighten.Tokenizer.Languages.Interfaces
{
    /// <summary>
    /// English token finder
    /// </summary>
    public interface IEnglishTokenFinder
    {
        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        int Order { get; }

        /// <summary>
        /// Determines whether the next set of item on the stream matches this finder.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The token.</returns>
        Token? IsMatch(TokenizableStream<char> stream);
    }
}