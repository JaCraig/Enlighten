using Enlighten.SynonymFinder.BaseClasses;
using Enlighten.SynonymFinder.Enum;
using Enlighten.SynonymFinder.Interfaces;

namespace Enlighten.SynonymFinder.Finders
{
    /// <summary>
    /// English synonym finder
    /// </summary>
    /// <seealso cref="ISynonymFinderLanguage"/>
    public class EnglishFinder : SynonymFinderBase
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name { get; } = SynonymFinderLanguage.English;
    }
}