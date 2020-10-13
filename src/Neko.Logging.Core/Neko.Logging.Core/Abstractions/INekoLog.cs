using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Logging.Abstractions
{
    /// <summary>
    /// 写日志文件的日志接口
    /// <para>继承至<see cref="Microsoft.Extensions.Logging.ILogger"/></para>
    /// </summary>
    public interface INekoLog:ILogger
    {
        /// <summary>
        /// 日志存放的路径
        /// <para>默认/Temp/Logs/{LogLevel}</para>
        /// </summary>
        string LogRootPath { get; set; }

        /// <summary>
        /// 日志文件名
        /// <para>默认yyyyMMdd</para>
        /// </summary>
        string LogFileName { get; set; }

        /// <summary>
        /// 日志文件的扩展名
        /// <para>默认 .log</para>
        /// </summary>
        string LogExtension { get; set; }

        /// <summary>
        /// 记录日志的等级
        /// <para>当日志等级大于或等于此等级时才会被记录</para>
        /// </summary>
        LogLevel LogLevel { get; set; }

        /// <summary>
        /// 输出日志到文件
        /// </summary>
        /// <param name="logMsg">要输出的内容</param>
        void WriteToFile(string logMsg);

        /// <summary>
        /// 输出日志到文件
        /// </summary>
        /// <param name="logLevel">输出的日志的等级</param>
        /// <param name="logMsg">要输出的内容</param>
        void WriteToFileAsync(LogLevel logLevel, string logMsg);
    }
}
