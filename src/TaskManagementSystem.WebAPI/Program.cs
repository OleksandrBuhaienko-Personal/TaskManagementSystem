using Microsoft.OpenApi.Models;
using TaskManagementSystem.Application;
using TaskManagementSystem.Auth;
using TaskManagementSystem.Infrastructure.Persistence;
using TaskManagementSystem.WebAPI.Middleware;

namespace TaskManagementSystem.WebAPI;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.ConfigureDataModule(builder.Configuration);
    builder.Services.ConfigureApplicationModules(builder.Configuration);
    builder.Services.AddAuth(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo 
      { 
        Title = "Task Management System API", 
        Version = "v1" 
      });
    });    
    builder.Services.AddTransient<GlobalExceptionHandler>();

    var app = builder.Build();
    
    
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Management System API V1");
        c.RoutePrefix = "swagger";
      });
    }

    app.UseMiddleware<GlobalExceptionHandler>();
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
  }
}
