using GNB.Core.Interfaces;
using GNB.Domain.Entities;
using System.IO;
using Newtonsoft.Json;

namespace GNB.Infrastructure.Services {
    public class LoggerService : ILoggerService {
        public string LogFilePath { get; }
        public LoggerService(string logFilePath) {
            LogFilePath = logFilePath;
        }
        public void Log(Log log) {
            //File.AppendAllText(LogFilePath, JsonConvert.SerializeObject(log));  
        }
    }
}
