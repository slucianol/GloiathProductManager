using Microsoft.VisualStudio.TestTools.UnitTesting;
using GNB.Core.Interfaces;
using GNB.Infrastructure.Services;
using GNB.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GNB.ProductManager.Tests {
    [TestClass]
    public class TransactionServiceTests {
        private readonly ITransactionService transactionService;
        public TransactionServiceTests() {
            transactionService = new TransactionService(new TransactionServiceUri("https://localhost:44318/transactions.json"), new RateConverterService(new RatesService(new RateServiceUri("https://localhost:44318/rates.json"), new RateCacheService(new RateCacheServiceUri("PruebaRateCache.json"), new LoggerService("PruebaLogs.json")), new LoggerService("PruebaLogs.json"))));
        }
        [TestMethod]
        public void Get_All_Transactions() {
            List<Transaction> transactions = transactionService.GetTransactions().ToList();
            Assert.IsTrue(transactions.Count > 0);
        }
        [TestMethod]
        public void Get_Transactions_By_Sku_D6745() {
            List<Transaction> transactions = transactionService.GetTransactions("D6745").ToList();
            Assert.IsTrue(transactions.Count > 0);
        }
    }
}
