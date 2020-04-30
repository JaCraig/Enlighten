using Enlighten.SynonymFinder.Enum;

namespace Enlighten.SynonymFinder.Interfaces
{
    public interface ISynonymFinder
    {
        /// <summary>
        /// Finds the synonym if it exists.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns>The synonym or the input if it doesn't exist.</returns>
        string FindSynonym(string input, SynonymFinderLanguage language);
    }
}