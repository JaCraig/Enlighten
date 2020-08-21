using BigBook;
using Enlighten.Indexer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.Indexer.Indexers
{
    /// <summary>
    /// Inverted index
    /// </summary>
    /// <seealso cref="IIndex"/>
    public class InvertedIndex : IIndex
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvertedIndex"/> class.
        /// </summary>
        public InvertedIndex()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvertedIndex"/> class.
        /// </summary>
        /// <param name="documents">The documents.</param>
        public InvertedIndex(Document[] documents)
        {
            documents ??= Array.Empty<Document>();
            for (var x = 0; x < documents.Length; ++x)
            {
                AddDoc(documents[x]);
            }
        }

        /// <summary>
        /// Gets the index.
        /// </summary>
        /// <value>The index.</value>
        private ListMapping<string, Guid> Index { get; } = new ListMapping<string, Guid>();

        /// <summary>
        /// Adds the document to the index.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>This.</returns>
        public IIndex AddDoc(Document document)
        {
            for (var x = 0; x < document.Tokens.Length; ++x)
            {
                var Token = document.Tokens[x].StemmedValue;
                if (string.IsNullOrEmpty(Token)
                    || document.Tokens[x].StopWord
                    || Index.Contains(Token, document.ID))
                {
                    continue;
                }

                Index.Add(Token, document.ID);
            }
            return this;
        }

        /// <summary>
        /// Queries the index and returns the IDs of the documents the query is found in.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The IDs of the documents matching the query.</returns>
        public Guid[] Query(Document query)
        {
            List<Guid> ReturnValues = new List<Guid>();
            bool InitialList = true;
            for (var x = 0; x < query.Tokens.Length; ++x)
            {
                var Token = query.Tokens[x].StemmedValue;
                if (string.IsNullOrEmpty(Token)
                    || query.Tokens[x].StopWord
                    || !Index.TryGetValue(Token, out var guids))
                {
                    continue;
                }
                if (InitialList)
                {
                    ReturnValues.AddRange(guids);
                    InitialList = false;
                }
                else
                {
                    ReturnValues.Remove(Item => !guids.Contains(Item));
                }
            }
            return ReturnValues.ToArray();
        }
    }
}