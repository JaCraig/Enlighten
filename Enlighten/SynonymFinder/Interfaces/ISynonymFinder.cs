using Enlighten.SynonymFinder.Enum;
using Enlighten.Tokenizer;

namespace Enlighten.SynonymFinder.Interfaces
{
    /// <summary>
    /// Synonym finder
    /// </summary>
    public interface ISynonymFinder
    {
        /// <summary>
        /// Finds the synonym if it exists.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns>The synonym or the input if it doesn't exist.</returns>
        string FindSynonym(string input, SynonymFinderLanguage language);

        /// <summary>
        /// Finds the synonyms and replaces the text.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The tokens</returns>
        Token[] FindSynonyms(Token[] tokens, SynonymFinderLanguage language);
    }
}