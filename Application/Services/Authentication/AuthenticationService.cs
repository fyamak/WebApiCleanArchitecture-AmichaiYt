using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(new Guid(), "FirstName", "LastName", email, "token", DateTime.UtcNow);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password, string ConfirmPassword)
    {
        return new AuthenticationResult(new Guid(), firstName, lastName, email, "token", DateTime.UtcNow);

    }
}