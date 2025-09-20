using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Dto.Roles;
using TaskManagementSystem.Domain.Entities.Auth;
using TaskManagementSystem.Domain.Interfaces;

namespace TaskManagementSystem.WebAPI.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api")]
public class RoleController(IRepository<Role> roleRepository)
{
  [HttpPost("add-role")]
  public async Task<ActionResult<Guid>> Register([FromBody] CreateRoleRequest request, CancellationToken ct)
  {
    var role = await roleRepository.AddAsync(new Role(request.Name), ct);
    return role.Id;
  }
}
