using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Result;
using TaskManagementSystem.Domain.Entities.Auth;

namespace TaskManagementSystem.Domain.Interfaces.Services.Identity
{
  public interface IRoleService
  {
    Task<Result<Role>> CreateRoleAsync(string roleName, CancellationToken cancellationToken = default);
    Task<Result> AssignRoleToUserAsync(Guid userId, string roleName, CancellationToken cancellationToken = default);
    Task<Result<List<string>>> GetUserRoles(Guid userId, CancellationToken cancellationToken = default);
  }
}
