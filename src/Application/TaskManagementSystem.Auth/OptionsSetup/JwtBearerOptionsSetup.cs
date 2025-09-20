using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManagementSystem.Auth.Authentication;
using TaskManagementSystem.Domain.Entities.Auth;

namespace TaskManagementSystem.Auth.OptionsSetup;

public class JwtBearerOptionsSetup : IConfigureOptions<JwtBearerOptions>
{
  private readonly JwtOptions _jwtOptions;

  public JwtBearerOptionsSetup(IOptions<JwtOptions> jwtOptions)
  {
    _jwtOptions = jwtOptions.Value;
  }

  public void Configure(JwtBearerOptions options)
  {
    // Don't require HTTPS metadata for local development without an authority
    options.RequireHttpsMetadata = false;

    var hasIssuer = !string.IsNullOrWhiteSpace(_jwtOptions.Issuer);
    var hasAudience = !string.IsNullOrWhiteSpace(_jwtOptions.Audience);

    options.TokenValidationParameters = new()
    {
      // ValidateAudience = hasAudience,
      ValidIssuer = _jwtOptions.Issuer,
      ValidateIssuer = hasIssuer,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      
      // ValidAudience = _jwtOptions.Audience,
      // Use the same symmetric key used to sign tokens
      IssuerSigningKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),

      // Small skew to avoid edge cases with clock differences
      ClockSkew = TimeSpan.FromMinutes(1)
    };
  }
}
