namespace Enlighten.Indexer.Interfaces
{
    /// <summary>
    /// Index creator
    /// </summary>
    public interface IIndexCreator
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Creates an index.
        /// </summary>
        /// <param name="docs">The docs.</param>
        /// <returns>The index.</returns>
        IIndex CreateIndex(Document[] docs);
    }
}