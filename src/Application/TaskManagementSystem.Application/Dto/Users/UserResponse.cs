namespace TaskManagementSystem.Application.Dto.Users;

public record UserResponse(Guid Id, uint Age, string FirstName, string LastName, string Email);
