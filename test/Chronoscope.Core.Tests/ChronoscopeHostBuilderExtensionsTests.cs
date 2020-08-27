using Microsoft.Extensions.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class ChronoscopeHostBuilderExtensionsTests
    {
        [Fact]
        public void UseChronoscopeThrowsOnNullBuilder()
        {
            // arrange
            IHostBuilder builder = null;
            Action<HostBuilderContext, IChronoscopeBuilder> configure = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => builder.UseChronoscope(configure));

            // assert
            Assert.Equal(nameof(builder), ex.ParamName);
        }

        [Fact]
        public void UseChronoscopeThrowsOnNullConfigureAction()
        {
            // arrange
            IHostBuilder builder = Mock.Of<IHostBuilder>();
            Action<HostBuilderContext, IChronoscopeBuilder> configure = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => builder.UseChronoscope(configure));

            // assert
            Assert.Equal(nameof(configure), ex.ParamName);
        }

        [Fact]
        public void UseChronoscopeUsesCurrentChronoscopeBuilder()
        {
            // arrange
            IHostBuilder builder = Mock.Of<IHostBuilder>();
            ChronoscopeBuilder cbuilder = new ChronoscopeBuilder(builder);
            var properties = new Dictionary<object, object>() { { nameof(ChronoscopeBuilder), cbuilder } };
            Mock.Get(builder).Setup(x => x.Properties).Returns(properties);

            static void configure(HostBuilderContext ctx, IChronoscopeBuilder b) { /* noop */ }

            // act
            var result = builder.UseChronoscope(configure);

            // assert
            Mock.Get(builder).VerifyAll();
            Assert.Same(builder, result);
        }

        [Fact]
        public void UseChronoscopeCreatesNewChronoscopeBuilder()
        {
            // arrange
            var properties = new Dictionary<object, object>();
            IHostBuilder builder = Mock.Of<IHostBuilder>(x => x.Properties == properties);

            static void configure(HostBuilderContext ctx, IChronoscopeBuilder b) { /* noop */ }

            // act
            var result = builder.UseChronoscope(configure);

            // assert
            Mock.Get(builder).VerifyAll();
            Assert.Same(builder, result);
            Assert.Collection(properties, x =>
            {
                Assert.Equal(nameof(ChronoscopeBuilder), x.Key);
                Assert.NotNull(x.Value);
            });
        }

        [Fact]
        public void UseChronoscopeWithBuilderOnlyThrowsOnNullBuilder()
        {
            // arrange
            IHostBuilder builder = null;
            Action<IChronoscopeBuilder> configure = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => builder.UseChronoscope(configure));

            // assert
            Assert.Equal(nameof(builder), ex.ParamName);
        }

        [Fact]
        public void UseChronoscopeWithBuilderOnlyThrowsOnNullConfigureAction()
        {
            // arrange
            IHostBuilder builder = Mock.Of<IHostBuilder>();
            Action<IChronoscopeBuilder> configure = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => builder.UseChronoscope(configure));

            // assert
            Assert.Equal(nameof(configure), ex.ParamName);
        }

        [Fact]
        public void UseChronoscopeWithBuilderOnlyDefersToMainExtension()
        {
            // arrange
            var builder = new HostBuilder();
            var called = false;
            void configure(IChronoscopeBuilder b) { called = true; }

            // act
            var result = builder.UseChronoscope(configure);
            result.Build();

            // assert
            Assert.Same(builder, result);
            Assert.True(called);
        }
    }
}