# Enlighten - NLP Library

[![.NET Publish](https://github.com/JaCraig/Enlighten/actions/workflows/dotnet-publish.yml/badge.svg)](https://github.com/JaCraig/Enlighten/actions/workflows/dotnet-publish.yml)

Enlighten is a simple C# library designed to assist with natural language processing (NLP) tasks. It provides a pipeline based set of tools for various NLP operations, including tokenization, sentence detection, summarization, stemming, and feature extraction.

## Features

Enlighten offers the following key features:

1. **Tokenization**: Efficiently break down text into individual tokens or words, making it easier to analyze and process textual data.

2. **Sentence Detection**: Identify and extract sentences from a given piece of text.

3. **Summarization**: Generate concise summaries of longer texts, allowing you to extract the most important information and reduce the overall length of the content.

4. **Stemming**: Reduce words to their base or root form, enabling effective analysis by grouping variations of a word together.

5. **Feature Extraction**: Extract relevant features using term frequency-inverse document frequency (TF-IDF) values, for use in machine learning or text mining tasks.

6. **Part-of-Speech (POS) Tagging**: Assign linguistic tags to words in a sentence, indicating their grammatical properties, which can aid in tasks like named entity recognition or syntactic parsing.

## Getting Started

To use Enlighten in your C# project, follow these steps:

1. **Installation**: Use NuGet to pull down the package.

   dotnet add package Enlighten

2. **Initializing the Library**: Create an instance of the Enlighten class to start using its methods and features:

   ```csharp
   // Add the library and dependencies to your ServiceCollection using the AddCanisterModules method
   var Services = new ServiceCollection()?.AddCanisterModules()?.BuildServiceProvider();
   // Get the Pipeline service from the ServiceCollection
   var DefaultPipeline = Services.GetService<Pipeline>();
   ```

3. **Utilizing the NLP Tools**: Once you have initialized the Enlighten library, you can utilize its various methods for tokenization, sentence detection, summarization, stemming, feature extraction, POS tagging, and more. Refer to the library's documentation or source code for detailed usage instructions and examples.

## Documentation

For more detailed information on using Enlighten and its available methods, please refer to the official documentation. It provides usage examples and class documentation.

## Contributing

Enlighten is an open-source project, and contributions are welcome! If you find a bug, have a feature request, or would like to contribute code improvements, please follow the guidelines in the project's GitHub repository.
