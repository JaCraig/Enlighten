using Enlighten.FeatureExtraction.Enum;

namespace Enlighten.FeatureExtraction.Interfaces
{
    /// <summary>
    /// Feature extractor interface
    /// </summary>
    public interface IFeatureExtractor
    {
        /// <summary>
        /// Extracts features from the doc specified.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="docs">The docs to use to compare.</param>
        /// <param name="featureCount">The number of features/terms to return.</param>
        /// <param name="language">The language/extraction algorithm to use.</param>
        /// <returns>The important features/terms of the doc.</returns>
        string[] Extract(string doc, string[] docs, int featureCount, FeatureExtractionType language);
    }
}