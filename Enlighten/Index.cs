using Enlighten.Indexer.Enums;
using Enlighten.Indexer.Interfaces;
using System;

namespace Enlighten
{
    /// <summary>
    /// Index
    /// </summary>
    public class Index
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Index"/> class.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <param name="indexer">The indexer.</param>
        public Index(Pipeline pipeline, IIndexer indexer)
        {
            Pipeline = pipeline;
            Indexer = indexer;
        }

        /// <summary>
        /// Gets the pipeline.
        /// </summary>
        /// <value>The pipeline.</value>
        public Pipeline Pipeline { get; }

        /// <summary>
        /// Gets the indexer.
        /// </summary>
        /// <value>The indexer.</value>
        private IIndexer Indexer { get; }

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>The index.</value>
        private IIndex? InternalIndex { get; set; }

        /// <summary>
        /// Indexes the specified document.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The document text.</returns>
        public Document AddDoc(string text)
        {
            return AddDoc(Pipeline.Process(text));
        }

        /// <summary>
        /// Indexes the specified document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>The document text.</returns>
        public Document AddDoc(Document document)
        {
            InternalIndex?.AddDoc(document);
            return document;
        }

        /// <summary>
        /// Creates the specified indexer type.
        /// </summary>
        /// <param name="indexerType">Type of the indexer.</param>
        /// <returns></returns>
        public Index Create(IndexerType indexerType)
        {
            InternalIndex = Indexer.CreateIndex(indexerType, Array.Empty<Document>());
            return this;
        }

        /// <summary>
        /// Queries the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>The query results.</returns>
        public Guid[] Query(string text)
        {
            return InternalIndex?.Query(Pipeline.Process(text)) ?? Array.Empty<Guid>();
        }
    }
}