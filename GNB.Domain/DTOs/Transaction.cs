using System;
using System.Collections.Generic;
using System.Text;

namespace GNB.Domain.DTOs {
    public class Transaction {
        public string Sku { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }
    }
}
