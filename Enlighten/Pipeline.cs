using Enlighten.Enums;
using Enlighten.FeatureExtraction.Interfaces;
using Enlighten.Normalizer.Interfaces;
using Enlighten.POSTagger.Enum;
using Enlighten.POSTagger.Interfaces;
using Enlighten.SentenceDetection.Enum;
using Enlighten.SentenceDetection.Interfaces;
using Enlighten.Stemmer.Enums;
using Enlighten.Stemmer.Interfaces;
using Enlighten.StopWords.Enum;
using Enlighten.StopWords.Interfaces;
using Enlighten.TextSummarization.Interfaces;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using System;

namespace Enlighten
{
    /// <summary>
    /// Main pipeline class
    /// </summary>
    public class Pipeline
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pipeline"/> class.
        /// </summary>
        /// <param name="normalizerManager">The normalizer manager.</param>
        /// <param name="pOSTagger">The p os tagger.</param>
        /// <param name="sentenceDetector">The sentence detector.</param>
        /// <param name="stemmer">The stemmer.</param>
        /// <param name="stopWordsManager">The stop words manager.</param>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <param name="featureExtractor">The feature extractor.</param>
        /// <param name="textSummarizer">The text summarizer.</param>
        /// <exception cref="ArgumentNullException">
        /// normalizerManager or pOSTagger or sentenceDetector or stemmer or stopWordsManager or
        /// tokenizer or featureExtractor or textSummarizer
        /// </exception>
        public Pipeline(
            INormalizerManager normalizerManager,
            IPOSTagger pOSTagger,
            ISentenceDetector sentenceDetector,
            IStemmer stemmer,
            IStopWordsManager stopWordsManager,
            ITokenizer tokenizer,
            IFeatureExtractor featureExtractor,
            ITextSummarizer textSummarizer)
        {
            NormalizerManager = normalizerManager ?? throw new ArgumentNullException(nameof(normalizerManager));
            POSTagger = pOSTagger ?? throw new ArgumentNullException(nameof(pOSTagger));
            SentenceDetector = sentenceDetector ?? throw new ArgumentNullException(nameof(sentenceDetector));
            Stemmer = stemmer ?? throw new ArgumentNullException(nameof(stemmer));
            StopWordsManager = stopWordsManager ?? throw new ArgumentNullException(nameof(stopWordsManager));
            Tokenizer = tokenizer ?? throw new ArgumentNullException(nameof(tokenizer));
            FeatureExtractor = featureExtractor ?? throw new ArgumentNullException(nameof(featureExtractor));
            TextSummarizer = textSummarizer ?? throw new ArgumentNullException(nameof(textSummarizer));
            SetLanguage(Languages.English);
        }

        /// <summary>
        /// Gets the feature extractor.
        /// </summary>
        /// <value>The feature extractor.</value>
        private IFeatureExtractor FeatureExtractor { get; }

        /// <summary>
        /// Gets the normalizer manager.
        /// </summary>
        /// <value>The normalizer manager.</value>
        private INormalizerManager NormalizerManager { get; }

        /// <summary>
        /// Gets the position tagger.
        /// </summary>
        /// <value>The position tagger.</value>
        private IPOSTagger POSTagger { get; }

        /// <summary>
        /// Gets or sets the position tagger language.
        /// </summary>
        /// <value>The position tagger language.</value>
        private POSTaggerLanguage POSTaggerLanguage { get; set; }

        /// <summary>
        /// Gets the sentence detector.
        /// </summary>
        /// <value>The sentence detector.</value>
        private ISentenceDetector SentenceDetector { get; }

        /// <summary>
        /// Gets or sets the sentence detector language.
        /// </summary>
        /// <value>The sentence detector language.</value>
        private SentenceDetectorLanguage SentenceDetectorLanguage { get; set; }

        /// <summary>
        /// Gets the stemmer.
        /// </summary>
        /// <value>The stemmer.</value>
        private IStemmer Stemmer { get; }

        /// <summary>
        /// Gets or sets the stemmer language.
        /// </summary>
        /// <value>The stemmer language.</value>
        private StemmerLanguage StemmerLanguage { get; set; }

        /// <summary>
        /// Gets or sets the stop words language.
        /// </summary>
        /// <value>The stop words language.</value>
        private StopWordsLanguage StopWordsLanguage { get; set; }

