using BigBook;
using Enlighten.Inflector.Interfaces;
using Enlighten.POSTagger;
using Enlighten.POSTagger.Enum;
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

namespace Enlighten.Tests.POSTagger
{
    public class DefaultTaggerTests : TestBaseClass
    {
        [Fact]
        public void Setup()
        {
            var TestObject = new DefaultTagger(new[] { new BrillTagger() });
            Assert.NotNull(TestObject);
        }

        [Fact]
        public void Tag()
        {
            var TestObject = new DefaultTagger(new[] { new SimpleTagger(Canister.Builder.Bootstrapper.Resolve<IInflector>(), Canister.Builder.Bootstrapper.Resolve<ISynonymFinder>()) });
            var Tokenizer = new DefaultTokenizer(new[] { new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool);
            var Results = TestObject.Tag(Tokenizer.Tokenize("I would go buy a computer.", TokenizerLanguage.EnglishRuleBased), POSTaggerLanguage.BrillTagger);
            Assert.Equal("NN VM VVB NN RR NN", Results.Where(x => x.TokenType == TokenType.Word).ToString(x => x.PartOfSpeech, " "));
        }
    }
}