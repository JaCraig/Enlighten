using BigBook;
using Enlighten.Frequency;
using Enlighten.SentenceDetection;
using Enlighten.SentenceDetection.Enum;
using Enlighten.SentenceDetection.Interfaces;
using Enlighten.Stemmer.Enums;
using Enlighten.Stemmer.Interfaces;
using Enlighten.TextSummarization.Interfaces;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
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
        /// <param name="tokenizer">The tokenizer.</param>
        /// <param name="stemmer">The stemmer.</param>
        /// <param name="sentenceDetector">The sentence detector.</param>
        /// <param name="frequencyAnalyzer">The frequency analyzer.</param>
        public EnglishDefault(ITokenizer tokenizer, IStemmer stemmer, ISentenceDetector sentenceDetector, FrequencyAnalyzer frequencyAnalyzer)
        {
            Tokenizer = tokenizer ?? throw new ArgumentNullException(nameof(tokenizer));
            Stemmer = stemmer ?? throw new ArgumentNullException(nameof(stemmer));
            SentenceDetector = sentenceDetector ?? throw new ArgumentNullException(nameof(sentenceDetector));
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
        /// Gets the sentence detector.
        /// </summary>
        /// <value>The sentence detector.</value>
        public ISentenceDetector SentenceDetector { get; }

        /// <summary>
        /// Gets the stemmer.
        /// </summary>
        /// <value>The stemmer.</value>
        public IStemmer Stemmer { get; }

        /// <summary>
        /// Gets the tokenizer.
        /// </summary>
        /// <value>The tokenizer.</value>
        public ITokenizer Tokenizer { get; }

        /// <summary>
        /// Summarizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="numberOfSentences">The number of sentences.</param>
        /// <returns>The summary</returns>
        public string Summarize(string input, int numberOfSentences)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            List<Tuple<Sentence, double, int>> SentenceScoresFinal = FindSentences(input);
            return Tokenizer.Detokenize(SentenceScoresFinal.OrderByDescending(x => x.Item2).Take(numberOfSentences).OrderBy(x => x.Item3).Select(x => x.Item1).ToArray(), TokenizerLanguage.EnglishRuleBased);
        }

        /// <summary>
        /// Summarizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="percentage">The percentage.</param>
        /// <returns>The summary</returns>
        public string Summarize(string input, float percentage)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            List<Tuple<Sentence, double, int>> SentenceScoresFinal = FindSentences(input);
            int NumberOfSentences = (int)Math.Round(SentenceScoresFinal.Count * percentage, MidpointRounding.AwayFromZero);
            return Tokenizer.Detokenize(SentenceScoresFinal.OrderByDescending(x => x.Item2).Take(NumberOfSentences).OrderBy(x => x.Item3).Select(x => x.Item1).ToArray(), TokenizerLanguage.EnglishRuleBased);
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
                var Key = Token.StemmedValue.ToLower();
                if (!ReturnValue.TryGetValue(Key, out var _))
                {
                    ReturnValue.Add(Key, 0);
                    for (int y = 0; y < sentences.Length; ++y)
                    {
                        if (sentences[y].Tokens.Any(z => z.StemmedValue.ToLower() == Key))
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
        private List<Tuple<Sentence, double, int>> FindSentences(string input)
        {
            var Tokens = Tokenizer.Tokenize(input, TokenizerLanguage.EnglishRuleBased);
            Tokens = Tokenizer.MarkStopWords(Tokens, TokenizerLanguage.EnglishRuleBased);
            Tokens = Stemmer.Stem(Tokens, StemmerLanguage.EnglishPorter2);

            var Sentences = SentenceDetector.Detect(Tokens, SentenceDetectorLanguage.Default);

            var WordTokens = Tokens.Where(x => (x.TokenType == TokenType.Abbreviation || x.TokenType == TokenType.Word) && !x.StopWord).ToArray();

            var DocumentTotalCount = WordTokens.Length;
            var DocumentFrequencies = FrequencyAnalyzer.Analyze(Tokens, DocumentTotalCount);

            var SentenceFrequency = FindSentenceFrequencies(WordTokens, Sentences);

            List<double> SentenceScore = new List<double>();

            for (int x = 0; x < Sentences.Length; ++x)
            {
                SentenceScore.Add(0);
                WordTokens = Sentences[x].Tokens.Where(x => (x.TokenType == TokenType.Abbreviation || x.TokenType == TokenType.Word) && !x.StopWord).ToArray();
                var SentenceTotalTokens = WordTokens.Length;
                var TermFrequencies = FrequencyAnalyzer.Analyze(WordTokens, SentenceTotalTokens);

                foreach (var Key in TermFrequencies.WordCount.Keys)
                {
                    double TermFrequency = TermFrequencies.TermFrequency[Key];

                    double InverseDocumentFrequency = Math.Log(Sentences.Length / (1 + (double)SentenceFrequency[Key]));
                    SentenceScore[x] += (TermFrequency * InverseDocumentFrequency);
                }
            }

            List<Tuple<Sentence, double, int>> SentenceScoresFinal = new List<Tuple<Sentence, double, int>>();
            for (int x = 0; x < Sentences.Length; ++x)
            {
                SentenceScoresFinal.Add(new Tuple<Sentence, double, int>(Sentences[x], SentenceScore[x], x));
            }

            return SentenceScoresFinal;
        }
    }
}