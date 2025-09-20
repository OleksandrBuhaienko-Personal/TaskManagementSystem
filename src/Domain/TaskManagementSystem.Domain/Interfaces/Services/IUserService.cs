using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Interfaces.Services
{
  public interface IUserService
  {
    public Task<Result<IEnumerable<User>>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Result<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<Result<User>> CreateAsync(User entity, CancellationToken cancellationToken = default);
    public Task<Result<User>> UpdateAsync(User entity, CancellationToken cancellationToken = default);
    public Task<Result<User>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
  }
}
