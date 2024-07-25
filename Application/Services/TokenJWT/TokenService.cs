using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.TokenJWT;

public class TokenService
{
    private readonly IConfiguration _configuration;

    // Constructor to initialize configuration dependency.
    public TokenService(IConfiguration configuration) {
        _configuration = configuration;
    }
    
    // Generates a JWT token for a given user ID, role, and session duration.
    public string GenerateJwtToken(string id, string role, bool isSessionOnly) {
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];
        var secretKey = _configuration["JwtSettings:SecretKey"];
        var validityHours = _configuration["JwtSettings:ValidityHours"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Define the claims for the token.
        var claims = new[] {
            new Claim(ClaimTypes.Name, id),
            new Claim(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, "Bearer");

        // Set the token expiration date.
        DateTime expireDate = DateTime.UtcNow.AddHours(Convert.ToDouble(validityHours));
        if (isSessionOnly) expireDate = DateTime.UtcNow.AddYears(1);
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: identity.Claims,
            expires: expireDate,
            signingCredentials: credentials
        );
        // Write and return the generated token.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    // Extracts user ID and role from the provided JWT token.
    public (string Id, string UserType) GetAuthCookieData(string token) {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secretKey = _configuration["JwtSettings:SecretKey"];
        var key = Encoding.ASCII.GetBytes(secretKey!);
        
        tokenHandler.ValidateToken(token, new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = _configuration["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = _configuration["JwtSettings:Audience"],
            ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);
        
        var jwtToken = (JwtSecurityToken)validatedToken;
        
        // Extract claims from the token.
        var id = jwtToken.Claims.First(x => x.Type == ClaimTypes.Name).Value;
        var userType = jwtToken.Claims.First(x => x.Type == ClaimTypes.Role).Value;
        
        // Return user ID and role as a tuple.
        return (id, userType);
    }
}