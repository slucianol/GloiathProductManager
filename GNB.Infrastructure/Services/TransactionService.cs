using GNB.Core.Interfaces;
using GNB.Domain.Entities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace GNB.Infrastructure.Services {
    public class TransactionService : JsonService, ITransactionService {
        private readonly IRateConverterService rateConverterService;
        private List<Transaction> transactions;
        public TransactionService(ITransactionServiceUri transactionServiceUri, IRateConverterService rateConverterService) : base(transactionServiceUri) {
            this.rateConverterService = rateConverterService;
        }
        public IQueryable<Transaction> GetTransactions(string sku = "") {
            string jsonString = GetHttpJsonContent();
            if (jsonString != string.Empty) {
                transactions = JsonConvert.DeserializeObject<List<Transaction>>(jsonString);
                if (sku != "" && sku != string.Empty) {
                    return transactions.Where(t => t.Sku == sku).Select(t => new Transaction {
                        Currency = "EUR",
                        Amount = t.Currency != "EUR" ? rateConverterService.ConvertAmount(t.Currency, "EUR", t.Amount) : t.Amount,
                        Sku = t.Sku
                    }).AsQueryable();
                }
                return transactions.Select(t => new Transaction {
                    Currency = "EUR",
                    Amount = t.Currency != "EUR" ? rateConverterService.ConvertAmount(t.Currency, "EUR", t.Amount) : t.Amount,
                    Sku = t.Sku
                }).AsQueryable(); ;
            }
            //TODO: RateService - Add a better way of reporting the error to up layer
            return null;
        }
    }
}
