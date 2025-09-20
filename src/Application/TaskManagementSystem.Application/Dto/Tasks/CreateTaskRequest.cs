namespace TaskManagementSystem.Application.Dto.Tasks;

public record CreateTaskRequest(Guid UserId, string Title, string Description, DateTime DueDateTime);
