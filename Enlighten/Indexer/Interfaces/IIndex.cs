using System;

namespace Enlighten.Indexer.Interfaces
{
    /// <summary>
    /// Index interface
    /// </summary>
    public interface IIndex
    {
        /// <summary>
        /// Adds the document to the index.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>This.</returns>
        IIndex AddDoc(Document document);

        /// <summary>
        /// Queries the index and returns the IDs of the documents the query is found in.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The IDs of the documents matching the query.</returns>
        Guid[] Query(Document query);
    }
}