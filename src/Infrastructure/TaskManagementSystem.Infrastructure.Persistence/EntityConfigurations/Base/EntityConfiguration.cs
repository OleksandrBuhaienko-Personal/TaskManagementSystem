using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain.Entities.Common;

namespace TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations.Base;

internal abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
  public void Configure(EntityTypeBuilder<T> builder)
  {
    builder.Property(p => p.Id)
      .ValueGeneratedNever();

    builder.Property(p => p.CreatedAt)
      .IsRequired();

    ConfigureEntity(builder);
  }

  public abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}