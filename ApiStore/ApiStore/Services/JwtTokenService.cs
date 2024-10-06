using ApiStore.Data.Entities.Identity;
using ApiStore.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiStore.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(UserEntity user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
        var securityKey = new SymmetricSecurityKey(key);

        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{user.LastName} {user.FirstName}"),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
            new Claim(ClaimTypes.Role, user.UserRoles.FirstOrDefault()?.Role.Name ?? "User")
        };

        var jwt = new JwtSecurityToken(
            signingCredentials: creds,
            claims: claims,
            expires: DateTime.Now.AddDays(20)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

}