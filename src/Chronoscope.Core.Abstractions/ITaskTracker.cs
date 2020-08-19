using System;
using System.Diagnostics.CodeAnalysis;

namespace Chronoscope
{
    /// <summary>
    /// Defines a tracking activity as created by a scope.
    /// </summary>
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
    public interface ITaskTracker
    {
        Guid Id { get; }

        void Start();

        void Stop();
    }
}