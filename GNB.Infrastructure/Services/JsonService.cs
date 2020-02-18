using System.Net;
using GNB.Core.Exceptions;
using System;
using GNB.Core.Interfaces;

namespace GNB.Infrastructure.Services {
    public abstract class JsonService {
        private IServiceUri jsonServiceUri;
        public JsonService(IServiceUri jsonServiceUri) {
            this.jsonServiceUri = jsonServiceUri;
        }
        protected string GetHttpJsonContent() {
            using (WebClient webClient = new WebClient()) {
                try {
                    return webClient.DownloadString(jsonServiceUri.ServiceUri);
                } catch (WebException ex) {
                    throw new ConnectionErrorException($"Ocurrió un error de conexión con el servicio: {jsonServiceUri.ServiceUri}. Mas información: {ex.Message}. Stack Trace: {ex.StackTrace}");
                } catch (Exception) {
                    throw;
                }
            }
        }
    }
}
