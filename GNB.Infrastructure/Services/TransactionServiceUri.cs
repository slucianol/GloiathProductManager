using GNB.Core.Interfaces;

namespace GNB.Infrastructure.Services {
    public class TransactionServiceUri : ITransactionServiceUri {
        public string ServiceUri { get; }
        public TransactionServiceUri(string serviceUri) {
            this.ServiceUri = serviceUri;
        }
    }
}
