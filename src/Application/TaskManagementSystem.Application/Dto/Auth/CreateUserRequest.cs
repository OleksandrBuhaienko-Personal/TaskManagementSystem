namespace TaskManagementSystem.Application.Dto.Auth;

public record CreateUserRequest(
  uint Age,
  string FirstName,
  string LastName,
  string Email,
  string Password);
