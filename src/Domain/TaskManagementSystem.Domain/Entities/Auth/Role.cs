using System;
using System.Collections.Generic;
using TaskManagementSystem.Domain.Entities.Common;

namespace TaskManagementSystem.Domain.Entities.Auth
{
  public class Role : Entity
  {
    public string Name { get; private set; } = string.Empty;

    // Navigation property for the many-to-many relationship
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    public Role() { }

    public Role(string name) : base()
    {
      UpdateName(name);
    }

    public void UpdateName(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name), "Role name cannot be null or empty.");
      }
      Name = name;
    }
  }
}
