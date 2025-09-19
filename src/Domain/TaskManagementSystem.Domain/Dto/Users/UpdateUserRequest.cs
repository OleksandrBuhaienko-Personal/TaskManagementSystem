namespace TaskManagementSystem.Domain.Dto.Users
{
  public class UpdateUserRequest
  {
    public uint Age { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
  };

}
