using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManagementSystem.Domain.Entities.Common;
using TaskManagementSystem.Domain.Interfaces;

namespace TaskManagementSystem.Infrastructure.Persistence.EntityConfigurations.Base;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity, IAggregateRoot
{
  public void Configure(EntityTypeBuilder<T> builder)
  {
    builder.Property(p => p.Id)
      .ValueGeneratedNever();

    builder.Property(p => p.CreatedAt)
      .IsRequired();

    ConfigureEntity(builder);
  }

  protected abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}
