using Ardalis.Specification;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Specifications;

public class GetUsersByEmailSpec : SingleResultSpecification<User>
{
  public GetUsersByEmailSpec(string email)
  {
    Query.Where(u => u.Email == email);
  }
}
