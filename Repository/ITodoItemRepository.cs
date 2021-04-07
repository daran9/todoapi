using System;   
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoApi.Repository
{
    public interface ITodoItemRepository
    {
        Task<TodoItem> GetByIdAsync(long id, string type = "Note");

        Task CreateAsync(TodoItem item);

        Task Delete(long id);

        Task Update(long id, TodoItem item);

        Task<IEnumerable<TodoItem>> GetAllAsync();
    }
}