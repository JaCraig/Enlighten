using System;
using System.Text.RegularExpressions;

namespace Enlighten.Inflector.Inflectors
{
    /// <summary>
    /// Inflector rule
    /// </summary>
    public class ConjugationRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rule"/> class.
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <param name="replacement">The replacement.</param>
        public ConjugationRule(string pattern, Func<string, string, string> replacement)
        {
            Regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Replacement = replacement;
        }

        /// <summary>
        /// Gets the regex.
        /// </summary>
        /// <value>The regex.</value>
        private Regex Regex { get; }

        /// <summary>
        /// Gets the replacement.
        /// </summary>
        /// <value>The replacement.</value>
        private Func<string, string, string> Replacement { get; }

        /// <summary>
        /// Applies the rule to the specified word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="partOfSpeech">Type of the conjugation.</param>
        /// <returns>The resulting word.</returns>
        public string Apply(string word, string partOfSpeech)
        {
            return Replacement(word, partOfSpeech);
        }

        /// <summary>
        /// Determines whether this instance can be applied to the specified word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns><c>true</c> if this instance can apply the specified word; otherwise, <c>false</c>.</returns>
        public bool CanApply(string word)
        {
            return Regex.IsMatch(word);
        }
    }
}