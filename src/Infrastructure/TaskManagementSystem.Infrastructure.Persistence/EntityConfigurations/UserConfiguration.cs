using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations.Base;

namespace TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations;

internal class UserConfiguration : EntityConfiguration<User>
{
  protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("Users");
    
    builder.HasKey(u => u.Id);
    
    builder.Property(u => u.FirstName)
      .IsRequired()
      .HasMaxLength(50);
    
    builder.Property(u => u.LastName)
      .IsRequired()
      .HasMaxLength(50);
    
    builder.HasMany(u => u.Tasks)
      .WithOne(t => t.User)
      .HasForeignKey(t => t.UserId);
  }
}
