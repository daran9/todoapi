using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoAPI.Test.Integration.Infrastructure.Repository
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private readonly ConcurrentDictionary<TodoId, Todo> _todos = new ConcurrentDictionary<TodoId, Todo>();

        public Task CreateAsync(Todo todo) 
            => Task.FromResult(_todos.TryAdd(todo.Id, todo));

        public Task Delete(TodoId id) 
            => Task.FromResult(_todos.TryRemove(id, out Todo _));

        public Task<IEnumerable<Todo>> GetAllAsync() 
            => Task.FromResult(_todos.Values.AsEnumerable());

        public Task<Todo> GetByIdAsync(TodoId id, string type = "Note") 
            => Task.FromResult(_todos.TryGetValue(id, out Todo todo) ? todo : null);

        public Task Update(TodoId id, Todo todo)
        {
            if(_todos.TryGetValue(id, out Todo oldTodo))
                _todos.TryUpdate(id, todo, oldTodo);
            
            return Task.CompletedTask;
        }
    }
}