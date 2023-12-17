using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using PTSL.Ovidhan.Business.TokenHelper;
using PTSL.Ovidhan.Common.Model;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PTSL.Ovidhan.Api.Helpers;

public class GenerateJSONWebToken : IGenerateJSONWebToken
{
    private readonly IConfiguration _config;
    private static readonly object BufferLock = new();
    private static readonly byte[] RefreshTokenBuffer = new byte[64];

    public GenerateJSONWebToken(IConfiguration config)
    {
        _config = config;
    }

    public string GetToken(UserVM? userInfo, bool rememberMe)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new Exception("Jwt key could be loaded from appsettings.json")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Name, userInfo?.UserName ?? string.Empty),
            new Claim(CustomClaimTypes.AuthTime, DateTime.Now.ToString()),
            new Claim(CustomClaimTypes.RememberMe, rememberMe.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(CustomClaimTypes.UserId, userInfo ?.Id ?? string.Empty),
        };

        var token = new JwtSecurityToken
            (
            _config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(31),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        using var rng = RandomNumberGenerator.Create();

        lock (BufferLock)
        {
            rng.GetBytes(RefreshTokenBuffer);
            return Convert.ToBase64String(RefreshTokenBuffer);
        }
    }
}
