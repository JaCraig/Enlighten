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

using Enlighten.Tokenizer.Interfaces;
using Enlighten.Tokenizer.Languages.English.TokenFinders;

namespace Enlighten.Tokenizer.Languages.English
{
    /// <summary>
    /// English language
    /// </summary>
    /// <seealso cref="ITokenizerLanguage"/>
    public class EnglishLanguage : ITokenizerLanguage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishLanguage"/> class.
        /// </summary>
        public EnglishLanguage()
        {
            ISOCode = "en-us";
            TokenFinders = new ITokenFinder[]
            {
                new Ellipsis(),
                new NewLine(),
                new OtherTokenFinder(),
                new Symbol(),
                new Whitespace(),
                new Word(),
                new Number(),
                new Emoji()
            };
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string ISOCode { get; }

        /// <summary>
        /// Gets the token finders.
        /// </summary>
        /// <value>The token finders.</value>
        public ITokenFinder[] TokenFinders { get; }
    }
}