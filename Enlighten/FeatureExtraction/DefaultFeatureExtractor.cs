using Enlighten.FeatureExtraction.Enum;
using Enlighten.FeatureExtraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.FeatureExtraction
{
    /// <summary>
    /// Class handles basic text
    /// </summary>
    public class DefaultFeatureExtractor : IFeatureExtractor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultFeatureExtractor"/> class.
        /// </summary>
        /// <param name="summarizers">The summarizers.</param>
        public DefaultFeatureExtractor(IEnumerable<IFeatureExtractorLanguage> summarizers)
        {
            Extractors = summarizers.Where(x => x.GetType().Assembly != typeof(DefaultFeatureExtractor).Assembly).ToDictionary(x => x.Name);
            foreach (var Extractor in summarizers.Where(x => x.GetType().Assembly == typeof(DefaultFeatureExtractor).Assembly
                && !Extractors.ContainsKey(x.Name)))
            {
                Extractors.Add(Extractor.Name, Extractor);
            }
        }

        /// <summary>
        /// Gets the summarizers.
        /// </summary>
        /// <value>The summarizers.</value>
        public Dictionary<string, IFeatureExtractorLanguage> Extractors { get; }

        /// <summary>
        /// Extracts features from the doc specified.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <param name="docs">The docs to use to compare.</param>
        /// <param name="featureCount">The number of features/terms to return.</param>
        /// <param name="language">The language/extraction algorithm to use.</param>
        /// <returns>The important features/terms of the doc.</returns>
        public string[] Extract(string doc, string[] docs, int featureCount, FeatureExtractionType language)
        {
            if (!Extractors.TryGetValue(language, out var Extractor))
                return Array.Empty<string>();
            return Extractor.Extract(doc, docs, featureCount);
        }
    }
}