using Enlighten.Indexer;
using Enlighten.Indexer.Enums;
using Enlighten.Indexer.Indexers;
using Enlighten.Indexer.Interfaces;
using Enlighten.Tests.BaseClasses;
using FileCurator;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Enlighten.Tests.Indexer
{
    public class DefaultIndexerTests : TestBaseClass<DefaultIndexer>
    {
        [Fact]
        public void AddDocument()
        {
            string Text = new FileInfo("./Data/MotherJonesArticle.txt");
            var Pipeline = GetServiceProvider().GetService<Pipeline>();
            var TestObject = new DefaultIndexer(new IIndexCreator[] { new InvertedIndexCreator() });
            var Index = TestObject.CreateIndex(IndexerType.InvertedIndex, Array.Empty<Document>());
            Index.AddDoc(Pipeline.Process(Text));
        }

        [Fact]
        public void IndexCreation()
        {
            var TestObject = new DefaultIndexer(new IIndexCreator[] { new InvertedIndexCreator() });
            var Index = TestObject.CreateIndex(IndexerType.InvertedIndex, Array.Empty<Document>());
        }

        [Fact]
        public void Query()
        {
            string MJText = new FileInfo("./Data/MotherJonesArticle.txt");
            string DoorText = new FileInfo("./Data/TheDoor.txt");
            var Pipeline = GetServiceProvider().GetService<Pipeline>();
            var TestObject = new DefaultIndexer(new IIndexCreator[] { new InvertedIndexCreator() });
            var Index = TestObject.CreateIndex(IndexerType.InvertedIndex, Array.Empty<Document>());
            var TestDoc = Pipeline.Process(MJText);
            var DummyDoc = Pipeline.Process(DoorText);
            Index.AddDoc(TestDoc);
            var Results = Index.Query(Pipeline.Process("mayor de Blasio"));
            Assert.Single(Results);
            Assert.Equal(TestDoc.ID, Results[0]);
        }
    }
}