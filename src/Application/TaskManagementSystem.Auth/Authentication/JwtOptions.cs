using TaskManagementSystem.Domain.Constants;

namespace TaskManagementSystem.Auth.Authentication;

public class JwtOptions
{
  public string Issuer { get; set; }
  public string Audience { get; set; }
  public string SecretKey { get; set; }
  public int Expires { get; set; } = AuthConstants.JwtExpiration;
}
