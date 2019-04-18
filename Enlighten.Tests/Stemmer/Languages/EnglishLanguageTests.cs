using Enlighten.Stemmer.Languages;
using Enlighten.Tests.BaseClasses;
using FileCurator;
using Xunit;

namespace Enlighten.Tests.Stemmer.Languages
{
    public class EnglishLanguageTests : TestBaseClass
    {
        [Fact]
        public void IsMatch()
        {
            var Input = new FileInfo("./Data/StemmerStart.txt").Read().Split('\n');
            var Expected = new FileInfo("./Data/StemmerExpected.txt").Read().Split('\n');
            var Stemmer = new EnglishLanguage();
            for (int x = 0; x < Input.Length; ++x)
            {
                var Result = Stemmer.StemWords(new string[] { Input[x] })[0];
                Assert.Equal(Expected[x], Result);
            }
        }
    }
}