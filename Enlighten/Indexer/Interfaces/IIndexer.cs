namespace Enlighten.Indexer.Interfaces
{
    /// <summary>
    /// Indexer interface
    /// </summary>
    public interface IIndexer
    {
        /// <summary>
        /// Creates an index.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="docs">The docs.</param>
        /// <returns>The index.</returns>
        IIndex CreateIndex(string type, Document[] docs);
    }
}