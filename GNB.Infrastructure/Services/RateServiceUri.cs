using GNB.Core.Interfaces;

namespace GNB.Infrastructure.Services {
    public class RateServiceUri : IRateServiceUri {
        public string ServiceUri { get; }
        public RateServiceUri(string serviceUri) {
            this.ServiceUri = serviceUri;
        }
    }
}
