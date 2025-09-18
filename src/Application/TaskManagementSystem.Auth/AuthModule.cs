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
    var jwtOptions = new JwtOptions();
    var jwtOptionsSetup = new JwtOptionsSetup(configuration);
    jwtOptionsSetup.Configure(jwtOptions);

    services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,

          ValidIssuer = jwtOptions.Issuer,
          ValidAudience = jwtOptions.Audience,
          IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
        };
      });

    services.AddAuthorization();

    services.AddScoped<ILoginService, LoginService>();
    services.AddScoped<IRegisterService, RegisterService>();
    services.AddScoped<IJwtProvider, JwtProvider>();
    services.AddScoped<IRoleService, RoleService>();
    services.AddScoped(typeof(PasswordHasher<User>));
    services.AddScoped<IPasswordService, PasswordService>();

    services.ConfigureOptions<JwtOptionsSetup>();
    services.ConfigureOptions<JwtBearerOptionsSetup>();
  }
}
