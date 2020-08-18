using System;

namespace Chronoscope
{
    /// <summary>
    /// Defines a tracking scope that can measure task lifetime properties.
    /// </summary>
    public interface ITaskScope : ICreateScope
    {
        /// <summary>
        /// The unique identifier of this scope.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// The name of this scope.
        /// </summary>
        string Name { get; }
    }
}