using Enlighten.SynonymFinder.Enum;
using Enlighten.SynonymFinder.Interfaces;
using Enlighten.Tokenizer;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.SynonymFinder
{
    /// <summary>
    /// Default synonym finder
    /// </summary>
    /// <seealso cref="ISynonymFinder"/>
    public class DefaultSynonymFinder : ISynonymFinder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultSynonymFinder"/> class.
        /// </summary>
        /// <param name="languages">The languages.</param>
        public DefaultSynonymFinder(IEnumerable<ISynonymFinderLanguage> languages)
        {
            Languages = languages.Where(x => x.GetType().Assembly != typeof(DefaultSynonymFinder).Assembly).ToDictionary(x => x.Name);
            foreach (var Language in languages.Where(x => x.GetType().Assembly == typeof(DefaultSynonymFinder).Assembly
                && !Languages.ContainsKey(x.Name)))
            {
                Languages.Add(Language.Name, Language);
            }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>The languages.</value>
        public Dictionary<string, ISynonymFinderLanguage> Languages { get; }

        /// <summary>
        /// Finds the synonym if it exists.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns>The synonym or the input if it doesn't exist.</returns>
        public string FindSynonym(string input, SynonymFinderLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Language))
                return input;
            return Language.FindSynonym(input);
        }

        /// <summary>
        /// Finds the synonyms and replaces the text.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The tokens</returns>
        public Token[] FindSynonyms(Token[] tokens, SynonymFinderLanguage language)
        {
            if (!Languages.TryGetValue(language, out var _))
                return tokens;
            for (int x = 0; x < tokens.Length; ++x)
            {
                tokens[x].NormalizedValue = FindSynonym(tokens[x].NormalizedValue, language);
            }
            return tokens;
        }
    }
}