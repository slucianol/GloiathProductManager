using GNB.Core.Interfaces;
using GNB.Domain.Entities;

namespace GNB.Infrastructure.Services {
    public class AuthenticationService : IAuthenticationService {
        public bool AuthenticateUser(Login login) {
            return (login.Username == "prueba" && login.Password == "123");
        }
    }
}
