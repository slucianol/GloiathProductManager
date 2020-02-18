using GNB.Domain.Entities;

namespace GNB.Core.Interfaces {
    public interface ILoggerService {
        string LogFilePath { get; }
        void Log(Log log);
    }
}
