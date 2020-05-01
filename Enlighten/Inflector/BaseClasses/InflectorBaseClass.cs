using Enlighten.Inflector.Inflectors;
using Enlighten.Inflector.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.Inflector.BaseClasses
{
    /// <summary>
    /// Inflector base class
    /// </summary>
    /// <seealso cref="IInflectorLanguage"/>
    public abstract class InflectorBaseClass : IInflectorLanguage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InflectorBaseClass"/> class.
        /// </summary>
        protected InflectorBaseClass()
        {
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the conjugation rules.
        /// </summary>
        /// <value>The conjugation rules.</value>
        private List<ConjugationRule> ConjugationRules { get; } = new List<ConjugationRule>();

        /// <summary>
        /// Gets the gerund exceptions.
        /// </summary>
        /// <value>The gerund exceptions.</value>
        private List<string> GerundExceptions { get; } = new List<string>();

        /// <summary>
        /// Gets the gerund rule.
        /// </summary>
        /// <value>The gerund rule.</value>
        private List<Rule> GerundRules { get; } = new List<Rule>();

        /// <summary>
        /// Gets the infinitive rules.
        /// </summary>
        /// <value>The infinitive rules.</value>
        private List<Rule> InfinitiveRules { get; } = new List<Rule>();

        /// <summary>
        /// Gets the past rules.
        /// </summary>
        /// <value>The past rules.</value>
        private List<Rule> PastRules { get; } = new List<Rule>();

        /// <summary>
        /// Gets the plural rules.
        /// </summary>
        /// <value>The plural rules.</value>
        private List<Rule> PluralRules { get; } = new List<Rule>();

        /// <summary>
        /// Gets the singular rules.
        /// </summary>
        /// <value>The singular rules.</value>
        private List<Rule> SingularRules { get; } = new List<Rule>();

        /// <summary>
        /// Gets the uncountables.
        /// </summary>
        /// <value>The uncountables.</value>
        private List<string> Uncountables { get; } = new List<string>();

        /// <summary>
        /// Gets the VBG.
        /// </summary>
        /// <value>The VBG.</value>
        protected const string VBG = "VBG";

        /// <summary>
        /// Gets the VBN.
        /// </summary>
        /// <value>The VBN.</value>
        protected const string VBN = "VBN";

        /// <summary>
        /// Gets the VBZ.
        /// </summary>
        /// <value>The VBZ.</value>
        protected const string VBZ = "VBZ";

        /// <summary>
        /// Conjugates the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="partOfSpeechTo">The part of speech to.</param>
        /// <returns></returns>
        public string Conjugate(string input, string partOfSpeechTo)
        {
            if (input is null)
                return input;
            for (int x = 0; x < ConjugationRules.Count; ++x)
            {
                var TempConjugationRule = ConjugationRules[x];
                if (TempConjugationRule.CanApply(input))
                    return TempConjugationRule.Apply(input, partOfSpeechTo);
            }
            return input;
        }

        /// <summary>
        /// Infinitives the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The infinitive tense of the verb.</returns>
        public string? Infinitive(string input)
        {
            return ApplyRules(InfinitiveRules, input);
        }

        /// <summary>
        /// Determines whether the specified input is gerund.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is gerund; otherwise, <c>false</c>.</returns>
        public bool IsGerund(string input)
        {
            return GerundRules.Any(x => x.CanApply(input)) && !IsGerundException(input);
        }

        /// <summary>
        /// Determines whether the specified input is past.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is past; otherwise, <c>false</c>.</returns>
        public bool IsPast(string input)
        {
            return PastRules.Any(x => x.CanApply(input));
        }

        /// <summary>
        /// Determines whether the specified input is plural.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is plural; otherwise, <c>false</c>.</returns>
        public bool IsPlural(string input)
        {
            return Pluralize(input) == input;
        }

        /// <summary>
        /// Determines whether the specified input is singular.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is singular; otherwise, <c>false</c>.</returns>
        public bool IsSingular(string input)
        {
            return Singularize(input) == input;
        }

        /// <summary>
        /// Determines whether the specified input is uncountable.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is uncountable; otherwise, <c>false</c>.</returns>
        public bool IsUncountable(string input)
        {
            return Uncountables.Contains(input);
        }

        /// <summary>
        /// Pluralizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The plural form of the word.</returns>
        public string Pluralize(string input)
        {
            var Result = ApplyRules(PluralRules, input);

            var asSingular = ApplyRules(SingularRules, input);
            var asSingularAsPlural = ApplyRules(PluralRules, asSingular);
            if (!(asSingular is null) && asSingular != input && asSingular + "s" != input && asSingularAsPlural == input && Result != input)
            {
                return input;
            }

            return Result ?? input;
        }

        /// <summary>
        /// Singularizes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The singular form of the word.</returns>
        public string Singularize(string input)
        {
            var Result = ApplyRules(SingularRules, input);

            var asPlural = ApplyRules(PluralRules, input);
            var asPluralAsSingular = ApplyRules(SingularRules, asPlural);
            if (asPlural != input && input + "s" != asPlural && asPluralAsSingular == input && Result != input)
            {
                return input;
            }

            return Result ?? input;
        }

        /// <summary>
        /// Converts to gerund.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public string ToGerund(string input)
        {
            return Conjugate(input, VBG);
        }

        /// <summary>
        /// Converts to past.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public string ToPast(string input)
        {
            return Conjugate(input, VBN);
        }

        /// <summary>
        /// Converts to present.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public string ToPresent(string input)
        {
            return Conjugate(input, VBZ);
        }

        /// <summary>
        /// Adds the conjugation rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="replacement">The replacement.</param>
        protected void AddConjugationRule(string rule, Func<string, string, string> replacement)
        {
            ConjugationRules.Add(new ConjugationRule(rule, replacement));
        }

        /// <summary>
        /// Adds the gerund exception.
        /// </summary>
        /// <param name="word">The word.</param>
        protected void AddGerundException(string word)
        {
            GerundExceptions.Add(word);
        }

        /// <summary>
        /// Adds the gerund rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        protected void AddGerundRule(string rule)
        {
            GerundRules.Add(new Rule(rule, string.Empty));
        }

        /// <summary>
        /// Adds the infinitive.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="replacement">The replacement.</param>
        protected void AddInfinitive(string rule, string replacement)
        {
            InfinitiveRules.Add(new Rule(rule, replacement));
        }

        /// <summary>
        /// Adds the irregular.
        /// </summary>
        /// <param name="singular">The singular.</param>
        /// <param name="plural">The plural.</param>
        /// <param name="matchEnding">if set to <c>true</c> [match ending].</param>
        protected void AddIrregular(string singular, string plural, bool matchEnding = true)
        {
            if (matchEnding)
            {
                AddPlural("(" + singular[0] + ")" + singular.Substring(1) + "$", "$1" + plural.Substring(1));
                AddSingular("(" + plural[0] + ")" + plural.Substring(1) + "$", "$1" + singular.Substring(1));
            }
            else
            {
                AddPlural($"^{singular}$", plural);
                AddSingular($"^{plural}$", singular);
            }
        }

        /// <summary>
        /// Adds the gerund rule.
        /// </summary>
        /// <param name="rule">The rule.</param>
        protected void AddPastRule(string rule)
        {
            PastRules.Add(new Rule(rule, string.Empty));
        }

        /// <summary>
        /// Adds the plural.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="replacement">The replacement.</param>
        protected void AddPlural(string rule, string replacement)
        {
            PluralRules.Add(new Rule(rule, replacement));
        }

        /// <summary>
        /// Adds the singular.
        /// </summary>
        /// <param name="rule">The rule.</param>
        /// <param name="replacement">The replacement.</param>
        protected void AddSingular(string rule, string replacement)
        {
            SingularRules.Add(new Rule(rule, replacement));
        }

        /// <summary>
        /// Adds an uncountable word.
        /// </summary>
        /// <param name="word">The word.</param>
        protected void AddUncountable(string word)
        {
            Uncountables.Add(word.ToLowerInvariant());
        }

        /// <summary>
        /// Determines whether [is gerund exception] [the specified word].
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns><c>true</c> if [is gerund exception] [the specified word]; otherwise, <c>false</c>.</returns>
        protected bool IsGerundException(string word)
        {
            return !GerundExceptions.Contains(word.ToLowerInvariant());
        }

        /// <summary>
        /// Applies the rules to the word.
        /// </summary>
        /// <param name="rules">The rules.</param>
        /// <param name="word">The word.</param>
        /// <returns>The resulting word</returns>
        private string? ApplyRules(List<Rule> rules, string? word)
        {
            if (word is null || IsUncountable(word))
                return word;
            for (var i = rules.Count - 1; i >= 0; i--)
            {
                var CurrentRule = rules[i];
                if (CurrentRule.CanApply(word))
                    return CurrentRule.Apply(word);
            }
            return word;
        }
    }
}