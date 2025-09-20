namespace TaskManagementSystem.Application.Dto.Tasks;

public record TaskResponse(Guid Id, Guid UserId, string Title, string Description, DateTime DueDateTime);
