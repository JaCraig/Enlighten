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

            //Clean up word.
            word = word.ToLowerInvariant();

            if (word.StartsWith("'", StringComparison.Ordinal))
            {
                word = word.Substring(1);
            }

            //Check for exceptions
            if (Exceptions.ContainsKey(word))
                return Exceptions[word];

            //Set initial y, or y after a vowel, to Y.
            if (word.Length > 0)
            {
                var Chars = word.ToCharArray();
                if (Chars[0] == 'y')
                {
                    Chars[0] = 'Y';
                }
                if (Chars.Length > 1)
                {
                    for (int x = 1; x < Chars.Length; x++)
                    {
                        if (Chars[x] == 'y' && IsVowel(Chars[x - 1]))
                        {
                            Chars[x] = 'Y';
                        }
                    }
                }
                word = new string(Chars);
            }

            var Data = new EnglishDataHolder
            {
                Word = word
            };

            (Data.R1Index, Data.R2Index) = CalculateR1AndR2(Data.Word);

            Data.Word = Step0(Data.Word);
            Data.Word = Step1A(Data.Word);

            //Check Exceptions
            if (Exceptions2.Contains(Data.Word))
                return Data.Word;

            SetR1AndR2(Data);

            Step1B(Data);

            SetR1AndR2(Data);

            Data.Word = Step1C(Data.Word);

            SetR1AndR2(Data);

            Step2(Data);

            SetR1AndR2(Data);

            Step3(Data);

            SetR1AndR2(Data);

            Step4(Data);

            SetR1AndR2(Data);

            Step5(Data);

            return Data.Word.ToLowerInvariant();
        }

        /// <summary>
        /// Sets the r1 and r2.
        /// </summary>
        /// <param name="Data">The data.</param>
        private static void SetR1AndR2(EnglishDataHolder Data)
        {
            Data.R1 = Data.R1Index < Data.Word.Length ? Data.Word.Substring(Data.R1Index) : "";
            Data.R2 = Data.R2Index < Data.Word.Length ? Data.Word.Substring(Data.R2Index) : "";
        }

        /// <summary>
        /// Calculates the r1 and r2 values.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The resulting r1 and r2 values.</returns>
        private (int R1, int R2) CalculateR1AndR2(string word)
        {
            int r1 = word.Length;
            int r2 = word.Length;

            char[] characters = word.ToCharArray();

            // Calculate R1
            if (word.StartsWith("gener", StringComparison.Ordinal) || word.StartsWith("arsen", StringComparison.Ordinal))
            {
                r1 = 5;
            }
            else if (word.StartsWith("commun", StringComparison.Ordinal))
            {
                r1 = 6;
            }
            else
            {
                for (int x = 1; x < characters.Length; x++)
                {
                    if (!IsVowel(characters[x]) && IsVowel(characters[x - 1]))
                    {
                        r1 = x + 1;
                        break;
                    }
                }
            }

            // Calculate R2
            for (int x = r1; x < characters.Length; ++x)
            {
                if (!IsVowel(characters[x]) && IsVowel(characters[x - 1]))
                {
                    r2 = x + 1;
                    break;
                }
            }

            return (r1, r2);
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
        private bool IsShortWord(string word, string r1)
        {
            return string.IsNullOrEmpty(r1) && IsShortSyllable(word.ToCharArray(), word.Length - 2);
        }

        /// <summary>
        /// Removes the word endings.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The word without the endings.</returns>
        private string Step0(string word)
        {
            if (word.Length >= 3 && word.EndsWith("'s'", StringComparison.Ordinal))
            {
                return word.Remove(word.Length - 3, 3);
            }
            if (word.Length >= 2 && word.EndsWith("'s", StringComparison.Ordinal))
            {
                return word.Remove(word.Length - 2, 2);
            }
            if (word.Length >= 1 && word.EndsWith("'", StringComparison.Ordinal))
            {
                return word.Remove(word.Length - 1, 1);
            }
            return word;
        }

        /// <summary>
        /// Removes endings
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The word without the endings.</returns>
        private string Step1A(string word)
        {
            if (word.EndsWith("sses", StringComparison.Ordinal))
            {
                return word.Remove(word.Length - 2);
            }
            if (word.EndsWith("ied", StringComparison.Ordinal) || word.EndsWith("ies", StringComparison.Ordinal))
            {
                if (word.Length > 4)
                {
                    return word.Remove(word.Length - 2);
                }
                return word.Remove(word.Length - 1);
            }
            if (word.EndsWith("us", StringComparison.Ordinal) || word.EndsWith("ss", StringComparison.Ordinal))
            {
                return word;
            }
            if (word.EndsWith("s", StringComparison.Ordinal))
            {
                var chars = word.ToCharArray();

                if (chars.Length >= 2)
                {
                    for (int i = 0; i < chars.Length - 2; i++)
                    {
                        if (IsVowel(chars[i]))
                        {
                            word = word.Remove(word.Length - 1, 1);
                            break;
                        }
                    }
                }
            }
            return word;
        }

        /// <summary>
        /// Removes endings.
        /// </summary>
        /// <param name="dataHolder">The data holder.</param>
        /// <returns>The word minus the endings.</returns>
        private void Step1B(EnglishDataHolder dataHolder)
        {
            for (int i = 0, Step1ReplacementsLength = Step1Replacements.Length; i < Step1ReplacementsLength; i++)
            {
                var Step1Replacement = Step1Replacements[i];
                if (Step1Replacement == "eedly" && dataHolder.R1.EndsWith("eedly", StringComparison.Ordinal))
                {
                    dataHolder.Word = dataHolder.Word.Length >= 2 ? dataHolder.Word.Remove(dataHolder.Word.Length - 2, 2) : dataHolder.Word;
                    return;
                }
                else if (Step1Replacement == "eed" && dataHolder.R1.EndsWith("eed", StringComparison.Ordinal))
                {
                    dataHolder.Word = dataHolder.Word.Length >= 1 ? dataHolder.Word.Remove(dataHolder.Word.Length - 1, 1) : dataHolder.Word;
                    return;
                }
                else if (dataHolder.Word.EndsWith(Step1Replacement, StringComparison.Ordinal))
                {
                    var Chars = dataHolder.Word.ToCharArray();

                    bool vowelIsFound = false;

                    if (Chars.Length > Step1Replacement.Length)
                    {
                        for (int x = 0; x < Chars.Length - Step1Replacement.Length; x++)
                        {
                            if (IsVowel(Chars[x]))
                            {
                                dataHolder.Word = dataHolder.Word.Remove(dataHolder.Word.Length - Step1Replacement.Length);
                                vowelIsFound = true;
                                break;
                            }
                        }
                    }

                    if (vowelIsFound)
                    {
                        dataHolder.R1 = dataHolder.R1Index < dataHolder.Word.Length ? dataHolder.Word.Substring(dataHolder.R1Index) : "";
                        dataHolder.R2 = dataHolder.R2Index < dataHolder.Word.Length ? dataHolder.Word.Substring(dataHolder.R2Index) : "";

                        if (dataHolder.Word.EndsWith("at", StringComparison.Ordinal)
                            || dataHolder.Word.EndsWith("bl", StringComparison.Ordinal)
                            || dataHolder.Word.EndsWith("iz", StringComparison.Ordinal))
                        {
                            dataHolder.Word += "e";
                        }
                        else
                        {
                            bool ContinueProcessing = true;
                            for (int x = 0; x < Doubles.Length; x++)
                            {
                                if (dataHolder.Word.EndsWith(Doubles[x], StringComparison.Ordinal))
                                {
                                    dataHolder.Word = dataHolder.Word.Remove(dataHolder.Word.Length - 1, 1);
                                    ContinueProcessing = false;
                                    break;
                                }
                            }

                            if (ContinueProcessing && IsShortWord(dataHolder.Word, dataHolder.R1))
                            {
                                dataHolder.Word += "e";
                            }
                        }
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// Replaces the ending.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The word with a replaced ending.</returns>
        private string Step1C(string word)
        {
            if (word.Length > 2
                && (word.EndsWith("y", StringComparison.Ordinal) || word.EndsWith("Y", StringComparison.Ordinal))
                && !IsVowel(word[word.Length - 2]))
            {
                word = word.Remove(word.Length - 1);
                word += "i";
            }
            return word;
        }

        /// <summary>
        /// Removes endings
        /// </summary>
        /// <param name="data">The data.</param>
        private void Step2(EnglishDataHolder data)
        {
            foreach (var Step2Replacement in Step2Replacements)
            {
                if (data.Word.EndsWith(Step2Replacement.Key, StringComparison.Ordinal))
                {
                    if (data.R1.EndsWith(Step2Replacement.Key, StringComparison.Ordinal))
                    {
                        if (Step2Replacement.Key == "ogi")
                        {
                            if (data.Word.EndsWith("logi", StringComparison.Ordinal))
                            {
                                data.Word = data.Word.Remove(data.Word.Length - 1);
                            }
                        }
                        else if (Step2Replacement.Key == "li")
                        {
                            if (data.Word.Length >= 3)
                            {
                                string liEnding = data.Word.Substring(data.Word.Length - 3, 1);
                                if (ValidLiEndings.Contains(liEnding))
                                {
                                    data.Word = data.Word.Remove(data.Word.Length - 2);
                                    break;
                                }
                            }
                        }
                        else if (data.Word.Length >= Step2Replacement.Key.Length)
                        {
                            data.Word = data.Word.Remove(data.Word.Length - Step2Replacement.Key.Length);
                            data.Word += Step2Replacement.Value;
                        }
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Remove endings
        /// </summary>
        /// <param name="data">The data.</param>
        private void Step3(EnglishDataHolder data)
        {
            foreach (var Step3Replacement in Step3Replacements)
            {
                if (data.R1.EndsWith(Step3Replacement.Key, StringComparison.Ordinal))
                {
                    if (Step3Replacement.Key == "ative")
                    {
                        if (data.R2.EndsWith("ative", StringComparison.Ordinal))
                        {
                            data.Word = data.Word.Remove(data.Word.Length - Step3Replacement.Key.Length);
                        }
                    }
                    else
                    {
                        data.Word = data.Word.Remove(data.Word.Length - Step3Replacement.Key.Length);
                        data.Word += Step3Replacement.Value;
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// Remove yet more endings.
        /// </summary>
        /// <param name="data">The data.</param>
        private void Step4(EnglishDataHolder data)
        {
            for (int i = 0; i < Step4Replacements.Length; ++i)
            {
                string end = Step4Replacements[i];

                if (data.Word.EndsWith(end, StringComparison.Ordinal))
                {
                    if (data.R2.EndsWith(end, StringComparison.Ordinal))
                    {
                        if (end == "ion")
                        {
                            char preChar = data.Word.Length > 4 ? data.Word[data.Word.Length - 4] : '\0';

                            if (preChar == 's' || preChar == 't')
                            {
                                data.Word = data.Word.Remove(data.Word.Length - Step4Replacements[i].Length);
                            }
                        }
                        else
                        {
                            data.Word = data.Word.Remove(data.Word.Length - Step4Replacements[i].Length);
                        }
                    }

                    return;
                }
            }
        }

        /// <summary>
        /// Remove even more endings.
        /// </summary>
        /// <param name="data">The data.</param>
        private void Step5(EnglishDataHolder data)
        {
            if (data.R2.EndsWith("e", StringComparison.Ordinal)
                || (data.R1.EndsWith("e", StringComparison.Ordinal) && !IsShortSyllable(data.Word.ToCharArray(), data.Word.Length - 3)))
            {
                data.Word = data.Word.Remove(data.Word.Length - 1);
            }
            else if (data.R2.EndsWith("l", StringComparison.Ordinal) && data.Word.EndsWith("ll", StringComparison.Ordinal))
            {
                data.Word = data.Word.Remove(data.Word.Length - 1);
            }
        }
    }
}