using System;

namespace Chronoscope
{
    /// <summary>
    /// Defines a means to create a monitoring scope or sub-scope.
    /// </summary>
    public interface ICreateScope
    {
        /// <summary>
        /// Creates a new child scope.
        /// </summary>
        /// <param name="id">The user-defined id for this scope. Must be universally unique.</param>
        /// <param name="name">The name of this scope. Does not have to be unique.</param>
        ITrackingScope CreateScope(Guid id, string? name);
    }

    /// <summary>
    /// Quality-of-life extension methods for <see cref="ICreateScope"/>.
    /// </summary>
    public static class CreateScopeExtensions
    {
        /// <summary>
        /// Creates a new child scope with an automatic identifier and the specified name.
        /// </summary>
        /// <param name="name">The name of this scope. Does not have to be unique.</param>
        public static ITrackingScope CreateScope(this ICreateScope obj, string? name)
        {
            if (obj is null) throw new ArgumentNullException(nameof(obj));

            return obj.CreateScope(Guid.NewGuid(), name);
        }

        /// <summary>
        /// Creates a new nameless child scope with an automatic identifier.
        /// </summary>
        public static ITrackingScope CreateScope(this ICreateScope obj)
        {
            return obj.CreateScope(null);
        }
    }
}