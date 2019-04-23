using Enlighten.SentenceDetection;
using Enlighten.SentenceDetection.Detectors;
using Enlighten.SentenceDetection.Enum;
using Enlighten.Tests.BaseClasses;
using Enlighten.Tokenizer;
using Enlighten.Tokenizer.Languages.English;
using Xunit;

namespace Enlighten.Tests.SentenceDetection
{
    public class DefaultSentenceDetectorTests : TestBaseClass
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

            var Results = new DefaultSentenceDetector(new[] { new DefaultDetector() }, new DefaultTokenizer(new[] { new EnglishLanguage() })).Detect(Text, SentenceDetectorLanguage.Default);
            Assert.Equal(3, Results.Length);
            Assert.Equal(@"That can I;
At least, the whisper goes so.", Results[0].ToString());
            Assert.Equal(@"Our last king,
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
His fell to Hamlet.", Results[1].ToString());
            Assert.Equal(@"Now, sir, young Fortinbras,
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
Of this post-haste and romage in the land.", Results[2].ToString());
        }

        [Fact]
        public void DetectWithinQuote()
        {
            string Text = "\"Darkness cannot drive out darkness: only light can do that. Hate cannot drive out hate: only love can do that.\"";
            var Results = new DefaultSentenceDetector(new[] { new DefaultDetector() }, new DefaultTokenizer(new[] { new EnglishLanguage() })).Detect(Text, SentenceDetectorLanguage.Default);
            Assert.Equal(2, Results.Length);
            Assert.Equal("Darkness cannot drive out darkness: only light can do that.", Results[0].ToString());
            Assert.Equal("Hate cannot drive out hate: only love can do that.", Results[1].ToString());
        }
    }
}