using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.Interfaces.Services.Auth
{
  public interface IRegisterService
  {
    Task<string> RegisterNewUser(string email, string password);
  }
}
