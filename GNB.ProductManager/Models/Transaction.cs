﻿using GNB.Domain.ValueObject;

namespace GNB.ProductManager.Models {
    public class Transaction {
        public string Sku { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
    }
}
