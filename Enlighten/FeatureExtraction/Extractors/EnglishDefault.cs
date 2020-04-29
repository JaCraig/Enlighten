using BigBook;
using Enlighten.FeatureExtraction.Interfaces;
using Enlighten.Frequency;
using Enlighten.Stemmer.Enums;
using Enlighten.Stemmer.Interfaces;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using Enlighten.Tokenizer.Languages.English.Enums;
using System;
using System.Linq;

namespace Enlighten.FeatureExtraction.Extractors
{
    /// <summary>
    /// English default extractor (uses TF-IDF)
    /// </summary>
    /// <seealso cref="IFeatureExtractorLanguage"/>
    public class EnglishDefault : IFeatureExtractorLanguage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishDefault"/> class.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <param name="stemmer">The stemmer.</param>
        /// <param name="frequencyAnalyzer">The frequency analyzer.</param>
        public EnglishDefault(ITokenizer tokenizer, IStemmer stemmer, FrequencyAnalyzer frequencyAnalyzer)
        {
            FrequencyAnalyzer = frequencyAnalyzer;
            Stemmer = stemmer;
            Tokenizer = tokenizer;
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
        public string Name => "default";

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
        /// Extracts features from the doc specified.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="docs">The docs to use to compare.</param>
        /// <param name="featureCount">The number of features/terms to return.</param>
        /// <returns>The important features/terms of the doc.</returns>
        public string[] Extract(string doc, string[] docs, int featureCount)
        {
            var DocsCopy = docs.ToArray();
            var Tokens = Tokenizer.Tokenize(doc, TokenizerLanguage.EnglishRuleBased);
            Tokens = Tokenizer.MarkStopWords(Tokens, TokenizerLanguage.EnglishRuleBased);
            Tokens = Stemmer.Stem(Tokens, StemmerLanguage.EnglishPorter2);

            for (int i = 0; i < docs.Length; i++)
            {
                var TempDoc = DocsCopy[i];
                var TempTokens = Tokenizer.Tokenize(TempDoc, TokenizerLanguage.EnglishRuleBased);
                TempTokens = Stemmer.Stem(TempTokens, StemmerLanguage.EnglishPorter2).ForEach(x => { x.Value = x.StemmedValue; }).ToArray();
                TempTokens = Tokenizer.MarkStopWords(TempTokens, TokenizerLanguage.EnglishRuleBased).Where(x => !x.StopWord).ToArray();
                DocsCopy[i] = Tokenizer.Detokenize(TempTokens, TokenizerLanguage.EnglishRuleBased);
            }

            var WordTokens = Tokens.Where(x => (x.TokenType == TokenType.Abbreviation || x.TokenType == TokenType.Word) && !x.StopWord).ToArray();

            var DocumentFrequencies = FrequencyAnalyzer.Analyze(WordTokens, WordTokens.Length);

            return WordTokens.Distinct((x, y) => x.StemmedValue == y.StemmedValue).OrderByDescending(WordToken =>
                {
                    var TermFrequency = DocumentFrequencies.TermFrequency[WordToken.StemmedValue.ToLower()];
                    var InverseDocFrequency = Math.Log(DocsCopy.Length / (1 + DocsCopy.Where(x => x.Contains(WordToken.Value)).Count()));
                    return TermFrequency * InverseDocFrequency;
                })
                .Select(x => x.Value)
                .Take(featureCount)
                .ToArray();
        }
    }
}