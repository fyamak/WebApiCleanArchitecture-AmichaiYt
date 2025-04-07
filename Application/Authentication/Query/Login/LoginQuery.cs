using Application.Authentication.Common;
using Application.Shared.Models;
using MediatR;

namespace Application.Authentication.Query.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<DataResult<AuthenticationResult>>;
