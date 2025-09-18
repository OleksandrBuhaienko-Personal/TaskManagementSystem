using TaskManagementSystem.Domain.Constants;

namespace TaskManagementSystem.Auth.Authentication;

public class JwtOptions
{
  public string Issuer { get; init; }
  public string Audience { get; init; }
  public string SecretKey { get; init; }
  public int Expires { get; init; } = AuthConstants.JwtExpiration;
}
