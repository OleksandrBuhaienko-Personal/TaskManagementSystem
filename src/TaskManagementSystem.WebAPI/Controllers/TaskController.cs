using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Domain.Dto.Tasks;
using TaskManagementSystem.Domain.Interfaces.Services;
using Task = TaskManagementSystem.Domain.Entities.Task;
namespace TaskManagementSystem.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
  private readonly TaskService _taskService;
  private readonly IUserService _userService;

  public TaskController(TaskService taskService, IUserService userService)
  {
    _taskService = taskService;
    _userService = userService;
  }

  [EnableQuery]
  [Authorize]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<TaskResponse>>> GetAll(CancellationToken cancellationToken)
  {
    var result = await _taskService.GetAllAsync(cancellationToken);
    if (result.Status == ResultStatus.NotFound) return NotFound();
    if (result.Status != ResultStatus.Ok) return StatusCode(StatusCodes.Status400BadRequest);
    var tasks = result.Value;
    
    return Ok(tasks.Select(t => new TaskResponse
    {
      Id = t.Id,
      UserId = t.UserId,
      Title = t.Title,
      Description = t.Description,
      DueDateTime = t.DueDateTime,
    }));  
  }

  [Authorize]
  [HttpGet("{id:guid}")]
  public async Task<ActionResult<TaskResponse>> GetById(Guid id, CancellationToken cancellationToken)
  {
    var result = await _taskService.GetByIdAsync(id, cancellationToken);
    var task = result.Value;
    return result.Status switch
    {
      ResultStatus.NotFound => NotFound(),
      ResultStatus.Ok => Ok(
        new TaskResponse
        {
          Id = task.Id,
          UserId = task.UserId,
          Title = task.Title,
          Description = task.Description,
          DueDateTime = task.DueDateTime
        }),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      _ => StatusCode(StatusCodes.Status500InternalServerError)
    };
  }
  
  [Authorize]
  [HttpPost]
  public async Task<ActionResult<TaskResponse>> Create([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
  {
    var task = new Task(
      request.Title,
      request.Description,
      request.DueDateTime,
      request.UserId);
    var result = await _taskService.CreateAsync(task, cancellationToken);
    
    task = result.Value;
    return result.Status switch
    {
      ResultStatus.Ok => Ok(
        new TaskResponse
        {
          Id = task.Id,
          UserId = task.UserId,
          Title = task.Title,
          Description = task.Description,
          DueDateTime = task.DueDateTime
        }),
      ResultStatus.Error => BadRequest(result.ValidationErrors),
      _ => StatusCode(StatusCodes.Status400BadRequest)
    };
  }

  [Authorize]
  [HttpPut("{id:guid}")]
  public async Task<ActionResult<TaskResponse>> Update(Guid id, [FromBody] UpdateTaskRequest? request, CancellationToken cancellationToken)
  {
    if (request is null)
    {
      return BadRequest("Entity must be provided!");
    }

    var existing = await _taskService.GetByIdAsync(id, cancellationToken);
    if (existing.Status == ResultStatus.NotFound) return NotFound();
    if (existing.Status != ResultStatus.Ok) return StatusCode(StatusCodes.Status400BadRequest);

    var task = existing.Value;
    task.Title = request.Title;
    task.Description = request.Description;
    task.DueDateTime = request.DueDateTime;

    if (existing.Value.UserId != request.UserId)
    {
      var user = await _userService.GetByIdAsync(request.UserId, cancellationToken);
      
      if (user.Status == ResultStatus.NotFound) return NotFound("User not found!");
      task.UserId = request.UserId;
    }
    
    var result = await _taskService.UpdateAsync(task , cancellationToken);
    task = result.Value;
    return result.Status switch
    {
      ResultStatus.Ok => Ok(
        new TaskResponse
        {
          Id = task.Id,
          UserId = task.UserId,
          Title = task.Title,
          Description = task.Description,
          DueDateTime = task.DueDateTime
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
    var result = await _taskService.DeleteAsync(id, cancellationToken);
    return result.Status switch
    {
      ResultStatus.Ok => Ok(result.Value.Id),
      ResultStatus.NotFound => NotFound(),
      ResultStatus.Invalid => BadRequest(result.ValidationErrors),
      _ => StatusCode(StatusCodes.Status500InternalServerError)
    };
  }
}

