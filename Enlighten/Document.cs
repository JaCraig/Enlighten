using Enlighten.FeatureExtraction.Enum;
using Enlighten.FeatureExtraction.Interfaces;
using Enlighten.SentenceDetection;
using Enlighten.TextSummarization.Enum;
using Enlighten.TextSummarization.Interfaces;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using System;

namespace Enlighten
{
    /// <summary>
    /// Document information holder
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Document"/> class.
        /// </summary>
        /// <param name="sentences">The sentences.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="originalText">The original text.</param>
        /// <param name="featureExtractor">The feature extractor.</param>
        /// <param name="textSummarizer">The text summarizer.</param>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <param name="tokenizerLanguage">The tokenizer language.</param>
        public Document(
            Sentence[] sentences,
            Token[] tokens,
            string originalText,
            IFeatureExtractor featureExtractor,
            ITextSummarizer textSummarizer,
            ITokenizer tokenizer,
            TokenizerLanguage tokenizerLanguage)
        {
            Sentences = sentences;
            OriginalText = originalText;
            Tokens = tokens;
            TextSummarizer = textSummarizer;
            FeatureExtractor = featureExtractor;
            TokenizerLanguage = tokenizerLanguage;
            Tokenizer = tokenizer;
            ID = Guid.NewGuid();
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid ID { get; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string OriginalText { get; }

        /// <summary>
        /// Gets the sentences.
        /// </summary>
        /// <value>The sentences.</value>
        public Sentence[] Sentences { get; }

        /// <summary>
        /// Gets the tokens.
        /// </summary>
        /// <value>The tokens.</value>
        public Token[] Tokens { get; }

        /// <summary>
        /// Gets the feature extractor.
        /// </summary>
        /// <value>The feature extractor.</value>
        internal IFeatureExtractor FeatureExtractor { get; }

        /// <summary>
        /// Gets the text summarizer.
        /// </summary>
        /// <value>The text summarizer.</value>
        internal ITextSummarizer TextSummarizer { get; }

        /// <summary>
        /// Gets the tokenizer.
        /// </summary>
        /// <value>The tokenizer.</value>
        internal ITokenizer Tokenizer { get; }

        /// <summary>
        /// Gets the tokenizer language.
        /// </summary>
        /// <value>The tokenizer language.</value>
        internal TokenizerLanguage TokenizerLanguage { get; }

        /// <summary>
        /// Detokenizes this instance.
        /// </summary>
        /// <returns>The string version of this instance.</returns>
        public string Detokenize()
        {
            return Tokenizer.Detokenize(Sentences, TokenizerLanguage);
        }

        /// <summary>
        /// Gets the topics important in this document
        /// </summary>
        /// <param name="comparisonDocuments">The comparison documents.</param>
        /// <param name="featureCount">The feature count.</param>
        /// <param name="featureExtractionType">Type of the feature extraction.</param>
        /// <returns>The features/topics important in this document.</returns>
        public string[] GetFeatures(Document[] comparisonDocuments, int featureCount, FeatureExtractionType featureExtractionType)
        {
            return FeatureExtractor.Extract(this, comparisonDocuments, featureCount, featureExtractionType);
        }

        /// <summary>
        /// Summarizes the specified sentence count.
        /// </summary>
        /// <param name="sentenceCount">The sentence count.</param>
        /// <param name="textSummarizationLanguage">The text summarization language.</param>
        /// <returns>The summarized text</returns>
        public Document Summarize(int sentenceCount, TextSummarizationLanguage textSummarizationLanguage)
        {
            return TextSummarizer.Summarize(this, sentenceCount, textSummarizationLanguage);
        }

        /// <summary>
        /// Summarizes the specified percentage.
        /// </summary>
        /// <param name="percentage">The percentage of the text to use.</param>
        /// <param name="textSummarizationLanguage">The text summarization language.</param>
        /// <returns>The summarized text</returns>
        public Document Summarize(float percentage, TextSummarizationLanguage textSummarizationLanguage)
        {
            return TextSummarizer.Summarize(this, percentage, textSummarizationLanguage);
        }
    }
}