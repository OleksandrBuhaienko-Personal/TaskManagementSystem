using Ardalis.GuardClauses;
using Ardalis.Result;
using TaskManagementSystem.Auth.Specifications;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Entities.Auth;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Domain.Interfaces.Services.Identity;

namespace TaskManagementSystem.Auth.Services;

public class RoleService : IRoleService
{
  private readonly IRepository<Role> _roleRepository;
  private readonly IRepository<UserRole> _userRoleRepository;
  private readonly IRepository<User> _userRepository;

  public RoleService(IRepository<Role> roleRepository, IRepository<UserRole> userRoleRepository, IRepository<User> userRepository)
  {
    _roleRepository = roleRepository;
    _userRoleRepository = userRoleRepository;
    _userRepository = userRepository;
  }

  public async Task<Result<Role>> CreateRoleAsync(string roleName, CancellationToken cancellationToken = default)
  {
    Guard.Against.NullOrWhiteSpace(roleName, nameof(roleName));
    var role = new Role(roleName);
    role = await _roleRepository.AddAsync(role, cancellationToken);
    return Result<Role>.Success(role);
  }

  public async Task<Result> AssignRoleToUserAsync(Guid userId, string roleName, CancellationToken cancellationToken = default)
  {
    Guard.Against.NullOrWhiteSpace(roleName, nameof(roleName));
    
    var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
    var requestedRole = await _roleRepository.SingleOrDefaultAsync(new GetRoleByNameSpec(roleName), cancellationToken);

    if (requestedRole == null)
    {
      return Result.NotFound($"Role with the name {roleName} doesn't exists");
    }

    var userRole = new UserRole
    {
      UserId = userId,
      RoleId = requestedRole.Id
    };

    await _userRoleRepository.AddAsync(userRole, cancellationToken);
    return Result.Success();
  }

  public async Task<List<string>> GetUserRoles(Guid userId, CancellationToken cancellationToken = default)
  {
    Guard.Against.Default(userId, nameof(userId));
    var userRoles = await _userRoleRepository.ListAsync(new GetUserRolesByUserIdSpec(userId), cancellationToken);
    var roles = userRoles.Select(ur => ur.Role.Name).ToList();
    return Result<List<string>>.Success(roles);
  }
}
