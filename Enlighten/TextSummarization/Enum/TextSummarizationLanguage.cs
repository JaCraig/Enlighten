using BigBook.Patterns.BaseClasses;

namespace Enlighten.TextSummarization.Enum
{
    /// <summary>
    /// Text summarization language
    /// </summary>
    /// <seealso cref="StringEnumBaseClass{TextSummarizationLanguage}"/>
    public class TextSummarizationLanguage : StringEnumBaseClass<TextSummarizationLanguage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextSummarizationLanguage"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public TextSummarizationLanguage(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextSummarizationLanguage"/> class.
        /// </summary>
        public TextSummarizationLanguage() : base(string.Empty)
        {
        }

        /// <summary>
        /// Gets the default for english. (Uses TF-IDF)
        /// </summary>
        /// <value>The default.</value>
        public static TextSummarizationLanguage EnglishDefault { get; } = new TextSummarizationLanguage("default");
    }
}