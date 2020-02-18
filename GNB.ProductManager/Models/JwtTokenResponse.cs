using System;

namespace GNB.ProductManager.Models {
    public class JwtTokenResponse {
        public DateTime ValidTo { get; set; }
        public string Value { get; set; }
    }
}
