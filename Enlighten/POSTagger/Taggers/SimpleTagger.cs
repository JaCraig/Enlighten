using Enlighten.Inflector.Enums;
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
            Lexicon = new Dictionary<string, LexiconRule>();
            Rules = new List<BrillRule>();
            Infinitives = new List<string>();
            BuildSuffixes();
            BuildLexicon();
            BuildRules();
            Inflector = inflector;
            SynonymFinder = synonymFinder;
        }

        /// <summary>
        /// Gets the infinitives.
        /// </summary>
        /// <value>The infinitives.</value>
        public List<string> Infinitives { get; }

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
        public Dictionary<string, LexiconRule> Lexicon { get; }

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
        /// The is capitalized
        /// </summary>
        private static readonly Regex IsCapitalized = new Regex("^[A-Z]", RegexOptions.Compiled);

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

        /// <summary>
        /// Gets the tag.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        public TagProbability GetTag(Token token)
        {
            return TestKnownTokens(token) ??
                   TestNormalTag(token) ??
                   TestSynonymTag(token) ??
                   TestRepetitiveChars(token) ??
                   TestLowerCase(token) ??
                   TestSuffixes(token) ??
                   TestComposedWords(token) ??
                   new TagProbability("NN", token.Value, 0, false);
        }

        /// <summary>
        /// Tags the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The tokens tagged.</returns>
        public Token[] Tag(Token[] tokens)
        {
            var Tags = new TagProbability[tokens.Length];
            for (int x = 0; x < tokens.Length; ++x)
            {
                Tags[x] = GetTag(tokens[x]);
            }
            for (int x = 0; x < tokens.Length; ++x)
            {
                var Tag = Tags[x];
                var Token = tokens[x];
                if (Tags[x].Tag == "SYM" || Tags[x].Tag == ".")
                    continue;
                if (x == 0)
                {
                    if (Token.NormalizedValue == "that")
                    {
                        Tag.Tag = "DT";
                        continue;
                    }
                    if ((Tag.Tag == "NN" || Tag.Tag == "VB") && Infinitives.Contains(Token.NormalizedValue))
                    {
                        Tag.Tag = "VB";
                        continue;
                    }
                }
            }
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
                var Item = LineData[1];
                Lexicon.Add(Key, new LexiconRule(Key, Item));
            }

            Data = new FileInfo("resource://Enlighten/Enlighten.POSTagger.Resources.English/regularverbs.txt").Read();
            Lines = Data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, LinesLength = Lines.Length; i < LinesLength; i++)
            {
                var Line = Lines[i];
                if (string.IsNullOrEmpty(Line))
                    continue;
                var Key = Line;
                Infinitives.Add(Key);
                var Item = Inflector.ToPresent(Key, InflectorLanguage.English);
                Lexicon.TryAdd(Item, new LexiconRule(Item, "VB"));
                Item = Inflector.ToPast(Key, InflectorLanguage.English);
                Lexicon.TryAdd(Item, new LexiconRule(Item, "VBN"));
                Item = Inflector.ToGerund(Key, InflectorLanguage.English);
                Lexicon.TryAdd(Item, new LexiconRule(Item, "VBG"));
            }

            Data = new FileInfo("resource://Enlighten/Enlighten.POSTagger.Resources.English/irregularverbs.txt").Read();
            Lines = Data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, LinesLength = Lines.Length; i < LinesLength; i++)
            {
                var Line = Lines[i];
                if (string.IsNullOrEmpty(Line))
                    continue;
                var Items = Line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Infinitives.Add(Items[0]);
                Lexicon.TryAdd(Items[0], new LexiconRule(Items[0], "VB"));
                Lexicon.TryAdd(Items[1], new LexiconRule(Items[1], "VBD"));
                Lexicon.TryAdd(Items[2], new LexiconRule(Items[2], "VBN"));
                Lexicon.TryAdd(Items[3], new LexiconRule(Items[3], "VBZ"));
                Lexicon.TryAdd(Items[4], new LexiconRule(Items[4], "VBG"));
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

        /// <summary>
        /// Tests the composed words.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private TagProbability? TestComposedWords(Token token)
        {
            if (!token.Value.Contains('-'))
                return null;
            if (IsCapitalized.IsMatch(token.Value))
                return new TagProbability("NNP", token.Value, 0.5, false);
            return new TagProbability("JJ", token.Value, 0.5, false);
        }

        /// <summary>
        /// Tests the emoji.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The tag probability</returns>
        private TagProbability? TestKnownTokens(Token token)
        {
            if (token.TokenType == TokenType.Word || token.TokenType == TokenType.Abbreviation || token.TokenType == TokenType.Other)
                return null;
            if (token.TokenType == TokenType.Number)
                return new TagProbability("CD", token.Value, 1, true);
            if (token.TokenType == TokenType.Email || token.TokenType == TokenType.HashTag || token.TokenType == TokenType.Username)
                return new TagProbability("NN", token.Value, 1, true);
            if (token.TokenType == TokenType.Emoji)
                return new TagProbability("EM", token.Value, 1, true);
            if (token.TokenType == TokenType.Period || token.TokenType == TokenType.ExclamationMark || token.TokenType == TokenType.QuestionMark)
                return new TagProbability(".", token.Value, 1, true);
            return new TagProbability("SYM", token.Value, 1, true);
        }

        /// <summary>
        /// Tests the lower case.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private TagProbability? TestLowerCase(Token token)
        {
            return Lexicon.TryGetValue(token.NormalizedValue, out var Tag)
                ? new TagProbability(Tag.Tag, token.Value, 0.75, false)
                : null;
        }

        /// <summary>
        /// Tests the normal tag.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>The tag probability.</returns>
        private TagProbability? TestNormalTag(Token token)
        {
            return Lexicon.TryGetValue(token.Value, out var Tag) && Tag.Tag != "-"
                ? new TagProbability(Tag.Tag, token.Value, 1, Tag.Blocked)
                : null;
        }

        /// <summary>
        /// Tests the repetitive chars.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private TagProbability? TestRepetitiveChars(Token token)
        {
            var RepetitiveCharsRemoved = RemoveRepetitiveChars(token.NormalizedValue);
            return !string.IsNullOrEmpty(RepetitiveCharsRemoved) && Lexicon.TryGetValue(RepetitiveCharsRemoved, out var Tag)
                ? new TagProbability(Tag.Tag, RepetitiveCharsRemoved, 0.8, false)
                : null;
        }

        /// <summary>
        /// Tests the suffixes.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private TagProbability? TestSuffixes(Token token)
        {
            var Tag = TestSuffixes(token.Value);
            return !string.IsNullOrEmpty(Tag) ? new TagProbability(Tag, token.Value, 0.25, false) : null;
        }

        /// <summary>
        /// Tests the synonym tag.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        private TagProbability? TestSynonymTag(Token token)
        {
            var Synonym = SynonymFinder.FindSynonym(token.NormalizedValue, SynonymFinderLanguage.English);
            return token.NormalizedValue != Synonym && Lexicon.TryGetValue(Synonym, out var Tag)
                ? new TagProbability(Tag.Tag, token.Value, 1, false)
                : null;
        }
    }
}