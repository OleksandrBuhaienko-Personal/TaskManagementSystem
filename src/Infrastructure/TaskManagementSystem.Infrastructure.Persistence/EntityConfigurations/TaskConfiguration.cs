using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations.Base;
using Task = TaskManagementSystem.Domain.Entities.Task;

namespace TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations;

public class TaskConfiguration : EntityConfiguration<Task>
{
  protected override void ConfigureEntity(EntityTypeBuilder<Task> builder)
  {
    builder.ToTable("Tasks");
    builder.HasKey(t => t.Id);
    
    builder.Property(t => t.Title)
      .IsRequired()
      .HasMaxLength(100);
    builder.Property(t => t.Description)
      .IsRequired()
      .HasMaxLength(200);
    builder.Property(t => t.DueDateTime)
      .IsRequired();
  }
}
