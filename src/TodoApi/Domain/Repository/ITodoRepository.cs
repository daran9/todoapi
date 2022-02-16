using System.Threading.Tasks;
using System.Collections.Generic;
using TodoApi.Domain.Models;
using CSharpFunctionalExtensions;

namespace TodoApi.Domain.Repository
{
    public interface ITodoRepository
    {
        Task<Result<Todo>> GetByIdAsync(TodoId id, string type = "Note");

        Task<Result> CreateAsync(Todo todo);

        Task<Result> Delete(TodoId id);

        Task<Result> Update(TodoId id, Todo todo);

        Task<Result<IEnumerable<Todo>>> GetAllAsync();
    }
}