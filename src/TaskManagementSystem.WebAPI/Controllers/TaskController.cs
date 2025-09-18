using System.Collections;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.Services;
using Task = TaskManagementSystem.Domain.Entities.Task;
namespace TaskManagementSystem.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
  private readonly TaskService _taskService;

  public TaskController(TaskService taskService)
  {
    _taskService = taskService;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Task>>> GetAll(CancellationToken cancellationToken)
  {
    var result = await _taskService.GetAllAsync(cancellationToken);
    return Ok(result.Value);
  }

  [HttpGet("{id:guid}")]
  public async Task<ActionResult<Task>> GetById(Guid id, CancellationToken cancellationToken)
  {
    var result = await _taskService.GetByIdAsync(id, cancellationToken);
    return result.Status switch
    {
      ResultStatus.NotFound => NotFound(),
      ResultStatus.Ok => Ok(result.Value),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      _ => StatusCode(StatusCodes.Status500InternalServerError)
    };
  }

  // Note: Accept the domain Task entity directly to avoid guessing its constructor/shape.
  // If you prefer DTOs, introduce CreateTaskDto/UpdateTaskDto and map to TaskEntity similarly to UserController.
  [HttpPost]
  public async Task<ActionResult<Task>> Create([FromBody] Task entity, CancellationToken cancellationToken)
  {
    var result = await _taskService.CreateAsync(entity, cancellationToken);
    return result.Status switch
    {
      ResultStatus.Ok => CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      _ => StatusCode(StatusCodes.Status500InternalServerError)
    };
  }

  [HttpPut("{id:guid}")]
  public async Task<ActionResult<Task>> Update(Guid id, [FromBody] Task? entity, CancellationToken cancellationToken)
  {
    if (entity is null || entity.Id != id)
    {
      return BadRequest("Entity must be provided and its Id must match the route id.");
    }

    // Optionally ensure it exists first
    var existing = await _taskService.GetByIdAsync(id, cancellationToken);
    if (existing.Status == ResultStatus.NotFound) return NotFound();
    if (existing.Status != ResultStatus.Ok) return StatusCode(StatusCodes.Status500InternalServerError);

    var result = await _taskService.UpdateAsync(entity, cancellationToken);
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
    var result = await _taskService.DeleteAsync(id, cancellationToken);
    return result.Status switch
    {
      ResultStatus.Ok => NoContent(),
      ResultStatus.NotFound => NotFound(),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      _ => StatusCode(StatusCodes.Status500InternalServerError)
    };
  }
}

