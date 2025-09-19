namespace TaskManagementSystem.Domain.Dto.Auth
{
  public class CreateUserRequest
  {
    public uint Age{ get; set; }
    public string FirstName {get; set; } = string.Empty;
    public string LastName {get; set; } = string.Empty;
    public string Email {get; set; } = string.Empty;
    public string Password {get; set; } = string.Empty;
  }
}
