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
using System.Linq;
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
            Inflector = inflector;
            SynonymFinder = synonymFinder;
            BuildSuffixes();
            BuildLexicon();
            BuildRules();
            BuildNationalities();
        }

        /// <summary>
        /// Gets the infinitives.
        /// </summary>
        /// <value>The infinitives.</value>
        public List<string> Infinitives { get; } = new List<string>();

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
        public Dictionary<string, LexiconRule> Lexicon { get; } = new Dictionary<string, LexiconRule>();

        /// <summary>
        /// Gets the rules.
        /// </summary>
        /// <value>The rules.</value>
        public List<BrillRule> Rules { get; } = new List<BrillRule>();

        /// <summary>
        /// Gets the suffixes.
        /// </summary>
        /// <value>The suffixes.</value>
        public Dictionary<string, string> Suffixes { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets the synonym finder.
        /// </summary>
        /// <value>The synonym finder.</value>
        public ISynonymFinder SynonymFinder { get; }

        /// <summary>
        /// Gets the nationalities.
        /// </summary>
        /// <value>The nationalities.</value>
        private List<string> Nationalities { get; } = new List<string>();

        /// <summary>
        /// The is capitalized
        /// </summary>
        private static readonly Regex IsCapitalized = new Regex("^[A-Z]", RegexOptions.Compiled);

        /// <summary>
        /// Creates new linesplitter.
        /// </summary>
        private static readonly char[] NewLineSplitter = new char[] { '\n', '\r' };

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
        /// Applies the rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="token">The input.</param>
        /// <param name="tag">The tag.</param>
        /// <param name="index">The index.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="run">The run.</param>
        public static void ApplyRule(BrillRule rule, Token token, TagProbability tag, int index, Token[] tokens, TagProbability[] tags, int run)
        {
            if (rule.From != tag.Tag || (rule.SecondRun && run == 0))
                return;

            var type = rule.Type;

            // Start word rule is case sensitive
            if (type == BrillConditions.STARTWORD)
            {
                if (index == 0 && token.Value == rule.C1)
                {
                    tags[index].Tag = rule.To;
                    return;
                }
                return;
            }

            switch (type)
            {
                case BrillConditions.PREVTAG:
                    {
                        if (index > 0 && tags[index - 1].Tag == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.PREVWORDPREVTAG:
                    {
                        if (index > 0 && tags[index - 1].Tag == rule.C2 && tokens[index - 1].NormalizedValue == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }
                        break;
                    }

                case BrillConditions.NEXTTAG:
                    {
                        if (index < tags.Length - 1 && tags[index + 1].Tag == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.NEXTTAG2:
                    {
                        if (index < tags.Length - 2 && tags[index + 2].Tag == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.PREVTAG2:
                    {
                        if (index > 1 && tags[index - 2].Tag == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.PREV1OR2TAG:
                    {
                        if ((index > 0 && tags[index - 1].Tag == rule.C1) || (index > 1 && tags[index - 2].Tag == rule.C1))
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.PREVWORD:
                    {
                        if (index > 0 && tokens[index - 1].NormalizedValue == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.CURRENTWD:
                    {
                        if (token.NormalizedValue == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.WDPREVTAG:
                    {
                        if (index > 0 && token.NormalizedValue == rule.C2 && tags[index - 1].Tag == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.WDPREVWD:
                    {
                        if (index > 0 && token.NormalizedValue == rule.C2 && tokens[index - 1].NormalizedValue == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.NEXT1OR2OR3TAG:
                    {
                        if ((index < tags.Length - 1 && tags[index + 1].Tag == rule.C1) || (index < tags.Length - 2 && tags[index + 2].Tag == rule.C1) || (index < tags.Length - 3 && tags[index + 3].Tag == rule.C1))
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.NEXT2WD:
                    {
                        if (index < tokens.Length - 2 && tokens[index + 2].NormalizedValue == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.WDNEXTWD:
                    {
                        if (index < tokens.Length - 1 && token.NormalizedValue == rule.C1 && tokens[index + 1].NormalizedValue == rule.C2)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.WDNEXTTAG:
                    {
                        if (index < tags.Length - 1 && token.NormalizedValue == rule.C1 && tags[index + 1].Tag == rule.C2)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.PREV1OR2OR3TAG:
                    {
                        if ((index > 0 && tags[index - 1].Tag == rule.C1) || (index > 1 && tags[index - 2].Tag == rule.C1) || (index > 2 && tags[index - 3].Tag == rule.C1))
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.SURROUNDTAG:
                    {
                        if (index > 0 && tags[index - 1].Tag == rule.C1 && index < tags.Length - 1 && tags[index + 1].Tag == rule.C2)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.SURROUNDTAGWD:
                    {
                        if (token.NormalizedValue == rule.C1 && index > 0 && tags[index - 1].Tag == rule.C2 && index < tags.Length - 1 && tags[index + 1].Tag == rule.C3)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.NEXTWD:
                    {
                        if (index < tokens.Length - 1 && tokens[index + 1].NormalizedValue == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.NEXT1OR2TAG:
                    {
                        if ((index < tags.Length - 1 && tags[index + 1].Tag == rule.C1) || (index < tags.Length - 2 && tags[index + 2].Tag == rule.C1))
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.PREV2TAG:
                    {
                        if (index > 1 && tags[index - 2].Tag == rule.C1 && tags[index - 1].Tag == rule.C2)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.PREV2TAGNEXTTAG:
                    {
                        if (index > 1 && tags[index - 2].Tag == rule.C1 && tags[index - 1].Tag == rule.C2 && index < tags.Length - 1 && tags[index + 1].Tag == rule.C3)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.NEXT2TAG:
                    {
                        if (index < tags.Length - 2 && tags[index + 1].Tag == rule.C1 && tags[index + 2].Tag == rule.C2)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.NEXT1OR2WD:
                    {
                        if ((index < tokens.Length - 1 && tokens[index + 1].NormalizedValue == rule.C1) || (index < tokens.Length - 2 && tokens[index + 2].NormalizedValue == rule.C1))
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.PREV2WD:
                    {
                        if (index > 1 && tokens[index - 2].NormalizedValue == rule.C1)
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }

                case BrillConditions.PREV1OR2WD:
                    {
                        if ((index > 0 && tokens[index - 1].NormalizedValue == rule.C1) || (index > 1 && tokens[index - 2].NormalizedValue == rule.C1))
                        {
                            tags[index].Tag = rule.To;
                            return;
                        }

                        break;
                    }
            }
        }

        /// <summary>
        /// Applies the specified tokens.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tags">The tags.</param>
        /// <returns></returns>
        public TagProbability[] Apply(Token[] tokens, TagProbability[] tags)
        {
            for (int x = 0; x < 2; ++x)
            {
                for (int y = 0; y < tokens.Length; ++y)
                {
                    var Tag = tags[y];
                    var Token = tokens[y];
                    if (!Tag.Blocked)
                    {
                        ApplyRules(Token, y, tokens, tags, x);
                    }
                }
            }
            return tags;
        }

        /// <summary>
        /// Applies the rules.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="index">The index.</param>
        /// <param name="tokens">The tokens.</param>
        /// <param name="tags">The tags.</param>
        /// <param name="run">The run.</param>
        public void ApplyRules(Token input, int index, Token[] tokens, TagProbability[] tags, int run)
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
            var TempTokens = tokens.Where(x => x.TokenType != TokenType.WhiteSpace).ToArray();
            var Tags = new TagProbability[TempTokens.Length];
            bool InNNP = false;
            for (int x = 0; x < TempTokens.Length; ++x)
            {
                Tags[x] = GetTag(TempTokens[x]);
            }
            TagProbability? Previous = null;
            Token? PreviousToken = null;
            for (int x = 0; x < TempTokens.Length; ++x)
            {
                var Tag = Tags[x];
                var Token = TempTokens[x];
                InNNP = ManualTagConversions(Tag, Previous, Token, PreviousToken, x, InNNP);
                Previous = Tag;
                PreviousToken = Token;
            }

            Apply(TempTokens, Tags);

            Previous = null;
            for (int x = 0; x < TempTokens.Length; ++x)
            {
                var Tag = Tags[x];
                var Token = TempTokens[x];
                if (Token.NormalizedValue.EndsWith("ed", StringComparison.Ordinal))
                {
                    if (Tag.Tag == "JJ" && (Previous?.Tag == "VBZ" || Previous?.Tag == "VBP") && Tags[x + 1].Tag == "TO")
                    {
                        Tag.Tag = "VBN";
                    }
                }

                Previous = Tag;
            }

            for (int x = 0; x < TempTokens.Length; ++x)
            {
                TempTokens[x].PartOfSpeech = Tags[x].Tag;
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
                if (input.EndsWith(Key, StringComparison.Ordinal))
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
            var Data = new FileInfo("resource://Enlighten/Enlighten.Resources.en_us.lexicon.txt").Read();
            var Lines = Data.Split(NewLineSplitter, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, LinesLength = Lines.Length; i < LinesLength; i++)
            {
                var Line = Lines[i];
                if (string.IsNullOrEmpty(Line))
                    continue;
                var LineData = Line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                var Key = LineData[0];
                var Item = LineData[1];
                Lexicon.TryAdd(Key, new LexiconRule(Key, Item));
            }

            Data = new FileInfo("resource://Enlighten/Enlighten.Resources.en_us.regularverbs.txt").Read();
            Lines = Data.Split(NewLineSplitter, StringSplitOptions.RemoveEmptyEntries);
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

            Data = new FileInfo("resource://Enlighten/Enlighten.Resources.en_us.irregularverbs.txt").Read();
            Lines = Data.Split(NewLineSplitter, StringSplitOptions.RemoveEmptyEntries);
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

        private void BuildNationalities()
        {
            var Data = new FileInfo("resource://Enlighten/Enlighten.Resources.en_us.nationalities.txt").Read();
            var Lines = Data.Split(NewLineSplitter, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, LinesLength = Lines.Length; i < LinesLength; i++)
            {
                var Line = Lines[i];
                if (string.IsNullOrEmpty(Line))
                    continue;
                Nationalities.Add(Line);
            }
        }

        /// <summary>
        /// Builds the rules.
        /// </summary>
        private void BuildRules()
        {
            var Data = new FileInfo("resource://Enlighten/Enlighten.Resources.en_us.rules.txt").Read();
            var Lines = Data.Split(NewLineSplitter, StringSplitOptions.RemoveEmptyEntries);
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
            var Data = new FileInfo("resource://Enlighten/Enlighten.Resources.en_us.suffixes.txt").Read();
            var Lines = Data.Split(NewLineSplitter, StringSplitOptions.RemoveEmptyEntries);
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
        /// Manuals the tag conversions.
        /// </summary>
        /// <param name="Tag">The tag.</param>
        /// <param name="Previous">The previous.</param>
        /// <param name="Token">The token.</param>
        /// <param name="PreviousToken">The previous token.</param>
        /// <param name="x">The x.</param>
        /// <param name="InNNP">if set to <c>true</c> [in NNP].</param>
        /// <returns></returns>
        private bool ManualTagConversions(TagProbability Tag, TagProbability? Previous, Token Token, Token? PreviousToken, int x, bool InNNP)
        {
            if (Tag.Tag == "SYM" || Tag.Tag == ".")
                return InNNP;

            if (x == 0)
            {
                if (Token.NormalizedValue == "that")
                {
                    Tag.Tag = "DT";
                    return InNNP;
                }
                if ((Tag.Tag == "NN" || Tag.Tag == "VB") && Infinitives.Contains(Token.NormalizedValue))
                {
                    Tag.Tag = "VB";
                    return InNNP;
                }
            }

            if (Token.Value.Length > 3
                && Inflector.IsPast(Token.NormalizedValue, InflectorLanguage.English)
                && Tag.Tag.StartsWith("N")
                && (x == 0 || !IsMatchForPotentialProperNoun(Token.Value)))
            {
                Tag.Tag = "VBN";
                return InNNP;
            }

            if (Token.Value.Length > 4 &&
                (Inflector.IsGerund(Token.NormalizedValue, InflectorLanguage.English) || Token.NormalizedValue.EndsWith("in")) &&
                (Tag.Tag.StartsWith("N") || Tag.Tag == "MD") &&
                (x == 0 || !IsMatchForPotentialProperNoun(Token.Value)) &&
                Previous?.Tag != "NN" &&
                Previous?.Tag != "JJ" &&
                Previous?.Tag != "DT" &&
                Previous?.Tag != "VBG")
            {
                if (Token.NormalizedValue.EndsWith("g")
                    || (Token.NormalizedValue.EndsWith("in")
                        && Lexicon.TryGetValue(Token.NormalizedValue + "g", out var TempRule)
                        && TempRule.Tag == "VBG"))
                {
                    Tag.Tag = "VBG";
                    return InNNP;
                }
            }

            if (Previous?.Tag == "TO" && Infinitives.Contains(Token.NormalizedValue))
            {
                Tag.Tag = "VB";
            }

            if (Tag.Tag == "NN" || Tag.Tag == "VB" || (Tag.Tag == "JJ" && !Nationalities.Contains(Token.NormalizedValue)))
            {
                if (Token.Value == Token.Value.ToUpperInvariant() || Token.TokenType == TokenType.Abbreviation)
                {
                    Tag.Tag = "NNP";
                    InNNP = true;
                }
                else if (x > 0 && IsMatchForPotentialProperNoun(Token.Value))
                {
                    Tag.Tag = "NNP";
                    InNNP = true;
                    if (x == 1
                        && (Previous?.Tag == "NN"
                            || Previous?.Tag == "NNS"
                            || Previous?.Tag == "JJ"
                            || Previous?.Tag == "VB")
                        && IsMatchForPotentialProperNoun(PreviousToken?.Value ?? string.Empty))
                    {
                        Previous.Tag = "NNP";
                    }
                }
                else
                {
                    InNNP = false;
                }
            }
            else if (InNNP && ((Tag.Tag == "CD" && Token.TokenType == TokenType.Number) || Token.Value == "I"))
            {
                Tag.Tag = "NNP";
            }
            else
            {
                InNNP = Tag.Tag == "NNP" || Tag.Tag == "NNPS";
            }

            if (Tag.Tag == "NN" && Inflector.IsPlural(Token.Value, InflectorLanguage.English))
            {
                Tag.Tag = "NNS";
            }
            return InNNP;
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