        /// <summary>
        /// Gets the stop words manager.
        /// </summary>
        /// <value>The stop words manager.</value>
        private IStopWordsManager StopWordsManager { get; }

        /// <summary>
        /// Gets the text summarizer.
        /// </summary>
        /// <value>The text summarizer.</value>
        private ITextSummarizer TextSummarizer { get; }

        /// <summary>
        /// Gets the tokenizer.
        /// </summary>
        /// <value>The tokenizer.</value>
        private ITokenizer Tokenizer { get; }

        /// <summary>
        /// Gets or sets the tokenizer language.
        /// </summary>
        /// <value>The tokenizer language.</value>
        private TokenizerLanguage TokenizerLanguage { get; set; }

        /// <summary>
        /// Processes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The resulting document object.</returns>
        public Document Process(string text)
        {
            var TempText = NormalizerManager.Normalize(text);
            var Tokens = Tokenizer.Tokenize(TempText, TokenizerLanguage);
            Tokens = NormalizerManager.Normalize(Tokens);
            Tokens = Stemmer.Stem(Tokens, StemmerLanguage);
            Tokens = StopWordsManager.MarkStopWords(Tokens, StopWordsLanguage);

            var Sentences = SentenceDetector.Detect(Tokens, SentenceDetectorLanguage);
            for (int x = 0; x < Sentences.Length; ++x)
            {
                var Sentence = Sentences[x];
                Sentence.Tokens = POSTagger.Tag(Sentence.Tokens, POSTaggerLanguage);
            }

            return new Document(Sentences, Tokens, text, FeatureExtractor, TextSummarizer, Tokenizer, TokenizerLanguage);
        }

        /// <summary>
        /// Sets the language.
        /// </summary>
        /// <param name="languages">The languages.</param>
        /// <returns>This.</returns>
        public Pipeline SetLanguage(Languages languages)
        {
            if (languages == Languages.English)
            {
                With(POSTaggerLanguage.BrillTagger)
                    .With(SentenceDetectorLanguage.Default)
                    .With(TokenizerLanguage.EnglishRuleBased)
                    .With(StopWordsLanguage.EnglishSpacy)
                    .With(StemmerLanguage.EnglishPorter2);
            }
            return this;
        }

        /// <summary>
        /// Withes the specified position tagger.
        /// </summary>
        /// <param name="posTagger">The position tagger.</param>
        /// <returns>This.</returns>
        /// <exception cref="ArgumentNullException">posTagger</exception>
        public Pipeline With(POSTaggerLanguage posTagger)
        {
            POSTaggerLanguage = posTagger ?? throw new ArgumentNullException(nameof(posTagger));
            return this;
        }

        /// <summary>
        /// Withes the specified sentence detector.
        /// </summary>
        /// <param name="sentenceDetector">The sentence detector.</param>
        /// <returns>This.</returns>
        /// <exception cref="ArgumentNullException">sentenceDetector</exception>
        public Pipeline With(SentenceDetectorLanguage sentenceDetector)
        {
            SentenceDetectorLanguage = sentenceDetector ?? throw new ArgumentNullException(nameof(sentenceDetector));
            return this;
        }

        /// <summary>
        /// Withes the specified stemmer.
        /// </summary>
        /// <param name="stemmer">The stemmer.</param>
        /// <returns>This.</returns>
        /// <exception cref="ArgumentNullException">stemmer</exception>
        public Pipeline With(StemmerLanguage stemmer)
        {
            StemmerLanguage = stemmer ?? throw new ArgumentNullException(nameof(stemmer));
            return this;
        }

        /// <summary>
        /// Withes the specified stop words.
        /// </summary>
        /// <param name="stopWords">The stop words.</param>
        /// <returns>This.</returns>
        /// <exception cref="ArgumentNullException">stopWords</exception>
        public Pipeline With(StopWordsLanguage stopWords)
        {
            StopWordsLanguage = stopWords ?? throw new ArgumentNullException(nameof(stopWords));
            return this;
        }

        /// <summary>
        /// Withes the specified tokenizer language.
        /// </summary>
        /// <param name="tokenizer">The tokenizer to use.</param>
        /// <returns>This.</returns>
        /// <exception cref="ArgumentNullException">tokenizerLanguage</exception>
        public Pipeline With(TokenizerLanguage tokenizer)
        {
            TokenizerLanguage = tokenizer ?? throw new ArgumentNullException(nameof(tokenizer));
            return this;
        }
    }
}