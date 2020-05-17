using Enlighten.Normalizer.Interfaces;
using Enlighten.Tokenizer;
using System.Collections.Generic;
using System.Linq;

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
        /// <param name="textNormalizers">The text normalizers.</param>
        public DefaultNormalizer(IEnumerable<INormalizer> normalizers, IEnumerable<ITextNormalizer> textNormalizers)
        {
            Normalizers = normalizers;
            TextNormalizers = textNormalizers;
        }

        /// <summary>
        /// Gets the normalizers.
        /// </summary>
        /// <value>The normalizers.</value>
        public IEnumerable<INormalizer> Normalizers { get; }

        /// <summary>
        /// Gets the text normalizers.
        /// </summary>
        /// <value>The text normalizers.</value>
        public IEnumerable<ITextNormalizer> TextNormalizers { get; }

        /// <summary>
        /// Normalizes the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The normalized tokens.</returns>
        public Token[] Normalize(Token[] tokens)
        {
            foreach (var Normalizer in Normalizers.OrderBy(x => x.Order))
            {
                tokens = Normalizer.Normalize(tokens);
            }
            return tokens;
        }

        /// <summary>
        /// Normalizes the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The normalized text.</returns>
        public string Normalize(string text)
        {
            foreach (var TextNormalizer in TextNormalizers.OrderBy(x => x.Order))
            {
                text = TextNormalizer.Normalize(text);
            }
            return text;
        }
    }
}