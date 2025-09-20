using Ardalis.Specification;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Application.Specifications;

public class GetUserByIdSpec : SingleResultSpecification<User>
{
  public GetUserByIdSpec(Guid id)
  {
    Query.Where(x => x.Id == id);
  }
}
