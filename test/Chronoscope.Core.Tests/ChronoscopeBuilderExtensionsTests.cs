﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class ChronoscopeBuilderExtensionsTests
    {
        [Fact]
        public void ConfigureChronoscopeWithBuilderOnlyRefusesNullBuilder()
        {
            // arrange
            IChronoscopeBuilder builder = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => builder.ConfigureChronoscope(b => { }));

            // assert
            Assert.Equal(nameof(builder), ex.ParamName);
        }

        [Fact]
        public void ConfigureChronoscopeWithBuilderOnlyRefusesNullAction()
        {
            // arrange
            var builder = Mock.Of<IChronoscopeBuilder>();
            Action<IChronoscopeBuilder> configure = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => builder.ConfigureChronoscope(configure));

            // assert
            Assert.Equal(nameof(configure), ex.ParamName);
        }

        [Fact]
        public void ConfigureChronoscopeWithBuilderOnlyReturnsBuilder()
        {
            // arrange
            var builder = Mock.Of<IChronoscopeBuilder>();
            Mock.Get(builder).Setup(x => x.ConfigureChronoscope(It.IsAny<Action<HostBuilderContext, IChronoscopeBuilder>>())).Returns(builder);
            static void configure(IChronoscopeBuilder b) { /* noop */ }

            // act
            var result = builder.ConfigureChronoscope(configure);

            // assert
            Mock.Get(builder).VerifyAll();
            Assert.Same(builder, result);
        }

        [Fact]
        public void ConfigureServicesWithServicesOnlyRefusesNullBuilder()
        {
            // arrange
            IChronoscopeBuilder builder = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => builder.ConfigureServices(s => { }));

            // assert
            Assert.Equal(nameof(builder), ex.ParamName);
        }

        [Fact]
        public void ConfigureServicesWithServicesOnlyRefusesNullAction()
        {
            // arrange
            var builder = Mock.Of<IChronoscopeBuilder>();
            Action<IServiceCollection> configure = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => builder.ConfigureServices(configure));

            // assert
            Assert.Equal(nameof(configure), ex.ParamName);
        }

        [Fact]
        public void ConfigureServicesWithServicesOnlyReturnsBuilder()
        {
            // arrange
            var builder = Mock.Of<IChronoscopeBuilder>();
            Mock.Get(builder).Setup(x => x.ConfigureServices(It.IsAny<Action<HostBuilderContext, IServiceCollection>>())).Returns(builder);
            static void configure(IServiceCollection b) { /* noop */ }

            // act
            var result = builder.ConfigureServices(configure);

            // assert
            Mock.Get(builder).VerifyAll();
            Assert.Same(builder, result);
        }
    }
}