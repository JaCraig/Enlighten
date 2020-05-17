using Enlighten.StopWords;
using Enlighten.StopWords.Enum;
using Enlighten.StopWords.Interfaces;
using Enlighten.StopWords.Languages;
using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer.Languages.English;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Languages.Interfaces;
using Enlighten.Tokenizer.Utils;
using System.Linq;
using Xunit;

namespace Enlighten.Tests.StopWords
{
    public class StopWordsManagerTests : TestBaseClass
    {
        [Fact]
        public void MarkStopWords()
        {
            var Tokenizer = new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() });
            var TestObject = new StopWordsManager(new IStopWordsLanguage[] { new EnglishDefault() });
            var Result = TestObject.MarkStopWords(Tokenizer.Tokenize(new TokenizableStream<char>("This is a test.".ToCharArray())), StopWordsLanguage.English).Where(x => !x.StopWord).ToArray();
            Assert.Equal("This   test.", Tokenizer.Detokenize(Result));
        }
    }
}