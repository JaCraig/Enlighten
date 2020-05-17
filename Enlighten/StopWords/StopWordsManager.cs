using Enlighten.StopWords.Enum;
using Enlighten.StopWords.Interfaces;
using Enlighten.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.StopWords
{
    /// <summary>
    /// Stop words manager
    /// </summary>
    /// <seealso cref="IStopWordsManager"/>
    public class StopWordsManager : IStopWordsManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StopWordsManager"/> class.
        /// </summary>
        /// <param name="stopWordsLanguages">The stop words languages.</param>
        public StopWordsManager(IEnumerable<IStopWordsLanguage> stopWordsLanguages)
        {
            StopWordsLanguages = stopWordsLanguages.Where(x => x.GetType().Assembly != typeof(StopWordsManager).Assembly).ToDictionary(x => x.Name);
            foreach (var Language in stopWordsLanguages.Where(x => x.GetType().Assembly == typeof(StopWordsManager).Assembly
                && !StopWordsLanguages.ContainsKey(x.Name)))
            {
                StopWordsLanguages.Add(Language.Name, Language);
            }
        }

        /// <summary>
        /// Gets the stop words languages.
        /// </summary>
        /// <value>The stop words languages.</value>
        public Dictionary<string, IStopWordsLanguage> StopWordsLanguages { get; }

        /// <summary>
        /// Determines whether [the specified token] [is a stop word].
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="language">The language.</param>
        /// <returns><c>true</c> if [the specified token] [is a stop word]; otherwise, <c>false</c>.</returns>
        public bool IsStopWord(string token, StopWordsLanguage language)
        {
            if (string.IsNullOrEmpty(token) || !StopWordsLanguages.TryGetValue(language, out var StopWords))
                return false;
            return StopWords.IsStopWord(token);
        }

        /// <summary>
        /// Marks the stop words.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The tokens.</returns>
        public Token[] MarkStopWords(Token[] tokens, StopWordsLanguage language)
        {
            if (tokens is null || tokens.Length == 0)
                return Array.Empty<Token>();
            if (!StopWordsLanguages.TryGetValue(language, out var StopWords))
                return tokens;
            return StopWords.MarkStopWords(tokens);
        }
    }
}