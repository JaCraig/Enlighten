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

namespace Enlighten.POSTagger.Enum
{
    /// <summary>
    /// Part of speech tagger language
    /// </summary>
    /// <seealso cref="StringEnumBaseClass{POSTaggerLanguage}"/>
    public class POSTaggerLanguage : StringEnumBaseClass<POSTaggerLanguage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="POSTaggerLanguage"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public POSTaggerLanguage(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="POSTaggerLanguage"/> class.
        /// </summary>
        public POSTaggerLanguage() : base(string.Empty)
        {
        }

        /// <summary>
        /// Gets the brill tagger.
        /// </summary>
        /// <value>The brill tagger.</value>
        public static POSTaggerLanguage BrillTagger { get; } = new POSTaggerLanguage("en-us");
    }
}