using FluentValidation;
using TaskManagementSystem.Application.Dto.Roles;

namespace TaskManagementSystem.WebAPI.Validators;

public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
{
  public CreateRoleRequestValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .MaximumLength(100)
      .WithMessage("Name is required and must be less than 100 characters");
  }
}
