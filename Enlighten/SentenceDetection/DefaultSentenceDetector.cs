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
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.SentenceDetection
{
    /// <summary>
    /// Default sentence detector
    /// </summary>
    /// <seealso cref="ISentenceDetector"/>
    public class DefaultSentenceDetector : ISentenceDetector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSentenceDetector"/> class.
        /// </summary>
        /// <param name="detectors">The detectors.</param>
        /// <param name="tokenizer">The tokenizer.</param>
        public DefaultSentenceDetector(IEnumerable<IDetector> detectors, ITokenizer tokenizer)
        {
            Detectors = detectors.Where(x => x.GetType().Assembly != typeof(DefaultSentenceDetector).Assembly).ToDictionary(x => x.Name);
            foreach (var Detector in detectors.Where(x => x.GetType().Assembly == typeof(DefaultSentenceDetector).Assembly
                && !Detectors.ContainsKey(x.Name)))
            {
                Detectors.Add(Detector.Name, Detector);
            }

            Tokenizer = tokenizer;
        }

        /// <summary>
        /// Gets the detectors.
        /// </summary>
        /// <value>The detectors.</value>
        public Dictionary<string, IDetector> Detectors { get; }

        /// <summary>
        /// Gets the tokenizer.
        /// </summary>
        /// <value>The tokenizer.</value>
        public ITokenizer Tokenizer { get; }

        /// <summary>
        /// Detects the sentences in the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="detector">The detector.</param>
        /// <returns>The sentences.</returns>
        public Sentence[] Detect(string input, SentenceDetectorLanguage detector)
        {
            return Detect(Tokenizer.Tokenize(input, TokenizerLanguage.EnglishRuleBased), detector);
        }

        /// <summary>
        /// Detects the sentences in the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="detector">The detector.</param>
        /// <returns>The sentences.</returns>
        public Sentence[] Detect(Token[] input, SentenceDetectorLanguage detector)
        {
            if (!Detectors.ContainsKey(detector))
                return Array.Empty<Sentence>();
            return Detectors[detector].DetectSentences(input);
        }
    }
}