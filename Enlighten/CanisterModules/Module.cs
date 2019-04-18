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

using Canister.Interfaces;
using Enlighten.Tokenizer.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Enlighten.CanisterModules
{
    /// <summary>
    /// Canister module
    /// </summary>
    /// <seealso cref="IModule"/>
    public class Module : IModule
    {
        /// <summary>
        /// Order to run this in
        /// </summary>
        public int Order => 1;

        public void Load(IBootstrapper bootstrapper)
        {
            if (bootstrapper == null)
                return;

            bootstrapper.RegisterAll<ITokenizerLanguage>(ServiceLifetime.Singleton);
            bootstrapper.RegisterAll<ITokenizer>(ServiceLifetime.Singleton);
        }
    }
}