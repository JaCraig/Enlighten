using Enlighten.Indexer.Interfaces;
using System;

namespace Enlighten.Indexer.Indexers
{
    /// <summary>
    /// Inverted index builder
    /// </summary>
    /// <seealso cref="IIndexCreator"/>
    public class InvertedIndexCreator : IIndexCreator
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = nameof(InvertedIndex);

        /// <summary>
        /// Creates the index.
        /// </summary>
        /// <param name="docs">The docs.</param>
        /// <returns>The index</returns>
        public IIndex CreateIndex(Document[] docs)
        {
            return new InvertedIndex(docs ?? Array.Empty<Document>());
        }
    }
}