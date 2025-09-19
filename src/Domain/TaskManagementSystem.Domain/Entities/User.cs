using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TaskManagementSystem.Domain.Constants;
using TaskManagementSystem.Domain.Entities.Auth;
using TaskManagementSystem.Domain.Entities.Common;
using TaskManagementSystem.Domain.Interfaces;

namespace TaskManagementSystem.Domain.Entities
{
  public class User : Entity
  {
    private uint _age;
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;
    private string _email = string.Empty;
    private readonly List<Task> _tasks = new List<Task>();
    private readonly List<UserRole> _userRoles = new List<UserRole>();
    
    public uint Age
    {
      get => _age;
      private set
      {
        if (value < UserDomainConstants.MinAge || value > UserDomainConstants.MaxAge)
        {
          throw new ArgumentOutOfRangeException(nameof(Age), "Age must be between 18 and 120 years old.");
        }

        _age = value;
      }
    }

    public string FirstName
    {
      get => _firstName;
      set => _firstName = value ?? throw new ArgumentNullException(nameof(value), "First name cannot be empty.");
    }

    public string LastName
    {
      get => _lastName;
      set => _lastName = value ?? throw new ArgumentNullException(nameof(value), "Last name cannot be empty.");
    }

    public string Email
    {
      get => _email;
      set => _email = value ?? throw new ArgumentNullException(nameof(value), "Email cannot be empty");
    }

    public string PasswordHash { get; set; }
    public User(uint age, string firstName, string lastName, string email)
    {
      Age = age;
      FirstName = firstName;
      LastName = lastName;
      Email = email;
    }
    
    public IReadOnlyList<Task> Tasks => _tasks.AsReadOnly();
    public IReadOnlyList<UserRole> UserRoles => _userRoles.AsReadOnly();
  }
}
