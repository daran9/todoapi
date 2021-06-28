using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace TodoApi.Domain.Repository
{
    public class DynamoDBTodoItemRepository : ITodoItemRepository
    {
        private const string NOTE = "Note";
        IDynamoDBContext _context;
        private readonly ILogger<DynamoDBTodoItemRepository> _logger;

        public DynamoDBTodoItemRepository(IDynamoDBContext context, ILogger<DynamoDBTodoItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TodoItemEntity> GetByIdAsync(long id, string type = NOTE)
        {
            _logger.LogInformation($"Retrieve TodoItem by Id:{id}");
            // Retrieve the Item.
            return await _context.LoadAsync<TodoItemEntity>(id, type);
        }

        public async Task CreateAsync(TodoItemEntity item)
        {
            _logger.LogInformation($"Save TodoItem by Id:{item.Id}");
            // Save the Item.
            await _context.SaveAsync(item);
        }

        public async Task Delete(long id)
        {
            _logger.LogInformation($"Delete TodoItem by Id:{id}");
            // Delete the Item.
            await _context.DeleteAsync<TodoItemEntity>(id);
        }

        public async Task Update(long id, TodoItemEntity item)
        {
            _logger.LogInformation($"Update TodoItem by Id:{id}");
            // Retrieve the item.
            var itemRetrieved = await _context.LoadAsync<TodoItemEntity>(id, NOTE);

            itemRetrieved.Type = NOTE;
            itemRetrieved.Name = item.Name;
            itemRetrieved.IsComplete = item.IsComplete;
            await _context.SaveAsync(itemRetrieved);
        }

        public async Task<IEnumerable<TodoItemEntity>> GetAllAsync()
        {
            _logger.LogInformation("Retrieve All TodoItem");

            var items = await _context.ScanAsync<TodoItemEntity>(new List<ScanCondition>(){
                    new ScanCondition("Type", ScanOperator.Equal, NOTE)
                }).GetRemainingAsync();

            return items;
        }
    }
}