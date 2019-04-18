using Enlighten.Stemmer.BaseClasses;
using System.Collections.Generic;

namespace Enlighten.Stemmer.Languages
{
    /// <summary>
    /// English language
    /// </summary>
    /// <seealso cref="StemmerLanguageBaseClass"/>
    public class EnglishLanguage : StemmerLanguageBaseClass
    {
        /// <summary>
        /// Gets the vowels.
        /// </summary>
        /// <value>The vowels.</value>
        protected override char[] Vowels { get; } = new char[] { 'a', 'e', 'i', 'o', 'u', 'y' };

        /// <summary>
        /// Gets the doubles.
        /// </summary>
        /// <value>The doubles.</value>
        private string[] Doubles { get; } = new string[] { "bb", "dd", "ff", "gg", "mm", "nn", "pp", "rr", "tt" };

        /// <summary>
        /// Gets the exceptions.
        /// </summary>
        /// <value>The exceptions.</value>
        private Dictionary<string, string> Exceptions { get; } = new Dictionary<string, string>
        {
            ["skis"] = "ski",
            ["skies"] = "sky",
            ["dying"] = "die",
            ["lying"] = "lie",
            ["tying"] = "tie",
            ["idly"] = "idl",
            ["gently"] = "gentl",
            ["ugly"] = "ugly",
            ["early"] = "early",
            ["only"] = "only",
            ["singly"] = "singl",
            ["sky"] = "sky",
            ["news"] = "news",
            ["howe"] = "howe",
            ["atlas"] = "atlas",
            ["cosmos"] = "cosmos",
            ["bias"] = "bias",
            ["andes"] = "andes"
        };

        /// <summary>
        /// Gets the exceptions2.
        /// </summary>
        /// <value>The exceptions2.</value>
        private string[] Exceptions2 { get; } = new string[] { "inning", "outing", "canning", "herring", "earring", "proceed", "exceed", "succeed" };

        /// <summary>
        /// Gets the step1 replacements.
        /// </summary>
        /// <value>The step1 replacements.</value>
        private Dictionary<string, string> Step1Replacements { get; } = new Dictionary<string, string>
        {
            ["eedly"] = "ee",
            ["ingly"] = "",
            ["edly"] = "",
            ["eed"] = "ee",
            ["ing"] = "",
            ["ed"] = ""
        };

        /// <summary>
        /// Gets the step2 replacements.
        /// </summary>
        /// <value>The step2 replacements.</value>
        private Dictionary<string, string> Step2Replacements { get; } = new Dictionary<string, string>
        {
            ["ization"] = "ize",
            ["iveness"] = "ive",
            ["fulness"] = "ful",
            ["ational"] = "ate",
            ["ousness"] = "ous",
            ["biliti"] = "ble",
            ["tional"] = "tion",
            ["lessli"] = "less",
            ["fulli"] = "ful",
            ["entli"] = "ent",
            ["ation"] = "ate",
            ["aliti"] = "al",
            ["iviti"] = "ive",
            ["ousli"] = "ous",
            ["alism"] = "al",
            ["abli"] = "able",
            ["anci"] = "ance",
            ["alli"] = "al",
            ["izer"] = "ize",
            ["enci"] = "ence",
            ["ator"] = "ate",
            ["bli"] = "ble",
            ["ogi"] = "og",
            ["li"] = ""
        };

        /// <summary>
        /// Gets the step3 replacements.
        /// </summary>
        /// <value>The step3 replacements.</value>
        private Dictionary<string, string> Step3Replacements { get; } = new Dictionary<string, string>
        {
            ["ational"] = "ate",
            ["tional"] = "tion",
            ["alize"] = "al",
            ["icate"] = "ic",
            ["iciti"] = "ic",
            ["ative"] = "",
            ["ical"] = "ic",
            ["ness"] = "",
            ["ful"] = ""
        };

        /// <summary>
        /// Gets the step4 replacements.
        /// </summary>
        /// <value>The step4 replacements.</value>
        private string[] Step4Replacements { get; } = new string[] { "ement", "ment", "ence", "able", "ible", "ance", "ism", "ent", "ate", "iti", "ant", "ous", "ive", "ize", "ion", "ic", "er", "al" };

        /// <summary>
        /// Gets the valid li endings.
        /// </summary>
        /// <value>The valid li endings.</value>
        private string[] ValidLiEndings { get; } = new string[] { "c", "d", "e", "g", "h", "k", "m", "n", "r", "t" };

        /// <summary>
        /// Stems the word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The stemmed word.</returns>
        protected override string StemWord(string word)
        {
            if (string.IsNullOrEmpty(word) || word.Length < 3)
                return word;
            word = word.ToLowerInvariant();
            return word;
        }

        /// <summary>
        /// Determines whether the characters [are a short syllable].
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="index">The index.</param>
        /// <returns><c>true</c> if it [is a short syllable]; otherwise, <c>false</c>.</returns>
        private bool IsShortSyllable(char[] characters, int index)
        {
            var PlusOne = index + 1;
            var MinusOne = index - 1;
            if (index == 0 && characters.Length > 1)
            {
                return index == 0 && IsVowel(characters[index]) && !IsVowel(characters[PlusOne]);
            }
            if (MinusOne > -1 && PlusOne < characters.Length)
            {
                return IsVowel(characters[index])
                    && !IsVowel(characters[PlusOne])
                    && characters[PlusOne] != 'w'
                    && characters[PlusOne] != 'x'
                    && characters[PlusOne] != 'Y'
                    && !IsVowel(characters[MinusOne]);
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified word [is a short word].
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="r1">The r1.</param>
        /// <returns><c>true</c> if it [is a short word]; otherwise, <c>false</c>.</returns>
        private bool IsShortWord(string word, string r1)
        {
            return r1?.Length == 0 && IsShortSyllable(word.ToCharArray(), word.Length - 2);
        }
    }
}