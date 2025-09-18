using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities.Common;

namespace TaskManagementSystem.Domain.Interfaces.Services.Base
{
  public interface IBaseEntityService<TEntity> where TEntity : Entity, IAggregateRoot
  {
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(Guid id);
  }
}