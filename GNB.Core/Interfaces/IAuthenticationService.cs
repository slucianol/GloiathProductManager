using GNB.Domain.Entities;

namespace GNB.Core.Interfaces {
    public interface IAuthenticationService {
        bool AuthenticateUser(Login login);
    }
}
