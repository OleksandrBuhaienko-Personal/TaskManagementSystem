using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using TaskManagementSystem.Application;
using TaskManagementSystem.Auth;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Persistence;
using TaskManagementSystem.WebAPI.Middleware;
using TaskManagementSystem.WebAPI.Validators;
using Task = TaskManagementSystem.Domain.Entities.Task;

namespace TaskManagementSystem.WebAPI;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Management System API", Version = "v1" });

      c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
          In = ParameterLocation.Header,
          Description = "Enter JWT with Bearer prefix",
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
        });

      c.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
          },
          new string[] { }
        }
      });
    });

    builder.Services.ConfigureDataModule(builder.Configuration);
    builder.Services.ConfigureApplicationModules(builder.Configuration);
    builder.Services.AddAuth(builder.Configuration);
    builder.Services.AddControllers().AddOData(o =>
    {
      var modelBuilder = new ODataConventionModelBuilder();
      modelBuilder.EntitySet<User>("Users");
      modelBuilder.EntitySet<Task>("Tasks");
      o.AddRouteComponents("odata", modelBuilder.GetEdmModel());
      o.EnableQueryFeatures();
    })
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateRoleRequestValidator>());

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddTransient<GlobalExceptionHandler>();

    builder.Services.AddHttpContextAccessor();

    var app = builder.Build();


    app.UseCors(options =>
    {
      options.AllowAnyHeader();
      options.AllowAnyMethod();
      options.AllowAnyOrigin();
    });

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseMiddleware<GlobalExceptionHandler>();

    app.UseHttpsRedirection();

    app.MapControllers();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseSwagger();

    app.Run();
  }
}
