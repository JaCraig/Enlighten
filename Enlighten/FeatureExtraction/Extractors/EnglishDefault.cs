using BigBook;
using Enlighten.FeatureExtraction.Interfaces;
using Enlighten.Frequency;
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
        /// <param name="frequencyAnalyzer">The frequency analyzer.</param>
        public EnglishDefault(FrequencyAnalyzer frequencyAnalyzer)
        {
            FrequencyAnalyzer = frequencyAnalyzer;
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
        /// Extracts features from the doc specified.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="docs">The docs to use to compare.</param>
        /// <param name="featureCount">The number of features/terms to return.</param>
        /// <returns>The important features/terms of the doc.</returns>
        public string[] Extract(Document doc, Document[] docs, int featureCount)
        {
            var WordTokens = doc.Tokens.Where(x => (x.TokenType == TokenType.Abbreviation || x.TokenType == TokenType.Word) && !x.StopWord).ToArray();

            var DocumentFrequencies = FrequencyAnalyzer.Analyze(WordTokens, WordTokens.Length);

            return WordTokens.Distinct((x, y) => x.StemmedValue == y.StemmedValue).OrderByDescending(WordToken =>
                {
                    var TermFrequency = DocumentFrequencies.TermFrequency[WordToken.StemmedValue.ToLower()];
                    var InverseDocFrequency = Math.Log(docs.Length / (1 + docs.Count(x => x.Tokens.Any(token => token.StemmedValue == WordToken.StemmedValue))));
                    return TermFrequency * InverseDocFrequency;
                })
                .Select(x => x.Value)
                .Take(featureCount)
                .ToArray();
        }
    }
}