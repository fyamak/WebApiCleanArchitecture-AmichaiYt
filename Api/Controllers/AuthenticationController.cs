using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Application.Authentication.Query.Login;
using Application.Shared.Models;
using AutoMapper;
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
    private readonly IMapper _mapper;

    public AuthenticationController(ILogger<AuthenticationController> logger, IMediator mediator, IMapper mapper)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
    }


    [HttpPost("register")]
    public async Task<DataResult<AuthenticationResult>> Register(RegisterRequest registerRequest)
    {
        var command = _mapper.Map<RegisterCommand>(registerRequest);
        return await _mediator.Send(command);
    }


    [HttpPost("login")]
    public async Task<DataResult<AuthenticationResult>> Login(LoginRequest loginRequest)
    {
        var query = _mapper.Map<LoginQuery>(loginRequest);
        return await _mediator.Send(query);
    }


}
