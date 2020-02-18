using GNB.Domain.Entities;
using System.Linq;

namespace GNB.Core.Interfaces {
    public interface ITransactionService {
        IQueryable<Transaction> GetTransactions(string sku = "");
    }
}
