using System;

namespace TaskManagementSystem.Domain.Dto.Users
{
  public class UserResponse
  {
    public Guid Id { get; set; }
    public uint Age{ get; set; }
    public string FirstName {get; set; } = string.Empty;
    public string LastName {get; set; } = string.Empty;
    public string Email {get; set; } = string.Empty;
  }
}
