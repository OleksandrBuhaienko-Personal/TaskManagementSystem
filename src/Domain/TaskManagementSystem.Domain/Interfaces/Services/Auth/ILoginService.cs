using System.Threading.Tasks;
using Ardalis.Result;


namespace TaskManagementSystem.Domain.Interfaces.Services.Auth
{
  public interface ILoginService
  {
    Task<Result<string>> LoginAsync(string email, string password);
  }
}
