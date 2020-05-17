using Enlighten.StopWords.Enum;
using Enlighten.Tokenizer;

namespace Enlighten.StopWords.Interfaces
{
    /// <summary>
    /// Stop words manager interface
    /// </summary>
    public interface IStopWordsManager
    {
        /// <summary>
        /// Determines whether [the specified token] [is a stop word].
        /// </summary>
        /// <param name="token">The token.</param>
        /// <param name="language">The language.</param>
        /// <returns><c>true</c> if [the specified token] [is a stop word]; otherwise, <c>false</c>.</returns>
        bool IsStopWord(string token, StopWordsLanguage language);

        /// <summary>
        /// Marks the stop words.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="language">The language.</param>
        /// <returns>The tokens.</returns>
        Token[] MarkStopWords(Token[] tokens, StopWordsLanguage language);
    }
}