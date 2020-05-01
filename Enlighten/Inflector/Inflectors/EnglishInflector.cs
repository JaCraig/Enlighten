using Enlighten.Inflector.Interfaces;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Enlighten.Inflector
{
    /// <summary>
    /// Default inflector
    /// </summary>
    /// <seealso cref="IInflector"/>
    public class EnglishInflector : IInflectorLanguage
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
        /// The consonant e
        /// </summary>
        private static readonly Regex ConsonantE = new Regex("[^aeiouy]e$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The consonant y
        /// </summary>
        private static readonly Regex ConsonantY = new Regex("[^aeiou]y$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The ee
        /// </summary>
        private static readonly Regex EE = new Regex("(ee)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The gerund exceptions
        /// </summary>
        private static readonly string[] GerundExceptions = new string[]
        {
            "anything",
            "spring",
            "something",
            "thing",
            "king",
            "nothing"
        };

        /// <summary>
        /// The ie
        /// </summary>
        private static readonly Regex IE = new Regex("(ie)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The is plural regex
        /// </summary>
        private static readonly Regex IsPluralRegex = new Regex("([saui]s|[^i]a)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The long vowel consonant
        /// </summary>
        private static readonly Regex LongVowelConsonant = new Regex("([uao]m[pb]|[oa]wn|ey|elp|[ei]gn|ilm|o[uo]r|[oa]ugh|igh|ki|ff|oubt|ount|awl|o[alo]d|[iu]rl|upt|[oa]y|ight|oid|empt|act|aud|e[ea]d|ound|[aeiou][srcln]t|ept|dd|[eia]n[dk]|[ioa][xk]|[oa]rm|[ue]rn|[ao]ng|uin|eam|ai[mr]|[oea]w|[eaoui][rscl]k|[oa]r[nd]|ear|er|it|ll)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The plural rules
        /// </summary>
        private static readonly (Regex, string)[] PluralRules = new (Regex, string)[]
        {
            (new Regex("(criteri|phenomen)on$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1a"),
            (new Regex("(alumn|alg|larv|vertebr)a$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ae"),
            (new Regex("(hoo|lea|loa|thie)f$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ves"),
            (new Regex("(buz|blit|walt)z$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1zes"),
            (new Regex("(quiz)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1zes"),
            (new Regex("^(ox)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1en"),
            (new Regex("(^[m|l])ouse$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ice"),
            (new Regex("(matr|vert|ind|d)ix|ex$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ices"),
            (new Regex("(x|ch|ss|sh)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1es"),
            (new Regex("([^aeiouy]|qu)y$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ies"),
            (new Regex("(hive)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1s"),
            (new Regex("(?:([^f])fe|([lr])f)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1$2ves"),
            (new Regex("sis$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "ses"),
            (new Regex("([dti])um$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1a"),
            (new Regex("(buffal|tomat|volcan|ech|embarg|her|mosquit|potat|torped|vet)o$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1oes"),
            (new Regex("(alias|bias|iris|status|campus|apparatus|virus|walrus|trellis)$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1es"),
            (new Regex("(octop|vir|alumn|fung|cact|foc|hippopotam|radi|stimul|syllab|nucle)us$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1i"),
            (new Regex("(ax|test)is$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1es"),
            (new Regex("s$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "s"),
            (new Regex("$",RegexOptions.IgnoreCase|RegexOptions.Compiled), "s")
        };

        /// <summary>
        /// The short vowel consonant
        /// </summary>
        private static readonly Regex ShortVowelConsonant = new Regex("([aeiuo][ptlgnm]|ir|cur|[^aeiuo][oua][db])$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The sibilant
        /// </summary>
        private static readonly Regex Sibilant = new Regex("([ieao]ss|[aeiouy]zz|[aeiouy]ch|nch|rch|[aeiouy]sh|[iae]tch|ax)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The singular rules
        /// </summary>
        private static readonly (Regex, string)[] SingularRules = new (Regex, string)[]
        {
            (new Regex("(m)en$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1an"),
            (new Regex("(pe)ople$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1rson"),
            (new Regex("(child)ren$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1"),
            (new Regex("([ti])a$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1um"),
            (new Regex("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1$2sis"),
            (new Regex("(hive)s$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1"),
            (new Regex("(tive)s$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1"),
            (new Regex("(curve)s$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1"),
            (new Regex("([lr])ves$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1f"),
            (new Regex("([^fo])ves$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1fe"),
            (new Regex("([^aeiouy]|qu)ies$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1y"),
            (new Regex("(s)eries$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1eries"),
            (new Regex("(m)ovies$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ovie"),
            (new Regex("(x|ch|ss|sh)es$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1"),
            (new Regex("([m|l])ice$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ouse"),
            (new Regex("(bus)es$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1"),
            (new Regex("(o)es$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1"),
            (new Regex("(shoe)s$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1"),
            (new Regex("(cris|ax|test)es$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1is"),
            (new Regex("(octop|vir)i$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1us"),
            (new Regex("(alias|status)es$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1"),
            (new Regex("^(ox)en", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1"),
            (new Regex("(vert|ind)ices$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ex"),
            (new Regex("(matr)ices$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1ix"),
            (new Regex("(quiz)zes$", RegexOptions.IgnoreCase|RegexOptions.Compiled), "$1"),
            (new Regex("s$", RegexOptions.IgnoreCase|RegexOptions.Compiled), string.Empty)
        };

        /// <summary>
        /// The ue
        /// </summary>
        private static readonly Regex UE = new Regex("(ue)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The uncountable words
        /// </summary>
        private static readonly string[] UncountableWords = new string[]
        {
            "tuna", "trout", "spacecraft", "salmon", "halibut", "aircraft",
            "equipment", "information", "rice", "money", "species", "series",
            "fish", "sheep", "moose", "deer", "news", "asbestos"
        };

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = "en-us";

        /// <summary>
        /// Conjugates the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partOfSpeechTo">The part of speech to convert it to.</param>
        /// <returns>The conjugated string</returns>
        public string Conjugate(string input, string partOfSpeechTo)
        {
            if (ConsonantY.IsMatch(input))
            {
                return ConjugateConsonentY(input, partOfSpeechTo);
            }
            if (ConsonantE.IsMatch(input))
            {
                return ConjugateConsonentE(input, partOfSpeechTo);
            }
            if (ShortVowelConsonant.IsMatch(input))
            {
                return ConjugateShortVowelConsonant(input, partOfSpeechTo);
            }
            if (Sibilant.IsMatch(input))
            {
                return ConjugateSibilant(input, partOfSpeechTo);
            }
            if (EE.IsMatch(input))
            {
                return ConjugateEE(input, partOfSpeechTo);
            }
            if (IE.IsMatch(input))
            {
                return ConjugateIE(input, partOfSpeechTo);
            }
            if (UE.IsMatch(input))
            {
                return ConjugateUE(input, partOfSpeechTo);
            }
            if (LongVowelConsonant.IsMatch(input))
            {
                return ConjugateLongVowelConsonant(input, partOfSpeechTo);
            }
            return input;
        }

        /// <summary>
        /// Infinitives the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The infinitive tense of the verb.</returns>
        public string Infinitive(string input)
        {
            return input == "are" || input == "am" ? "be" : input;
        }

        /// <summary>
        /// Determines whether the specified input is gerund.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is gerund; otherwise, <c>false</c>.</returns>
        public bool IsGerund(string input)
        {
            var Lower = input.ToLowerInvariant();
            return Lower.EndsWith("ing", StringComparison.Ordinal) && !GerundExceptions.Contains(Lower);
        }

        /// <summary>
        /// Determines whether the specified input is past.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is past; otherwise, <c>false</c>.</returns>
        public bool IsPast(string input)
        {
            var Lower = input.ToLowerInvariant();
            return Lower.EndsWith("ed", StringComparison.Ordinal) && !Lower.EndsWith("eed", StringComparison.Ordinal);
        }

        /// <summary>
        /// Determines whether the specified input is plural.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is plural; otherwise, <c>false</c>.</returns>
        public bool IsPlural(string input)
        {
            return !IsPluralRegex.IsMatch(input) && IsMatch(input, SingularRules);
        }

        /// <summary>
        /// Determines whether the specified input is singular.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is singular; otherwise, <c>false</c>.</returns>
        public bool IsSingular(string input)
        {
            return IsUncountable(input) || IsMatch(input, PluralRules);
        }

        /// <summary>
        /// Determines whether the specified input is uncountable.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is uncountable; otherwise, <c>false</c>.</returns>
        public bool IsUncountable(string input)
        {
            return UncountableWords.Contains(input);
        }

        /// <summary>
        /// Pluralizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The plural form of the word.</returns>
        public string Pluralize(string input)
        {
            return IsSingular(input) ? Apply(input, PluralRules) : input;
        }

        /// <summary>
        /// Singularizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The singular form of the word.</returns>
        public string Singularize(string input)
        {
            return IsPlural(input) ? Apply(input, SingularRules) : input;
        }

        /// <summary>
        /// Converts to gerund.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The gerund form of the input</returns>
        public string ToGerund(string input)
        {
            return Conjugate(input, VBG);
        }

        /// <summary>
        /// Converts to past tense
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The past tense form of the input.</returns>
        public string ToPast(string input)
        {
            return Conjugate(input, VBN);
        }

        /// <summary>
        /// Converts to present.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The present tense of the input.</returns>
        public string ToPresent(string input)
        {
            return Conjugate(input, VBZ);
        }

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
        /// Conjugates the consonent e.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partOfSpeech">The part of speech.</param>
        /// <returns>The conjugated value.</returns>
        private string ConjugateConsonentE(string input, string partOfSpeech)
        {
            var Base = input.Substring(0, input.Length - 1);
            return partOfSpeech switch
            {
                VBZ => input + "s",
                VBG => Base + "ing",
                VBN => Base + "ed",
                _ => input,
            };
        }

        /// <summary>
        /// Conjugates the consonent y.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partOfSpeech">The part of speech.</param>
        /// <returns>The conjugated value.</returns>
        private string ConjugateConsonentY(string input, string partOfSpeech)
        {
            var Base = input.Substring(0, input.Length - 1);
            return partOfSpeech switch
            {
                VBZ => Base + "ies",
                VBG => input + "ing",
                VBN => Base + "ied",
                _ => input,
            };
        }

        /// <summary>
        /// Conjugates the ee.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partOfSpeech">The part of speech.</param>
        /// <returns>The conjugated value.</returns>
        private string ConjugateEE(string input, string partOfSpeech)
        {
            return partOfSpeech switch
            {
                VBZ => input + "s",
                VBG => input + "ing",
                VBN => input + "d",
                _ => input,
            };
        }

        /// <summary>
        /// Conjugates the ie.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partOfSpeech">The part of speech.</param>
        /// <returns>The conjugated value.</returns>
        private string ConjugateIE(string input, string partOfSpeech)
        {
            var Base = input.Substring(0, input.Length - 2);
            return partOfSpeech switch
            {
                VBZ => input + "s",
                VBG => Base + "ying",
                VBN => input + "d",
                _ => input,
            };
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
        /// Conjugates the short vowel consonant.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partOfSpeech">The part of speech.</param>
        /// <returns>The input conjugated</returns>
        private string ConjugateShortVowelConsonant(string input, string partOfSpeech)
        {
            return partOfSpeech switch
            {
                VBZ => input + "s",
                VBG => input + input[^1] + "ing",
                VBN => input + input[^1] + "ed",
                _ => input,
            };
        }

        /// <summary>
        /// Conjugates the sibilant.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partOfSpeech">The part of speech.</param>
        /// <returns>The conjugated result.</returns>
        private string ConjugateSibilant(string input, string partOfSpeech)
        {
            return partOfSpeech switch
            {
                VBZ => input + "es",
                VBG => input + "ing",
                VBN => input + "ed",
                _ => input,
            };
        }

        /// <summary>
        /// Conjugates the ue.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partOfSpeech">The part of speech.</param>
        /// <returns>The conjugated value.</returns>
        private string ConjugateUE(string input, string partOfSpeech)
        {
            var Base = input.Substring(0, input.Length - 1);
            return partOfSpeech switch
            {
                VBZ => input + "s",
                VBG => Base + "ing",
                VBN => input + "d",
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