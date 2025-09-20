using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TaskManagementSystem.Auth.Authentication;

namespace TaskManagementSystem.Auth.OptionsSetup;

public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
  private const string SectionName = "Jwt";
  private readonly IConfiguration _configuration;

  public JwtOptionsSetup(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public void Configure(JwtOptions options)
  {
    _configuration.GetSection(SectionName).Bind(options);

    // Fallback for commonly used key name "Secret" -> map to SecretKey if not provided.
    if (string.IsNullOrWhiteSpace(options.SecretKey))
    {
      var altSecret = _configuration.GetSection(SectionName)["SecretKey"];
      if (!string.IsNullOrWhiteSpace(altSecret))
      {
        options.SecretKey = altSecret!;
      }
    }

    // Support "ExpirationMinutes" in configuration by converting it to hours for the existing Expires option.
    var expMinutes = _configuration.GetSection(SectionName)["Expires"];
    if (!string.IsNullOrWhiteSpace(expMinutes) && int.TryParse(expMinutes, out var minutes) && minutes > 0)
    {
      var hours = (int)Math.Ceiling(minutes / 60.0);
      options.Expires = Math.Max(1, hours); // at least 1 hour if minutes were provided
    }

    // Fallback for commonly used key name "Secret" -> map to SecretKey if not provided.
    if (string.IsNullOrWhiteSpace(options.SecretKey))
    {
      var altSecret = _configuration.GetSection(SectionName)["Secret"];
      if (!string.IsNullOrWhiteSpace(altSecret))
      {
        options.SecretKey = altSecret!;
      }
    }
  }
}
