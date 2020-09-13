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
            var WordTokens = tokens.Where(x => x.TokenType == TokenType.Word || x.TokenType == TokenType.Abbreviation).ToArray();
            CalculateWordCount(WordTokens, ReturnValue.WordCount);
            ReturnValue.NumberOfWords = WordTokens.Length;
            ReturnValue.NumberOfTypes = ReturnValue.WordCount.Keys.Count;
            //CalculateAverageTypeTokenRatio(ReturnValue, WordTokens);
            CalculateTermFrequency(ReturnValue);
            return ReturnValue;
        }

        /// <summary>
        /// Calculates the average type token ratio.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        //private void CalculateAverageTypeTokenRatio(FrequencyResults returnValue, Token[] tokens)
        //{
        //    List<decimal> TTRValues = new List<decimal>();
        //    for (int x = 0; x < tokens.Length - returnValue.WindowSize; ++x)
        //    {
        //        var TempWordCount = new SortedDictionary<string, int>();
        //        for (int y = 0; y < returnValue.WindowSize; ++y)
        //        {
        //            var CurrentToken = tokens[x + y];
        //            var TempWord = (CurrentToken.StemmedValue ?? CurrentToken.Value).ToLower();
        //            if (TempWordCount.ContainsKey(TempWord))
        //            {
        //                ++TempWordCount[TempWord];
        //            }
        //            else
        //            {
        //                TempWordCount.Add(TempWord, 1);
        //            }
        //        }
        //        TTRValues.Add((decimal)TempWordCount.Keys.Count / returnValue.WindowSize);
        //    }
        //    if (TTRValues.Count > 0)
        //        returnValue.AverageTypeTokenRatio = TTRValues.Average();
        //}

        /// <summary>
        /// Calculates the term frequency.
        /// </summary>
        /// <param name="returnValue">The return value.</param>
        private void CalculateTermFrequency(FrequencyResults returnValue)
        {
            foreach (var Key in returnValue.WordCount.Keys)
            {
                returnValue.TermFrequency.Add(Key, returnValue.WordCount[Key] / (double)returnValue.NumberOfWords);
            }
        }

        /// <summary>
        /// Calculates the word count.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        private void CalculateWordCount(IEnumerable<Token> tokens, SortedDictionary<string, int> wordCount)
        {
            foreach (var CurrentToken in tokens)
            {
                var CurrentWord = (CurrentToken.StemmedValue ?? CurrentToken.Value).ToLower();
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