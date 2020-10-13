using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// NekoLog的日志提供器
    /// </summary>
    internal class NekoLogProvider : ILoggerProvider
    {
        private readonly NekoLogConfiguration _logConfiguration;
        private readonly ConcurrentDictionary<string, INekoLog> _loggers = new ConcurrentDictionary<string, INekoLog>();

        public NekoLogProvider(NekoLogConfiguration configuration)
        {
            _logConfiguration = configuration;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new NekoLogger());
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
