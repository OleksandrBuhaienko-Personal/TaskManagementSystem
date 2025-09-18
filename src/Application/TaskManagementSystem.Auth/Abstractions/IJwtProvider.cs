using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Auth.Abstractions;

public interface IJwtProvider
{
  Task<string> Generate(User user);
}
