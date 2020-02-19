using Microsoft.VisualStudio.TestTools.UnitTesting;
using GNB.Core.Interfaces;
using GNB.Infrastructure.Services;

namespace GNB.ProductManager.Tests {
    [TestClass]
    public class RateConverterServiceTests {
        private readonly IRateConverterService rateConverterService;
        public RateConverterServiceTests() {
            rateConverterService = new RateConverterService(new RatesService(new RateServiceUri("https://localhost:44318/rates.json"), new RateCacheService(new RateCacheServiceUri("PruebaRateCache.json"), new LoggerService("PruebaLogs.json")), new LoggerService("PruebaLogs.json")));
        }
        [TestMethod]
        public void Convert_CAD_To_EUR() {
            decimal amount = rateConverterService.ConvertAmount("CAD", "EUR", 1);
            Assert.AreEqual(1.4M, amount);
        }
        [TestMethod]
        public void Convert_EUR_To_CAD() {
            decimal amount = rateConverterService.ConvertAmount("EUR", "CAD", 1);
            Assert.AreEqual(0.71M, amount);
        }
        [TestMethod]
        public void Convert_CAD_To_AUD() {
            decimal amount = rateConverterService.ConvertAmount("CAD", "AUD", 1);
            Assert.AreEqual(0.6M, amount);
        }
        [TestMethod]
        public void Convert_AUD_To_EUR() {
            decimal amount = rateConverterService.ConvertAmount("AUD", "EUR", 1);
            Assert.AreEqual(2.338M, amount);
        }
    }
}
