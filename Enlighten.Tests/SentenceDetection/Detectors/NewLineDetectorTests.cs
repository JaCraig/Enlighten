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
    public class NewLineDetectorTests : TestBaseClass<NewLineDetector>
    {
        [Fact]
        public void Detect()
        {
            string Text = @"That can I;
At least, the whisper goes so. Our last king,
Whose image even but now appear'd to us,
Was, as you know, by Fortinbras of Norway,
Thereto prick'd on by a most emulate pride,
Dared to the combat; in which our valiant Hamlet--
For so this side of our known world esteem'd him--
Did slay this Fortinbras; who by a seal'd compact,
Well ratified by law and heraldry,
Did forfeit, with his life, all those his lands
Which he stood seized of, to the conqueror:
Against the which, a moiety competent
Was gaged by our king; which had return'd
To the inheritance of Fortinbras,
Had he been vanquisher; as, by the same covenant,
And carriage of the article design'd,
His fell to Hamlet. Now, sir, young Fortinbras,
Of unimproved mettle hot and full,
Hath in the skirts of Norway here and there
Shark'd up a list of lawless resolutes,
For food and diet, to some enterprise
That hath a stomach in't; which is no other--
As it doth well appear unto our state--
But to recover of us, by strong hand
And terms compulsatory, those foresaid lands
So by his father lost: and this, I take it,
Is the main motive of our preparations,
The source of this our watch and the chief head
Of this post-haste and romage in the land.";
            var Tokenizer = new DefaultTokenizer(new[] { new EnglishLanguage(new IEnglishTokenFinder[] { new Word(), new Whitespace(), new Symbol(), new NewLine() }) }, ObjectPool);
            var Results = new NewLineDetector().DetectSentences(Tokenizer.Tokenize(Text, TokenizerLanguage.EnglishRuleBased));
            Assert.Equal(29, Results.Length);
        }
    }
}