namespace GNB.Domain.Entities {
    public class Transaction {
        public string Sku { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}