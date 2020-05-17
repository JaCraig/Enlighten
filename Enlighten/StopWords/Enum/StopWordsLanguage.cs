using BigBook.Patterns.BaseClasses;

namespace Enlighten.StopWords.Enum
{
    /// <summary>
    /// Stop words language
    /// </summary>
    /// <seealso cref="StringEnumBaseClass{StopWordsLanguage}"/>
    public class StopWordsLanguage : StringEnumBaseClass<StopWordsLanguage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StopWordsLanguage"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public StopWordsLanguage(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StopWordsLanguage"/> class.
        /// </summary>
        public StopWordsLanguage() : base(string.Empty)
        {
        }

        /// <summary>
        /// Gets the english default stop words.
        /// </summary>
        /// <value>The english default stop words.</value>
        public static StopWordsLanguage English { get; } = new StopWordsLanguage("en-us");

        /// <summary>
        /// Gets the english spacy.
        /// </summary>
        /// <value>The english spacy.</value>
        public static StopWordsLanguage EnglishSpacy { get; } = new StopWordsLanguage("en-us_spacy");
    }
}