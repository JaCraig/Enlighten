/*
Copyright 2019 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using Enlighten.Stemmer.BaseClasses;
using Enlighten.Stemmer.Enums;
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
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string ISOCode { get; } = StemmerLanguage.EnglishPorter2;

        /// <summary>
        /// Gets the vowels.
        /// </summary>
        /// <value>The vowels.</value>
        protected override char[] Vowels { get; } = ['a', 'e', 'i', 'o', 'u', 'y'];

        /// <summary>
        /// Gets the doubles.
        /// </summary>
        /// <value>The doubles.</value>
        private static char[][] Doubles { get; } =
        [
            ['b','b'],
            ['d','d'],
            ['f','f'],
            ['g','g'],
            ['m','m'],
            ['n','n'],
            ['p','p'],
            ['r','r'],
            ['t','t']
        ];

        /// <summary>
        /// Gets the exceptions.
        /// </summary>
        /// <value>The exceptions.</value>
        private static Dictionary<string, string> Exceptions { get; } = new Dictionary<string, string>
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
        private static string[] Exceptions2 { get; } = ["inning", "outing", "canning", "herring", "earring", "proceed", "exceed", "succeed"];

        /// <summary>
        /// Gets the step1 replacements.
        /// </summary>
        /// <value>The step1 replacements.</value>
        private static char[][] Step1Replacements { get; } =
        [
            ['e','e','d','l','y'],
            ['i','n','g','l','y'],
            ['e','d','l','y'],
            ['e','e','d'],
            ['i','n','g'],
            ['e','d'],
        ];

        /// <summary>
        /// Gets the step2 replacements.
        /// </summary>
        /// <value>The step2 replacements.</value>
        private static Dictionary<string, string> Step2Replacements { get; } = new Dictionary<string, string>
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
        private static Dictionary<string, string> Step3Replacements { get; } = new Dictionary<string, string>
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
        private static char[][] Step4Replacements { get; } =
        [
            ['e','m','e','n','t'],
            ['m','e','n','t'],
            ['e','n','c','e'],
            ['a','b','l','e'],
            ['i','b','l','e'],
            ['a','n','c','e'],
            ['i','s','m'],
            ['e','n','t'],
            ['a','t','e'],
            ['i','t','i'],
            ['a','n','t'],
            ['o','u','s'],
            ['i','v','e'],
            ['i','z','e'],
            ['i','o','n'],
            ['i','c'],
            ['e','r'],
            ['a','l']
        ];

        /// <summary>
        /// Gets the valid li endings.
        /// </summary>
        /// <value>The valid li endings.</value>
        private static char[] ValidLiEndings { get; } = ['c', 'd', 'e', 'g', 'h', 'k', 'm', 'n', 'r', 't'];

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
                word = word[1..];

            //Check for exceptions
            if (Exceptions.TryGetValue(word, out var Value))
                return Value;

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

            (var R1Index, var R2Index) = CalculateR1AndR2(WordSpan);

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
        private static Span<char> GetRValue(int RIndex, Span<char> word) => RIndex < word.Length ? word[RIndex..] : Span<char>.Empty;

        /// <summary>
        /// Removes the word endings.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The word without the endings.</returns>
        private static Span<char> Step0(Span<char> word)
        {
            if (word.Length >= 3 && word[^3] == '\'' && word[^2] == 's' && word[^1] == '\'')
            {
                return word[..^3];
            }
            if (word.Length >= 2 && word[^2] == '\'' && word[^1] == 's')
            {
                return word[..^2];
            }
            if (word.Length >= 1 && word[^1] == '\'')
            {
                return word[..^1];
            }
            return word;
        }

        /// <summary>
        /// Removes endings
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="r1">The r1.</param>
        private static Span<char> Step2(Span<char> word, Span<char> r1)
        {
            foreach (KeyValuePair<string, string> Step2Replacement in Step2Replacements)
            {
                if (word.EndsWith(Step2Replacement.Key.ToCharArray()))
                {
                    if (!r1.EndsWith(Step2Replacement.Key.ToCharArray()))
                        return word;
                    if (Step2Replacement.Key == "ogi")
                    {
                        if (word.EndsWith(['l', 'o', 'g', 'i']))
                        {
                            return word[..^1];
                        }
                    }
                    else if (Step2Replacement.Key == "li")
                    {
                        if (word.Length >= 3)
                        {
                            var liEnding = word[^3];
                            if (ValidLiEndings.Contains(liEnding))
                            {
                                return word[..^2];
                            }
                        }
                        return word;
                    }
                    else if (word.Length >= Step2Replacement.Key.Length)
                    {
                        word = word[..^Step2Replacement.Key.Length];
                        var Final = new char[word.Length + Step2Replacement.Value.Length];
                        Array.Copy(word.ToArray(), Final, word.Length);
                        Array.Copy(Step2Replacement.Value.ToCharArray(), 0, Final, word.Length, Step2Replacement.Value.Length);
                        return Final.AsSpan();
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
        private static Span<char> Step3(Span<char> word, Span<char> r1, Span<char> r2)
        {
            foreach (KeyValuePair<string, string> Step3Replacement in Step3Replacements)
            {
                if (r1.EndsWith(Step3Replacement.Key.ToCharArray()))
                {
                    if (Step3Replacement.Key == "ative")
                    {
                        return r2.EndsWith(['a', 't', 'i', 'v', 'e']) ? word[..^Step3Replacement.Key.Length] : word;
                    }
                    word = word[..^Step3Replacement.Key.Length];
                    var Final = new char[word.Length + Step3Replacement.Value.Length];
                    Array.Copy(word.ToArray(), Final, word.Length);
                    Array.Copy(Step3Replacement.Value.ToCharArray(), 0, Final, word.Length, Step3Replacement.Value.Length);
                    return Final.AsSpan();
                }
            }
            return word;
        }

        /// <summary>
        /// Remove yet more endings.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <param name="r2">The r2.</param>
        /// <returns>The word without more endings.</returns>
        private static Span<char> Step4(Span<char> word, Span<char> r2)
        {
            for (var i = 0; i < Step4Replacements.Length; ++i)
            {
                var end = Step4Replacements[i];

                if (word.EndsWith(end))
                {
                    if (r2.EndsWith(end))
                    {
                        if (end[0] == 'i' && end[1] == 'o' && end[2] == 'n')
                        {
                            var preChar = word.Length > 4 ? word[^4] : '\0';

                            if (preChar is 's' or 't')
                            {
                                return word[..^Step4Replacements[i].Length];
                            }
                        }
                        else
                        {
                            return word[..^Step4Replacements[i].Length];
                        }
                    }

                    return word;
                }
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
            var r1 = wordSpan.Length;
            var r2 = wordSpan.Length;
            if (wordSpan.StartsWith(['g', 'e', 'n', 'e', 'r']) || wordSpan.StartsWith(['a', 'r', 's', 'e', 'n']))
            {
                r1 = 5;
            }
            else if (wordSpan.StartsWith(['c', 'o', 'm', 'm', 'u', 'n']))
            {
                r1 = 6;
            }
            else
            {
                for (var x = 1; x < wordSpan.Length; x++)
                {
                    if (!IsVowel(wordSpan[x]) && IsVowel(wordSpan[x - 1]))
                    {
                        r1 = x + 1;
                        break;
                    }
                }
            }

            for (var x = r1; x < wordSpan.Length; ++x)
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

            for (var x = 1; x < WordSpan.Length; x++)
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
        private bool IsShortWord(Span<char> word, Span<char> r1) => r1.IsEmpty && IsShortSyllable(word, word.Length - 2);

        /// <summary>
        /// Removes endings
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The word without the endings.</returns>
        private Span<char> Step1A(Span<char> word)
        {
            if (word.EndsWith(['s', 's', 'e', 's']))
            {
                return word[..^2];
            }
            if (word.EndsWith(['i', 'e', 'd']) || word.EndsWith(['i', 'e', 's']))
            {
                return word.Length > 4 ? word[..^2] : word[..^1];
            }
            if (word.EndsWith(['u', 's']) || word.EndsWith(['s', 's']))
            {
                return word;
            }
            if (word[^1] == 's' && word.Length >= 2)
            {
                for (var i = 0; i < word.Length - 2; i++)
                {
                    if (IsVowel(word[i]))
                    {
                        return word[..^1];
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
                if (i == 0 && r1.EndsWith(Step1Replacement))
                {
                    return word.Length >= 2 ? word[..^2] : word;
                }
                if (i == 3 && r1.EndsWith(Step1Replacement))
                {
                    return word.Length >= 1 ? word[..^1] : word;
                }
                if (word.EndsWith(Step1Replacement))
                {
                    var vowelIsFound = false;

                    if (word.Length > Step1Replacement.Length)
                    {
                        for (var x = 0; x < word.Length - Step1Replacement.Length; x++)
                        {
                            if (IsVowel(word[x]))
                            {
                                word = word[..^Step1Replacement.Length];
                                vowelIsFound = true;
                                break;
                            }
                        }
                    }

                    if (!vowelIsFound)
                        return word;
                    r1 = GetRValue(r1Index, word);

                    if ((word[^2] == 'a' && word[^1] == 't')
                        || (word[^2] == 'b' && word[^1] == 'l')
                        || (word[^2] == 'i' && word[^1] == 'z'))
                    {
                        var Final = new char[word.Length + 1];
                        Array.Copy(word.ToArray(), Final, word.Length);
                        Array.Copy(new char[] { 'e' }, 0, Final, word.Length, 1);
                        return Final.AsSpan();
                    }
                    for (var x = 0; x < Doubles.Length; x++)
                    {
                        if (word.EndsWith(Doubles[x]))
                        {
                            return word[..^1];
                        }
                    }

                    if (IsShortWord(word, r1))
                    {
                        var Final = new char[word.Length + 1];
                        Array.Copy(word.ToArray(), Final, word.Length);
                        Array.Copy(new char[] { 'e' }, 0, Final, word.Length, 1);
                        return Final.AsSpan();
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
                && (word[^1] == 'y' || word[^1] == 'Y')
                && !IsVowel(word[^2]))
            {
                var Final = new char[word.Length];
                Array.Copy(word[..^1].ToArray(), Final, word.Length - 1);
                Array.Copy(new char[] { 'i' }, 0, Final, word.Length - 1, 1);
                return Final.AsSpan();
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
            if (r1.Length > 0 && r1[^1] == 'e' && !IsShortSyllable(word, word.Length - 3))
                return word[..^1];
            if (r2.Length == 0)
                return word;
            if (r2[^1] == 'e' || (r2[^1] == 'l' && word[^2] == 'l' && word[^1] == 'l'))
            {
                return word[..^1];
            }
            return word;
        }
    }
}