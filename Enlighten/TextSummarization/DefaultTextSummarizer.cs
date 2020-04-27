using Enlighten.TextSummarization.Enum;
using Enlighten.TextSummarization.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.TextSummarization
{
    /// <summary>
    /// Class handles basic text
    /// </summary>
    public class DefaultTextSummarizer : ITextSummarizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTextSummarizer"/> class.
        /// </summary>
        /// <param name="summarizers">The summarizers.</param>
        public DefaultTextSummarizer(IEnumerable<ITextSummarizerLanguage> summarizers)
        {
            Summarizers = summarizers.Where(x => x.GetType().Assembly != typeof(DefaultTextSummarizer).Assembly).ToDictionary(x => x.Name);
            foreach (var Summarizer in summarizers.Where(x => x.GetType().Assembly == typeof(DefaultTextSummarizer).Assembly
                && !Summarizers.ContainsKey(x.Name)))
            {
                Summarizers.Add(Summarizer.Name, Summarizer);
            }
        }

        /// <summary>
        /// Gets the summarizers.
        /// </summary>
        /// <value>The summarizers.</value>
        public Dictionary<string, ITextSummarizerLanguage> Summarizers { get; }

        /// <summary>
        /// Summarizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="sentenceCount">The number of sentences to have in the final text.</param>
        /// <param name="language">The language.</param>
        /// <returns>The summarized text</returns>
        public string Summarize(string text, int sentenceCount, TextSummarizationLanguage language)
        {
            if (!Summarizers.TryGetValue(language, out var Summarizer))
                return text;
            return Summarizer.Summarize(text, sentenceCount);
        }

        /// <summary>
        /// Summarizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="percentage">The percentage of sentences to have in the final text.</param>
        /// <param name="language">The language.</param>
        /// <returns>The summarized text</returns>
        public string Summarize(string text, float percentage, TextSummarizationLanguage language)
        {
            if (!Summarizers.TryGetValue(language, out var Summarizer))
                return text;
            return Summarizer.Summarize(text, percentage);
        }
    }
}