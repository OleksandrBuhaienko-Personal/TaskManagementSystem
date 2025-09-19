using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskManagementSystem.Auth.Abstractions;
using TaskManagementSystem.Auth.Authentication;
using TaskManagementSystem.Auth.OptionsSetup;
using TaskManagementSystem.Auth.Services;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces.Services.Auth;
using TaskManagementSystem.Domain.Interfaces.Services.Identity;

namespace TaskManagementSystem.Auth;

public static class AuthModule
{
  public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddAuthorization();
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(o =>
      {
        o.RequireHttpsMetadata = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ?? string.Empty)),
          ValidateIssuer = configuration["Jwt::Issuer"] != null,
          ValidAudience = configuration["Jwt::Audience"] ?? string.Empty,
          ClockSkew = TimeSpan.Zero,
        };
      });

    services.AddAuthorization();

    // Register services
    services.AddScoped<ILoginService, LoginService>();
    services.AddScoped<IRegisterService, RegisterService>();
    services.AddScoped<IJwtProvider, JwtProvider>();
    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped(typeof(PasswordHasher<User>));
    services.AddScoped<IPasswordService, PasswordService>();
  }

}
