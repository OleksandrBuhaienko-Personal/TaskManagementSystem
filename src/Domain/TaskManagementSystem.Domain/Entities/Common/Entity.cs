using System;
using System.Collections.Generic;
using TaskManagementSystem.Domain.Interfaces;

namespace TaskManagementSystem.Domain.Entities.Common
{
  public abstract class Entity : IAggregateRoot
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
  }
}