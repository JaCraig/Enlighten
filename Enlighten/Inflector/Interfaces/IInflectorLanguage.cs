namespace Enlighten.Inflector.Interfaces
{
    /// <summary>
    /// Inflector language interface
    /// </summary>
    public interface IInflectorLanguage
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Conjugates the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partOfSpeechTo">The part of speech to.</param>
        /// <returns></returns>
        string Conjugate(string input, string partOfSpeechTo);

        /// <summary>
        /// Infinitives the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        string Infinitive(string input);

        /// <summary>
        /// Determines whether the specified input is gerund.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is gerund; otherwise, <c>false</c>.</returns>
        bool IsGerund(string input);

        /// <summary>
        /// Determines whether the specified input is past.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is past; otherwise, <c>false</c>.</returns>
        bool IsPast(string input);

        /// <summary>
        /// Determines whether the specified input is plural.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is plural; otherwise, <c>false</c>.</returns>
        bool IsPlural(string input);

        /// <summary>
        /// Determines whether the specified input is singular.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is singular; otherwise, <c>false</c>.</returns>
        bool IsSingular(string input);

        /// <summary>
        /// Determines whether the specified input is uncountable.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is uncountable; otherwise, <c>false</c>.</returns>
        bool IsUncountable(string input);

        /// <summary>
        /// Pluralizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        string Pluralize(string input);

        /// <summary>
        /// Singularizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        string Singularize(string input);

        /// <summary>
        /// Converts to gerund.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        string ToGerund(string input);

        /// <summary>
        /// Converts to past.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        string ToPast(string input);

        /// <summary>
        /// Converts to present.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        string ToPresent(string input);
    }
}