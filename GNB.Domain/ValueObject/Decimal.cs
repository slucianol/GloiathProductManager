using System;
using System.Collections.Generic;
using System.Text;

namespace GNB.Domain.ValueObject {
    public class Decimal {
        public int Value { get; set; }
        public int Exponent { get; set; }
        public bool Sign { get; set; }
        public static Decimal operator *(Decimal number1, Decimal number2) {
            return new Decimal {
                Value = number1.Value * number2.Value,
                Exponent = number1.Exponent + number2.Exponent,
                Sign = number1.Sign == true ? (number2.Sign == true ? false : true) : true
            };
        }
    }
}
