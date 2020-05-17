using Enlighten.StopWords.BaseClasses;
using Enlighten.StopWords.Interfaces;
using System.Collections.Generic;

namespace Enlighten.StopWords.Languages
{
    /// <summary>
    /// English default stop words
    /// </summary>
    /// <seealso cref="IStopWordsLanguage"/>
    public class EnglishDefault : StopWordsBase
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name => "en-us";

        /// <summary>
        /// Gets the stop words.
        /// </summary>
        /// <value>The stop words.</value>
        protected override HashSet<string> StopWords { get; } = new HashSet<string>(new string[] { "a", "an", "and", "are", "as", "at", "be", "but", "by", "for", "if", "in", "into", "is", "it", "no", "not", "of", "on", "or", "s", "such", "t", "that", "the", "their", "then", "there", "these", "they", "this", "to", "was", "will", "with" });
    }
}