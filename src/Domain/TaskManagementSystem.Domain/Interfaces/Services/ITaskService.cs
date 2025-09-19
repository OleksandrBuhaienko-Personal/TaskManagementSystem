using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using Task = TaskManagementSystem.Domain.Entities.Task;

namespace TaskManagementSystem.Domain.Interfaces.Services
{
  public interface ITaskService
  {
    public Task<Result<IEnumerable<Task>>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Result<Task>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<Result<Task>> CreateAsync(Task entity, CancellationToken cancellationToken = default);
    public Task<Result<Task>> UpdateAsync(Task entity, CancellationToken cancellationToken = default);
    public Task<Result<Task>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
  }
}
