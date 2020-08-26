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

namespace Enlighten.NameFinder
{
    /// <summary>
    /// Default name finder
    /// </summary>
    /// <seealso cref="IEntityFinder"/>
    public class DefaultEntityFinder : IEntityFinder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultEntityFinder"/> class.
        /// </summary>
        /// <param name="finders">The finders.</param>
        public DefaultEntityFinder(IEnumerable<IFinder> finders)
        {
            Finders = finders.Where(x => x.GetType().Assembly != typeof(DefaultEntityFinder).Assembly).ToDictionary(x => x.Name);
            foreach (var Finder in finders.Where(x => x.GetType().Assembly == typeof(DefaultEntityFinder).Assembly
                && !Finders.ContainsKey(x.Name)))
            {
                Finders.Add(Finder.Name, Finder);
            }
        }

        /// <summary>
        /// Gets the finders.
        /// </summary>
        /// <value>The finders.</value>
        private Dictionary<string, IFinder> Finders { get; }

        /// <summary>
        /// Finds the entities in the specified document.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="entityFinder">The entity finder.</param>
        /// <returns>The document after it is processed.</returns>
        public Token[] Find(Token[] tokens, string entityFinder)
        {
            if (!Finders.TryGetValue(entityFinder, out var Finder))
                return tokens;
            return Finder.Find(tokens);
        }
    }
}