using Enlighten.Tokenizer;

namespace Enlighten.Normalizer.Interfaces
{
    /// <summary>
    /// Normalizer manager interface
    /// </summary>
    public interface INormalizerManager
    {
        /// <summary>
        /// Normalizes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The normalized tokens.</returns>
        Token[] Normalize(Token[] tokens);

        /// <summary>
        /// Normalizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The normalized text.</returns>
        string Normalize(string text);
    }
}