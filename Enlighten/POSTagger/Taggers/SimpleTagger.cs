using BigBook;
using Enlighten.Inflector.Interfaces;
using Enlighten.POSTagger.Enum;
using Enlighten.POSTagger.Interfaces;
using Enlighten.SynonymFinder.Enum;
using Enlighten.SynonymFinder.Interfaces;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English.Enums;
using FileCurator;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Enlighten.POSTagger.Taggers
{
    /// <summary>
    /// Simple POS tagger
    /// </summary>
    /// <seealso cref="IPOSTaggerLanguage"/>
    public class SimpleTagger : IPOSTaggerLanguage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleTagger"/> class.
        /// </summary>
        /// <param name="inflector">The inflector.</param>
        /// <param name="synonymFinder">The synonym finder.</param>
        public SimpleTagger(IInflector inflector, ISynonymFinder synonymFinder)
        {
            Suffixes = new Dictionary<string, string>();
            Lexicon = new ListMapping<string, string>();
            Rules = new List<BrillRule>();
            BuildSuffixes();
            BuildLexicon();
            BuildRules();
            Inflector = inflector;
            SynonymFinder = synonymFinder;
        }

        /// <summary>
        /// The potential proper noun
        /// </summary>
        private static readonly Regex PotentialProperNoun = new Regex(@"(^[A-Z][a-z\.]+$)|^[A-Z]+[0-9]+$|^[A-Z][a-z]+[A-Z][a-z]+$", RegexOptions.Compiled);

        /// <summary>
        /// The repetitive chars
        /// </summary>
        private static readonly Regex RepetitiveChars = new Regex(@"(.)\1{2,}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// The repetitive chars2
        /// </summary>
        private static readonly Regex RepetitiveChars2 = new Regex(@"(.)\1{1,}", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Gets the inflector.
        /// </summary>
        /// <value>The inflector.</value>
        public IInflector Inflector { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string ISOCode { get; } = POSTaggerLanguage.BrillTagger;

        /// <summary>
        /// Gets the lexicon.
        /// </summary>
        /// <value>The lexicon.</value>
        public ListMapping<string, string> Lexicon { get; }

        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <value>The rules.</value>
        public List<BrillRule> Rules { get; }

        /// <summary>
        /// Gets the suffixes.
        /// </summary>
        /// <value>The suffixes.</value>
        public Dictionary<string, string> Suffixes { get; }

        /// <summary>
        /// Gets the synonym finder.
        /// </summary>
        /// <value>The synonym finder.</value>
        public ISynonymFinder SynonymFinder { get; }

        /// <summary>
        /// Applies the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="blocked">The blocked.</param>
        /// <returns></returns>
        public string[] Apply(string[] tokens, string[] tags, bool[] blocked)
        {
            for (int x = 0; x < 2; ++x)
            {
                for (int y = 0; y < tokens.Length; ++y)
                {
                    if (!blocked[y])
                    {
                        ApplyRules(tokens[y], y, tokens, tags, x);
                    }
                }
            }
            return tags;
        }

        /// <summary>
        /// Applies the rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="input">The input.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="index">The index.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="run">The run.</param>
        public void ApplyRule(BrillRule rule, string input, string tag, int index, string[] tokens, string[] tags, int run)
        {
            if (rule.From != tag || (rule.SecondRun && run == 0))
                return;

            var type = rule.Type;
            string tmp, tmp2;

            // Start word rule is case sensitive
            if (type == BrillConditions.STARTWORD)
            {
                if (index == 0 && input == rule.C1)
                {
                    tags[index] = rule.To;
                    return;
                }
                return;
            }

            input = input.ToLowerInvariant();
            switch (type)
            {
                case BrillConditions.PREVTAG:
                {
                    if (index > 0 && tags[index - 1] == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.PREVWORDPREVTAG:
                {
                    tmp = tokens[index - 1] ?? string.Empty;
                    if (tags[index - 1] == rule.C2 && tmp.ToLowerInvariant() == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.NEXTTAG:
                {
                    if (tags[index + 1] == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.NEXTTAG2:
                {
                    if (tags[index + 2] == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.PREVTAG2:
                {
                    if (tags[index - 2] == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.PREV1OR2TAG:
                {
                    tmp = tags[index - 1] ?? string.Empty;
                    tmp2 = tags[index - 2] ?? string.Empty;
                    if (tmp == rule.C1 || tmp2 == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.PREVWORD:
                {
                    tmp = tokens[index - 1] ?? string.Empty;
                    if (tmp.ToLowerInvariant() == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.CURRENTWD:
                {
                    if (input == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.WDPREVTAG:
                {
                    if (input == rule.C2 && tags[index - 1] == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.WDPREVWD:
                {
                    tmp = tokens[index - 1] ?? string.Empty;
                    if (input == rule.C2 && tmp.ToLowerInvariant() == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.NEXT1OR2OR3TAG:
                {
                    if (tags[index + 1] == rule.C1 || tags[index + 2] == rule.C1 || tags[index + 3] == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.NEXT2WD:
                {
                    tmp = tokens[index + 2] ?? string.Empty;
                    if (tmp.ToLowerInvariant() == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.WDNEXTWD:
                {
                    tmp = tokens[index + 1] ?? string.Empty;
                    if (input == rule.C1 && tmp.ToLowerInvariant() == rule.C2)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.WDNEXTTAG:
                {
                    if (input == rule.C1 && tags[index + 1] == rule.C2)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.PREV1OR2OR3TAG:
                {
                    if (tags[index - 1] == rule.C1 || tags[index - 2] == rule.C1 || tags[index - 3] == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.SURROUNDTAG:
                {
                    if (tags[index - 1] == rule.C1 && tags[index + 1] == rule.C2)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.SURROUNDTAGWD:
                {
                    if (input == rule.C1 && tags[index - 1] == rule.C2 && tags[index + 1] == rule.C3)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.NEXTWD:
                {
                    tmp = tokens[index + 1] ?? string.Empty;
                    if (tmp.ToLowerInvariant() == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.NEXT1OR2TAG:
                {
                    if (tags[index + 1] == rule.C1 || tags[index + 2] == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.PREV2TAG:
                {
                    if (tags[index - 2] == rule.C1 && tags[index - 1] == rule.C2)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.PREV2TAGNEXTTAG:
                {
                    if (tags[index - 2] == rule.C1 && tags[index - 1] == rule.C2 && tags[index + 1] == rule.C3)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.NEXT2TAG:
                {
                    if (tags[index + 1] == rule.C1 && tags[index + 2] == rule.C2)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.NEXT1OR2WD:
                {
                    tmp = tokens[index + 1] ?? string.Empty;
                    tmp2 = tokens[index + 2] ?? string.Empty;
                    if (tmp.ToLowerInvariant() == rule.C1 || tmp2.ToLowerInvariant() == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.PREV2WD:
                {
                    tmp2 = tokens[index - 2] ?? string.Empty;
                    if (tmp2.ToLowerInvariant() == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }

                case BrillConditions.PREV1OR2WD:
                {
                    tmp = tokens[index - 1] ?? string.Empty;
                    tmp2 = tokens[index - 2] ?? string.Empty;
                    if (tmp.ToLowerInvariant() == rule.C1 || tmp2.ToLowerInvariant() == rule.C1)
                    {
                        tags[index] = rule.To;
                        return;
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// Applies the rules.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="index">The index.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="run">The run.</param>
        public void ApplyRules(string input, int index, string[] tokens, string[] tags, int run)
        {
            for (var i = 0; i < Rules.Count; i++)
            {
                ApplyRule(Rules[i], input, tags[index], index, tokens, tags, run);
            }
        }

        public TagProbability GetTag(Token token)
        {
            TagProbability tagObject = new TagProbability(string.Empty, string.Empty, 0, false)
            {
                Norm = token.Value
            };

            if (token.TokenType == TokenType.Emoji)
            {
                return new TagProbability("EM", token.Value, 1, true);
            }

            Lexicon.TryGetValue(token, out )

            // Attempt to get pos in a case sensitive way
            tag = compendium.lexicon[jsKeywords.indexOf(token) > -1 ? '_' + token : token];

            if (!!tag && tag !== '-')
            {
                tagObject.tag = tag;
                tagObject.blocked = tag.blocked;
                tagObject.confidence = 1;
                return tagObject;
            }

            lower = token.toLowerCase();

            // Test synonyms
            tmp = compendium.synonym(lower);
            if (tmp !== lower)
            {
                tag = compendium.lexicon[tmp];

                if (!!tag)
                {
                    tagObject.tag = tag;
                    tagObject.confidence = 1;
                    return tagObject;
                }
            }

            // Test chars streak
            if (lower.match(/ (\w)\1 +/ g)) {
                tmp = removeRepetitiveChars(lower);
                if (!!tmp)
                {
                    tagObject.norm = tmp;
                    tag = compendium.lexicon[tmp];

                    tagObject.tag = tag;
                    tagObject.confidence = 0.8;
                    return tagObject;
                }
            }

            // If none, try with lower cased
            if (typeof token === 'string' && token.match(/[A - Z] / g))
            {
                tag = compendium.lexicon[lower];

                if (!!tag && tag !== '-')
                {
                    tagObject.tag = tag;
                    tagObject.confidence = 0.75;
                    return tagObject;
                }
            }

            // Test common suffixes.
            tag = pos.testSuffixes(token);
            if (!!tag)
            {
                tagObject.tag = tag;
                tagObject.confidence = 0.25;
                return tagObject;
            }

            // If no tag, check composed words
            if (token.indexOf('-') > -1)
            {
                // If capitalized, likely NNP
                if (token.match(/ ^[A - Z] / g))
                {
                    tagObject.tag = 'NNP';
                    // Composed words are very often adjectives
                }
                else
                {
                    tagObject.tag = 'JJ';
                }
                tagObject.confidence /= 2;
                return tagObject;
            }

            // We default to NN if still no tag
            return tagObject;
        }

        /// <summary>
        /// Tags the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The tokens tagged.</returns>
        public Token[] Tag(Token[] tokens)
        {
            return tokens;
        }

        /// <summary>
        /// Tests the suffixes.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public string TestSuffixes(string input)
        {
            foreach (var Key in Suffixes.Keys)
            {
                if (input.EndsWith(Key))
                {
                    return Suffixes[Key];
                }
            }

            return input;
        }

        /// <summary>
        /// Builds the lexicon.
        /// </summary>
        private void BuildLexicon()
        {
            var Data = new FileInfo("resource://Enlighten/Enlighten.POSTagger.Resources.English/lexicon.txt").Read();
            var Lines = Data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, LinesLength = Lines.Length; i < LinesLength; i++)
            {
                var Line = Lines[i];
                if (string.IsNullOrEmpty(Line))
                    continue;
                var LineData = Line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var Key = LineData[0];
                for (int j = 1, LineDataLength = LineData.Length; j < LineDataLength; j++)
                {
                    var Item = LineData[j];
                    Lexicon.Add(Key, Item);
                }
            }
        }

        /// <summary>
        /// Builds the rules.
        /// </summary>
        private void BuildRules()
        {
            var Data = new FileInfo("resource://Enlighten/Enlighten.POSTagger.Resources.English/rules.txt").Read();
            var Lines = Data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, LinesLength = Lines.Length; i < LinesLength; i++)
            {
                var Line = Lines[i];
                if (string.IsNullOrEmpty(Line))
                    continue;

                var Values = Line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                bool SecondRun = Values[^1] == "+";
                var From = Values[0];
                var To = Values[1];
                var Type = (BrillConditions)int.Parse(Values[2]);
                string? C1 = null, C2 = null, C3 = null;
                if (Values.Length > 3)
                    C1 = Values[3];
                if (Values.Length > 4)
                    C2 = Values[4];
                if (Values.Length > 5)
                    C3 = Values[5];
                Rules.Add(new BrillRule(From, To, Type, C1, C2, C3, SecondRun));
            }
        }

        /// <summary>
        /// Builds the suffixes.
        /// </summary>
        private void BuildSuffixes()
        {
            var Data = new FileInfo("resource://Enlighten/Enlighten.POSTagger.Resources.English/suffixes.txt").Read();
            var Lines = Data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, LinesLength = Lines.Length; i < LinesLength; i++)
            {
                var Line = Lines[i];
                if (string.IsNullOrEmpty(Line))
                    continue;
                var LineData = Line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Suffixes.Add(LineData[0], LineData[1]);
            }
        }

        /// <summary>
        /// Determines whether [is match for potential proper noun] [the specified input].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        /// <c>true</c> if [is match for potential proper noun] [the specified input]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMatchForPotentialProperNoun(string input)
        {
            return PotentialProperNoun.IsMatch(input);
        }

        /// <summary>
        /// Removes the repetitive chars.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The resulting string.</returns>
        private string RemoveRepetitiveChars(string input)
        {
            var TempString = RepetitiveChars.Replace(input, "$1$1");
            if (Lexicon.ContainsKey(TempString))
                return TempString;

            var TempString2 = SynonymFinder.FindSynonym(TempString, SynonymFinderLanguage.English);
            if (TempString != TempString2)
                return TempString2;

            TempString = RepetitiveChars2.Replace(input, "$1");
            if (Lexicon.ContainsKey(TempString))
                return TempString;

            TempString2 = SynonymFinder.FindSynonym(TempString, SynonymFinderLanguage.English);
            if (TempString != TempString2)
                return TempString2;

            return input;
        }
    }
}