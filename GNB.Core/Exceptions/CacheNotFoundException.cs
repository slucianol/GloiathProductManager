using System;

namespace GNB.Core.Exceptions {
    public class CacheNotFoundException : Exception {
        private readonly string message;
        public override string Message {
            get {
                return message;
            }
        }
        public CacheNotFoundException(string message) {
            this.message = message;
        }
    }
}
