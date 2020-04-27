using Microsoft.Extensions.ObjectPool;
using System;
using System.Text;
using Xunit;

namespace Enlighten.Tests.BaseClasses
{
    /// <summary>
    /// Test base class
    /// </summary>
    /// <seealso cref="IDisposable"/>
    [Collection("Test collection")]
    public abstract class TestBaseClass
    {
        protected ObjectPool<StringBuilder> ObjectPool => Canister.Builder.Bootstrapper.Resolve<ObjectPool<StringBuilder>>();
    }
}