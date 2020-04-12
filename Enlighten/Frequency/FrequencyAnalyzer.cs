using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.Frequency
{
    /// <summary>
    /// Frequency Analyzer
    /// </summary>
    public class FrequencyAnalyzer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Frequency"/> class.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="windowSize">Size of the window.</param>
        public FrequencyAnalyzer(IEnumerable<Token> tokens, int windowSize)
        {
            WindowSize = windowSize <= 0 ? 1 : windowSize;
            var WordTokens = tokens.Where(x => x.TokenType == TokenType.Word);
            WordCount = new SortedDictionary<string, int>();
            CalculateWordCount(WordTokens, WordCount);
            NumberOfWords = WordTokens.Count();
            NumberOfTypes = WordCount.Keys.Count;
            CalculateAverageTypeTokenRatio(WordTokens.ToArray());
        }

        /// <summary>
        /// Gets the average type token ratio.
        /// </summary>
        /// <value>The average type token ratio.</value>
        public decimal AverageTypeTokenRatio { get; private set; }

        /// <summary>
        /// Gets the number of types (distinct words).
        /// </summary>
        /// <value>The number of types (distinct words).</value>
        public int NumberOfTypes { get; }

        /// <summary>
        /// Gets the number of words.
        /// </summary>
        /// <value>The number of words.</value>
        public int NumberOfWords { get; }

        /// <summary>
        /// Gets the type token ratio (Types/Words).
        /// </summary>
        /// <value>The type token ratio (Types/Words).</value>
        public decimal TypeTokenRatio => NumberOfWords == 0 ? 0 : (decimal)NumberOfTypes / NumberOfWords;

        /// <summary>
        /// Gets the size of the window.
        /// </summary>
        /// <value>The size of the window.</value>
        public int WindowSize { get; }

        /// <summary>
        /// Gets the word count.
        /// </summary>
        /// <value>The word count.</value>
        public SortedDictionary<string, int> WordCount { get; }

        /// <summary>
        /// Calculates the average type token ratio.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        private void CalculateAverageTypeTokenRatio(Token[] tokens)
        {
            List<decimal> TTRValues = new List<decimal>();
            for (int x = 0; x < tokens.Length - WindowSize; ++x)
            {
                var TempWordCount = new SortedDictionary<string, int>();
                for (int y = 0; y < WindowSize; ++y)
                {
                    var TempWord = tokens[x + y].Value.ToLower();
                    if (TempWordCount.ContainsKey(TempWord))
                    {
                        ++TempWordCount[TempWord];
                    }
                    else
                    {
                        TempWordCount.Add(TempWord, 1);
                    }
                }
                TTRValues.Add((decimal)TempWordCount.Keys.Count / WindowSize);
            }
            AverageTypeTokenRatio = TTRValues.Average();
        }

        /// <summary>
        /// Calculates the word count.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        private void CalculateWordCount(IEnumerable<Token> tokens, SortedDictionary<string, int> wordCount)
        {
            foreach (var CurrentToken in tokens)
            {
                var CurrentWord = CurrentToken.Value.ToLower();
                if (wordCount.ContainsKey(CurrentWord))
                {
                    ++wordCount[CurrentWord];
                }
                else
                {
                    wordCount.Add(CurrentWord, 1);
                }
            }
        }
    }
}