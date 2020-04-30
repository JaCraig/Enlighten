namespace Enlighten.POSTagger.Taggers
{
    /// <summary>
    /// Lexicon rule
    /// </summary>
    public class LexiconRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LexiconRule"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="tag">The tag.</param>
        public LexiconRule(string value, string tag)
        {
            if (value.EndsWith('-'))
            {
                Blocked = true;
                value = value.Substring(0, value.Length - 1);
            }
            Value = value;
            Tag = tag;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LexiconRule"/> is blocked.
        /// </summary>
        /// <value><c>true</c> if blocked; otherwise, <c>false</c>.</value>
        public bool Blocked { get; set; }

        /// <summary>
        /// Gets or sets the tag.
        /// </summary>
        /// <value>The tag.</value>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }
    }
}