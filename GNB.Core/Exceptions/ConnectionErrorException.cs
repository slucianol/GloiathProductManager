using System;

namespace GNB.Core.Exceptions {
    public class ConnectionErrorException : Exception {
        private readonly string message;
        public override string Message {
            get {
                return message;
            }
        }
        public ConnectionErrorException(string message) {
            this.message = message;
        }
    }
}
