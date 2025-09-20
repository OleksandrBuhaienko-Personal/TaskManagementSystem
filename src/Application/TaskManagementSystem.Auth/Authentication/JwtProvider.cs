using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManagementSystem.Auth.Abstractions;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces.Services.Identity;

namespace TaskManagementSystem.Auth.Authentication;

internal sealed class JwtProvider : IJwtProvider
{
  private readonly JwtOptions _options;
  private readonly IRoleService _roleService;

  public JwtProvider(IOptions<JwtOptions> options, IRoleService roleService)
  {
    _roleService = roleService;
    _options = options.Value;
  }

  public async Task<string> Generate(User user)
  {
    string secretKey = _options.SecretKey;
    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
    
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new List<Claim>
    {
      new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
      new(JwtRegisteredClaimNames.Email, user.Email),
      new Claim(ClaimTypes.Role, "Admin")
    };

    // var roles = await _roleService.GetUserRoles(user.Id);
    //
    // claims.AddRange(roles.Value.Select(role => new Claim(ClaimTypes.Role, role)));
    var tokenDescriptor = new SecurityTokenDescriptor()
    {
      Subject = new ClaimsIdentity(claims),
      NotBefore = DateTime.UtcNow,
      Expires = DateTime.UtcNow.AddHours(_options.Expires),
      SigningCredentials = credentials,
      Issuer = _options.Issuer,
      Audience = _options.Audience
    };
    
    var handler = new JwtSecurityTokenHandler();
    
    string token = handler.WriteToken(handler.CreateToken(tokenDescriptor));
    return token;
  }
}
