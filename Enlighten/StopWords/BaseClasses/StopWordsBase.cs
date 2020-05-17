using Enlighten.StopWords.Interfaces;
using Enlighten.Tokenizer;
using System.Collections.Generic;

namespace Enlighten.StopWords.BaseClasses
{
    /// <summary>
    /// Stop words base
    /// </summary>
    /// <seealso cref="IStopWordsLanguage"/>
    public abstract class StopWordsBase : IStopWordsLanguage
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the stop words.
        /// </summary>
        /// <value>The stop words.</value>
        protected abstract HashSet<string> StopWords { get; }

        /// <summary>
        /// Determines whether [the specified token] [is a stop word].
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns><c>true</c> if [the specified token] [is a stop word]; otherwise, <c>false</c>.</returns>
        public bool IsStopWord(string token)
        {
            return StopWords.Contains(token);
        }

        /// <summary>
        /// Marks the stop words.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The tokens.</returns>
        public Token[] MarkStopWords(Token[] tokens)
        {
            for (int x = 0; x < tokens.Length; ++x)
            {
                var TempToken = tokens[x];
                TempToken.StopWord = IsStopWord(TempToken.NormalizedValue);
            }
            return tokens;
        }
    }
}