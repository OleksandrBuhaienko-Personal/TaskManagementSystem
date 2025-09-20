namespace TaskManagementSystem.Application.Dto.Users;

public record UpdateUserRequest(uint Age, string FirstName, string LastName, string Email);
