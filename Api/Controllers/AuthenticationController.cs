using Application.Services.Authentication;
using Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthenticationService _authenticationService;
    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }


    [HttpPost("register")]
    public IActionResult Register(RegisterRequest registerRequest)
    {
        var authResult = _authenticationService.Register(
            registerRequest.FirstName,
            registerRequest.LastName,
            registerRequest.Email,
            registerRequest.Password,
            registerRequest.ConfirmPassword
        );

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.User.CreatedAt,
            authResult.Token
        );
        return Ok(response);
    }


    [HttpPost("login")]
    public IActionResult Login(LoginRequest loginRequest)
    {
        var authResult = _authenticationService.Login(
            loginRequest.Email,
            loginRequest.Password);

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.User.CreatedAt,
            authResult.Token
        );

        return Ok(response);
    }


}
