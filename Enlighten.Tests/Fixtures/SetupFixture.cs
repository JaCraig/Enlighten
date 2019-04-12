using System;
using System.Reflection;
using Xunit;

namespace Enlighten.Tests.Fixtures
{
    /// <summary>
    /// Setup collection
    /// </summary>
    /// <seealso cref="ICollectionFixture{SetupFixture}"/>
    [CollectionDefinition("Test collection")]
    public class SetupCollection : ICollectionFixture<SetupFixture>
    {
    }

    /// <summary>
    /// Setup fixture
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public class SetupFixture : IDisposable
    {
        public SetupFixture()
        {
            if (Canister.Builder.Bootstrapper == null)
            {
                Canister.Builder.CreateContainer(null, typeof(SetupFixture).GetTypeInfo().Assembly)
                   .RegisterEnlighten()
                   .Build();
            }
        }

        public void Dispose()
        {
        }
    }
}