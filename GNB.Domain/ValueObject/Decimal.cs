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
        public override string ToString() {
            string decimalValue = (Value * (Sign ? Math.Pow(10, (-1 * Exponent)) : Math.Pow(10, Exponent))).ToString();
            if (decimalValue.Contains(".")) {
                string decimalPart = decimalValue.Substring(decimalValue.LastIndexOf(".") + 1);
                if (decimalPart.Length >= 3) {
                    if (int.Parse(decimalPart[2].ToString()) > 5) {
                        return $"{decimalValue.Substring(0, decimalValue.LastIndexOf("."))}.{decimalPart[0]}{int.Parse(decimalPart[1].ToString()) + 1}";
                    }
                    return $"{decimalValue.Substring(0, decimalValue.LastIndexOf("."))}.{decimalPart[0]}{decimalPart[1]}";
                } else if (decimalPart.Length >= 2) {
                    return decimalValue;
                } else {
                    return $"{decimalValue}0";
                }
            } else {
                return $"{decimalValue}.00";
            }
        }
    }
}
