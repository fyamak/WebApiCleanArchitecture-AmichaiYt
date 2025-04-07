using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Application.Authentication.Query.Login;
using Application.Shared.Models;
using Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers;


[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IMediator _mediator;

    public AuthenticationController(ILogger<AuthenticationController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }


    [HttpPost("register")]
    public async Task<DataResult<AuthenticationResult>> Register(RegisterRequest registerRequest)
    {
        var command = new RegisterCommand(registerRequest.FirstName,
                                          registerRequest.LastName,
                                          registerRequest.Email,
                                          registerRequest.Password,
                                          registerRequest.ConfirmPassword);
        return await _mediator.Send(command);
    }


    [HttpPost("login")]
    public async Task<DataResult<AuthenticationResult>> Login(LoginRequest loginRequest)
    {
        var query = new LoginQuery(loginRequest.Email, loginRequest.Password);
        return await _mediator.Send(query);
    }


}
