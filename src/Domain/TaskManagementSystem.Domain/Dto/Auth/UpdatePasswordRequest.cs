using System;

namespace TaskManagementSystem.Domain.Dto.Auth
{
  public class UpdatePasswordRequest
  {
    public Guid UserId { get; set; }
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
  }
}
