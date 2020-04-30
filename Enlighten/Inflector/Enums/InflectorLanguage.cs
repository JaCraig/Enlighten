using BigBook.Patterns.BaseClasses;

namespace Enlighten.Inflector.Enums
{
    /// <summary>
    /// Inflector language
    /// </summary>
    /// <seealso cref="BigBook.Patterns.BaseClasses.StringEnumBaseClass{Enums.InflectorLanguage}"/>
    public class InflectorLanguage : StringEnumBaseClass<InflectorLanguage>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InflectorLanguage"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public InflectorLanguage(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InflectorLanguage"/> class.
        /// </summary>
        public InflectorLanguage() : base(string.Empty)
        {
        }

        /// <summary>
        /// Gets the english.
        /// </summary>
        /// <value>The english.</value>
        public static InflectorLanguage English { get; } = new InflectorLanguage("en-us");
    }
}