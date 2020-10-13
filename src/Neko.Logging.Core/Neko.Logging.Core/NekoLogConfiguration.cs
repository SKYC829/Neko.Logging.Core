using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// 日志的配置信息
    /// </summary>
    public class NekoLogConfiguration
    {
        /// <summary>
        /// 日志存放的路径
        /// <para>默认/Temp/Logs/{LogLevel}</para>
        /// </summary>
        public string LogRootPath { get; set; }

        /// <summary>
        /// 日志文件名
        /// <para>默认yyyyMMdd</para>
        /// </summary>
        public string LogFileName { get; set; }

        /// <summary>
        /// 日志文件的扩展名
        /// <para>默认 .log</para>
        /// </summary>
        public string LogExtension { get; set; }

        /// <summary>
        /// 记录日志的等级
        /// <para>当日志等级大于或等于此等级时才会被记录</para>
        /// </summary>
        public LogLevel LogLevel { get; set; }

        public NekoLogConfiguration()
        {
            LogExtension = ".log";
            LogFileName = DateTime.Today.ToString("yyyyMMdd");
            LogLevel = LogLevel.Information;
            LogRootPath = "Temp\\Logs";
        }
    }
}
