using GNB.Core.Exceptions;
using GNB.Core.Interfaces;
using GNB.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace GNB.Infrastructure.Services {
    public class RateCacheService : IRateCacheService {
        private readonly ILoggerService loggerService;
        private readonly IRateCacheServiceUri rateCacheServiceUri;
        public RateCacheService(IRateCacheServiceUri rateCacheServiceUri, ILoggerService loggerService) {
            this.rateCacheServiceUri = rateCacheServiceUri;
            this.loggerService = loggerService;
        }
        public bool ExistsRatesCache() {
            return File.Exists(rateCacheServiceUri.ServiceUri);
        }

        public IQueryable<RateConverter> GetCacheRates() {
            if (ExistsRatesCache()) {
                string jsonString = File.ReadAllText(rateCacheServiceUri.ServiceUri);
                loggerService.Log(new Log {
                    LogType = Domain.ValueObject.LogType.Information,
                    Message = $"Fueron cargados los Rates del cache de almacenamiento {rateCacheServiceUri.ServiceUri}.",
                    StackTrace = "GNB.Infrastructure.Services.RateCacheService.GetCacheRates"
                });
                return JsonConvert.DeserializeObject<List<RateConverter>>(jsonString).AsQueryable();
            }
            loggerService.Log(new Log {
                LogType = Domain.ValueObject.LogType.Error,
                Message = $"El cache de almacenamiento {rateCacheServiceUri.ServiceUri} no fue encontrado.",
                StackTrace = "GNB.Infrastructure.Services.RateCacheService.GetCacheRates"
            });
            throw new CacheNotFoundException($"El almacenamiento para el caché no fue encontrado en la ruta: {rateCacheServiceUri.ServiceUri}");
        }

        public void PersistRatesInCache(List<RateConverter> rates) {
            string jsonString = JsonConvert.SerializeObject(rates);
            File.WriteAllText(rateCacheServiceUri.ServiceUri, jsonString);
            loggerService.Log(new Log {
                LogType = Domain.ValueObject.LogType.Information,
                Message = $"Fueron persistidos los Rates en el almacenamiento de caché {rateCacheServiceUri.ServiceUri}.",
                StackTrace = "GNB.Infrastructure.Services.RateCacheService.GetCacheRates"
            });
        }
    }
}
