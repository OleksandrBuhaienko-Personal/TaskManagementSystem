using Microsoft.OpenApi.Models;
using TaskManagementSystem.Application;
using TaskManagementSystem.Infrastructure.Persistence;

namespace TaskManagementSystem.WebAPI;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    
    
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });

    var app = builder.Build();
    
    builder.Services.ConfigureDataModule(builder.Configuration);
    builder.Services.ConfigureApplicationModules(builder.Configuration);
    
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();


    app.MapControllers();

    app.Run();
  }
}
