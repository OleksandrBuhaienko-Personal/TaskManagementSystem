using FluentValidation;
using TaskManagementSystem.Application.Dto.Auth;

namespace TaskManagementSystem.WebAPI.Validators;

public class UpdatePasswordRequestValidator : AbstractValidator<UpdatePasswordRequest>
{
  public UpdatePasswordRequestValidator()
  {
    RuleFor(x => x.UserId)
      .NotEmpty();

    RuleFor(x => x.CurrentPassword)
      .NotEmpty()
      .MinimumLength(10)
      .MaximumLength(100);
    
    RuleFor(x => x.NewPassword)
      .NotEmpty()
      .MinimumLength(10)
      .MaximumLength(100);
  }
}
