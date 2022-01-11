using BigBook;
using Enlighten.Normalizer;
using Enlighten.Normalizer.Default;
using Enlighten.Normalizer.Interfaces;
using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer.Languages.English;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Languages.Interfaces;
using Enlighten.Tokenizer.Utils;
using Xunit;

namespace Enlighten.Tests.Normalizer
{
    public class DefaultNormalizerTests : TestBaseClass<DefaultNormalizer>
    {
        [Fact]
        public void NormalizeText()
        {
            var TestObject = new DefaultNormalizer(new INormalizer[] { new ASCIIFolder(ObjectPool), new LowerCase() }, new ITextNormalizer[] { new HTMLToText(ObjectPool) });
            var Result = TestObject.Normalize("Testing out <p>this stuff</p>");
            Assert.Equal("Testing out this stuff \r\n", Result);
        }

        [Fact]
        public void NormalizeTokens()
        {
            var Tokenizer = new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() });
            var TestObject = new DefaultNormalizer(new INormalizer[] { new ASCIIFolder(ObjectPool), new LowerCase() }, new ITextNormalizer[] { new HTMLToText(ObjectPool) });
            var Result = TestObject.Normalize("Tësting out <p>this stuff</p>");
            var Tokens = Tokenizer.Tokenize(new TokenizableStream<char>(Result.ToCharArray()));
            Tokens = TestObject.Normalize(Tokens);
            Assert.Equal("testing out this stuff ", Tokens.ToString(x => x.NormalizedValue, string.Empty));
        }
    }
}