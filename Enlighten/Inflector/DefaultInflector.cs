using Enlighten.Inflector.Interfaces;
using System.Linq;
using System.Text.RegularExpressions;

namespace Enlighten.Inflector
{
    /// <summary>
    /// Default inflector
    /// </summary>
    /// <seealso cref="IInflector"/>
    public class DefaultInflector : IInflector
    {
        /// <summary>
        /// Gets the VBG.
        /// </summary>
        /// <value>The VBG.</value>
        private const string VBG = "VBG";

        /// <summary>
        /// Gets the VBN.
        /// </summary>
        /// <value>The VBN.</value>
        private const string VBN = "VBN";

        /// <summary>
        /// Gets the VBZ.
        /// </summary>
        /// <value>The VBZ.</value>
        private const string VBZ = "VBZ";

        /// <summary>
        /// The plural rules
        /// </summary>
        private (Regex, string)[] PluralRules = new (Regex, string)[]
        {
            (new Regex("^index$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "indices"),
            (new Regex("^criterion$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "criteria"),
            (new Regex("dix$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "dices"),
            (new Regex("(a|o)ch$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1chs"),
            (new Regex("(m)an$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1en"),
            (new Regex("(pe)rson$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ople"),
            (new Regex("(child)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ren"),
            (new Regex("^(ox)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1en"),
            (new Regex("(ax|test)is$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1es"),
            (new Regex("(octop|vir)us$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1i"),
            (new Regex("(alias|status)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1es"),
            (new Regex("(bu)s$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ses"),
            (new Regex("(buffal|tomat|potat|her)o$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1oes"),
            (new Regex("([ti])um$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1a"),
            (new Regex("sis$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "ses"),
            (new Regex("(?:([^f])fe|([lr])f)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1$2ves"),
            (new Regex("(hive)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1s"),
            (new Regex("([^aeiouy]|qu)y$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ies"),
            (new Regex("(x|ch|ss|sh)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1es"),
            (new Regex("(matr|vert|ind)ix|ex$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ices"),
            (new Regex("([m|l])ouse$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ice"),
            (new Regex("(quiz)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1zes"),
            (new Regex("^gas$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "gases"),
            (new Regex("s$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "s"),
            (new Regex("$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "s")
        };

        /// <summary>
        /// The singular rules
        /// </summary>
        private (Regex, string)[] SingularRules = new (Regex, string)[]
        {
            (new Regex("(m)en$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1an"),
            (new Regex("(pe)ople$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1rson"),
            (new Regex("(child)ren$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1"),
            (new Regex("([ti])a$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1um"),
            (new Regex("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1$2sis"),
            (new Regex("(hive)s$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1"),
            (new Regex("(tive)s$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1"),
            (new Regex("(curve)s$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1"),
            (new Regex("([lr])ves$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1f"),
            (new Regex("([^fo])ves$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1fe"),
            (new Regex("([^aeiouy]|qu)ies$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1y"),
            (new Regex("(s)eries$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1eries"),
            (new Regex("(m)ovies$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1ovie"),
            (new Regex("(x|ch|ss|sh)es$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1"),
            (new Regex("([m|l])ice$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1ouse"),
            (new Regex("(bus)es$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1"),
            (new Regex("(o)es$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1"),
            (new Regex("(shoe)s$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1"),
            (new Regex("(cris|ax|test)es$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1is"),
            (new Regex("(octop|vir)i$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1us"),
            (new Regex("(alias|status)es$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1"),
            (new Regex("^(ox)en", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1"),
            (new Regex("(vert|ind)ices$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1ex"),
            (new Regex("(matr)ices$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1ix"),
            (new Regex("(quiz)zes$", RegexOptions.IgnoreCase | RegexOptions.Compiled), "$1"),
            (new Regex("s$", RegexOptions.IgnoreCase | RegexOptions.Compiled), string.Empty)
        };

        /// <summary>
        /// The uncountable words
        /// </summary>
        private string[] UncountableWords = new string[]
        {
            "tuna", "trout", "spacecraft", "salmon", "halibut", "aircraft",
            "equipment", "information", "rice", "money", "species", "series",
            "fish", "sheep", "moose", "deer", "news", "asbestos"
        };

        /// <summary>
        /// Applies the specified rules to the input string.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="rules">The rules.</param>
        /// <returns>The resulting string.</returns>
        private string Apply(string input, (Regex, string)[] rules)
        {
            if (UncountableWords.Contains(input.ToLowerInvariant()))
            {
                return input;
            }

            for (int x = 0; x < rules.Length; ++x)
            {
                var RuleMatch = rules[x].Item1.Replace(input, rules[x].Item2);
                if (RuleMatch != input)
                    return RuleMatch;
            }
            return input;
        }

        /// <summary>
        /// Conjugates the long vowel consonant.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partOfSpeech">The part of speech.</param>
        /// <returns>The input conjugated</returns>
        private string ConjugateLongVowelConsonant(string input, string partOfSpeech)
        {
            return partOfSpeech switch
            {
                VBZ => input + "s",
                VBG => input + "ing",
                VBN => input + "ed",
                _ => input,
            };
        }

        /// <summary>
        /// Determines whether the specified input is a match for any of the rules..
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="rules">The rules.</param>
        /// <returns><c>true</c> if the specified input is a match; otherwise, <c>false</c>.</returns>
        private bool IsMatch(string input, (Regex, string)[] rules)
        {
            if (UncountableWords.Contains(input.ToLowerInvariant()))
            {
                return false;
            }

            for (int x = 0; x < rules.Length; ++x)
            {
                if (rules[x].Item1.IsMatch(input))
                    return true;
            }
            return false;
        }
    }
}