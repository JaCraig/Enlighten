using Enlighten.Normalizer.Interfaces;
using Enlighten.Tokenizer;

namespace Enlighten.Normalizer.Default
{
    /// <summary>
    /// Lower case normalizer
    /// </summary>
    /// <seealso cref="INormalizer"/>
    public class LowerCase : INormalizer
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name => nameof(LowerCase);

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public int Order => int.MinValue;

        /// <summary>
        /// Normalizes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The normalized tokens.</returns>
        public Token[] Normalize(Token[] tokens)
        {
            for (int x = 0; x < tokens.Length; ++x)
            {
                tokens[x].NormalizedValue = tokens[x].NormalizedValue.ToLowerInvariant();
            }
            return tokens;
        }
    }
}