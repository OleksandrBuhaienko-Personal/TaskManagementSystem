using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces.Services;
using TaskManagementSystem.Domain.Interfaces.Services.Auth;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementSystem.Auth.Services;

public class PasswordService : IPasswordService
{
  private readonly PasswordHasher<User> _passwordHasher;
  private readonly IUserService _userService;

  public PasswordService(PasswordHasher<User> passwordHasher, IUserService userService)
  {
    _passwordHasher = passwordHasher;
    _userService = userService;
  }

  public string HashPassword(string password)
  {
    return _passwordHasher.HashPassword(null!, password);
  }

  public async Task<Result> VerifyPasswordAsync(Guid userId, string password, CancellationToken ct = default)
  {
    var user = await _userService.GetByIdAsync(userId, ct);
    var verificationResult = _passwordHasher.VerifyHashedPassword(null!, user.Value.PasswordHash, password);
    return verificationResult == PasswordVerificationResult.Success
      ? Result.Success()
      : Result.Error("Invalid credentials");
  }

  public Result VerifyPassword(User user, string password)
  {
    var verificationResult = _passwordHasher.VerifyHashedPassword(null!, user.PasswordHash, password);
    return verificationResult == PasswordVerificationResult.Success
      ? Result.Success()
      : Result.Error("Invalid credentials");
  }

  public async Task<Result> UpdatePasswordAsync(Guid userId, string newPassword)
  {
    var result = await _userService.GetByIdAsync(userId);
    if (!result.IsSuccess)
    {
      return Result.NotFound("User not found!");
    }

    var isPasswordVerified = VerifyPassword(result.Value, newPassword);
    if (!isPasswordVerified.IsSuccess)
    {
      return Result.Error("Invalid credentials!");
    }

    var user = result.Value;
    var passwordHash = HashPassword(newPassword);
    user.PasswordHash = passwordHash;
    result = await _userService.UpdateAsync(user);
    return result.IsSuccess ? Result.Success() : Result.Error("Error updating password!");
  }
}
