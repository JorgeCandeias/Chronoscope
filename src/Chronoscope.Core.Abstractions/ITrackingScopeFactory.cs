using System;

namespace Chronoscope
{
    /// <summary>
    /// Defines a way to create new monitoring scopes.
    /// </summary>
    public interface ITrackingScopeFactory
    {
        /// <summary>
        /// Creates a new scope with the specified properties, optionally as a child of the specified scope.
        /// </summary>
        /// <param name="id">The universally unique identifier for the new scope.</param>
        /// <param name="name">The optional name of the new scope.</param>
        /// <param name="parent">The optional id of the parent scope.</param>
        ITrackingScope CreateScope(Guid id, string? name, Guid? parentId);
    }
}