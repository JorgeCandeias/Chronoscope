using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Chronoscope.Tests.Fakes
{
    public class FakeLogger<TCategoryName> : ILogger<TCategoryName>
    {
        public IDisposable BeginScope<TState>(TState state) => new DisposableAction(() => { });

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Items.Add(new FakeLoggerItem
            {
                LogLevel = logLevel,
                EventId = eventId,
                Exception = exception
            });
        }

        public IList<FakeLoggerItem> Items { get; } = new List<FakeLoggerItem>();
    }

    public class FakeLoggerItem
    {
        public LogLevel LogLevel { get; set; }
        public EventId EventId { get; set; }
        public Exception Exception { get; set; }
    }
}