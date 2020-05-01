using Enlighten.Inflector.Enums;

namespace Enlighten.Inflector.Interfaces
{
    /// <summary>
    /// Inflector interface
    /// </summary>
    public interface IInflector
    {
        /// <summary>
        /// Infinitives the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        string Infinitive(string input, InflectorLanguage language);

        /// <summary>
        /// Determines whether the specified input is gerund.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns><c>true</c> if the specified input is gerund; otherwise, <c>false</c>.</returns>
        bool IsGerund(string input, InflectorLanguage language);

        /// <summary>
        /// Determines whether the specified input is past.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns><c>true</c> if the specified input is past; otherwise, <c>false</c>.</returns>
        bool IsPast(string input, InflectorLanguage language);

        /// <summary>
        /// Determines whether the specified input is plural.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns><c>true</c> if the specified input is plural; otherwise, <c>false</c>.</returns>
        bool IsPlural(string input, InflectorLanguage language);

        /// <summary>
        /// Determines whether the specified input is singular.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns><c>true</c> if the specified input is singular; otherwise, <c>false</c>.</returns>
        bool IsSingular(string input, InflectorLanguage language);

        /// <summary>
        /// Determines whether the specified input is uncountable.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns><c>true</c> if the specified input is uncountable; otherwise, <c>false</c>.</returns>
        bool IsUncountable(string input, InflectorLanguage language);

        /// <summary>
        /// Pluralizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        string Pluralize(string input, InflectorLanguage language);

        /// <summary>
        /// Singularizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        string Singularize(string input, InflectorLanguage language);

        /// <summary>
        /// Converts to gerund.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        string ToGerund(string input, InflectorLanguage language);

        /// <summary>
        /// Converts to past.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        string ToPast(string input, InflectorLanguage language);

        /// <summary>
        /// Converts to present.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        string ToPresent(string input, InflectorLanguage language);
    }
}