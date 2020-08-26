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

using Enlighten.Tokenizer;

namespace Enlighten.NameFinder.Interfaces
{
    /// <summary>
    /// Name finder interface
    /// </summary>
    public interface IEntityFinder
    {
        /// <summary>
        /// Finds the entities in the specified document.
        /// </summary>
        /// <param name="tokens">The tokens.</param>
        /// <param name="entityFinder">The entity finder.</param>
        /// <returns>The document after it is processed.</returns>
        Token[] Find(Token[] tokens, string entityFinder);
    }
}