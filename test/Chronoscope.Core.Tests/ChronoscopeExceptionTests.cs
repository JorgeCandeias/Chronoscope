using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class ChronoscopeExceptionTests
    {
        [Fact]
        public void ConstructorWorks1()
        {
            // act
            var ex = new ChronoscopeException();

            // assert
            Assert.NotNull(ex.Message);
            Assert.Null(ex.InnerException);
        }

        [Fact]
        public void ConstructorWorks2()
        {
            // arrange
            var message = Guid.NewGuid().ToString();

            // act
            var ex = new ChronoscopeException(message);

            // assert
            Assert.Same(message, ex.Message);
            Assert.Null(ex.InnerException);
        }

        [Fact]
        public void ConstructorWorks3()
        {
            // arrange
            var message = Guid.NewGuid().ToString();
            var inner = new Exception();

            // act
            var ex = new ChronoscopeException(message, inner);

            // assert
            Assert.Same(message, ex.Message);
            Assert.Same(inner, ex.InnerException);
        }

        [Fact]
        public void SerializationWorks()
        {
            // arrange
            var message = Guid.NewGuid().ToString();
            var inner = new InvalidOperationException();
            var ex = new ChronoscopeException(message, inner);
            var formatter = new BinaryFormatter();
            using var stream = new MemoryStream();

            // act
            formatter.Serialize(stream, ex);
            stream.Seek(0, SeekOrigin.Begin);
            var obj = formatter.Deserialize(stream);

            // assert
            var result = Assert.IsType<ChronoscopeException>(obj);
            Assert.Equal(ex.Message, result.Message);
            var innerResult = Assert.IsType<InvalidOperationException>(result.InnerException);
            Assert.Equal(inner.Message, innerResult.Message);
        }
    }
}