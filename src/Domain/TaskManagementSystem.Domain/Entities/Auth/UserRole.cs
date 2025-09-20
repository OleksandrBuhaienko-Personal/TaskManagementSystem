using System;
using TaskManagementSystem.Domain.Entities.Common;

namespace TaskManagementSystem.Domain.Entities.Auth
{
  public class UserRole : Entity
  {
    public UserRole() {}
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public Guid RoleId { get; set; }
    public virtual Role Role { get; set; }
  }
}
