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
using Enlighten.FeatureExtraction.Interfaces;
using Enlighten.Frequency;
using Enlighten.Inflector.Interfaces;
using Enlighten.POSTagger.Interfaces;
using Enlighten.SentenceDetection.Interfaces;
using Enlighten.Stemmer.Interfaces;
using Enlighten.SynonymFinder.Interfaces;
using Enlighten.TextSummarization.Interfaces;
using Enlighten.Tokenizer.Interfaces;
using Enlighten.Tokenizer.Languages.Interfaces;
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

        /// <summary>
        /// Loads the module using the bootstrapper
        /// </summary>
        /// <param name="bootstrapper">The bootstrapper.</param>
        public void Load(IBootstrapper bootstrapper)
        {
            bootstrapper?.RegisterAll<ITokenizerLanguage>(ServiceLifetime.Singleton)
                        .RegisterAll<ITokenizer>(ServiceLifetime.Singleton)
                        .RegisterAll<IStemmerLanguage>(ServiceLifetime.Singleton)
                        .RegisterAll<IStemmer>(ServiceLifetime.Singleton)
                        .RegisterAll<IDetector>(ServiceLifetime.Singleton)
                        .RegisterAll<ISentenceDetector>(ServiceLifetime.Singleton)
                        .RegisterAll<IPOSTagger>(ServiceLifetime.Singleton)
                        .RegisterAll<IPOSTaggerLanguage>(ServiceLifetime.Singleton)
                        .RegisterAll<IEnglishTokenFinder>(ServiceLifetime.Singleton)
                        .Register<FrequencyAnalyzer>(ServiceLifetime.Singleton)
                        .RegisterAll<ITextSummarizer>(ServiceLifetime.Singleton)
                        .RegisterAll<ITextSummarizerLanguage>(ServiceLifetime.Singleton)
                        .RegisterAll<IInflector>(ServiceLifetime.Singleton)
                        .RegisterAll<IInflectorLanguage>(ServiceLifetime.Singleton)
                        .RegisterAll<ISynonymFinder>(ServiceLifetime.Singleton)
                        .RegisterAll<ISynonymFinderLanguage>(ServiceLifetime.Singleton)
                        .RegisterAll<IFeatureExtractor>(ServiceLifetime.Singleton)
                        .RegisterAll<IFeatureExtractorLanguage>(ServiceLifetime.Singleton);
        }
    }
}