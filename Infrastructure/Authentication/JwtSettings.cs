using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string? SecretKey { get; init; }
    public int ExpiryMinutes { get; init; }
    public string? Issuer { get; init; }
    public string? Audience { get; init; }
}
