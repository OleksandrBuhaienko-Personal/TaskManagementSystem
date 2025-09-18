using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Infrastructure.Persistence.Data;
using TaskManagementSystem.Infrastructure.Persistence.Repository;

namespace TaskManagementSystem.Infrastructure.Persistence;

public static class DataModule
{
  public static void AddDbAndRepositoryServices(this IServiceCollection services, IConfiguration configuration)
  {
    //Database configuration
    services.AddDbContext<AppDbContext>(opt => opt
      .UseNpgsql(configuration
        .GetConnectionString("DB:ConnectionString")));

    //Generic Repository
    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
  }
}