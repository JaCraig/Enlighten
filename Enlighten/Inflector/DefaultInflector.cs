using Enlighten.Inflector.Enums;
using Enlighten.Inflector.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.Inflector
{
    /// <summary>
    /// Default inflector
    /// </summary>
    /// <seealso cref="IInflector"/>
    public class DefaultInflector : IInflector
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultInflector"/> class.
        /// </summary>
        /// <param name="languages">The languages.</param>
        public DefaultInflector(IEnumerable<IInflectorLanguage> languages)
        {
            Languages = languages.Where(x => x.GetType().Assembly != typeof(DefaultInflector).Assembly).ToDictionary(x => x.Name);
            foreach (var Language in languages.Where(x => x.GetType().Assembly == typeof(DefaultInflector).Assembly
                && !Languages.ContainsKey(x.Name)))
            {
                Languages.Add(Language.Name, Language);
            }
        }

        /// <summary>
        /// Gets the languages.
        /// </summary>
        /// <value>The languages.</value>
        public Dictionary<string, IInflectorLanguage> Languages { get; }

        /// <summary>
        /// Infinitives the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public string Infinitive(string input, InflectorLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Language))
                return input;
            return Language.Infinitive(input);
        }
        /// <summary>
        /// Determines whether the specified input is plural.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns><c>true</c> if the specified input is plural; otherwise, <c>false</c>.</returns>
        public bool IsPlural(string input, InflectorLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Language))
                return false;
            return Language.IsPlural(input);
        }
        /// <summary>
        /// Determines whether the specified input is singular.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns><c>true</c> if the specified input is singular; otherwise, <c>false</c>.</returns>
        public bool IsSingular(string input, InflectorLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Language))
                return false;
            return Language.IsSingular(input);
        }
        /// <summary>
        /// Determines whether the specified input is uncountable.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns><c>true</c> if the specified input is uncountable; otherwise, <c>false</c>.</returns>
        public bool IsUncountable(string input, InflectorLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Language))
                return false;
            return Language.IsUncountable(input);
        }
        /// <summary>
        /// Pluralizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public string Pluralize(string input, InflectorLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Language))
                return input;
            return Language.Pluralize(input);
        }
        /// <summary>
        /// Singularizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public string Singularize(string input, InflectorLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Language))
                return input;
            return Language.Singularize(input);
        }
        /// <summary>
        /// Converts to gerund.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public string ToGerund(string input, InflectorLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Language))
                return input;
            return Language.ToGerund(input);
        }
        /// <summary>
        /// Converts to past.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public string ToPast(string input, InflectorLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Language))
                return input;
            return Language.ToPast(input);
        }
        /// <summary>
        /// Converts to present.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public string ToPresent(string input, InflectorLanguage language)
        {
            if (!Languages.TryGetValue(language, out var Language))
                return input;
            return Language.ToPresent(input);
        }
    }
}