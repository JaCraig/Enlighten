﻿using BigBook;
using Enlighten.FeatureExtraction.Enum;
using Enlighten.TextSummarization.Enum;
using Enlighten.Tokenizer.Languages.English.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Enlighten.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var Services = new ServiceCollection()?.AddCanisterModules()?.BuildServiceProvider();
            var DefaultPipeline = Services.GetService<Pipeline>();

            var PhineasAndFerb = DefaultPipeline.Process("There's 104 days of summer vacation\r\nAnd school comes along just to end it\r\nSo the annual problem of our generation\r\nIs finding a good way to spend it\r\nLike maybe\r\nBuilding a rocket\r\nOr fighting a mummy\r\nOr climbing up the Eiffel Tower\r\nDiscovering something that doesn't exist (Hey!)\r\nOr giving a monkey a shower\r\nSurfing a tidal wave\r\nCreating nanobots\r\nOr locating Frankenstein's brain (It's over here!)\r\nFinding a dodo bird\r\nPainting a continent\r\nOr driving their sister insane (Phineas!)\r\nAs you can see, there's a whole lot of stuff to do\r\nBefore school starts this fall (Come on, Perry!)\r\nSo stick with us, cause Phineas and Ferb\r\nAre gonna do it all\r\nSo stick with us, cause Phineas and Ferb\r\nAre gonna do it all\r\n(Mom! Phineas and Ferb are making a title sequence!)");
            var WordCrimes = DefaultPipeline.Process("[Intro]\r\nEverybody shut up! (Woo!)\r\nEveryone listen up!\r\nHey, hey, hey\r\nHey, hey, hey (Uh, woo!)\r\nHey, hey, hey (Woo!)\r\n\r\n[Verse 1]\r\nIf you can't write in the proper way (Woo!)\r\nIf you don't know how to conjugate (Woo!)\r\nHey, maybe you flunked that class (Hey, hey, hey)\r\nAnd maybe now you find (Hey, hey, hey)\r\nThat people mock you online (Hey, hey, hey)\r\n(Everybody wise up!)\r\n\r\n[Verse 2]\r\nOkay, now here's the deal\r\nI'll try to educate ya (Woo!) (Ow!)\r\nGonna familiarize\r\nYou with the nomenclature (Woo!) (Meow!)\r\nYou'll learn the definitions (Hey, hey, hey)\r\nOf nouns and prepositions (Hey, hey, hey)\r\nLiteracy's your mission (Hey, hey, hey)\r\n\r\n[Pre-Chorus]\r\nAnd that's why I think it's a good time (Woo!)\r\nTo learn some grammar (What)\r\nNow, did I stammer?\r\nWork on that grammar (Woo!)\r\nYou should know when\r\nIt's \"less\" or it's \"fewer\" (Woo!) (Oh, yeah)\r\nLike people who were (All right)\r\nNever raised in a sewer\r\nYou might also like\r\nAmish Paradise\r\n“Weird Al” Yankovic\r\nFoil\r\n“Weird Al” Yankovic\r\nWhite & Nerdy\r\n“Weird Al” Yankovic\r\n[Chorus 1]\r\nI hate these word crimes\r\nLike \"I could care less\" (Hey, hey, hey)\r\nThat means you do care (Whoa)\r\nAt least a little\r\nDon't be a moron (Hey, hey)\r\nYou'd better slow down\r\nAnd use the right pronoun (Hey, hey)\r\nShow the world you're no clown\r\n(Everybody wise up!)\r\n\r\n[Verse 2]\r\nSay you've got an \"i-t\"\r\nFollowed by apostrophe \"s\" (Woo!)\r\nNow, what does that mean?\r\nYou would not use \"it's\" in this case (Woo!)\r\nAs a possessive (No, no, no!)\r\nIt's a contraction (Yeah, yeah, yeah!)\r\nWhat's a contraction?\r\nWell, it's the shortening of a word or a group of words\r\nBy omission of a sound or letter (Woo!)\r\n\r\n[Pre-Chorus 2]\r\nOkay, now here are some notes\r\nSyntax you're always mangling (Woo!)\r\nNo X in \"espresso\"\r\nYour participle's danglin' (Uh-huh)\r\nBut I don't want your drama (Hey, hey, hey) (Uh-huh)\r\nIf you really wanna (Hey, hey, hey) (Uh-huh)\r\nLeave out that Oxford comma (Hey, hey, hey) (Uh-huh)\r\n[Chorus 2]\r\nJust keep in mind that\r\n\"Be\", \"see\", \"are\", \"you\"\r\nAre words, not letters (Woo!)\r\nGet it together (Hey, hey)\r\nUse your spell checker\r\nYou should never\r\nWrite words using numbers (Woo!) (Hey, hey)\r\nUnless you're seven (Yeah-ay)\r\nOr your name is Prince\r\n(Everybody wise up!)\r\n\r\n[Chorus 1]\r\nI hate these word crimes (I hate them crimes)\r\nYou really need a (Woo!) (I hate them crimes)\r\nFull-time proofreader (I mean those crimes)\r\nYou dumb mouth-breather (Woo!)\r\nWell, you should hire (I'll hire)\r\nSome cunning linguist (Woo!)\r\nTo help you distinguish\r\nWhat is proper English\r\n(Everybody wise up!)\r\n\r\n[Bridge]\r\nOne thing I ask of you (Okay)\r\nTime to learn your homophones is past due (Woo!)\r\nLearn to diagram a sentence, too\r\nAlways say \"to whom\", don't ever say \"to who\" (Woo!)\r\nAnd listen up when I tell you this\r\nI hope you never use quotation marks for emphasis (Woo!)\r\nYou finished second grade, I hope you can tell\r\nIf you're doing good or doing well (Woo!)\r\nYou better figure out the difference\r\nIrony is not coincidence (Woo!)\r\nAnd I thought that you'd gotten it through your skull\r\nAbout what's figurative and what's literal (Woo!)\r\nOh, but just now (Just now), you said (You said)\r\nYou \"literally\" couldn't get out of bed (Woo!) (What?)\r\nThat really makes me want to literally\r\nSmack a crowbar upside your stupid head\r\n[Pre-Chorus]\r\nI read your e-mail (Yeah, yeah)\r\nIt's quite apparent (Woo!)\r\nYour grammar's errant\r\nYou're incoherent (Woo!)\r\nSaw your blog post (Hey, hey)\r\nIt's really fantastic (Oh, woah)\r\nThat was sarcastic (Oh, psych!)\r\n'Cause you write like a spastic (Woo!)\r\n\r\n[Chorus 1]\r\nI hate these word crimes (Everybody wise up)\r\nYour prose is dopey (Woo!) (Hey)\r\nThink you should only (Woah, woah)\r\nWrite in emoji (Woo!)\r\nOh, you're a lost cause (Hey, hey, hey)\r\nGo back to preschool (Woo!)\r\nGet out of the gene pool (Hey, hey, hey)\r\nTry your best to not drool (Woo!)\r\n\r\n[Outro]\r\nNever mind, I give up (Woo!)\r\nReally now, I give up (Woo!)\r\nHey, hey, hey\r\nHey, hey, hey\r\nGo away!");
            var FreshPrince = DefaultPipeline.Process("Now this is the story all about how,\r\nMy life got flipped-turned upside down,\r\nAnd I'd like to take a minute, just sit right there,\r\nI'll tell you how I became the prince of a town called Bel Air.\r\n\r\nIn West Philadelphia, born and raised\r\nOn the playground is where I spent most of my days.\r\nChillin' out, maxin', relaxin all cool,\r\nAnd all shootin' some B-ball outside of the school.\r\n\r\nWhen a couple of guys who were up to no good,\r\nStarted makin' trouble in my neighborhood.\r\nI got in one little fight and my mom got scared,\r\nAnd said \"You're movin' with your auntie and uncle in Bel Air.\"\r\n\r\nI whistled for a cab, and when it came near,\r\nThe license plate said \"fresh\" and it had dice in the mirror.\r\nIf anything I could say that this cab was rare,\r\nBut I thought \"Nah forget it, Yo home to Bel Air.\"\r\n\r\nI pulled up to the house about seven or eight,\r\nand I yelled to the cabby \"Yo homes, smell ya later.\"\r\nLooked at my kingdom, I was finally there,\r\nTo sit on my throne as the Prince of Bel Air.");
            var Summary = WordCrimes.Summarize(3, TextSummarizationLanguage.EnglishDefault);
            Console.WriteLine($"Summary: {Summary.Detokenize()}");
            Console.WriteLine();
            var Features = WordCrimes.GetFeatures(new[] { PhineasAndFerb, FreshPrince }, 4, FeatureExtractionType.EnglishDefault);
            Console.WriteLine($"Features: {Features.ToString(x => x, ", ")}");
            Console.WriteLine();
            Console.WriteLine("Word Tokens:");
            Console.WriteLine("Value - Stemmed Value - Token Type - Entity Type");
            Console.WriteLine("------------------------------------------------");
            WordCrimes.Tokens.Where(x => x.TokenType == TokenType.Word).ForEach(x => Console.WriteLine($"{x.Value} - {x.StemmedValue} - {x.TokenType} - {x.EntityType}"));
        }
    }
}