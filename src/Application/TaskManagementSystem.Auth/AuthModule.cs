using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)),
          ValidIssuer = configuration["Jwt:Issuer"],
          ValidAudience = configuration["Jwt:Audience"],
          ClockSkew = TimeSpan.Zero
        };
      });

    // Register services
    services.AddScoped<JwtOptionsSetup>();
    services.AddScoped(typeof(PasswordHasher<User>));
    services.AddScoped<ILoginService, LoginService>();
    services.AddScoped<IRegisterService, RegisterService>();
    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped<IPasswordService, PasswordService>();
    services.AddScoped<TokenProvider>();
  }

}
