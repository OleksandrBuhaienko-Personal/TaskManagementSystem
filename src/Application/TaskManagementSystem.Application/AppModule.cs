using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces.Services.Base;
using Task = TaskManagementSystem.Domain.Entities.Task;

namespace TaskManagementSystem.Application;

public static class AppModule
{
  public static void ConfigureApplicationModules(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped(typeof(IBaseEntityService<User>), typeof(UserService));
    services.AddScoped(typeof(IBaseEntityService<Task>), typeof(TaskService));
  }
}
