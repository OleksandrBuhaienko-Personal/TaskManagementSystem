using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Domain.Entities;

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

  [HttpGet]
  public async Task<ActionResult<IEnumerable<User>>> GetAll(CancellationToken cancellationToken)
  {
    var result = await _userService.GetAllAsync(cancellationToken);
    return Ok(result.Value);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<User>> GetById(Guid id, CancellationToken cancellationToken)
  {
    var result = await _userService.GetByIdAsync(id, cancellationToken);
    return result.Status switch
    {
      ResultStatus.NotFound => NotFound(),
      ResultStatus.Ok => Ok(result.Value),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      _ => StatusCode(StatusCodes.Status500InternalServerError)
    };
  }

  public record CreateUserDto(uint Age, string FirstName, string LastName, string Email);
  public record UpdateUserDto(string FirstName, string LastName, string Email);

  [HttpPost]
  public async Task<ActionResult<User>> Create([FromBody] CreateUserDto dto, CancellationToken cancellationToken)
  {
    // Domain requires Age via constructor (Age setter is private)
    var user = new User(dto.Age, dto.FirstName, dto.LastName, dto.Email);
    var result = await _userService.CreateAsync(user, cancellationToken);

    return result.Status switch
    {
      ResultStatus.Ok => CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      _ => StatusCode(StatusCodes.Status500InternalServerError)
    };
  }

  [HttpPut("{id:guid}")]
  public async Task<ActionResult<User>> Update(Guid id, [FromBody] UpdateUserDto dto, CancellationToken cancellationToken)
  {
    // Fetch existing aggregate and update mutable fields
    var existing = await _userService.GetByIdAsync(id, cancellationToken);
    if (existing.Status == ResultStatus.NotFound) return NotFound();
    if (existing.Status != ResultStatus.Ok) return StatusCode(StatusCodes.Status500InternalServerError);

    var user = existing.Value!;
    user.FirstName = dto.FirstName;
    user.LastName = dto.LastName;
    user.Email = dto.Email;

    var result = await _userService.UpdateAsync(user, cancellationToken);
    return result.Status switch
    {
      ResultStatus.Ok => Ok(result.Value),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      ResultStatus.NotFound => NotFound(),
      _ => StatusCode(StatusCodes.Status500InternalServerError)
    };
  }

  [HttpDelete("{id:guid}")]
  public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
  {
    var result = await _userService.DeleteAsync(id, cancellationToken);
    return result.Status switch
    {
      ResultStatus.Ok => NoContent(),
      ResultStatus.NotFound => NotFound(),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      _ => StatusCode(StatusCodes.Status500InternalServerError)
    };
  }
}

