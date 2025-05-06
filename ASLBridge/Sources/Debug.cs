using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASLBridge
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }

        public Exception Exception { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            if (Exception != null)
            {
                return $"{Timestamp:yyyy-MM-dd HH:mm:ss} - [ERROR] {Exception.GetType().Name}: {Message} // {Exception.StackTrace}";
            }
            return $"{Timestamp:yyyy-MM-dd HH:mm:ss} - {Message}";
        }
    }

    public static class DebugLog
    {
        public static List<LogEntry> logEntries = new List<LogEntry>();
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly string LogFilePath = Path.Combine(LogDirectory, "aslBridge_log.txt");
        private const long MaxLogFileSizeBytes = 5 * 1024 * 1024; // 5 MB
        private static StreamWriter _streamWriter;

        public static void Initialize()
        {
            try
            {
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }

                if (File.Exists(LogFilePath))
                {
                    var fileInfo = new FileInfo(LogFilePath);
                    if (fileInfo.Length > MaxLogFileSizeBytes)
                    {
                        File.Delete(LogFilePath);
                    }
                }

                _streamWriter = new StreamWriter(LogFilePath, append: true) { AutoFlush = true };
                var separator = $"\n==================== NEW ASLBRIDGE SESSION [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ====================\n";
                _streamWriter.WriteLine(separator);
            }
            catch
            {
            }
        }

        public static void LogMessage(string message, Exception exception = null)
        {
            var entry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Message = message,
                Exception = exception
            };

            logEntries.Add(entry);
            WriteToFile(entry.ToString());

            Console.WriteLine(message);
        }

        private static void WriteToFile(string message)
        {
            try
            {
                _streamWriter?.WriteLine(message);
            }
            catch
            {
                // Ignora errores de escritura para no interrumpir el programa
            }
        }

        public static void Close()
        {
            try
            {
                _streamWriter?.Close();
                _streamWriter = null;
            }
            catch
            {
            }
        }
    }
}
