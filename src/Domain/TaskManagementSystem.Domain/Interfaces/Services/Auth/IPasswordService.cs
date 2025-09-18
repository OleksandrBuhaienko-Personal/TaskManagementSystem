using System;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementSystem.Domain.Interfaces.Services.Auth
{
  public interface IPasswordService
  {
    string HashPassword(string password);
    Task<bool> VerifyPasswordAsync(Guid userId, string password);
    Task UpdatePasswordAsync(Guid userId, string password);
    Task<bool> VerifyPasswordAsync(User user, string password);

  }
}
