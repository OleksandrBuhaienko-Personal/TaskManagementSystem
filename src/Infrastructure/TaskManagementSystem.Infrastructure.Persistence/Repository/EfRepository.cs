using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Entities.Common;
using TaskManagementSystem.Domain.Interfaces;

namespace TaskManagementSystem.Infrastructure.Persistence.Repository;

internal class EfRepository<T>(DbContext dbContext) : RepositoryBase<T>(dbContext), IRepository<T>
  where T : Entity, IAggregateRoot;