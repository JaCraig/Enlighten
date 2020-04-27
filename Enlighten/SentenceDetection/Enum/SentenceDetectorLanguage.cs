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

using BigBook.Patterns.BaseClasses;

namespace Enlighten.SentenceDetection.Enum
{
    /// <summary>
    /// Sentence detector language
    /// </summary>
    /// <seealso cref="StringEnumBaseClass{SentenceDetectorLanguage}"/>
    public class SentenceDetectorLanguage : StringEnumBaseClass<SentenceDetectorLanguage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SentenceDetectorLanguage"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public SentenceDetectorLanguage(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SentenceDetectorLanguage"/> class.
        /// </summary>
        public SentenceDetectorLanguage() : base(string.Empty)
        {
        }

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static SentenceDetectorLanguage Default { get; } = new SentenceDetectorLanguage("default");

        /// <summary>
        /// Gets the new line.
        /// </summary>
        /// <value>The new line.</value>
        public static SentenceDetectorLanguage NewLine { get; } = new SentenceDetectorLanguage("newline");
    }
}