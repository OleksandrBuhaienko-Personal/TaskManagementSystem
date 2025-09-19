using System;
using TaskManagementSystem.Domain.Constants;

namespace TaskManagementSystem.Domain.Dto.Tasks
{
  public class CreateTaskRequest
  {
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;

    public DateTime DueDateTime { get; set; }
  }
}
