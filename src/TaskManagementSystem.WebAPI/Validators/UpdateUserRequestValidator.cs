using FluentValidation;
using TaskManagementSystem.Application.Dto.Users;

namespace TaskManagementSystem.WebAPI.Validators;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
  public UpdateUserRequestValidator()
  {
    RuleFor(x => x.Age)
      .NotEmpty()
      .WithMessage("Age is required");
    
    RuleFor(x => x.FirstName)
      .NotEmpty()
      .MaximumLength(100)
      .WithMessage("First name is required and must be less than 100 characters");
    
    RuleFor(x => x.LastName)
      .NotEmpty()
      .MaximumLength(100)
      .WithMessage("Last name is required and must be less than 100 characters");
    
    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress()
      .MinimumLength(10)
      .MaximumLength(100)
      .WithMessage("Email is required and must be between 10 and 100 characters");
  }
}
