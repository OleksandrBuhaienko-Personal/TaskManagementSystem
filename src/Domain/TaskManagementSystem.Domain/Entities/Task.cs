using System;
using TaskManagementSystem.Domain.Entities.Common;
using TaskManagementSystem.Domain.Interfaces;

namespace TaskManagementSystem.Domain.Entities
{
  public class Task : Entity, IAggregateRoot
  {
    private string _title = string.Empty;

    public string Title
    {
      get => _title;
      set => _title = value ?? throw new ArgumentNullException(nameof(value), "Title cannot be empty.");
    }
    
    public string Description { get; set; }

    public DateTime DueDateTime { get; set; }

    public Guid? UserId { get; set; }
    //Navigational property
    public User? User { get; set; }
    
    public Task(string title, string description, DateTime dueDateTime)
    {
      Title = title;
      Description = description;
      DueDateTime = dueDateTime;
    }
  }
}
