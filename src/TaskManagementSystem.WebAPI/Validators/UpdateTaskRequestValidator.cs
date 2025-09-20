using FluentValidation;
using TaskManagementSystem.Application.Dto.Tasks;

namespace TaskManagementSystem.WebAPI.Validators;

public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
  public UpdateTaskRequestValidator()
  {
    RuleFor(x => x.UserId)
      .NotEmpty()
      .WithMessage("User id is required");
    
    RuleFor(x => x.Title)
      .NotEmpty()
      .MinimumLength(1)
      .MaximumLength(100)
      .WithMessage("Title is required and must be less than 100 characters");
    
    RuleFor(x => x.Description)
      .MinimumLength(1)
      .MaximumLength(200)
      .WithMessage("Description must be less than 200 characters");
    
    RuleFor(x => x.DueDateTime)
      .NotEmpty()
      .LessThan(DateTime.MaxValue)
      .GreaterThan(DateTime.MinValue)
      .GreaterThanOrEqualTo(DateTime.UtcNow)
      .WithMessage("Due date is required and must be greater than current date");
  }
}
