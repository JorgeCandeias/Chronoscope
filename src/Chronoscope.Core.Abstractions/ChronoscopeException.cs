using System;
using System.Runtime.Serialization;

namespace Chronoscope
{
    [Serializable]
    public class ChronoscopeException : Exception
    {
        public ChronoscopeException()
        {
        }

        public ChronoscopeException(string message) : base(message)
        {
        }

        public ChronoscopeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        #region Serialization

        protected ChronoscopeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Serialization
    }
}