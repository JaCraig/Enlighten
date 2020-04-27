using System.Collections.Generic;

namespace Enlighten.Frequency
{
    /// <summary>
    /// Frequency results
    /// </summary>
    public class FrequencyResults
    {
        /// <summary>
        /// Gets the average type token ratio.
        /// </summary>
        /// <value>The average type token ratio.</value>
        public decimal AverageTypeTokenRatio { get; internal set; }

        /// <summary>
        /// Gets the number of types (distinct words).
        /// </summary>
        /// <value>The number of types (distinct words).</value>
        public int NumberOfTypes { get; internal set; }

        /// <summary>
        /// Gets the number of words.
        /// </summary>
        /// <value>The number of words.</value>
        public int NumberOfWords { get; internal set; }

        /// <summary>
        /// Gets the type token ratio (Types/Words).
        /// </summary>
        /// <value>The type token ratio (Types/Words).</value>
        public decimal TypeTokenRatio => NumberOfWords == 0 ? 0 : (decimal)NumberOfTypes / NumberOfWords;

        /// <summary>
        /// Gets the size of the window.
        /// </summary>
        /// <value>The size of the window.</value>
        public int WindowSize { get; internal set; }

        /// <summary>
        /// Gets the word count.
        /// </summary>
        /// <value>The word count.</value>
        public SortedDictionary<string, int> WordCount { get; } = new SortedDictionary<string, int>();
    }
}