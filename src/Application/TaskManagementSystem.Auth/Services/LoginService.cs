using Ardalis.Result;
using TaskManagementSystem.Auth.Abstractions;
using TaskManagementSystem.Domain.Dto;
using TaskManagementSystem.Domain.Dto.Auth;
using TaskManagementSystem.Domain.Interfaces.Services;
using TaskManagementSystem.Domain.Interfaces.Services.Auth;

namespace TaskManagementSystem.Auth.Services;

internal sealed class LoginService : ILoginService
{
  private readonly IUserService _userService;
  private readonly IJwtProvider _jwtProvider;
  private readonly IPasswordService _passwordService;

  public LoginService(
    IUserService userService,
    IPasswordService passwordService,
    IJwtProvider jwtProvider)
  {
    _userService = userService;
    _passwordService = passwordService;
    _jwtProvider = jwtProvider;
  }

  public async Task<Result<string>> LoginAsync(LoginRequest request)
  {
    var result = await _userService.GetUserByEmailAsync(request.Email);
    if (!result.IsSuccess)
    {
      return Result.Error("Invalid email!");
    }
    var isPasswordVerified = _passwordService.VerifyPassword(result.Value, request.Password);
    if (!isPasswordVerified.IsSuccess)
    {
      return Result.Error("Invalid credentials");
    }
    string token = await _jwtProvider.Generate(result.Value);
    return Result.Success(token);
  }
}
