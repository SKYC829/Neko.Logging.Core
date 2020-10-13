using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// NekoLogger主要逻辑
    /// </summary>
    internal class NekoLogger:INekoLog
    {
        public string LogRootPath { get; set; }
        public string LogFileName { get; set; }
        public string LogExtension { get; set; }
        public LogLevel LogLevel { get; set; }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string msgFormat = "[{0:HH:mm:ss}][{1}]:\t{2}\r\n{3}\r\n";
            string logMsg = string.Format(msgFormat, DateTime.Now, logLevel, eventId.Name, formatter(state, exception));
            if (IsEnabled(logLevel))
            {
                //Console.WriteLine(logMsg);
                System.Console.WriteLine(logMsg);
                WriteToFileAsync(logLevel, logMsg);
            }
        }

        public void WriteToFile(string logMsg)
        {
            WriteToFileAsync(LogLevel.Information, logMsg);
        }

        public async void WriteToFileAsync(LogLevel logLevel, string logMsg)
        {
            string logDirectory = CheckDirectory(logLevel);
            string logFileName = CheckFile(logLevel);
            await System.IO.File.AppendAllTextAsync(System.IO.Path.Combine(logDirectory, logFileName), logMsg);
        }

        private string CheckFile(LogLevel logLevel)
        {
            string fileName = string.Format("{0}_{1}", logLevel, LogFileName ?? DateTime.Today.ToString("yyyyMMdd"));
            string fileExtension = string.IsNullOrEmpty(LogExtension) ? ".log" : LogExtension.StartsWith('.') ? LogExtension : string.Format(".{0}", LogExtension);
            return string.Format("{0}{1}", fileName, fileExtension);
        }

        private string CheckDirectory(LogLevel logLevel)
        {
            if (string.IsNullOrEmpty(LogRootPath))
            {
                LogRootPath = "Temp\\Logs";
            }
            string checkPath = System.IO.Path.Combine(LogRootPath, logLevel.ToString());
            if (!System.IO.Directory.Exists(checkPath))
            {
                System.IO.Directory.CreateDirectory(checkPath);
            }
            return checkPath;
        }

    }
}
