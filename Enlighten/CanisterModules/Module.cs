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
using Enlighten.Indexer.Interfaces;
using Enlighten.Inflector.Interfaces;
using Enlighten.NameFinder.Interfaces;
using Enlighten.Normalizer.Interfaces;
using Enlighten.POSTagger.Interfaces;
using Enlighten.SentenceDetection.Interfaces;
using Enlighten.Stemmer.Interfaces;
using Enlighten.StopWords.Interfaces;
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
        public void Load(IServiceCollection bootstrapper)
        {
            bootstrapper?.AddAllSingleton<ITokenizerLanguage>()
                        ?.AddAllSingleton<ITokenizer>()
                        ?.AddAllSingleton<IStemmerLanguage>()
                        ?.AddAllSingleton<IStemmer>()
                        ?.AddAllSingleton<IDetector>()
                        ?.AddAllSingleton<ISentenceDetector>()
                        ?.AddAllSingleton<IPOSTagger>()
                        ?.AddAllSingleton<IPOSTaggerLanguage>()
                        ?.AddAllSingleton<IEnglishTokenFinder>()
                        ?.AddSingleton<FrequencyAnalyzer>()
                        ?.AddAllSingleton<ITextSummarizer>()
                        ?.AddAllSingleton<ITextSummarizerLanguage>()
                        ?.AddAllSingleton<IInflector>()
                        ?.AddAllSingleton<IInflectorLanguage>()
                        ?.AddAllSingleton<ISynonymFinder>()
                        ?.AddAllSingleton<ISynonymFinderLanguage>()
                        ?.AddAllSingleton<IFeatureExtractor>()
                        ?.AddAllSingleton<IFeatureExtractorLanguage>()
                        ?.AddAllSingleton<IStopWordsManager>()
                        ?.AddAllSingleton<IStopWordsLanguage>()
                        ?.AddAllSingleton<INormalizer>()
                        ?.AddAllSingleton<ITextNormalizer>()
                        ?.AddAllSingleton<INormalizerManager>()
                        ?.AddTransient<Pipeline>()
                        ?.AddAllSingleton<IIndexer>()
                        ?.AddAllSingleton<IIndexCreator>()
                        ?.AddTransient<Index>()
                        ?.AddAllSingleton<IFinder>()
                        ?.AddAllSingleton<IEntityFinder>();
        }
    }
}