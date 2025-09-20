using Ardalis.GuardClauses;
using Ardalis.Result;
using TaskManagementSystem.Application.Specifications;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Domain.Interfaces.Services;

namespace TaskManagementSystem.Application.Services;

//TODO: Update interface to the user specific, remove base one
public class UserService : IUserService
{
  private readonly IRepository<User> _repository;
  
  public UserService(IRepository<User> repository)
  {
    _repository = repository;
  }
  
  public async Task<Result<IEnumerable<User>>> GetAllAsync(CancellationToken cancellationToken = default)
  {
    var users = await _repository.ListAsync(cancellationToken);
    return Result<IEnumerable<User>>.Success(users);
  }

  public async Task<Result<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    Guard.Against.Default(id, nameof(id));
    var result = await _repository.GetByIdAsync(id, cancellationToken);
    return result is not null ? Result<User>.Success(result) : Result<User>.NotFound();
  }

  public async Task<Result<User>> CreateAsync(User entity, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(entity, nameof(entity));
    var result = await _repository.AddAsync(entity, cancellationToken);
    return Result<User>.Success(result);
  }

  public async Task<Result<User>> UpdateAsync(User entity, CancellationToken cancellationToken = default)
  {
    Guard.Against.Null(entity, nameof(entity));
    await _repository.UpdateAsync(entity, cancellationToken);
    return Result<User>.Success(entity);
  }

  public async Task<Result<User>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    Guard.Against.Default(id, nameof(id));
    var entity = await _repository.GetByIdAsync(id, cancellationToken);
    if (entity is null)
    {
      return Result<User>.NotFound();
    }
    await _repository.DeleteAsync(entity, cancellationToken);
    return Result<User>.Success(entity);
  }

  public async Task<Result<User>> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
  {
    Guard.Against.NullOrWhiteSpace(email, nameof(email), "Email cannot be empty!");
    var result = await _repository.SingleOrDefaultAsync(new GetUsersByEmailSpec(email), cancellationToken);

    return result ?? Result<User>.NotFound();
  }
}
