using GNB.Domain.ValueObject;

namespace GNB.Domain.Entities {
    public class Log {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public LogType LogType { get; set; }
    }
}
