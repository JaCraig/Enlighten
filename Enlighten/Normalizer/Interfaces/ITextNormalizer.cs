namespace Enlighten.Normalizer.Interfaces
{
    /// <summary>
    /// Text normalizer interface
    /// </summary>
    public interface ITextNormalizer
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        int Order { get; }

        /// <summary>
        /// Normalizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The normalized text.</returns>
        string Normalize(string text);
    }
}