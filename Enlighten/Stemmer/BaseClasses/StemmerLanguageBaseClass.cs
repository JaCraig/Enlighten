using Enlighten.Stemmer.Interfaces;
using System;
using System.Linq;

namespace Enlighten.Stemmer.BaseClasses
{
    /// <summary>
    /// Stemmer language base class
    /// </summary>
    /// <seealso cref="IStemmerLanguage"/>
    public abstract class StemmerLanguageBaseClass : IStemmerLanguage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StemmerLanguageBaseClass"/> class.
        /// </summary>
        protected StemmerLanguageBaseClass()
        {
        }

        /// <summary>
        /// Gets the vowels.
        /// </summary>
        /// <value>The vowels.</value>
        protected abstract char[] Vowels { get; }

        /// <summary>
        /// Stems the words.
        /// </summary>
        /// <param name="words">The words.</param>
        /// <returns>The resulting stemmed words.</returns>
        public string[] StemWords(string[] words)
        {
            if (words == null || words.Length == 0)
                return Array.Empty<string>();

            var ReturnValue = new string[words.Length];
            for (int x = 0; x < words.Length; ++x)
            {
                ReturnValue[x] = StemWord(words[x]);
            }
            return ReturnValue;
        }

        /// <summary>
        /// Determines whether the specified character is a vowel.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <returns><c>true</c> if the specified character is a vowel; otherwise, <c>false</c>.</returns>
        protected bool IsVowel(char character)
        {
            return Vowels.Contains(character);
        }

        /// <summary>
        /// Stems the word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The stemmed word.</returns>
        protected abstract string StemWord(string word);
    }
}