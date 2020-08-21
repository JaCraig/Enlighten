using Enlighten.Indexer.Enums;
using Enlighten.Indexer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.Indexer
{
    /// <summary>
    /// Default indexer
    /// </summary>
    /// <seealso cref="IIndexer"/>
    public class DefaultIndexer : IIndexer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultIndexer"/> class.
        /// </summary>
        /// <param name="indexCreators">The index creators.</param>
        public DefaultIndexer(IEnumerable<IIndexCreator> indexCreators)
        {
            Languages = indexCreators.Where(x => x.GetType().Assembly != typeof(DefaultIndexer).Assembly).ToDictionary(x => x.Name);
            foreach (var Language in indexCreators.Where(x => x.GetType().Assembly == typeof(DefaultIndexer).Assembly
                && !Languages.ContainsKey(x.Name)))
            {
                Languages.Add(Language.Name, Language);
            }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>The languages.</value>
        public Dictionary<string, IIndexCreator> Languages { get; }

        /// <summary>
        /// Creates an index.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="docs">The docs.</param>
        /// <returns>The index.</returns>
        public IIndex CreateIndex(string type, Document[] docs)
        {
            if (Languages.TryGetValue(type, out var Language))
                return Language.CreateIndex(docs);
            Languages.TryGetValue(IndexerType.InvertedIndex, out Language);
            return Language.CreateIndex(docs);
        }
    }
}