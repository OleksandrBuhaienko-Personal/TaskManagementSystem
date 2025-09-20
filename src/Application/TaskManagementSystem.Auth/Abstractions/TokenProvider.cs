using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TaskManagementSystem.Auth.Authentication;
using TaskManagementSystem.Auth.OptionsSetup;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces.Services.Identity;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace TaskManagementSystem.Auth.Abstractions;

public sealed class TokenProvider (IConfiguration configuration, IRoleService roleService, JwtOptionsSetup jwtOptionsSetup)
{
  public async Task<string> Create(User user)
  {
    var jwtOptions = new JwtOptions();
    jwtOptionsSetup.Configure(jwtOptions);
    
    string secretKey = jwtOptions.SecretKey;
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>
    {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new Claim(JwtRegisteredClaimNames.Email, user.Email)
    };
    var roles =  await roleService.GetUserRoles(user.Id);
    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList());
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims),
      Expires = DateTime.UtcNow.AddHours(jwtOptions.Expires),
      SigningCredentials = credentials,
      Issuer = jwtOptions.Issuer,
      Audience = jwtOptions.Audience
    };
    var handler = new JsonWebTokenHandler();
    string token = handler.CreateToken(tokenDescriptor);
    
    return token;
  }
}
