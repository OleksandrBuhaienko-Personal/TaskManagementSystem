using Ardalis.Specification;
using Task = TaskManagementSystem.Domain.Entities.Task;
namespace TaskManagementSystem.Application.Specifications;

public class GetTaskByIdSpec : SingleResultSpecification<Task>
{
  public GetTaskByIdSpec(Guid id)
  {
    Query.Where(x => x.Id == id);
  }
}
