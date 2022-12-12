using Enlighten.Inflector.Interfaces;
using Enlighten.NameFinder;
using Enlighten.NameFinder.Enums;
using Enlighten.NameFinder.Finders;
using Enlighten.NameFinder.Interfaces;
using Enlighten.Normalizer;
using Enlighten.Normalizer.Default;
using Enlighten.Normalizer.Interfaces;
using Enlighten.POSTagger;
using Enlighten.POSTagger.Enum;
using Enlighten.POSTagger.Interfaces;
using Enlighten.POSTagger.Taggers;
using Enlighten.SynonymFinder.Interfaces;
using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Interfaces;
using Enlighten.Tokenizer.Languages.English;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Languages.Interfaces;
using FileCurator;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Enlighten.Tests.EntityFinder
{
    public class DefaultEntityFinderTests : TestBaseClass<DefaultEntityFinder>
    {
        [Fact]
        public void BasicTest()
        {
            var EntityFinder = new DefaultEntityFinder(new IFinder[] { new DefaultFinder() });
            var TestObject = new DefaultTagger(new[] { new SimpleTagger(GetServiceProvider().GetService<IInflector>(), GetServiceProvider().GetService<ISynonymFinder>()) });
            var Normalizer = new DefaultNormalizer(new INormalizer[] { new ASCIIFolder(ObjectPool), new LowerCase() }, new ITextNormalizer[] { new HTMLToText(ObjectPool) });
            var Tokenizer = new DefaultTokenizer(new[] { new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool);
            var Results = EntityFinder.Find(TestObject.Tag(Normalizer.Normalize(Tokenizer.Tokenize(Normalizer.Normalize("I wish G.M. made slightly better products."), TokenizerLanguage.EnglishRuleBased)), POSTaggerLanguage.BrillTagger), EntityFinderLanguage.DefaultFinder);
            Assert.True(Results[0].Entity);
            Assert.False(Results[1].Entity);
            Assert.False(Results[2].Entity);
            Assert.False(Results[3].Entity);
            Assert.True(Results[4].Entity);
            Assert.False(Results[5].Entity);
        }

        [Fact]
        public void NewsItemTest()
        {
            var EntityFinder = new DefaultEntityFinder(new IFinder[] { new DefaultFinder() });
            var TestObject = GetServiceProvider().GetService<IPOSTagger>();
            var Normalizer = GetServiceProvider().GetService<INormalizerManager>();
            var Tokenizer = GetServiceProvider().GetService<ITokenizer>();
            var Results = EntityFinder.Find(TestObject.Tag(Normalizer.Normalize(Tokenizer.Tokenize(Normalizer.Normalize(new FileInfo("./Data/MotherJonesArticle.txt")), TokenizerLanguage.EnglishRuleBased)), POSTaggerLanguage.BrillTagger), EntityFinderLanguage.DefaultFinder);
            Assert.True(System.Array.Find(Results, x => x.Value == "New York City")?.Entity);
            Assert.True(System.Array.Find(Results, x => x.Value == "Mayor Bill")?.Entity);
        }
    }
}