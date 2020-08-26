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

using Enlighten.NameFinder.Interfaces;
using Enlighten.Tokenizer;
using System.Collections.Generic;
using System.Linq;

namespace Enlighten.NameFinder.Finders
{
    /// <summary>
    /// Default name finder
    /// </summary>
    /// <seealso cref="IFinder"/>
    public class DefaultFinder : IFinder
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = "naive";

        /// <summary>
        /// Finds the entities in the specified document.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <returns>The document after it is processed.</returns>
        public Token[] Find(Token[] tokens)
        {
            if (tokens.Count(x => x.Value.Length > 0 && char.IsUpper(x.Value[0])) > (tokens.Length * .75f))
                return tokens;
            for (var x = 0; x < tokens.Length; ++x)
            {
                var Token = tokens[x];
                if (Token.PartOfSpeech == "NNP" || Token.PartOfSpeech == "NNPS")
                {
                    tokens = FindTokenEnd(tokens, x);
                }
            }
            return tokens;
        }

        private Token[] FindTokenEnd(Token[] tokens, int start)
        {
            List<Token> Tokens = new List<Token>();
            int Start = start;
            int End = start;
            for (int x = start; x < tokens.Length; ++x)
            {
                var Token = tokens[x];
                if (Token.PartOfSpeech == "NNP" || Token.PartOfSpeech == "NNPS" || Token.PartOfSpeech is null || Token.PartOfSpeech == "SYM" || Token.PartOfSpeech == ".")
                {
                    End = x;
                }
                else
                {
                    break;
                }
            }
            while (tokens[End].PartOfSpeech is null || tokens[End].PartOfSpeech == "SYM")
            {
                --End;
            }
            if (Start == End)
            {
                tokens[start].Entity = true;
                return tokens;
            }
            Token? NewToken = null;
            for (int x = 0; x < tokens.Length; ++x)
            {
                if (x >= Start && x < End)
                {
                    if (NewToken is null)
                    {
                        NewToken = tokens[x];
                    }
                    else
                    {
                        NewToken = NewToken.Join(tokens[x]);
                    }
                }
                else if (x == End && !(NewToken is null))
                {
                    NewToken = NewToken.Join(tokens[x]);
                    NewToken.Entity = true;
                    Tokens.Add(NewToken);
                }
                else
                {
                    Tokens.Add(tokens[x]);
                }
            }
            return Tokens.ToArray();
        }
    }
}