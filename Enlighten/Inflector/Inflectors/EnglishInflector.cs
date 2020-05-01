using Enlighten.Inflector.BaseClasses;
using Enlighten.Inflector.Interfaces;

namespace Enlighten.Inflector
{
    /// <summary>
    /// Default inflector
    /// </summary>
    /// <seealso cref="IInflector"/>
    public class EnglishInflector : InflectorBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnglishInflector"/> class.
        /// </summary>
        public EnglishInflector()
        {
            AddPlural("$", "s");
            AddPlural("s$", "s");
            AddPlural("(ax|test)is$", "$1es");
            AddPlural("(octop|vir|alumn|fung|cact|foc|hippopotam|radi|stimul|syllab|nucle)us$", "$1i");
            AddPlural("(alias|bias|iris|status|campus|apparatus|virus|walrus|trellis)$", "$1es");
            AddPlural("(buffal|tomat|volcan|ech|embarg|her|mosquit|potat|torped|vet)o$", "$1oes");
            AddPlural("([dti])um$", "$1a");
            AddPlural("sis$", "ses");
            AddPlural("(?:([^f])fe|([lr])f)$", "$1$2ves");
            AddPlural("(hive)$", "$1s");
            AddPlural("([^aeiouy]|qu)y$", "$1ies");
            AddPlural("(x|ch|ss|sh)$", "$1es");
            AddPlural("(matr|vert|ind|d)ix|ex$", "$1ices");
            AddPlural("(^[m|l])ouse$", "$1ice");
            AddPlural("^(ox)$", "$1en");
            AddPlural("(quiz)$", "$1zes");
            AddPlural("(buz|blit|walt)z$", "$1zes");
            AddPlural("(hoo|lea|loa|thie)f$", "$1ves");
            AddPlural("(alumn|alg|larv|vertebr)a$", "$1ae");
            AddPlural("(criteri|phenomen)on$", "$1a");

            AddSingular("s$", "");
            AddSingular("(n)ews$", "$1ews");
            AddSingular("([dti])a$", "$1um");
            AddSingular("(analy|ba|diagno|parenthe|progno|synop|the|ellip|empha|neuro|oa|paraly)ses$", "$1sis");
            AddSingular("([^f])ves$", "$1fe");
            AddSingular("(hive)s$", "$1");
            AddSingular("(tive)s$", "$1");
            AddSingular("([lr]|hoo|lea|loa|thie)ves$", "$1f");
            AddSingular("(^zomb)?([^aeiouy]|qu)ies$", "$2y");
            AddSingular("(s)eries$", "$1eries");
            AddSingular("(m)ovies$", "$1ovie");
            AddSingular("(x|ch|ss|sh)es$", "$1");
            AddSingular("(^[m|l])ice$", "$1ouse");
            AddSingular("(o)es$", "$1");
            AddSingular("(shoe)s$", "$1");
            AddSingular("(cris|ax|test)es$", "$1is");
            AddSingular("(octop|vir|alumn|fung|cact|foc|hippopotam|radi|stimul|syllab|nucle)i$", "$1us");
            AddSingular("(alias|bias|iris|status|campus|apparatus|virus|walrus|trellis)es$", "$1");
            AddSingular("^(ox)en", "$1");
            AddSingular("(matr|d)ices$", "$1ix");
            AddSingular("(vert|ind)ices$", "$1ex");
            AddSingular("(quiz)zes$", "$1");
            AddSingular("(buz|blit|walt)zes$", "$1z");
            AddSingular("(alumn|alg|larv|vertebr)ae$", "$1a");
            AddSingular("(criteri|phenomen)a$", "$1on");
            AddSingular("([b|r|c]ook|room|smooth)ies$", "$1ie");

            AddIrregular("person", "people");
            AddIrregular("man", "men");
            AddIrregular("human", "humans");
            AddIrregular("child", "children");
            AddIrregular("sex", "sexes");
            AddIrregular("move", "moves");
            AddIrregular("goose", "geese");
            AddIrregular("wave", "waves");
            AddIrregular("die", "dice");
            AddIrregular("foot", "feet");
            AddIrregular("tooth", "teeth");
            AddIrregular("curriculum", "curricula");
            AddIrregular("database", "databases");
            AddIrregular("zombie", "zombies");
            AddIrregular("personnel", "personnel");
            AddIrregular("cache", "caches");
            AddIrregular("is", "are", false);
            AddIrregular("that", "those", false);
            AddIrregular("this", "these", false);
            AddIrregular("bus", "buses", false);
            AddIrregular("staff", "staff", false);
            AddIrregular("training", "training", false);

            AddUncountable("equipment");
            AddUncountable("information");
            AddUncountable("corn");
            AddUncountable("milk");
            AddUncountable("rice");
            AddUncountable("money");
            AddUncountable("species");
            AddUncountable("series");
            AddUncountable("fish");
            AddUncountable("sheep");
            AddUncountable("deer");
            AddUncountable("aircraft");
            AddUncountable("oz");
            AddUncountable("tsp");
            AddUncountable("tbsp");
            AddUncountable("ml");
            AddUncountable("l");
            AddUncountable("water");
            AddUncountable("waters");
            AddUncountable("semen");
            AddUncountable("sperm");
            AddUncountable("bison");
            AddUncountable("grass");
            AddUncountable("hair");
            AddUncountable("mud");
            AddUncountable("elk");
            AddUncountable("luggage");
            AddUncountable("moose");
            AddUncountable("offspring");
            AddUncountable("salmon");
            AddUncountable("shrimp");
            AddUncountable("someone");
            AddUncountable("swine");
            AddUncountable("trout");
            AddUncountable("tuna");
            AddUncountable("corps");
            AddUncountable("scissors");
            AddUncountable("means");
            AddUncountable("mail");

            AddGerundException("anything");
            AddGerundException("spring");
            AddGerundException("something");
            AddGerundException("thing");
            AddGerundException("king");
            AddGerundException("nothing");

            AddInfinitive("^are$", "be");
            AddInfinitive("^am$", "be");

            AddConjugationRule("[^aeiouy]e$", ConjugateConsonentE);
            AddConjugationRule("[^aeiou]y$", ConjugateConsonentY);
            AddConjugationRule("(ee)$", ConjugateEE);
            AddConjugationRule("(ie)$", ConjugateIE);
            AddConjugationRule("([uao]m[pb]|[oa]wn|ey|elp|[ei]gn|ilm|o[uo]r|[oa]ugh|igh|ki|ff|oubt|ount|awl|o[alo]d|[iu]rl|upt|[oa]y|ight|oid|empt|act|aud|e[ea]d|ound|[aeiou][srcln]t|ept|dd|[eia]n[dk]|[ioa][xk]|[oa]rm|[ue]rn|[ao]ng|uin|eam|ai[mr]|[oea]w|[eaoui][rscl]k|[oa]r[nd]|ear|er|it|ll)$", ConjugateLongVowelConsonant);
            AddConjugationRule("([aeiuo][ptlgnm]|ir|cur|[^aeiuo][oua][db])$", ConjugateShortVowelConsonant);
            AddConjugationRule("([ieao]ss|[aeiouy]zz|[aeiouy]ch|nch|rch|[aeiouy]sh|[iae]tch|ax)$", ConjugateSibilant);
            AddConjugationRule("(ue)$", ConjugateUE);

            AddGerundRule("ing$");
            AddPastRule("[^e]ed$");
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public override string Name { get; } = "en-us";

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
    }
}