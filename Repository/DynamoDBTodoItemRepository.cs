using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoApi.Repository
{
    public class DynamoDBTodoItemRepository: ITodoItemRepository
    {
        IDynamoDBContext _context;
        private const string NOTE = "Note";

        public DynamoDBTodoItemRepository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> GetByIdAsync(long id, string type = NOTE)
        {
            // Retrieve the Item.
            return await _context.LoadAsync<TodoItem>(id, type);
        }

        public async Task CreateAsync(TodoItem item)
        {
            // Save the Item.
            await _context.SaveAsync(item);
        }

        public async Task Delete(long id)
        {      
            // Delete the Item.
            await _context.DeleteAsync<TodoItem>(id);
        }

        public async Task Update(long id, TodoItem item)
        {
            // Retrieve the item.
            var itemRetrieved = await _context.LoadAsync<TodoItem>(id, NOTE);
            
            itemRetrieved.Type = NOTE;
            itemRetrieved.Name = item.Name;
            itemRetrieved.IsComplete = item.IsComplete;
            await _context.SaveAsync(itemRetrieved);
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            var items = await _context.ScanAsync<TodoItem>(new List<ScanCondition>(){
                    new ScanCondition("Type", ScanOperator.Equal, NOTE)
                }).GetRemainingAsync();

            return items;
        }
    }
}