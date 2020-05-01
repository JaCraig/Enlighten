using Enlighten.SynonymFinder.Interfaces;
using FileCurator;
using System;
using System.Collections.Generic;

namespace Enlighten.SynonymFinder.BaseClasses
{
    /// <summary>
    /// Synonym finder base class
    /// </summary>
    /// <seealso cref="ISynonymFinderLanguage"/>
    public abstract class SynonymFinderBase : ISynonymFinderLanguage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SynonymFinderBase"/> class.
        /// </summary>
        protected SynonymFinderBase()
        {
            var Data = new FileInfo($"resource://Enlighten/Enlighten.Resources.{Name.Replace("-", "_", StringComparison.Ordinal)}.synonyms.txt").Read();
            var Lines = Data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, LinesLength = Lines.Length; i < LinesLength; i++)
            {
                var Line = Lines[i];
                if (string.IsNullOrEmpty(Line))
                    continue;
                var LineData = Line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int x = 1; x < LineData.Length; ++x)
                {
                    SynonymDictionary.Add(LineData[x], LineData[0]);
                }
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the synonym dictionary.
        /// </summary>
        /// <value>The synonym dictionary.</value>
        protected Dictionary<string, string> SynonymDictionary { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Finds the synonym if it exists.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The synonym or the input if it doesn't exist.</returns>
        public string FindSynonym(string input)
        {
            if (SynonymDictionary.TryGetValue(input, out var Synonym))
                return Synonym;
            return input;
        }
    }
}