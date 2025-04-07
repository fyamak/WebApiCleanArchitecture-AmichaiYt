using Application.Authentication.Common;
using Application.Shared.Models;
using MediatR;

namespace Application.Authentication.Commands.Register;

// same as RegisterRequest
public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword) : IRequest<DataResult<AuthenticationResult>>;