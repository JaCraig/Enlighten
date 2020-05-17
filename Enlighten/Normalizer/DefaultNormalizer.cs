using Enlighten.Normalizer.Interfaces;
using Enlighten.Tokenizer;
using System.Collections.Generic;

namespace Enlighten.Normalizer
{
    /// <summary>
    /// Default normalizer
    /// </summary>
    /// <seealso cref="INormalizerManager"/>
    public class DefaultNormalizer : INormalizerManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultNormalizer"/> class.
        /// </summary>
        /// <param name="normalizers">The normalizers.</param>
        public DefaultNormalizer(IEnumerable<INormalizer> normalizers)
        {
            Normalizers = normalizers;
        }

        /// <summary>
        /// Gets the normalizers.
        /// </summary>
        /// <value>The normalizers.</value>
        public IEnumerable<INormalizer> Normalizers { get; }

        /// <summary>
        /// Normalizes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The normalized tokens.</returns>
        public Token[] Normalize(Token[] tokens)
        {
            foreach (var Normalizer in Normalizers)
            {
                tokens = Normalizer.Normalize(tokens);
            }
            return tokens;
        }
    }
}