using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain.Entities.Auth;
using TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations.Base;

namespace TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations;

public class UserRoleConfiguration : EntityConfiguration<UserRole>
{
  protected override void ConfigureEntity(EntityTypeBuilder<UserRole> builder)
  {
    // Composite Primary Key
    builder.HasKey(ur => new { ur.UserId, ur.RoleId });

    // Configure relationships
    builder.HasOne(ur => ur.User)
      .WithMany(u => u.UserRoles)
      .HasForeignKey(ur => ur.UserId);

    builder.HasOne(ur => ur.Role)
      .WithMany(r => r.UserRoles)
      .HasForeignKey(ur => ur.RoleId);

    builder.ToTable("UserRoles");
  }
}
