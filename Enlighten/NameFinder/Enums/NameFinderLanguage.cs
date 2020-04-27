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

namespace Enlighten.NameFinder.Enums
{
    /// <summary>
    /// Name finder language
    /// </summary>
    /// <seealso cref="StringEnumBaseClass{NameFinderLanguage}"/>
    public class NameFinderLanguage : StringEnumBaseClass<NameFinderLanguage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NameFinderLanguage"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public NameFinderLanguage(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NameFinderLanguage"/> class.
        /// </summary>
        public NameFinderLanguage() : base(string.Empty)
        {
        }

        /// <summary>
        /// Gets the default finder.
        /// </summary>
        /// <value>The default finder.</value>
        public static NameFinderLanguage DefaultFinder { get; } = new NameFinderLanguage("en-us");
    }
}