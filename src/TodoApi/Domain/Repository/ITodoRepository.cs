using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TodoApi.Domain.Entities;

namespace TodoApi.Domain.Repository;

public interface ITodoRepository
{
    Task<Result<Todo>> GetByIdAsync(TodoId id, string type = "Note");

    Task<Result> CreateAsync(Todo todo);

    Task<Result> Delete(TodoId id);

    Task<Result> Update(TodoId id, Todo todo);

    Task<Result<IEnumerable<Todo>>> GetAllAsync();
}