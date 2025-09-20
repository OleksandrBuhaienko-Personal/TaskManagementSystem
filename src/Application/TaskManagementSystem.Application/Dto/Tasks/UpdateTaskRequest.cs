namespace TaskManagementSystem.Application.Dto.Tasks;

public record UpdateTaskRequest(Guid UserId, string Title, string Description, DateTime DueDateTime);
