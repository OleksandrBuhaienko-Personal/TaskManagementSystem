namespace TaskManagementSystem.Application.Dto.Auth;

public record UpdatePasswordRequest(Guid UserId, string CurrentPassword, string NewPassword);
