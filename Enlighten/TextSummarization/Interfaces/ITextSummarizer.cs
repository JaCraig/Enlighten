using Enlighten.TextSummarization.Enum;

namespace Enlighten.TextSummarization.Interfaces
{
    /// <summary>
    /// Text summarizer interface
    /// </summary>
    public interface ITextSummarizer
    {
        /// <summary>
        /// Summarizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="sentenceCount">The number of sentences to have in the final text.</param>
        /// <param name="language">The language.</param>
        /// <returns>The summarized text</returns>
        string Summarize(string text, int sentenceCount, TextSummarizationLanguage language);

        /// <summary>
        /// Summarizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="percentage">The percentage of sentences to have in the final text.</param>
        /// <param name="language">The language.</param>
        /// <returns>The summarized text</returns>
        string Summarize(string text, float percentage, TextSummarizationLanguage language);
    }
}