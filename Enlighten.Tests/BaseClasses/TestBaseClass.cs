﻿using Mecha.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Enlighten.Tests.BaseClasses
{
    /// <summary>
    /// Test base class
    /// </summary>
    /// <typeparam name="TTestObject">The type of the test object.</typeparam>
    public abstract class TestBaseClass<TTestObject> : TestBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseClass{TTestObject}"/> class.
        /// </summary>
        protected TestBaseClass()
        {
            ObjectType = null;
        }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected override Type? ObjectType { get; set; }

        /// <summary>
        /// Gets or sets the test object.
        /// </summary>
        /// <value>The test object.</value>
        protected TTestObject TestObject { get; set; }

        /// <summary>
        /// Attempts to break the object.
        /// </summary>
        /// <returns>The async task.</returns>
        [Fact]
        public Task BreakObject()
        {
            if (TestObject is null)
                return Task.CompletedTask;
            return Mech.BreakAsync(TestObject, new Options
            {
                MaxDuration = 1000,
                ExceptionHandlers = new ExceptionHandler()
                    .IgnoreException<NotImplementedException>()
                    .IgnoreException<ArgumentOutOfRangeException>((_, __) => true)
                    .IgnoreException<ArgumentException>()
                    .IgnoreException<ObjectDisposedException>((_, __) => true),
                DiscoverInheritedMethods = false
            });
        }
    }

    /// <summary>
    /// Test base class
    /// </summary>
    public abstract class TestBaseClass
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseClass{TTestObject}"/> class.
        /// </summary>
        protected TestBaseClass()
        {
            lock (LockObject)
            {
                _ = Mech.Default;
            }
        }

        protected ObjectPool<StringBuilder> ObjectPool => new ServiceCollection().AddCanisterModules().BuildServiceProvider().GetService<ObjectPool<StringBuilder>>();

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
        protected abstract Type? ObjectType { get; set; }

        /// <summary>
        /// The lock object
        /// </summary>
        private static readonly object LockObject = new object();

        /// <summary>
        /// The service provider lock
        /// </summary>
        private static readonly object ServiceProviderLock = new object();

        /// <summary>
        /// The service provider
        /// </summary>
        private static IServiceProvider ServiceProvider;

        /// <summary>
        /// Attempts to break the object.
        /// </summary>
        /// <returns>The async task.</returns>
        [Fact]
        public Task BreakType()
        {
            if (ObjectType is null)
                return Task.CompletedTask;
            return Mech.BreakAsync(ObjectType, new Options
            {
                MaxDuration = 1000,
                ExceptionHandlers = new ExceptionHandler()
                    .IgnoreException<NotImplementedException>()
                    .IgnoreException<ArgumentOutOfRangeException>((_, __) => true)
                    .IgnoreException<ArgumentException>((_, __) => true)
                    .IgnoreException<FormatException>((_, __) => true)
                    .IgnoreException<ObjectDisposedException>((_, __) => true)
                    .IgnoreException<EndOfStreamException>((_, __) => true)
                    .IgnoreException<OutOfMemoryException>((_, __) => true)
            });
        }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        /// <returns></returns>
        protected static IServiceProvider GetServiceProvider()
        {
            if (ServiceProvider is not null)
                return ServiceProvider;
            lock (ServiceProviderLock)
            {
                if (ServiceProvider is not null)
                    return ServiceProvider;
                ServiceProvider = new ServiceCollection().AddCanisterModules()?.BuildServiceProvider();
            }
            return ServiceProvider;
        }
    }
}