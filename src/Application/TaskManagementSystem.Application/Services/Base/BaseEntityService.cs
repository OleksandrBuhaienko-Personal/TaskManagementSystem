using TaskManagementSystem.Domain.Entities.Common;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Domain.Interfaces.Services.Base;

namespace TaskManagementSystem.Application.Services.Base;

public class BaseEntityService<T> : IBaseEntityService<T> where T : Entity
{
  private readonly IRepository<T> _repository;
  
  public BaseEntityService(IRepository<T> repository)
  {
    _repository = repository;
  }
  
  public Task<IEnumerable<T>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<T> GetByIdAsync(Guid id)
  {
    throw new NotImplementedException();
  }

  public Task<T> CreateAsync(T entity)
  {
    throw new NotImplementedException();
  }

  public Task<T> UpdateAsync(T entity)
  {
    throw new NotImplementedException();
  }

  public Task<T> DeleteAsync(Guid id)
  {
    throw new NotImplementedException();
  }
}