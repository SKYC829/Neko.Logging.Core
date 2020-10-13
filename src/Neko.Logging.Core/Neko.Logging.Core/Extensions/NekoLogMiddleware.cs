using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Neko.Logging.Core.Extensions
{
    /// <summary>
    /// NekoLog的中间件
    /// </summary>
    internal class NekoLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly NekoLogConfiguration _logConfiguration;
        public NekoLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public NekoLogMiddleware(RequestDelegate next,Action<NekoLogConfiguration> configuration):this(next)
        {
            if(_logConfiguration == null)
            {
                _logConfiguration = new NekoLogConfiguration();
            }
            configuration?.Invoke(_logConfiguration);
        }

        public async Task InvokeAsync(HttpContext context,INekoLog nekoLog)
        {
            nekoLog.LogRootPath = _logConfiguration.LogRootPath;
            nekoLog.LogFileName = _logConfiguration.LogFileName;
            nekoLog.LogExtension = _logConfiguration.LogExtension;
            nekoLog.LogLevel = _logConfiguration.LogLevel;
            await _next(context);
        }
    }
}
