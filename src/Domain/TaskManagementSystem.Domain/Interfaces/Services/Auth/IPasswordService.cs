using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using TaskManagementSystem.Domain.Entities;

using Task = System.Threading.Tasks.Task;

namespace TaskManagementSystem.Domain.Interfaces.Services.Auth
{
  public interface IPasswordService
  {
    string HashPassword(string password);
    Task<Result> VerifyPasswordAsync(Guid userId, string password, CancellationToken ct = default);
    Task<Result> UpdatePasswordAsync(Guid userId, string password);
    Result VerifyPassword(User user, string password);

  }
}
