using Ardalis.Result;
using TaskManagementSystem.Auth.Abstractions;
using TaskManagementSystem.Domain.Constants;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces.Services;
using TaskManagementSystem.Domain.Interfaces.Services.Auth;
using TaskManagementSystem.Domain.Interfaces.Services.Identity;

namespace TaskManagementSystem.Auth.Services;

public class RegisterService : IRegisterService
{
  private readonly IJwtProvider _jwtProvider;
  private readonly IUserService _userService;
  private readonly IPasswordService _passwordService;
  private readonly IRoleService _roleService;

  public RegisterService(IUserService userService, IJwtProvider jwtProvider, IPasswordService passwordService,
    IRoleService roleService)
  {
    _userService = userService;
    _jwtProvider = jwtProvider;
    _passwordService = passwordService;
    _roleService = roleService;
  }

  public async Task<Result<string>> RegisterNewUser(uint age, string firstName, string lastName, string email,
    string password)
  {
    var result = await _userService.GetUserByEmailAsync(email);
    if (result.Value is not null)
    {
      return Result.Error("User already exists!");
    }

    var newUser = new User(age, firstName, lastName, email) { PasswordHash = _passwordService.HashPassword(password) };
    var createdUser = await _userService.CreateAsync(newUser);
    await _roleService.AssignRoleToUserAsync(createdUser.Value.Id, UserDomainConstants.DefaultRoleName);
    string token = await _jwtProvider.Generate(createdUser);
    return token;
  }
}
