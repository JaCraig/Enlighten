using Enlighten.Tokenizer;

namespace Enlighten.StopWords.Interfaces
{
    /// <summary>
    /// Stop words language
    /// </summary>
    public interface IStopWordsLanguage
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; }

        /// <summary>
        /// Determines whether [the specified token] [is a stop word].
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns><c>true</c> if [the specified token] [is a stop word]; otherwise, <c>false</c>.</returns>
        bool IsStopWord(string token);

        /// <summary>
        /// Marks the stop words.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The tokens.</returns>
        Token[] MarkStopWords(Token[] tokens);
    }
}