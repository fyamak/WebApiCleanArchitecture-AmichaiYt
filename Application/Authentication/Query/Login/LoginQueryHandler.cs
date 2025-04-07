using Application.Authentication.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Persistence;
using Application.Shared.Models;
using Domain.Entities;
using MediatR;

namespace Application.Authentication.Query.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, DataResult<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<DataResult<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(request.Email) is not User user)
        {
            return DataResult<AuthenticationResult>.Invalid("User credentials are not correct (email)");
        }

        if (user.Password != request.Password)
        {
            return DataResult<AuthenticationResult>.Invalid("User credentials are not correct (password)");
        }

        var token = _jwtTokenGenerator.GenerateToken(user);
        return DataResult<AuthenticationResult>.Success(new AuthenticationResult(user, token), "Login successfull");
    }
}
