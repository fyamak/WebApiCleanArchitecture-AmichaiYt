using Application.Common.Interfaces.Authentication;
using Application.Common.Persistence;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        // validate the email is exists
        if(_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User credentials are not correct (email)");
        }

        // check if the password is correct
        if(user.Password != password)
        {
            throw new Exception("User credentials are not correct (password)");
        }
        
        // create jwt token 
        var token = _jwtTokenGenerator.GenerateToken(user);


        return new AuthenticationResult(user, token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password, string ConfirmPassword)
    {
        // check if the email(user) is already registered
        if (_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User with this email already exists");
        }

        // create a new user with unique id
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);


        // create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);

    }
}