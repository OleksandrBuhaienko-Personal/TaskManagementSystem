using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Domain.Interfaces.Services;

namespace TaskManagementSystem.Application;

public static class AppModule
{
  public static void ConfigureApplicationModules(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ITaskService, TaskService>();

    // Register concrete implementations so controllers requesting concrete types can be resolved
    services.AddScoped<UserService>();
    services.AddScoped<TaskService>();
  }
}
