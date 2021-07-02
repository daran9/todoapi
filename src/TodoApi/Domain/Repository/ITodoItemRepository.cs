using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoApi.Domain.Repository
{
    public interface ITodoItemRepository
    {
        Task<TodoItemEntity> GetByIdAsync(long id, string type = "Note");

        Task CreateAsync(TodoItemEntity item);

        Task Delete(long id);

        Task Update(long id, TodoItemEntity item);

        Task<IEnumerable<TodoItemEntity>> GetAllAsync();
    }
}