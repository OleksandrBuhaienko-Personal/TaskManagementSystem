using System;
using System.Collections.Generic;
using TaskManagementSystem.Domain.Entities.Common;

namespace TaskManagementSystem.Domain.Entities.Auth
{
  public class Role : Entity
  {
    private readonly List<UserRole> _userRoles = new List<UserRole>();
    public string Name { get; private set; } = string.Empty;

    // Navigation property for the many-to-many relationship
    public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();

    public Role() { }

    public Role(string name) : base()
    {
      UpdateName(name);
    }

    private void UpdateName(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
      {
        throw new ArgumentNullException(nameof(name), "Role name cannot be null or empty.");
      }
      Name = name;
    }
  }
}
