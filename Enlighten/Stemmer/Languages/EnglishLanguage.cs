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

            var WordSpan = new Span<char>(word.ToCharArray());
            if (WordSpan[0] == '\'')
                WordSpan = WordSpan.Slice(1);

            word = new string(WordSpan.ToArray());

            //Check for exceptions
            if (Exceptions.ContainsKey(word))
                return Exceptions[word];

            //Set initial y, or y after a vowel, to Y.
            if (WordSpan.Length > 0)
            {
                if (WordSpan[0] == 'y')
                {
                    WordSpan[0] = 'Y';
                }
                if (WordSpan.Length > 1)
                {
                    for (int x = 1; x < WordSpan.Length; x++)
                    {
                        if (WordSpan[x] == 'y' && IsVowel(WordSpan[x - 1]))
                        {
                            WordSpan[x] = 'Y';
                        }
                    }
                }
            }

            var Data = new EnglishDataHolder();

            (Data.R1Index, Data.R2Index) = CalculateR1AndR2(WordSpan);

            WordSpan = Step0(WordSpan);

            WordSpan = Step1A(WordSpan);

            Data.Word = new string(WordSpan.ToArray());

            //Check Exceptions
            if (Exceptions2.Contains(Data.Word))
                return Data.Word;

            var Region1 = GetRValue(Data.R1Index, WordSpan);
            var Region2 = GetRValue(Data.R2Index, WordSpan);

            Step1B(Data);

            Region1 = GetRValue(Data.R1Index, WordSpan);
            Region2 = GetRValue(Data.R2Index, WordSpan);

            Data.Word = Step1C(Data.Word);

            Region1 = GetRValue(Data.R1Index, WordSpan);
            Region2 = GetRValue(Data.R2Index, WordSpan);

            Step2(Data);

            Region1 = GetRValue(Data.R1Index, WordSpan);
            Region2 = GetRValue(Data.R2Index, WordSpan);

            Step3(Data);

            Region1 = GetRValue(Data.R1Index, WordSpan);
            Region2 = GetRValue(Data.R2Index, WordSpan);

            Step4(Data);

            Region1 = GetRValue(Data.R1Index, WordSpan);
            Region2 = GetRValue(Data.R2Index, WordSpan);

            Step5(Data);

            return Data.Word.ToLowerInvariant();
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

            // Calculate R2
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
        private Span<char> Step0(Span<char> word)
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
                if (word.Length > 4)
                {
                    return word.Slice(0, word.Length - 2);
                }
                return word.Slice(0, word.Length - 1);
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
                        word = word.Slice(0, word.Length - 1);
                        break;
                    }
                }
            }
            return word;
        }

        /// <summary>
        /// Removes endings.
        /// </summary>
        /// <param name="dataHolder">The data holder.</param>
        /// <param name="R1">The r1.</param>
        /// <param name="word">The word.</param>
        /// <returns>The word minus the endings.</returns>
        private Span<char> Step1B(EnglishDataHolder dataHolder, Span<char> R1, Span<char> word)
        {
            for (int i = 0, Step1ReplacementsLength = Step1Replacements.Length; i < Step1ReplacementsLength; i++)
            {
                var Step1Replacement = Step1Replacements[i];
                if (Step1Replacement == "eedly" && R1.EndsWith("eedly".ToCharArray()))
                {
                    return word.Length >= 2 ? word.Slice(word.Length - 2) : word;
                }
                else if (Step1Replacement == "eed" && R1.EndsWith("eed".ToCharArray()))
                {
                    return word.Length >= 1 ? word.Slice(word.Length - 1) : word;
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
                                word = word.Slice(word.Length - Step1Replacement.Length);
                                vowelIsFound = true;
                                break;
                            }
                        }
                    }

                    if (vowelIsFound)
                    {
                        R1 = GetRValue(dataHolder.R1Index, word);

                        if (word.EndsWith("at".ToCharArray())
                            || word.EndsWith("bl".ToCharArray())
                            || word.EndsWith("iz".ToCharArray()))
                        {
                            word = word + new Span<char>("e".ToCharArray());
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