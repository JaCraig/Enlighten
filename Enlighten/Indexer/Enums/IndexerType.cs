using BigBook.Patterns.BaseClasses;

namespace Enlighten.Indexer.Enums
{
    /// <summary>
    /// Indexer language
    /// </summary>
    /// <seealso cref="BigBook.Patterns.BaseClasses.StringEnumBaseClass{Enums.IndexerType}"/>
    public class IndexerType : StringEnumBaseClass<IndexerType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexerType"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public IndexerType(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexerType"/> class.
        /// </summary>
        public IndexerType() : base(string.Empty)
        {
        }

        /// <summary>
        /// Gets the index of the inverted.
        /// </summary>
        /// <value>The index of the inverted.</value>
        public static IndexerType InvertedIndex { get; } = new IndexerType(nameof(InvertedIndex));
    }
}