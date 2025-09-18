using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using TaskManagementSystem.Domain.Entities.Common;

namespace TaskManagementSystem.Domain.Interfaces.Services.Base
{
  public interface IBaseEntityService<TEntity> where TEntity : Entity, IAggregateRoot
  {
    Task<Result<IEnumerable<TEntity>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<TEntity>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<TEntity>> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<Result<TEntity>> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    Task<Result<TEntity>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
  }
}
