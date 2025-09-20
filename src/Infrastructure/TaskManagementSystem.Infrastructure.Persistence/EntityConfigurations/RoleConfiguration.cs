using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain.Entities.Auth;
using TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations.Base;

namespace TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations;

public class RoleConfiguration : EntityConfiguration<Role>
{
  protected override void ConfigureEntity(EntityTypeBuilder<Role> builder)
  {
    builder.ToTable("Roles");
    builder.Property(e => e.Name)
      .HasMaxLength(50)
      .IsRequired();

    builder.HasMany(e => e.UserRoles)
      .WithOne(e => e.Role)
      .HasForeignKey(e => e.RoleId);
  }
}
