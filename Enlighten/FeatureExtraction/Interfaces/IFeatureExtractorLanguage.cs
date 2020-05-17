namespace Enlighten.FeatureExtraction.Interfaces
{
    /// <summary>
    /// Feature extractor language interface
    /// </summary>
    public interface IFeatureExtractorLanguage
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Extracts features from the doc specified.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="docs">The docs to use to compare.</param>
        /// <param name="featureCount">The number of features/terms to return.</param>
        /// <returns>The important features/terms of the doc.</returns>
        string[] Extract(Document doc, Document[] docs, int featureCount);
    }
}