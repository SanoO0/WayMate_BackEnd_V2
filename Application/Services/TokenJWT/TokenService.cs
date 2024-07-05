using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Services.TokenJWT.dto;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.TokenJWT;

public class TokenService
{
    private const double EXPIRY_DURATION_MINUTES = 60;

    public string BuildToken(string key, string issuer, DtoInputToken token) {
        var claims = new[] {
            new Claim(ClaimTypes.Name, token.username),
            new Claim(ClaimTypes.Role, token.userType.ToString()),
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
            expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    public bool IsTokenValid(string key, string issuer, string token) {
        var mySecret = Encoding.UTF8.GetBytes(key);
        var mySecurityKey = new SymmetricSecurityKey(mySecret);
        var tokenHandler = new JwtSecurityTokenHandler();
        try {
            tokenHandler.ValidateToken(token,
                new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = mySecurityKey
                }, out var validatedToken);
        }
        catch (SecurityTokenInvalidSignatureException) {
            return false;
        }
        return true;
    }
}