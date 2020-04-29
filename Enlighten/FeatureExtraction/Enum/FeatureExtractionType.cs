using BigBook.Patterns.BaseClasses;

namespace Enlighten.FeatureExtraction.Enum
{
    /// <summary>
    /// Feature extraction type
    /// </summary>
    /// <seealso cref="StringEnumBaseClass{FeatureExtractionType}"/>
    public class FeatureExtractionType : StringEnumBaseClass<FeatureExtractionType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureExtractionType"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public FeatureExtractionType(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeatureExtractionType"/> class.
        /// </summary>
        public FeatureExtractionType() : base(string.Empty)
        {
        }

        /// <summary>
        /// Gets the default for english. (Uses TF-IDF)
        /// </summary>
        /// <value>The default.</value>
        public static FeatureExtractionType EnglishDefault { get; } = new FeatureExtractionType("default");
    }
}