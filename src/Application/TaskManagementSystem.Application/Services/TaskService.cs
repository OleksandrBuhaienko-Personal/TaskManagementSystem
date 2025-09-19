using Ardalis.GuardClauses;
using Ardalis.Result;
using TaskManagementSystem.Application.Specifications;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Domain.Interfaces.Services;
using Task = TaskManagementSystem.Domain.Entities.Task;

namespace TaskManagementSystem.Application.Services;

public class TaskService : ITaskService
{
  private readonly IRepository<Task> _repository;
  
  public TaskService(IRepository<Task> repository)
  {
    _repository = repository;
  }
  
  public async Task<Result<IEnumerable<Task>>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    var tasks = await _repository.ListAsync(cancellationToken);
    return Result<IEnumerable<Task>>.Success(tasks);
  }

  public async Task<Result<Task>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    Guard.Against.Default(id, nameof(id));
    var result = await _repository.GetByIdAsync(new GetTaskByIdSpec(id), cancellationToken);
    return result is not null ? Result<Task>.Success(result) : Result<Task>.NotFound();
  }

  public async Task<Result<Task>> CreateAsync(Task entity, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(entity, nameof(entity));
    var result = await _repository.AddAsync(entity, cancellationToken);
    return Result<Task>.Success(result);
  }

  public async Task<Result<Task>> UpdateAsync(Task entity, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(entity, nameof(entity));
    await _repository.UpdateAsync(entity, cancellationToken);
    return Result<Task>.Success(entity);
  }

  public async Task<Result<Task>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    Guard.Against.Default(id, nameof(id));
    var entity = await _repository.GetByIdAsync(new GetTaskByIdSpec(id), cancellationToken);
    if (entity is null)
    {
      return Result<Task>.NotFound();
    }
    await _repository.DeleteAsync(entity, cancellationToken);
    return Result<Task>.Success(entity);
  }
}
