using System.Threading.Tasks;
using TaskManagementSystem.Domain.Dto;

namespace TaskManagementSystem.Domain.Interfaces.Services.Auth
{
  public interface ILoginService
  {
    Task<string> LoginAsync(LoginDto loginDto);
  }
}
