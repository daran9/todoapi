using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoAPI.Test.Integration.Infrastructure.Repository
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private readonly ConcurrentDictionary<TodoId, Todo> _todos = new ConcurrentDictionary<TodoId, Todo>();

        public Task<Result> CreateAsync(Todo todo) 
        {
           return _todos.TryAdd(todo.Id, todo) 
            ? Task.FromResult(Result.Success())
            : Task.FromResult(Result.Failure("Todo already exists"));
        }

        public Task<Result> Delete(TodoId id) 
        {
            return _todos.TryRemove(id, out Todo _) 
                ? Task.FromResult(Result.Success()) 
                : Task.FromResult(Result.Failure("Todo not found"));
        }

        public Task<Result<IEnumerable<Todo>>> GetAllAsync() 
            => Task.FromResult(Result.Success(_todos.Values.AsEnumerable()));

        public Task<Result<Todo>> GetByIdAsync(TodoId id, string type = "Note") 
            => _todos.TryGetValue(id, out var todo)
                ? Task.FromResult(Result.Success(todo))
                : Task.FromResult(Result.Failure<Todo>("Todo not found"));


        public Task<Result> Update(TodoId id, Todo todo)
        {
            return _todos.TryGetValue(id, out var oldTodo) ? 
                _todos.TryUpdate(id, todo, oldTodo)
                    ? Task.FromResult(Result.Success()) 
                    : Task.FromResult(Result.Failure("Todo not found"))
                : Task.FromResult(Result.Failure("Todo not found"));
        }
    }
}