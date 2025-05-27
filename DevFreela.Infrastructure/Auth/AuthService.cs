using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    public AuthService(IConfiguration configuration)
    {
        
    }
    
    public string ComputeHash(string password)
    {
        using (var hash = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            
            var hashBytes = hash.ComputeHash(bytes);
            
            var builder = new StringBuilder();
            foreach (var b in hashBytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public string GenerateToken(string email, string role)
    {
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("username", email),
            new Claim(ClaimTypes.Role, role),
        };

        var token = new JwtSecurityToken(issuer, audience, claims, null, DateTime.Now.AddHours(2));
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}