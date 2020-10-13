using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neko.Logging.Core
{
    /// <summary>
    /// NekoLog扩展方法
    /// </summary>
    public static partial class NekoLogExtensions
    {
        /// <summary>
        /// 配置全局的日志注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configurationAction">日志配置信息</param>
        /// <returns></returns>
        public static IHostBuilder BuildNekoLog(this IHostBuilder builder, Action<NekoLogConfiguration> configurationAction)
        {
            return builder.ConfigureLogging(op =>
            {
                op.ClearProviders();
                NekoLogConfiguration logConfiguration = new NekoLogConfiguration();
                configurationAction?.Invoke(logConfiguration);
                op.AddProvider(new NekoLogProvider(logConfiguration));
            });
        }

        /// <summary>
        /// 配置全局的日志注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configJson">日志配置信息的Json字符串</param>
        /// <returns></returns>
        public static IHostBuilder BuildNekoLog(this IHostBuilder builder, string configJson)
        {
            return builder.BuildNekoLog(op => JsonConvert.DeserializeObject<NekoLogConfiguration>(configJson));
        }

        /// <summary>
        /// 配置全局的日志注入
        /// <para>将使用默认配置</para>
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IHostBuilder BuildNekoLog(this IHostBuilder builder)
        {
            return builder.BuildNekoLog(op =>
            {
                op.LogExtension = ".log";
                op.LogFileName = DateTime.Today.ToString("yyyyMMdd");
                op.LogLevel = LogLevel.Information;
                op.LogRootPath = "Temp\\Logs";
            });
        }
    }
}
