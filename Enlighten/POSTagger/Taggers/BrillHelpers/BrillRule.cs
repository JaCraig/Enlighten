using Enlighten.POSTagger.Enum;

namespace Enlighten.POSTagger.Taggers
{
    /// <summary>
    /// Brill rule
    /// </summary>
    public class BrillRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrillRule"/> class.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="type">The type.</param>
        /// <param name="c1">The c1.</param>
        /// <param name="c2">The c2.</param>
        /// <param name="c3">The c3.</param>
        /// <param name="secondRun">if set to <c>true</c> [second run].</param>
        public BrillRule(string from, string to, BrillConditions type, string? c1, string? c2, string? c3, bool secondRun)
        {
            From = from;
            To = to;
            Type = type;
            C1 = c1;
            C2 = c2;
            C3 = c3;
            SecondRun = secondRun;
        }

        /// <summary>
        /// Gets or sets the c1.
        /// </summary>
        /// <value>The c1.</value>
        public string? C1 { get; set; }

        /// <summary>
        /// Gets or sets the c2.
        /// </summary>
        /// <value>The c2.</value>
        public string? C2 { get; set; }

        /// <summary>
        /// Gets or sets the c3.
        /// </summary>
        /// <value>The c3.</value>
        public string? C3 { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>From.</value>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [second run].
        /// </summary>
        /// <value><c>true</c> if [second run]; otherwise, <c>false</c>.</value>
        public bool SecondRun { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>To.</value>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public BrillConditions Type { get; set; }
    }
}