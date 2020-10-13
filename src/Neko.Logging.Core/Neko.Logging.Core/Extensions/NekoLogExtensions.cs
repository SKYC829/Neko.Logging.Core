using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Neko.Logging.Core.Extensions;
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
        /// 添加NekoLog依赖注入
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddNekoLog(this IServiceCollection services)
        {
            return services.AddScoped<INekoLog, NekoLogger>();
        }

        /// <summary>
        /// 使用NekoLog记录日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configurationAction">日志的配置信息</param>
        /// <returns></returns>
        public static IApplicationBuilder UseNekoLog(this IApplicationBuilder builder, Action<NekoLogConfiguration> configurationAction)
        {
            return builder.UseMiddleware<NekoLogMiddleware>(configurationAction);
        }

        /// <summary>
        /// 使用NekoLog记录日志
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configJson">日志的配置信息的Json字符串</param>
        /// <returns></returns>
        public static IApplicationBuilder UseNekoLog(this IApplicationBuilder builder, string configJson)
        {
            return builder.UseNekoLog(op => JsonConvert.DeserializeObject<NekoLogConfiguration>(configJson));
        }

        /// <summary>
        /// 使用NekoLog记录日志
        /// <para>将使用默认配置</para>
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseNekoLog(this IApplicationBuilder builder)
        {
            return builder.UseNekoLog(op =>
            {
                op.LogExtension = ".log";
                op.LogFileName = DateTime.Today.ToString("yyyyMMdd");
                op.LogLevel = LogLevel.Information;
                op.LogRootPath = "Temp\\Logs";
            });
        }
    }
}
