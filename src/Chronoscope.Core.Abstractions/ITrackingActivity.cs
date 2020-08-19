using System;
using System.Diagnostics.CodeAnalysis;

namespace Chronoscope
{
    /// <summary>
    /// Defines a tracking activity as created by a scope.
    /// </summary>
    [SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
    public interface ITrackingActivity
    {
        Guid Id { get; }

        void Start();

        void Stop();
    }
}