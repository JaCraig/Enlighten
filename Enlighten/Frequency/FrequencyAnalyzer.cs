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
        /// Analyzes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="windowSize">Size of the window.</param>
        /// <returns>The frequency results</returns>
        public FrequencyResults Analyze(IEnumerable<Token> tokens, int windowSize)
        {
            var ReturnValue = new FrequencyResults();
            ReturnValue.WindowSize = windowSize <= 0 ? 1 : windowSize;
            var WordTokens = tokens.Where(x => x.TokenType == TokenType.Word || x.TokenType == TokenType.Abbreviation);
            CalculateWordCount(WordTokens, ReturnValue.WordCount);
            ReturnValue.NumberOfWords = WordTokens.Count();
            ReturnValue.NumberOfTypes = ReturnValue.WordCount.Keys.Count;
            CalculateAverageTypeTokenRatio(ReturnValue, WordTokens.ToArray());
            return ReturnValue;
        }

        /// <summary>
        /// Calculates the average type token ratio.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        private void CalculateAverageTypeTokenRatio(FrequencyResults returnValue, Token[] tokens)
        {
            List<decimal> TTRValues = new List<decimal>();
            for (int x = 0; x < tokens.Length - returnValue.WindowSize; ++x)
            {
                var TempWordCount = new SortedDictionary<string, int>();
                for (int y = 0; y < returnValue.WindowSize; ++y)
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
                TTRValues.Add((decimal)TempWordCount.Keys.Count / returnValue.WindowSize);
            }
            if (TTRValues.Count > 0)
                returnValue.AverageTypeTokenRatio = TTRValues.Average();
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