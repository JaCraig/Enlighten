namespace Enlighten.TextSummarization.Interfaces
{
    /// <summary>
    /// Text summarizer language interface
    /// </summary>
    public interface ITextSummarizerLanguage
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Summarizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="percentage">The percentage.</param>
        /// <returns>The summary</returns>
        string Summarize(string input, float percentage);

        /// <summary>
        /// Summarizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="numberOfSentences">The number of sentences.</param>
        /// <returns>The summary</returns>
        string Summarize(string input, int numberOfSentences);
    }
}