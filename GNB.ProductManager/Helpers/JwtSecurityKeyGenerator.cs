using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GNB.ProductManager.Helpers {
    public class JwtSecurityKeyGenerator {
        public static SecurityKey Get(string secret) {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
