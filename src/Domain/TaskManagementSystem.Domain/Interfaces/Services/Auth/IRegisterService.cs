using System.Threading.Tasks;
using Ardalis.Result;

namespace TaskManagementSystem.Domain.Interfaces.Services.Auth
{
  public interface IRegisterService
  {
    Task<Result<string>> RegisterNewUser(uint age, string firstName, string lastName, string email, string password);
  }
}
