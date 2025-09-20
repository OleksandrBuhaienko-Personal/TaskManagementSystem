using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Domain.Dto.Users;

namespace TaskManagementSystem.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
  private readonly UserService _userService;

  public UserController(UserService userService)
  {
    _userService = userService;
  }

  [EnableQuery]
  [Authorize(Roles = "Admin")]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll(CancellationToken cancellationToken)
  {
    var result = await _userService.GetAllAsync(cancellationToken);
    if (result.Status == ResultStatus.NotFound) return NotFound();
    if (result.Status != ResultStatus.Ok) return StatusCode(StatusCodes.Status400BadRequest);
    
    var users = result.Value;
    
    return Ok(users.Select(u => new UserResponse()
    {
      Id = u.Id,
      Age = u.Age,
      Email = u.Email,
      FirstName = u.FirstName,
      LastName = u.LastName,
    }));
  }

  [Authorize]
  [HttpGet("{id:guid}")]
  public async Task<ActionResult<UserResponse>> GetById(Guid id, CancellationToken cancellationToken)
  {
    var result = await _userService.GetByIdAsync(id, cancellationToken);
    var user = result.Value;
    return result.Status switch
    {
      ResultStatus.NotFound => NotFound(),
      ResultStatus.Ok => Ok(
        new UserResponse
        {
         Id = user.Id,
         Age = user.Age,
         Email = user.Email,
         FirstName = user.FirstName,
         LastName = user.LastName,
        }),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      _ => StatusCode(StatusCodes.Status400BadRequest)
    };
  }
  
  [Authorize]
  [HttpPut("{id:guid}")]
  public async Task<ActionResult<UserResponse>> Update(Guid id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
  {
    var existing = await _userService.GetByIdAsync(id, cancellationToken);
    if (existing.Status == ResultStatus.NotFound) return NotFound();
    if (existing.Status != ResultStatus.Ok) return StatusCode(StatusCodes.Status400BadRequest);

    var existingUser = existing.Value!;
    existingUser.FirstName = request.FirstName;
    existingUser.LastName = request.LastName;
    existingUser.Email = request.Email;

    var result = await _userService.UpdateAsync(existingUser, cancellationToken);
    var user = result.Value;
    return result.Status switch
    {
      ResultStatus.Ok => Ok(
        new UserResponse
        {
          Id = user.Id,
          Age = user.Age,
          FirstName = user.FirstName,
          LastName = user.LastName,
          Email = user.Email,
        }),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      ResultStatus.NotFound => NotFound(),
      _ => StatusCode(StatusCodes.Status400BadRequest)
    };
  }
  [Authorize]
  [HttpDelete("{id:guid}")]
  public async Task<ActionResult<Guid>> Delete(Guid id, CancellationToken cancellationToken)
  {
    var result = await _userService.DeleteAsync(id, cancellationToken);
    return result.Status switch
    {
      ResultStatus.Ok => Ok(result.Value.Id),
      ResultStatus.NotFound => NotFound(),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      _ => StatusCode(StatusCodes.Status400BadRequest)
    };
  }
}

