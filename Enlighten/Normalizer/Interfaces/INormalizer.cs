using Enlighten.Tokenizer;

namespace Enlighten.Normalizer.Interfaces
{
    /// <summary>
    /// Normalizer interface
    /// </summary>
    public interface INormalizer
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
        /// Normalizes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The normalized tokens.</returns>
        Token[] Normalize(Token[] tokens);
    }
}