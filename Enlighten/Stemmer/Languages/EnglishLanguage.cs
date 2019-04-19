using Enlighten.Stemmer.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private string[] Step1Replacements { get; } = new string[]
        {
             "eedly",
            "ingly",
            "edly",
            "eed",
            "ing",
            "ed"
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
        private char[] ValidLiEndings { get; } = new char[] { 'c', 'd', 'e', 'g', 'h', 'k', 'm', 'n', 'r', 't' };

        /// <summary>
        /// Stems the word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The stemmed word.</returns>
        protected override string StemWord(string word)
        {
            if (string.IsNullOrEmpty(word) || word.Length < 3)
                return word;

            //Clean up word.
            word = word.ToLowerInvariant();

            if (word[0] == '\'')
                word = word.Substring(1);

            //Check for exceptions
            if (Exceptions.ContainsKey(word))
                return Exceptions[word];

            var WordSpan = new Span<char>(word.ToCharArray());

            WordSpan = FixYValues(WordSpan);

            WordSpan = Step0(WordSpan);

            WordSpan = Step1A(WordSpan);

            for (int i = 0, Exceptions2Length = Exceptions2.Length; i < Exceptions2Length; i++)
            {
                var Exception = Exceptions2[i];
                if (WordSpan.SequenceEqual(Exception.AsSpan()))
                    return Exception;
            }

            (int R1Index, int R2Index) = CalculateR1AndR2(WordSpan);

            WordSpan = Step1B(GetRValue(R1Index, WordSpan), R1Index, WordSpan);

            WordSpan = Step1C(WordSpan);

            WordSpan = Step2(WordSpan, GetRValue(R1Index, WordSpan));

            WordSpan = Step3(WordSpan, GetRValue(R1Index, WordSpan), GetRValue(R2Index, WordSpan));

            WordSpan = Step4(WordSpan, GetRValue(R2Index, WordSpan));

            WordSpan = Step5(WordSpan, GetRValue(R1Index, WordSpan), GetRValue(R2Index, WordSpan));

            return new string(WordSpan.ToArray()).ToLowerInvariant();
        }

        /// <summary>
        /// Gets the R* value.
        /// </summary>
        /// <param name="RIndex">Index of the r.</param>
        /// <param name="word">The word.</param>
        /// <returns>The sliced region.</returns>
        private static Span<char> GetRValue(int RIndex, Span<char> word)
        {
            return RIndex < word.Length ? word.Slice(RIndex) : Span<char>.Empty;
        }

        /// <summary>
        /// Removes the word endings.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The word without the endings.</returns>
        private static Span<char> Step0(Span<char> word)
        {
            if (word.Length >= 3 && word[word.Length - 3] == '\'' && word[word.Length - 2] == 's' && word[word.Length - 1] == '\'')
            {
                return word.Slice(0, word.Length - 3);
            }
            if (word.Length >= 2 && word[word.Length - 2] == '\'' && word[word.Length - 1] == 's')
            {
                return word.Slice(0, word.Length - 2);
            }
            if (word.Length >= 1 && word[word.Length - 1] == '\'')
            {
                return word.Slice(0, word.Length - 1);
            }
            return word;
        }

        /// <summary>
        /// Calculates the r1 and r2 values.
        /// </summary>
        /// <param name="wordSpan">The word.</param>
        /// <returns>The resulting r1 and r2 values.</returns>
        private (int R1, int R2) CalculateR1AndR2(Span<char> wordSpan)
        {
            int r1 = wordSpan.Length;
            int r2 = wordSpan.Length;
            if (wordSpan.StartsWith("gener".ToCharArray()) || wordSpan.StartsWith("arsen".ToCharArray()))
            {
                r1 = 5;
            }
            else if (wordSpan.StartsWith("commun".ToCharArray()))
            {
                r1 = 6;
            }
            else
            {
                for (int x = 1; x < wordSpan.Length; x++)
                {
                    if (!IsVowel(wordSpan[x]) && IsVowel(wordSpan[x - 1]))
                    {
                        r1 = x + 1;
                        break;
                    }
                }
            }

            for (int x = r1; x < wordSpan.Length; ++x)
            {
                if (!IsVowel(wordSpan[x]) && IsVowel(wordSpan[x - 1]))
                {
                    r2 = x + 1;
                    break;
                }
            }

            return (r1, r2);
        }

        /// <summary>
        /// Set initial y, or y after a vowel, to Y.
        /// </summary>
        /// <param name="WordSpan">The word span.</param>
        /// <returns>The word with the y values fixed.</returns>
        private Span<char> FixYValues(Span<char> WordSpan)
        {
            if (WordSpan[0] == 'y')
                WordSpan[0] = 'Y';

            for (int x = 1; x < WordSpan.Length; x++)
            {
                if (WordSpan[x] == 'y' && IsVowel(WordSpan[x - 1]))
                {
                    WordSpan[x] = 'Y';
                }
            }

            return WordSpan;
        }

        /// <summary>
        /// Determines whether the characters [are a short syllable].
        /// </summary>
        /// <param name="characters">The characters.</param>
        /// <param name="index">The index.</param>
        /// <returns><c>true</c> if it [is a short syllable]; otherwise, <c>false</c>.</returns>
        private bool IsShortSyllable(Span<char> characters, int index)
        {
            var PlusOne = index + 1;
            var MinusOne = index - 1;
            if (index == 0 && characters.Length > 1)
            {
                return IsVowel(characters[index]) && !IsVowel(characters[PlusOne]);
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
        private bool IsShortWord(Span<char> word, Span<char> r1)
        {
            return r1.IsEmpty && IsShortSyllable(word, word.Length - 2);
        }

        /// <summary>
        /// Removes endings
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The word without the endings.</returns>
        private Span<char> Step1A(Span<char> word)
        {
            if (word.EndsWith("sses".ToCharArray()))
            {
                return word.Slice(0, word.Length - 2);
            }
            if (word.EndsWith("ied".ToCharArray()) || word.EndsWith("ies".ToCharArray()))
            {
                return word.Length > 4 ? word.Slice(0, word.Length - 2) : word.Slice(0, word.Length - 1);
            }
            if (word.EndsWith("us".ToCharArray()) || word.EndsWith("ss".ToCharArray()))
            {
                return word;
            }
            if (word[word.Length - 1] == 's' && word.Length >= 2)
            {
                for (int i = 0; i < word.Length - 2; i++)
                {
                    if (IsVowel(word[i]))
                    {
                        return word.Slice(0, word.Length - 1);
                    }
                }
            }
            return word;
        }

        /// <summary>
        /// Removes endings.
        /// </summary>
        /// <param name="r1">The r1.</param>
        /// <param name="r1Index">Index of the r1.</param>
        /// <param name="word">The word.</param>
        /// <returns>The word minus the endings.</returns>
        private Span<char> Step1B(Span<char> r1, int r1Index, Span<char> word)
        {
            for (int i = 0, Step1ReplacementsLength = Step1Replacements.Length; i < Step1ReplacementsLength; i++)
            {
                var Step1Replacement = Step1Replacements[i];
                if (Step1Replacement == "eedly" && r1.EndsWith("eedly".ToCharArray()))
                {
                    return word.Length >= 2 ? word.Slice(0, word.Length - 2) : word;
                }
                else if (Step1Replacement == "eed" && r1.EndsWith("eed".ToCharArray()))
                {
                    return word.Length >= 1 ? word.Slice(0, word.Length - 1) : word;
                }
                else if (word.EndsWith(Step1Replacement.ToCharArray()))
                {
                    bool vowelIsFound = false;

                    if (word.Length > Step1Replacement.Length)
                    {
                        for (int x = 0; x < word.Length - Step1Replacement.Length; x++)
                        {
                            if (IsVowel(word[x]))
                            {
                                word = word.Slice(0, word.Length - Step1Replacement.Length);
                                vowelIsFound = true;
                                break;
                            }
                        }
                    }

                    if (!vowelIsFound)
                        return word;
                    r1 = GetRValue(r1Index, word);

                    if (word.EndsWith("at".ToCharArray())
                        || word.EndsWith("bl".ToCharArray())
                        || word.EndsWith("iz".ToCharArray()))
                    {
                        return word.ToArray().Concat(new char[] { 'e' }).ToArray().AsSpan();
                    }
                    for (int x = 0; x < Doubles.Length; x++)
                    {
                        if (word.EndsWith(Doubles[x].ToCharArray()))
                        {
                            return word.Slice(0, word.Length - 1);
                        }
                    }

                    if (IsShortWord(word, r1))
                    {
                        return word.ToArray().Concat(new char[] { 'e' }).ToArray().AsSpan();
                    }
                    return word;
                }
            }
            return word;
        }

        /// <summary>
        /// Replaces the ending.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The word with a replaced ending.</returns>
        private Span<char> Step1C(Span<char> word)
        {
            if (word.Length > 2
                && (word.EndsWith("y".ToCharArray()) || word.EndsWith("Y".ToCharArray()))
                && !IsVowel(word[word.Length - 2]))
            {
                return word.Slice(0, word.Length - 1).ToArray().Concat(new char[] { 'i' }).ToArray().AsSpan();
            }
            return word;
        }

        /// <summary>
        /// Removes endings
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="r1">The r1.</param>
        private Span<char> Step2(Span<char> word, Span<char> r1)
        {
            foreach (var Step2Replacement in Step2Replacements)
            {
                if (word.EndsWith(Step2Replacement.Key.ToCharArray()))
                {
                    if (!r1.EndsWith(Step2Replacement.Key.ToCharArray()))
                        return word;
                    if (Step2Replacement.Key == "ogi")
                    {
                        if (word.EndsWith("logi".ToCharArray()))
                        {
                            return word.Slice(0, word.Length - 1);
                        }
                    }
                    else if (Step2Replacement.Key == "li")
                    {
                        if (word.Length >= 3)
                        {
                            var liEnding = word[word.Length - 3];
                            if (ValidLiEndings.Contains(liEnding))
                            {
                                return word.Slice(0, word.Length - 2);
                            }
                        }
                        return word;
                    }
                    else if (word.Length >= Step2Replacement.Key.Length)
                    {
                        return word
                            .Slice(0, word.Length - Step2Replacement.Key.Length)
                            .ToArray()
                            .Concat(Step2Replacement.Value)
                            .ToArray()
                            .AsSpan();
                    }
                }
            }
            return word;
        }

        /// <summary>
        /// Remove endings
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="r1">The r1.</param>
        /// <param name="r2">The r2.</param>
        /// <returns></returns>
        private Span<char> Step3(Span<char> word, Span<char> r1, Span<char> r2)
        {
            foreach (var Step3Replacement in Step3Replacements)
            {
                if (r1.EndsWith(Step3Replacement.Key.ToCharArray()))
                {
                    if (Step3Replacement.Key == "ative")
                    {
                        return r2.EndsWith("ative".ToCharArray()) ? word.Slice(0, word.Length - Step3Replacement.Key.Length) : word;
                    }
                    return word
                        .Slice(0, word.Length - Step3Replacement.Key.Length)
                        .ToArray()
                        .Concat(Step3Replacement.Value)
                        .ToArray()
                        .AsSpan();
                }
            }
            return word;
        }

        /// <summary>
        /// Remove yet more endings.
        /// </summary>
        /// <param name="data">The</param>
        private Span<char> Step4(Span<char> word, Span<char> r2)
        {
            for (int i = 0; i < Step4Replacements.Length; ++i)
            {
                string end = Step4Replacements[i];

                if (word.EndsWith(end.ToCharArray()))
                {
                    if (r2.EndsWith(end.ToCharArray()))
                    {
                        if (end == "ion")
                        {
                            char preChar = word.Length > 4 ? word[word.Length - 4] : '\0';

                            if (preChar == 's' || preChar == 't')
                            {
                                return word.Slice(0, word.Length - Step4Replacements[i].Length);
                            }
                        }
                        else
                        {
                            return word.Slice(0, word.Length - Step4Replacements[i].Length);
                        }
                    }

                    return word;
                }
            }
            return word;
        }

        /// <summary>
        /// Remove even more endings.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="r1">The r1.</param>
        /// <param name="r2">The r2.</param>
        /// <returns>The resulting word</returns>
        private Span<char> Step5(Span<char> word, Span<char> r1, Span<char> r2)
        {
            if (r1.Length > 0 && r1[r1.Length - 1] == 'e' && !IsShortSyllable(word, word.Length - 3))
                return word.Slice(0, word.Length - 1);
            if (r2.Length == 0)
                return word;
            if (r2[r2.Length - 1] == 'e' || (r2[r2.Length - 1] == 'l' && word.EndsWith("ll".ToCharArray())))
            {
                return word.Slice(0, word.Length - 1);
            }
            return word;
        }
    }
}