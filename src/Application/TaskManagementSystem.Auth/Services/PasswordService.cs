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

  public async Task<bool> VerifyPasswordAsync(Guid userId, string password)
  {
    var user = await _userService.GetByIdAsync(userId);
    var verificationResult = _passwordHasher.VerifyHashedPassword(null!, user.Value.PasswordHash, password);
    return verificationResult == PasswordVerificationResult.Success;
  }
  public async Task<bool> VerifyPasswordAsync(User user, string password)
  {
    //TODO: Add passwordHash to user entity
    var verificationResult = _passwordHasher.VerifyHashedPassword(null!, user.PasswordHash, password);
    return verificationResult == PasswordVerificationResult.Success;
  }

  public async Task UpdatePasswordAsync(Guid userId, string newPassword)
  {
    var user = await _userService.GetByIdAsync(userId);
    var isPasswordVerified = await VerifyPasswordAsync(user, newPassword);
    if (!isPasswordVerified)
    {
      throw new InvalidOperationException("Invalid credentials!");
    }
    var passwordHash = HashPassword(newPassword);
    user.Value.PasswordHash = passwordHash;
    await _userService.UpdateAsync(user);
  }

}
