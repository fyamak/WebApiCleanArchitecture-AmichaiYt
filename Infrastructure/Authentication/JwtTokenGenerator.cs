using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens; 

namespace Infrastructure.Authentication;


public class JwtTokengenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtSettings;

    public JwtTokengenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtOptions.Value;
    }

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        // Create signing credentials using a symmetric security key
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)), // Secret key for encryption
            SecurityAlgorithms.HmacSha256 // Hashing algorithm for signing
        );

        // Define the claims (user identity data included in the token)
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()), // Unique identifier for the user
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique token identifier
        };

        // Create the JWT security token
        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer, // The server issuing the token (who created the token)
            audience: _jwtSettings.Audience, // Who the token is intended for (client)
            claims: claims, // User claims (identity data)
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: signingCredentials // The security credentials (used to sign the token)
        );

        // Convert the token into a string and return it
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
