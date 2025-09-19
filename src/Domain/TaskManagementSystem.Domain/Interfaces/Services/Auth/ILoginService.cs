using System.Threading.Tasks;
using Ardalis.Result;
using TaskManagementSystem.Domain.Dto;
using TaskManagementSystem.Domain.Dto.Auth;

namespace TaskManagementSystem.Domain.Interfaces.Services.Auth
{
  public interface ILoginService
  {
    Task<Result<string>> LoginAsync(LoginRequest loginDto);
  }
}
