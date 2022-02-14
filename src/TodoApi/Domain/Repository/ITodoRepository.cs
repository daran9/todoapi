using System.Threading.Tasks;
using System.Collections.Generic;
using TodoApi.Domain.Models;

namespace TodoApi.Domain.Repository
{
    public interface ITodoRepository
    {
        Task<Todo> GetByIdAsync(TodoId id, string type = "Note");

        Task CreateAsync(Todo todo);

        Task Delete(TodoId id);

        Task Update(TodoId id, Todo todo);

        Task<IEnumerable<Todo>> GetAllAsync();
    }
}