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
    _options = options.Value;
    _roleService = roleService;
  }

  public async Task<string> Generate(User user)
  {
    //   var jwtToken = JwtBearer.CreateToken(o =>
    //   {
    //     o.SigningKey = _options.SecretKey;
    //     o.ExpireAt = DateTime.UtcNow.AddDays(1);
    //     o.User.Roles.Add("Admin");
    //     o.User.Claims.Add(("Email", user.Email));
    //     o.User["UserId"] = "001"; //indexer-based claim setting
    //   });
    //
    // return jwtToken;

    var claims = new List<Claim>
    {
      new(JwtRegisteredClaimNames.Sub, user.Id.ToString()), new(JwtRegisteredClaimNames.Email, user.Email)
    };

    var result = await _roleService.GetUserRoles(user.Id);
    if (result.IsSuccess)
    {
      var roles = result.Value;
      claims.AddRange(roles.Select(role => new Claim("Roles", role)));
    }


    var signingCredentials = new SigningCredentials(
      new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_options.SecretKey)),
      SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
      _options.Issuer,
      _options.Audience,
      claims,
      null,
      DateTime.UtcNow.AddHours(_options.Expires),
      signingCredentials);


    string tokenValue = new JwtSecurityTokenHandler()
      .WriteToken(token);

    return tokenValue;
  }
}
