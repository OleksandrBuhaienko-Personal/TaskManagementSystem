using System;

namespace TaskManagementSystem.Domain.Dto.Tasks
{
  public class UpdateTaskRequest
  {
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public DateTime DueDateTime { get; set; }
  }
}
