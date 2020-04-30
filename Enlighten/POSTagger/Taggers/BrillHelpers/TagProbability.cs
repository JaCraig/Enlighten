namespace Enlighten.POSTagger.Taggers
{
    /// <summary>
    /// Tag probability
    /// </summary>
    public class TagProbability
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagProbability"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="norm">The norm.</param>
        /// <param name="confidence">The confidence.</param>
        /// <param name="blocked">if set to <c>true</c> [blocked].</param>
        public TagProbability(string tag, string norm, double confidence, bool blocked)
        {
            Tag = tag ?? "NN";
            Norm = norm;
            Confidence = confidence;
            Blocked = blocked;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="TagProbability"/> is blocked.
        /// </summary>
        /// <value><c>true</c> if blocked; otherwise, <c>false</c>.</value>
        public bool Blocked { get; set; }

        /// <summary>
        /// Gets the confidence.
        /// </summary>
        /// <value>The confidence.</value>
        public double Confidence { get; set; }

        /// <summary>
        /// Gets the norm.
        /// </summary>
        /// <value>The norm.</value>
        public string Norm { get; set; }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <value>The tag.</value>
        public string Tag { get; set; }
    }
}