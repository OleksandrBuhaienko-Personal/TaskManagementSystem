using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Entities.Common;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.Infrastructure.Persistence.Data;

namespace TaskManagementSystem.Infrastructure.Persistence.Repository;

internal class EfRepository<T>(AppDbContext dbContext) : RepositoryBase<T>(dbContext), IRepository<T>
  where T : Entity, IAggregateRoot;
