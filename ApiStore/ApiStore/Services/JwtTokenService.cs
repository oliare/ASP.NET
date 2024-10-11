using ApiStore.Data.Entities.Identity;
using ApiStore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiStore.Services;

public class JwtTokenService(IConfiguration _configuration,
        UserManager<UserEntity> userManager) : IJwtTokenService
{
    public async Task<string> GenerateTokenAsync(UserEntity user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
        var securityKey = new SymmetricSecurityKey(key);

        var claims = new List<Claim>
        {
             new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"),
             new Claim("firstName", user.FirstName ?? string.Empty),
             new Claim("lastName", user.LastName ?? string.Empty),
             new Claim("email", user.Email ?? string.Empty),
             new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
        };

        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
            claims.Add(new Claim("roles", role));

        var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            signingCredentials: creds,
            claims: claims,
            expires: DateTime.Now.AddDays(20)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

}