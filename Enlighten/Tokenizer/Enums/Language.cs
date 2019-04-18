﻿/*
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

namespace Enlighten.Tokenizer.Enums
{
    /// <summary>
    /// Language enum
    /// </summary>
    /// <seealso cref="StringEnumBaseClass{Language}"/>
    public class Language : StringEnumBaseClass<Language>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Language(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Language"/> class.
        /// </summary>
        public Language() : base("")
        {
        }

        /// <summary>
        /// Gets the english rule based tokenizer.
        /// </summary>
        /// <value>The english rule based tokenizer.</value>
        public static Language EnglishRuleBased => new Language("en-us");
    }
}