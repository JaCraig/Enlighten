namespace Enlighten.SynonymFinder.Interfaces
{
    /// <summary>
    /// Synonym finder language interface
    /// </summary>
    public interface ISynonymFinderLanguage
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Finds the synonym if it exists.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The synonym or the input if it doesn't exist.</returns>
        string FindSynonym(string input);
    }
}