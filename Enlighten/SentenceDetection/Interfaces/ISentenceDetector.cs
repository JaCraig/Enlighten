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
using Enlighten.Tokenizer;

namespace Enlighten.SentenceDetection.Interfaces
{
    /// <summary>
    /// Sentence detector interface
    /// </summary>
    public interface ISentenceDetector
    {
        /// <summary>
        /// Detects the sentences in the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="detector">The detector.</param>
        /// <returns>The sentences.</returns>
        Sentence[] Detect(string input, SentenceDetectorLanguage detector);

        /// <summary>
        /// Detects the sentences in the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="detector">The detector.</param>
        /// <returns>The sentences.</returns>
        Sentence[] Detect(Token[] input, SentenceDetectorLanguage detector);
    }
}