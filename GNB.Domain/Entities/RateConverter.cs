﻿namespace GNB.Domain.Entities {
    public class RateConverter {
        public string From { get; set; }
        public string To { get; set; }
        public decimal Rate { get; set; }
        public GNB.Domain.ValueObject.Decimal RateNoFloatingPoint { get; set; }
    }
}
