﻿using Enlighten.SentenceDetection.Detectors;
using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Enums;
using Enlighten.Tokenizer.Languages.English;
using Enlighten.Tokenizer.Languages.English.TokenFinders;
using Enlighten.Tokenizer.Languages.Interfaces;
using Xunit;

namespace Enlighten.Tests.SentenceDetection.Detectors
{
    public class DefaultDetectorTests : TestBaseClass<DefaultDetector>
    {
        [Fact]
        public void Detect()
        {
            string Text = "\"Darkness cannot drive out darkness: only light can do that. Hate cannot drive out hate: only love can do that.\"";
            var Tokenizer = new DefaultTokenizer(new[] { new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol() }) }, ObjectPool);
            var Results = new DefaultDetector().DetectSentences(Tokenizer.Tokenize(Text, TokenizerLanguage.EnglishRuleBased));
            Assert.Equal(2, Results.Length);
            Assert.Equal("Darkness cannot drive out darkness: only light can do that.", Results[0].ToString());
            Assert.Equal("Hate cannot drive out hate: only love can do that.", Results[1].ToString());
        }
    }
}