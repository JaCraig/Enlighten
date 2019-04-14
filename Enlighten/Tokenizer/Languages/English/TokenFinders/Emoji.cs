/*
Copyright 2019 James Craig

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using Enlighten.Tokenizer.BaseClasses;
using Enlighten.Tokenizer.Languages.English.Enums;
using Enlighten.Tokenizer.Utils;
using System.Text.RegularExpressions;

namespace Enlighten.Tokenizer.Languages.English.TokenFinders
{
    /// <summary>
    /// Emoji finder
    /// </summary>
    /// <seealso cref="TokenFinderBaseClass"/>
    public class Emoji : TokenFinderBaseClass
    {
        private static Regex EmojiRegex = new Regex(@"\\uD83D[\\uDC68-\\uDC69](?:\\uD83C[\\uDFFB-\\uDFFF])?\\u200D(?:\\u2695\\uFE0F|\\u2696\\uFE0F|\\u2708\\uFE0F|\\uD83C[\\uDF3E\\uDF73\\uDF93\\uDFA4\\uDFA8\\uDFEB\\uDFED]|\\uD83D[\\uDCBB\\uDCBC\\uDD27\\uDD2C\\uDE80\\uDE92])|(?:\\uD83C[\\uDFCB\\uDFCC]|\\uD83D\\uDD75|\\u26F9)(?:\\uFE0F|\\uD83C[\\uDFFB-\\uDFFF])\\u200D[\\u2640\\u2642]\\uFE0F|(?:\\uD83C[\\uDFC3\\uDFC4\\uDFCA]|\\uD83D[\\uDC6E\\uDC71\\uDC73\\uDC77\\uDC81\\uDC82\\uDC86\\uDC87\\uDE45-\\uDE47\\uDE4B\\uDE4D\\uDE4E\\uDEA3\\uDEB4-\\uDEB6]|\\uD83E[\\uDD26\\uDD37-\\uDD39\\uDD3D\\uDD3E])(?:\\uD83C[\\uDFFB-\\uDFFF])?\\u200D[\\u2640\\u2642]\\uFE0F|\\uD83D\\uDC68\\u200D\\u2764\\uFE0F\\u200D\\uD83D\\uDC8B\\u200D\\uD83D\\uDC68|\\uD83D\\uDC68\\u200D\\uD83D\\uDC68\\u200D\\uD83D\\uDC66\\u200D\\uD83D\\uDC66|\\uD83D\\uDC68\\u200D\\uD83D\\uDC68\\u200D\\uD83D\\uDC67\\u200D\\uD83D[\\uDC66\\uDC67]|\\uD83D\\uDC68\\u200D\\uD83D\\uDC69\\u200D\\uD83D\\uDC66\\u200D\\uD83D\\uDC66|\\uD83D\\uDC68\\u200D\\uD83D\\uDC69\\u200D\\uD83D\\uDC67\\u200D\\uD83D[\\uDC66\\uDC67]|\\uD83D\\uDC69\\u200D\\u2764\\uFE0F\\u200D\\uD83D\\uDC8B\\u200D\\uD83D[\\uDC68\\uDC69]|\\uD83D\\uDC69\\u200D\\uD83D\\uDC69\\u200D\\uD83D\\uDC66\\u200D\\uD83D\\uDC66|\\uD83D\\uDC69\\u200D\\uD83D\\uDC69\\u200D\\uD83D\\uDC67\\u200D\\uD83D[\\uDC66\\uDC67]|\\uD83D\\uDC68\\u200D\\u2764\\uFE0F\\u200D\\uD83D\\uDC68|\\uD83D\\uDC68\\u200D\\uD83D\\uDC66\\u200D\\uD83D\\uDC66|\\uD83D\\uDC68\\u200D\\uD83D\\uDC67\\u200D\\uD83D[\\uDC66\\uDC67]|\\uD83D\\uDC68\\u200D\\uD83D\\uDC68\\u200D\\uD83D[\\uDC66\\uDC67]|\\uD83D\\uDC68\\u200D\\uD83D\\uDC69\\u200D\\uD83D[\\uDC66\\uDC67]|\\uD83D\\uDC69\\u200D\\u2764\\uFE0F\\u200D\\uD83D[\\uDC68\\uDC69]|\\uD83D\\uDC69\\u200D\\uD83D\\uDC66\\u200D\\uD83D\\uDC66|\\uD83D\\uDC69\\u200D\\uD83D\\uDC67\\u200D\\uD83D[\\uDC66\\uDC67]|\\uD83D\\uDC69\\u200D\\uD83D\\uDC69\\u200D\\uD83D[\\uDC66\\uDC67]|\\uD83C\\uDFF3\\uFE0F\\u200D\\uD83C\\uDF08|\\uD83C\\uDFF4\\u200D\\u2620\\uFE0F|\\uD83D\\uDC41\\u200D\\uD83D\\uDDE8|\\uD83D\\uDC68\\u200D\\uD83D[\\uDC66\\uDC67]|\\uD83D\\uDC69\\u200D\\uD83D[\\uDC66\\uDC67]|\\uD83D\\uDC6F\\u200D\\u2640\\uFE0F|\\uD83D\\uDC6F\\u200D\\u2642\\uFE0F|\\uD83E\\uDD3C\\u200D\\u2640\\uFE0F|\\uD83E\\uDD3C\\u200D\\u2642\\uFE0F|(?:[\\u0023\\u002A\\u0030-\\u0039])\\uFE0F?\\u20E3|(?:(?:\\uD83C[\\uDFCB\\uDFCC]|\\uD83D[\\uDD74\\uDD75\\uDD90]|[\\u261D\\u26F7\\u26F9\\u270C\\u270D])(?:\\uFE0F|(?!\\uFE0E))|\\uD83C[\\uDF85\\uDFC2-\\uDFC4\\uDFC7\\uDFCA]|\\uD83D[\\uDC42\\uDC43\\uDC46-\\uDC50\\uDC66-\\uDC69\\uDC6E\\uDC70-\\uDC78\\uDC7C\\uDC81-\\uDC83\\uDC85-\\uDC87\\uDCAA\\uDD7A\\uDD95\\uDD96\\uDE45-\\uDE47\\uDE4B-\\uDE4F\\uDEA3\\uDEB4-\\uDEB6\\uDEC0\\uDECC]|\\uD83E[\\uDD18-\\uDD1C\\uDD1E\\uDD26\\uDD30\\uDD33-\\uDD39\\uDD3D\\uDD3E]|[\\u270A\\u270B])(?:\\uD83C[\\uDFFB-\\uDFFF]|)|\\uD83C\\uDDE6\\uD83C[\\uDDE8-\\uDDEC\\uDDEE\\uDDF1\\uDDF2\\uDDF4\\uDDF6-\\uDDFA\\uDDFC\\uDDFD\\uDDFF]|\\uD83C\\uDDE7\\uD83C[\\uDDE6\\uDDE7\\uDDE9-\\uDDEF\\uDDF1-\\uDDF4\\uDDF6-\\uDDF9\\uDDFB\\uDDFC\\uDDFE\\uDDFF]|\\uD83C\\uDDE8\\uD83C[\\uDDE6\\uDDE8\\uDDE9\\uDDEB-\\uDDEE\\uDDF0-\\uDDF5\\uDDF7\\uDDFA-\\uDDFF]|\\uD83C\\uDDE9\\uD83C[\\uDDEA\\uDDEC\\uDDEF\\uDDF0\\uDDF2\\uDDF4\\uDDFF]|\\uD83C\\uDDEA\\uD83C[\\uDDE6\\uDDE8\\uDDEA\\uDDEC\\uDDED\\uDDF7-\\uDDFA]|\\uD83C\\uDDEB\\uD83C[\\uDDEE-\\uDDF0\\uDDF2\\uDDF4\\uDDF7]|\\uD83C\\uDDEC\\uD83C[\\uDDE6\\uDDE7\\uDDE9-\\uDDEE\\uDDF1-\\uDDF3\\uDDF5-\\uDDFA\\uDDFC\\uDDFE]|\\uD83C\\uDDED\\uD83C[\\uDDF0\\uDDF2\\uDDF3\\uDDF7\\uDDF9\\uDDFA]|\\uD83C\\uDDEE\\uD83C[\\uDDE8-\\uDDEA\\uDDF1-\\uDDF4\\uDDF6-\\uDDF9]|\\uD83C\\uDDEF\\uD83C[\\uDDEA\\uDDF2\\uDDF4\\uDDF5]|\\uD83C\\uDDF0\\uD83C[\\uDDEA\\uDDEC-\\uDDEE\\uDDF2\\uDDF3\\uDDF5\\uDDF7\\uDDFC\\uDDFE\\uDDFF]|\\uD83C\\uDDF1\\uD83C[\\uDDE6-\\uDDE8\\uDDEE\\uDDF0\\uDDF7-\\uDDFB\\uDDFE]|\\uD83C\\uDDF2\\uD83C[\\uDDE6\\uDDE8-\\uDDED\\uDDF0-\\uDDFF]|\\uD83C\\uDDF3\\uD83C[\\uDDE6\\uDDE8\\uDDEA-\\uDDEC\\uDDEE\\uDDF1\\uDDF4\\uDDF5\\uDDF7\\uDDFA\\uDDFF]|\\uD83C\\uDDF4\\uD83C\\uDDF2|\\uD83C\\uDDF5\\uD83C[\\uDDE6\\uDDEA-\\uDDED\\uDDF0-\\uDDF3\\uDDF7-\\uDDF9\\uDDFC\\uDDFE]|\\uD83C\\uDDF6\\uD83C\\uDDE6|\\uD83C\\uDDF7\\uD83C[\\uDDEA\\uDDF4\\uDDF8\\uDDFA\\uDDFC]|\\uD83C\\uDDF8\\uD83C[\\uDDE6-\\uDDEA\\uDDEC-\\uDDF4\\uDDF7-\\uDDF9\\uDDFB\\uDDFD-\\uDDFF]|\\uD83C\\uDDF9\\uD83C[\\uDDE6\\uDDE8\\uDDE9\\uDDEB-\\uDDED\\uDDEF-\\uDDF4\\uDDF7\\uDDF9\\uDDFB\\uDDFC\\uDDFF]|\\uD83C\\uDDFA\\uD83C[\\uDDE6\\uDDEC\\uDDF2\\uDDF3\\uDDF8\\uDDFE\\uDDFF]|\\uD83C\\uDDFB\\uD83C[\\uDDE6\\uDDE8\\uDDEA\\uDDEC\\uDDEE\\uDDF3\\uDDFA]|\\uD83C\\uDDFC\\uD83C[\\uDDEB\\uDDF8]|\\uD83C\\uDDFD\\uD83C\\uDDF0|\\uD83C\\uDDFE\\uD83C[\\uDDEA\\uDDF9]|\\uD83C\\uDDFF\\uD83C[\\uDDE6\\uDDF2\\uDDFC]|\\uD800\\uDC00|\\uD83C[\\uDCCF\\uDD8E\\uDD91-\\uDD9A\\uDDE6-\\uDDFF\\uDE01\\uDE32-\\uDE36\\uDE38-\\uDE3A\\uDE50\\uDE51\\uDF00-\\uDF20\\uDF2D-\\uDF35\\uDF37-\\uDF7C\\uDF7E-\\uDF84\\uDF86-\\uDF93\\uDFA0-\\uDFC1\\uDFC5\\uDFC6\\uDFC8\\uDFC9\\uDFCF-\\uDFD3\\uDFE0-\\uDFF0\\uDFF4\\uDFF8-\\uDFFF]|\\uD83D[\\uDC00-\\uDC3E\\uDC40\\uDC44\\uDC45\\uDC51-\\uDC65\\uDC6A-\\uDC6D\\uDC6F\\uDC79-\\uDC7B\\uDC7D-\\uDC80\\uDC84\\uDC88-\\uDCA9\\uDCAB-\\uDCFC\\uDCFF-\\uDD3D\\uDD4B-\\uDD4E\\uDD50-\\uDD67\\uDDA4\\uDDFB-\\uDE44\\uDE48-\\uDE4A\\uDE80-\\uDEA2\\uDEA4-\\uDEB3\\uDEB7-\\uDEBF\\uDEC1-\\uDEC5\\uDED0-\\uDED2\\uDEEB\\uDEEC\\uDEF4-\\uDEF6]|\\uD83E[\\uDD10-\\uDD17\\uDD1D\\uDD20-\\uDD25\\uDD27\\uDD3A\\uDD3C\\uDD40-\\uDD45\\uDD47-\\uDD4B\\uDD50-\\uDD5E\\uDD80-\\uDD91\\uDDC0]|[\\u23E9-\\u23EC\\u23F0\\u23F3\\u2640\\u2642\\u2695\\u26CE\\u2705\\u2728\\u274C\\u274E\\u2753-\\u2755\\u2795-\\u2797\\u27B0\\u27BF\\uE50A]|(?:\\uD83C[\\uDC04\\uDD70\\uDD71\\uDD7E\\uDD7F\\uDE02\\uDE1A\\uDE2F\\uDE37\\uDF21\\uDF24-\\uDF2C\\uDF36\\uDF7D\\uDF96\\uDF97\\uDF99-\\uDF9B\\uDF9E\\uDF9F\\uDFCD\\uDFCE\\uDFD4-\\uDFDF\\uDFF3\\uDFF5\\uDFF7]|\\uD83D[\\uDC3F\\uDC41\\uDCFD\\uDD49\\uDD4A\\uDD6F\\uDD70\\uDD73\\uDD76-\\uDD79\\uDD87\\uDD8A-\\uDD8D\\uDDA5\\uDDA8\\uDDB1\\uDDB2\\uDDBC\\uDDC2-\\uDDC4\\uDDD1-\\uDDD3\\uDDDC-\\uDDDE\\uDDE1\\uDDE3\\uDDE8\\uDDEF\\uDDF3\\uDDFA\\uDECB\\uDECD-\\uDECF\\uDEE0-\\uDEE5\\uDEE9\\uDEF0\\uDEF3]|[\\u00A9\\u00AE\\u203C\\u2049\\u2122\\u2139\\u2194-\\u2199\\u21A9\\u21AA\\u231A\\u231B\\u2328\\u23CF\\u23ED-\\u23EF\\u23F1\\u23F2\\u23F8-\\u23FA\\u24C2\\u25AA\\u25AB\\u25B6\\u25C0\\u25FB-\\u25FE\\u2600-\\u2604\\u260E\\u2611\\u2614\\u2615\\u2618\\u2620\\u2622\\u2623\\u2626\\u262A\\u262E\\u262F\\u2638-\\u263A\\u2648-\\u2653\\u2660\\u2663\\u2665\\u2666\\u2668\\u267B\\u267F\\u2692-\\u2694\\u2696\\u2697\\u2699\\u269B\\u269C\\u26A0\\u26A1\\u26AA\\u26AB\\u26B0\\u26B1\\u26BD\\u26BE\\u26C4\\u26C5\\u26C8\\u26CF\\u26D1\\u26D3\\u26D4\\u26E9\\u26EA\\u26F0-\\u26F5\\u26F8\\u26FA\\u26FD\\u2702\\u2708\\u2709\\u270F\\u2712\\u2714\\u2716\\u271D\\u2721\\u2733\\u2734\\u2744\\u2747\\u2757\\u2763\\u2764\\u27A1\\u2934\\u2935\\u2B05-\\u2B07\\u2B1B\\u2B1C\\u2B50\\u2B55\\u3030\\u303D\\u3297\\u3299])(?:\\uFE0F|(?!\\uFE0E))");

        /// <summary>
        /// Gets the order.
        /// </summary>
        /// <value>The order.</value>
        public override int Order => 3;

        /// <summary>
        /// The actual implementation of the IsMatch done by the individual classes.
        /// </summary>
        /// <param name="tokenizer">The tokenizer.</param>
        /// <returns>The token.</returns>
        protected override Token IsMatchImpl(TokenizableStream<char> tokenizer)
        {
            if (tokenizer.End() || !IsEmoji(tokenizer.Current))
                return null;

            var StartPosition = tokenizer.Index;

            while (!tokenizer.End() && IsEmoji(tokenizer.Current))
            {
                tokenizer.Consume();
            }

            var EndPosition = tokenizer.Index - 1;

            return new Token
            {
                EndPosition = EndPosition,
                StartPosition = StartPosition,
                TokenType = TokenType.Emoji,
                Value = new string(tokenizer.Slice(StartPosition, EndPosition).ToArray())
            };
        }

        /// <summary>
        /// Determines whether the specified peek character is emoji.
        /// </summary>
        /// <param name="peekCharacter">The peek character.</param>
        /// <returns><c>true</c> if the specified peek character is emoji; otherwise, <c>false</c>.</returns>
        private static bool IsEmoji(char peekCharacter)
        {
            if (peekCharacter >= 0x0080 && peekCharacter <= 0x02AF) return true;
            if (peekCharacter >= 0x0300 && peekCharacter <= 0x03FF) return true;
            if (peekCharacter >= 0x0600 && peekCharacter <= 0x06FF) return true;
            if (peekCharacter >= 0x0C00 && peekCharacter <= 0x0C7F) return true;
            if (peekCharacter >= 0x1DC0 && peekCharacter <= 0x1DFF) return true;
            if (peekCharacter >= 0x1E00 && peekCharacter <= 0x1EFF) return true;
            if (peekCharacter >= 0x2000 && peekCharacter <= 0x209F) return true;
            if (peekCharacter >= 0x20D0 && peekCharacter <= 0x214F) return true;
            if (peekCharacter >= 0x2190 && peekCharacter <= 0x23FF) return true;
            if (peekCharacter >= 0x2460 && peekCharacter <= 0x25FF) return true;
            if (peekCharacter >= 0x2600 && peekCharacter <= 0x27EF) return true;
            if (peekCharacter >= 0x2900 && peekCharacter <= 0x29FF) return true;
            if (peekCharacter >= 0x2B00 && peekCharacter <= 0x2BFF) return true;
            if (peekCharacter >= 0x2C60 && peekCharacter <= 0x2C7F) return true;
            if (peekCharacter >= 0x2E00 && peekCharacter <= 0x2E7F) return true;
            if (peekCharacter >= 0x3000 && peekCharacter <= 0x303F) return true;
            if (peekCharacter >= 0xA490 && peekCharacter <= 0xA4CF) return true;
            if (peekCharacter >= 0xE000 && peekCharacter <= 0xF8FF) return true;
            if (peekCharacter >= 0xFE00 && peekCharacter <= 0xFE0F) return true;
            if (peekCharacter >= 0xFE30 && peekCharacter <= 0xFE4F) return true;
            if (peekCharacter >= 0x1F000 && peekCharacter <= 0x1F02F) return true;
            if (peekCharacter >= 0x1F0A0 && peekCharacter <= 0x1F0FF) return true;
            if (peekCharacter >= 0x1F100 && peekCharacter <= 0x1F64F) return true;
            if (peekCharacter >= 0x1F680 && peekCharacter <= 0x1F6FF) return true;
            if (peekCharacter >= 0x1F910 && peekCharacter <= 0x1F96B) return true;
            if (peekCharacter >= 0x1F980 && peekCharacter <= 0x1F9E0) return true;
            if (peekCharacter == 0x200d)
                return true;

            return false;
        }
    }
}