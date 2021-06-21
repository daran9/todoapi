using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace TodoApi.Repository
{
    public class DynamoDBTodoItemRepository: ITodoItemRepository
    {
        private const string NOTE = "Note";
        IDynamoDBContext _context;
        private readonly ILogger<DynamoDBTodoItemRepository> _logger;
        
        public DynamoDBTodoItemRepository(IDynamoDBContext context, ILogger<DynamoDBTodoItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TodoItem> GetByIdAsync(long id, string type = NOTE)
        {
            _logger.LogInformation($"Retrieve TodoItem by Id:{id}");
            // Retrieve the Item.
            return await _context.LoadAsync<TodoItem>(id, type);
        }

        public async Task CreateAsync(TodoItem item)
        {
            _logger.LogInformation($"Save TodoItem by Id:{item.Id}");
            // Save the Item.
            await _context.SaveAsync(item);
        }

        public async Task Delete(long id)
        {
            _logger.LogInformation($"Delete TodoItem by Id:{id}");      
            // Delete the Item.
            await _context.DeleteAsync<TodoItem>(id);
        }

        public async Task Update(long id, TodoItem item)
        {
            _logger.LogInformation($"Update TodoItem by Id:{id}"); 
            // Retrieve the item.
            var itemRetrieved = await _context.LoadAsync<TodoItem>(id, NOTE);
            
            itemRetrieved.Type = NOTE;
            itemRetrieved.Name = item.Name;
            itemRetrieved.IsComplete = item.IsComplete;
            await _context.SaveAsync(itemRetrieved);
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            _logger.LogInformation("Retrieve All TodoItem");

            var items = await _context.ScanAsync<TodoItem>(new List<ScanCondition>(){
                    new ScanCondition("Type", ScanOperator.Equal, NOTE)
                }).GetRemainingAsync();

            return items;
        }
    }
}