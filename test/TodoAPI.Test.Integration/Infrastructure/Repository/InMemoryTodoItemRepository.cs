using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoAPI.Test.Integration.Infrastructure.Repository
{
    public class InMemoryTodoItemRepository : ITodoItemRepository
    {
        private readonly ConcurrentDictionary<long, TodoItem> _todoItems = new ConcurrentDictionary<long, TodoItem>();

        public Task CreateAsync(TodoItem item) 
            => Task.FromResult(_todoItems.TryAdd(item.Id, item));

        public Task Delete(long id) 
            => Task.FromResult(_todoItems.TryRemove(id, out TodoItem _));

        public Task<IEnumerable<TodoItem>> GetAllAsync() 
            => Task.FromResult(_todoItems.Values.AsEnumerable());

        public Task<TodoItem> GetByIdAsync(long id, string type = "Note") 
            => Task.FromResult(_todoItems.TryGetValue(id, out TodoItem item) ? item : null);

        public Task Update(long id, TodoItem item)
        {
            if(_todoItems.TryGetValue(id, out TodoItem olditem))
                _todoItems.TryUpdate(id, item, olditem);
            
            return Task.CompletedTask;
        }
    }
}