using Microsoft.VisualStudio.TestTools.UnitTesting;
using GNB.Core.Interfaces;
using GNB.Infrastructure.Services;
using GNB.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GNB.ProductManager.Tests {
    [TestClass]
    public class RateCacheServiceTests {
        private readonly IRateCacheService rateCacheService;
        public RateCacheServiceTests() {
            rateCacheService = new RateCacheService(new RateCacheServiceUri("PruebaRateCache.json"), new LoggerService("PruebaLogs.json"));
        }
        [TestMethod]
        public void Persist_Rate_Cache() {
            List<RateConverter> rates = new List<RateConverter>() {
                new RateConverter{
                    From = "EUR",
                    Rate = 1.6M,
                    To = "CAD"
                    },
                new RateConverter{ 
                    From = "USD",
                    To = "AUD",
                    Rate = 1.5M
                    }
            };
            rateCacheService.PersistRatesInCache(rates);
            Assert.IsTrue(rateCacheService.ExistsRatesCache());
        }
        [TestMethod]
        public void Get_Rates_From_Cache() {
            List<RateConverter> rates = rateCacheService.GetCacheRates().ToList();
            Assert.AreEqual(2, rates.Count);
        }
    }
}
