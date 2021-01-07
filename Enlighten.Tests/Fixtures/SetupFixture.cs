using Microsoft.Extensions.DependencyInjection;
using System;
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
                new ServiceCollection().AddCanisterModules(x => x.AddAssembly(typeof(SetupFixture).Assembly)
                   .RegisterEnlighten()
                   .RegisterFileCurator());
            }
        }

        public void Dispose()
        {
        }
    }
}