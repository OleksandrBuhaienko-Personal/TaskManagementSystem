using Ardalis.Specification;
using TaskManagementSystem.Domain.Entities.Auth;

namespace TaskManagementSystem.Auth.Specifications;

public class GetUserRolesByUserIdSpec : Specification<UserRole>
{
  public GetUserRolesByUserIdSpec(Guid userId) =>
    Query.Where(x => x.UserId == userId)
      .Include(x => x.Role);
}
