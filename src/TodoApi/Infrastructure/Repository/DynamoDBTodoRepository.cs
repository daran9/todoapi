using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TodoApi.Domain.Repository;
using TodoApi.Domain.Models;
using System.Linq;

namespace TodoApi.Infrastructure.Repository
{
    public class DynamoDBTodoRepository : ITodoRepository
    {
        private const string NOTE = "Note";
        IDynamoDBContext _context;
        private readonly ILogger<DynamoDBTodoRepository> _logger;

        public DynamoDBTodoRepository(IDynamoDBContext context, ILogger<DynamoDBTodoRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Todo> GetByIdAsync(TodoId id, string type = NOTE)
        {
            _logger.LogInformation($"Retrieve Todo by Id:{id}");
            // Retrieve the Item.
            var todos = await _context.LoadAsync<TodoEntity>(id, type);
            return todos.ToTodo();
        }

        public async Task CreateAsync(Todo todo)
        {
            _logger.LogInformation($"Save Todo by Id:{todo.Id}");
            // Save the Item.
            await _context.SaveAsync(todo);
        }

        public async Task Delete(TodoId id)
        {
            _logger.LogInformation($"Delete Todo by Id:{id}");
            // Delete the Item.
            await _context.DeleteAsync<TodoEntity>(id);
        }

        public async Task Update(TodoId id, Todo todo)
        {
            _logger.LogInformation($"Update Todo by Id:{id}");
            // Retrieve the item.
            var itemRetrieved = await _context.LoadAsync<TodoEntity>(id, NOTE);

            itemRetrieved.Type = NOTE;
            itemRetrieved.Name = todo.Name;
            itemRetrieved.IsComplete = todo.IsComplete;
            await _context.SaveAsync(itemRetrieved);
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
            _logger.LogInformation("Retrieve All Todos");

            var todos = await _context.ScanAsync<TodoEntity>(new List<ScanCondition>(){
                    new ScanCondition("Type", ScanOperator.Equal, NOTE)
                }).GetRemainingAsync();

            return todos.Select(x => x.ToTodo());
        }
    }
}