using Ardalis.Specification;
using TaskManagementSystem.Domain.Entities.Auth;

namespace TaskManagementSystem.Auth.Specifications;

public class GetRoleByNameSpec : SingleResultSpecification<Role>
{
  public GetRoleByNameSpec(string roleName)
  {
    Query.Where(x => x.Name == roleName);
  }
}
