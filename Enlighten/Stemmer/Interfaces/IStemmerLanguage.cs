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

namespace Enlighten.Stemmer.Interfaces
{
    /// <summary>
    /// Stemmer language interface
    /// </summary>
    public interface IStemmerLanguage
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string ISOCode { get; }

        /// <summary>
        /// Stems the words.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <returns>The resulting stemmed words.</returns>
        string[] StemWords(string[] words);
    }
}