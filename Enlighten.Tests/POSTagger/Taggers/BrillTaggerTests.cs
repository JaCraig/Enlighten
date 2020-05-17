using BigBook;
using Enlighten.Inflector.Interfaces;
using Enlighten.Normalizer;
using Enlighten.Normalizer.Default;
using Enlighten.Normalizer.Interfaces;
using Enlighten.POSTagger.Taggers;
using Enlighten.SynonymFinder.Interfaces;
using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Languages.English;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Languages.Interfaces;
using System.Linq;
using Xunit;

namespace Enlighten.Tests.POSTagger.Taggers
{
    public class BrillTaggerTests : TestBaseClass
    {
        [Fact]
        public void Setup()
        {
            var TestObject = new SimpleTagger(Canister.Builder.Bootstrapper.Resolve<IInflector>(), Canister.Builder.Bootstrapper.Resolve<ISynonymFinder>());
            Assert.Equal(93361, TestObject.Lexicon.Keys.Count);
        }

        [Fact]
        public void Tag()
        {
            var TestObject = new SimpleTagger(Canister.Builder.Bootstrapper.Resolve<IInflector>(), Canister.Builder.Bootstrapper.Resolve<ISynonymFinder>());
            var Normalizer = new DefaultNormalizer(new INormalizer[] { new ASCIIFolder(ObjectPool), new LowerCase() }, new ITextNormalizer[] { new HTMLToText(ObjectPool) });
            var Tokenizer = new DefaultTokenizer(new[] { new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool);
            var Results = TestObject.Tag(Normalizer.Normalize(Tokenizer.Tokenize(Normalizer.Normalize("I would go buy a computer."), TokenizerLanguage.EnglishRuleBased)));
            Assert.Equal("NNP MD VB VB DT NN", Results.Where(x => x.TokenType == TokenType.Word).ToString(x => x.PartOfSpeech, " "));
        }

        [Fact]
        public void TagProperNoun()
        {
            var TestObject = new SimpleTagger(Canister.Builder.Bootstrapper.Resolve<IInflector>(), Canister.Builder.Bootstrapper.Resolve<ISynonymFinder>());
            var Normalizer = new DefaultNormalizer(new INormalizer[] { new ASCIIFolder(ObjectPool), new LowerCase() }, new ITextNormalizer[] { new HTMLToText(ObjectPool) });
            var Tokenizer = new DefaultTokenizer(new[] { new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool);
            var Results = TestObject.Tag(Normalizer.Normalize(Tokenizer.Tokenize(Normalizer.Normalize("I want to go to New York City."), TokenizerLanguage.EnglishRuleBased)));
            Assert.Equal("NNP VBP TO VB TO NNP NNP NNP", Results.Where(x => x.TokenType == TokenType.Word).ToString(x => x.PartOfSpeech, " "));
        }
    }
}