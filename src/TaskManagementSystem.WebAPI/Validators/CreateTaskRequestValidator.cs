using FluentValidation;
using TaskManagementSystem.Application.Dto.Tasks;

namespace TaskManagementSystem.WebAPI.Validators;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
  public CreateTaskRequestValidator()
  {
    RuleFor(x => x.UserId)
      .NotEmpty();
    
    RuleFor(x => x.Title)
      .NotEmpty()
      .MinimumLength(1)
      .MaximumLength(100);
    
    RuleFor(x => x.Description)
      .NotEmpty()
      .MinimumLength(1)
      .MaximumLength(200);
    
    RuleFor(x => x.DueDateTime)
      .NotEmpty()
      .LessThan(DateTime.MaxValue)
      .GreaterThan(DateTime.MinValue)
      .GreaterThanOrEqualTo(DateTime.UtcNow);
  }
}
