using GNB.Core.Interfaces;
using System;

namespace GNB.Infrastructure.Services {
    public class RateCacheServiceUri : IRateCacheServiceUri {
        public string ServiceUri { get; }
        public RateCacheServiceUri(string serviceUri) {
            this.ServiceUri = serviceUri;
        }
    }
}
