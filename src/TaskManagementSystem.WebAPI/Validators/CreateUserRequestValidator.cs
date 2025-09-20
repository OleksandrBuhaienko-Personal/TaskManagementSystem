using FluentValidation;
using TaskManagementSystem.Application.Dto.Auth;

namespace TaskManagementSystem.WebAPI.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
  public CreateUserRequestValidator()
  {
    RuleFor(x => x.Age)
      .NotEmpty();
    
    RuleFor(x => x.FirstName)
      .NotEmpty()
      .MaximumLength(100);
    
    RuleFor(x => x.LastName)
      .NotEmpty()
      .MaximumLength(100);
    
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
