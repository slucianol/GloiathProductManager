using GNB.Core.Exceptions;
using GNB.Core.Interfaces;
using GNB.Domain.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace GNB.Infrastructure.Services {
    public class RateCacheService : IRateCacheService {
        private readonly string cacheStoreFilePath;
        private readonly ILoggerService loggerService;
        public RateCacheService(string cacheStoreFilePath, ILoggerService loggerService) {
            this.cacheStoreFilePath = cacheStoreFilePath;
            this.loggerService = loggerService;
        }
        public bool ExistsRatesCache() {
            return File.Exists(cacheStoreFilePath);
        }

        public IQueryable<RateConverter> GetCacheRates() {
            if (ExistsRatesCache()) {
                string jsonString = File.ReadAllText(cacheStoreFilePath);
                loggerService.Log(new Log {
                    LogType = Domain.ValueObject.LogType.Information,
                    Message = $"Fueron cargados los Rates del cache de almacenamiento {cacheStoreFilePath}.",
                    StackTrace = "GNB.Infrastructure.Services.RateCacheService.GetCacheRates"
                });
                return JsonConvert.DeserializeObject<List<RateConverter>>(jsonString).AsQueryable();
            }
            loggerService.Log(new Log {
                LogType = Domain.ValueObject.LogType.Error,
                Message = $"El cache de almacenamiento {cacheStoreFilePath} no fue encontrado.",
                StackTrace = "GNB.Infrastructure.Services.RateCacheService.GetCacheRates"
            });
            throw new CacheNotFoundException($"El almacenamiento para el caché no fue encontrado en la ruta: {cacheStoreFilePath}");
        }

        public void PersistRatesInCache(List<RateConverter> rates) {
            string jsonString = JsonConvert.SerializeObject(rates);
            File.WriteAllText(cacheStoreFilePath, jsonString);
            loggerService.Log(new Log {
                LogType = Domain.ValueObject.LogType.Information,
                Message = $"Fueron persistidos los Rates en el almacenamiento de caché {cacheStoreFilePath}.",
                StackTrace = "GNB.Infrastructure.Services.RateCacheService.GetCacheRates"
            });
        }
    }
}
