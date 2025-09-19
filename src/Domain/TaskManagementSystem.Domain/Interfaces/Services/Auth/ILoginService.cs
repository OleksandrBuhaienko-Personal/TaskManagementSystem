using System.Threading.Tasks;
using Ardalis.Result;
using TaskManagementSystem.Domain.Dto;

namespace TaskManagementSystem.Domain.Interfaces.Services.Auth
{
  public interface ILoginService
  {
    Task<Result<string>> LoginAsync(LoginDto loginDto);
  }
}
