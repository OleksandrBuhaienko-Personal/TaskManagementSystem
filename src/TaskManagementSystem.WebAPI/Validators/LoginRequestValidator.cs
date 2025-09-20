using FluentValidation;
using TaskManagementSystem.Application.Dto.Auth;

namespace TaskManagementSystem.WebAPI.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
  public LoginRequestValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress()
      .MinimumLength(10)
      .MaximumLength(100);
    
    RuleFor(x => x.Password)
      .NotEmpty()
      .MinimumLength(10)
      .MaximumLength(100);
  }
}
