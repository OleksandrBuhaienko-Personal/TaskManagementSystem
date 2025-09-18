using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskManagementSystem.Infrastructure.Persistence.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  public AppDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

    var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Default");

    if (string.IsNullOrWhiteSpace(connectionString))
    {
      var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
      var basePath = Directory.GetCurrentDirectory();

      var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(basePath)
        .AddJsonFile("appsettings.json", optional: true)
        .AddJsonFile($"appsettings.{env}.json", optional: true)
        .AddJsonFile(Path.Combine("..", "TaskManagementSystem.WebAPI", "appsettings.json"), optional: true)
        .AddJsonFile(Path.Combine("..", "TaskManagementSystem.WebAPI", $"appsettings.{env}.json"), optional: true)
        .AddEnvironmentVariables();

      var configuration = configurationBuilder.Build();
      connectionString = configuration.GetConnectionString("Default");
    }

    if (string.IsNullOrWhiteSpace(connectionString))
    {
      throw new InvalidOperationException(
        "Connection string 'Default' is not configured for design-time operations. " +
        "Provide ConnectionStrings__Default env var or ensure appsettings.* can be found.");
    }

    optionsBuilder.UseNpgsql(connectionString);
    return new AppDbContext(optionsBuilder.Options);

  }
}
