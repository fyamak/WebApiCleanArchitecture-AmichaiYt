using Application.Authentication.Commands.Register;
using Application.Authentication.Query.Login;
using AutoMapper;
using Contracts.Authentication;

namespace Api.Common.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<LoginRequest, LoginQuery>();
    }
}