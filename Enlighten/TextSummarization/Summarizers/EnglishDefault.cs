using BigBook;
using Enlighten.Frequency;
using Enlighten.SentenceDetection;
using Enlighten.TextSummarization.Interfaces;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.TextSummarization.Languages
{
    /// <summary>
    /// English default summarizer
    /// </summary>
    /// <seealso cref="ITextSummarizerLanguage"/>
    public class EnglishDefault : ITextSummarizerLanguage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishDefault"/> class.
        /// </summary>
        /// <param name="frequencyAnalyzer">The frequency analyzer.</param>
        /// <exception cref="ArgumentNullException">
        /// tokenizer or stemmer or sentenceDetector or frequencyAnalyzer
        /// </exception>
        public EnglishDefault(FrequencyAnalyzer frequencyAnalyzer)
        {
            FrequencyAnalyzer = frequencyAnalyzer ?? throw new ArgumentNullException(nameof(frequencyAnalyzer));
        }

        /// <summary>
        /// Gets the frequency analyzer.
        /// </summary>
        /// <value>The frequency analyzer.</value>
        public FrequencyAnalyzer FrequencyAnalyzer { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = "default";

        /// <summary>
        /// Summarizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="numberOfSentences">The number of sentences.</param>
        /// <returns>The summary</returns>
        public Document Summarize(Document input, int numberOfSentences)
        {
            if (input is null)
                return input!;
            List<Tuple<Sentence, double, int>> SentenceScoresFinal = FindSentences(input);
            return GetFinalDocument(input, SentenceScoresFinal, numberOfSentences);
        }

        /// <summary>
        /// Summarizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="percentage">The percentage.</param>
        /// <returns>The summary</returns>
        public Document Summarize(Document input, float percentage)
        {
            if (input is null)
                return input!;
            List<Tuple<Sentence, double, int>> SentenceScoresFinal = FindSentences(input);
            int NumberOfSentences = (int)Math.Round(SentenceScoresFinal.Count * percentage, MidpointRounding.AwayFromZero);
            return GetFinalDocument(input, SentenceScoresFinal, NumberOfSentences);
        }

        /// <summary>
        /// Gets the final document.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="SentenceScoresFinal">The sentence scores final.</param>
        /// <param name="NumberOfSentences">The number of sentences.</param>
        /// <returns>The final document</returns>
        private static Document GetFinalDocument(Document input, List<Tuple<Sentence, double, int>> SentenceScoresFinal, int NumberOfSentences)
        {
            if (NumberOfSentences <= 0)
                NumberOfSentences = 1;
            var FinalSentences = SentenceScoresFinal.OrderByDescending(x => x.Item2).Take(NumberOfSentences).OrderBy(x => x.Item3).Select(x => x.Item1).ToArray();
            var FinalTokens = FinalSentences.SelectMany(x => x.Tokens).ToArray();
            var FinalText = FinalTokens.Select(x => x.Value).ToString(x => x, string.Empty);
            return new Document(FinalSentences, FinalTokens, FinalText, input.FeatureExtractor, input.TextSummarizer, input.Tokenizer, input.TokenizerLanguage);
        }

        /// <summary>
        /// Finds the sentence frequencies.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="sentences">The sentences.</param>
        /// <returns>The sentence frequencies.</returns>
        private Dictionary<string, int> FindSentenceFrequencies(Token[] tokens, Sentence[] sentences)
        {
            var ReturnValue = new Dictionary<string, int>();
            for (int x = 0; x < tokens.Length; ++x)
            {
                var Token = tokens[x];
                var Key = Token.StemmedValue?.ToLower() ?? string.Empty;
                if (!ReturnValue.TryGetValue(Key, out var _))
                {
                    ReturnValue.Add(Key, 0);
                    for (int y = 0; y < sentences.Length; ++y)
                    {
                        if (sentences[y].Tokens.Any(z => z.StemmedValue?.ToLower() == Key))
                        {
                            ReturnValue[Key]++;
                        }
                    }
                }
            }
            return ReturnValue;
        }

        /// <summary>
        /// Finds the sentences with the highest TF-IDF score
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The sentences</returns>
        private List<Tuple<Sentence, double, int>> FindSentences(Document input)
        {
            var WordTokens = input.Tokens.Where(x => (x.TokenType == TokenType.Abbreviation || x.TokenType == TokenType.Word) && !x.StopWord).ToArray();

            var DocumentTotalCount = WordTokens.Length;
            var DocumentFrequencies = FrequencyAnalyzer.Analyze(input.Tokens, DocumentTotalCount);

            var SentenceFrequency = FindSentenceFrequencies(WordTokens, input.Sentences);

            List<double> SentenceScore = new List<double>();

            for (int x = 0; x < input.Sentences.Length; ++x)
            {
                SentenceScore.Add(0);
                WordTokens = input.Sentences[x].Tokens.Where(x => (x.TokenType == TokenType.Abbreviation || x.TokenType == TokenType.Word) && !x.StopWord).ToArray();
                var SentenceTotalTokens = WordTokens.Length;
                var TermFrequencies = FrequencyAnalyzer.Analyze(WordTokens, SentenceTotalTokens);

                foreach (var Key in TermFrequencies.WordCount.Keys)
                {
                    double TermFrequency = TermFrequencies.TermFrequency[Key];

                    double InverseDocumentFrequency = Math.Log(input.Sentences.Length / (1 + (double)SentenceFrequency[Key]));
                    SentenceScore[x] += (TermFrequency * InverseDocumentFrequency);
                }
            }

            List<Tuple<Sentence, double, int>> SentenceScoresFinal = new List<Tuple<Sentence, double, int>>();
            for (int x = 0; x < input.Sentences.Length; ++x)
            {
                SentenceScoresFinal.Add(new Tuple<Sentence, double, int>(input.Sentences[x], SentenceScore[x], x));
            }

            return SentenceScoresFinal;
        }
    }
}