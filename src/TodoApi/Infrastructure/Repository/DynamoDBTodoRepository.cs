using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using TodoApi.Domain.Repository;
using TodoApi.Domain.Models;
using System.Linq;
using CSharpFunctionalExtensions;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using System;

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

        public async Task<Result<Todo>> GetByIdAsync(TodoId id, string type = NOTE)
            => await HandleException(async () =>
                {
                    _logger.LogInformation($"Retrieve Todo by Id:{id}");
                    // Retrieve the Item.
                    var todos = await _context.LoadAsync<TodoEntity>(id.Value, type);
                    return Result.Success(todos.ToTodo());
                });

        public async Task<Result> CreateAsync(Todo todo)
            => await HandleException(async () => 
                { 
                    _logger.LogInformation($"Save Todo by Id:{todo.Id}");
                    // Save the Item.
                    await _context.SaveAsync(TodoEntity.Create(todo));
                    return Result.Success();
                });

        public async Task<Result> Delete(TodoId id)
            => await HandleException(async () =>       
                {
                    _logger.LogInformation($"Delete Todo by Id:{id}");
                    // Delete the Item.
                    await _context.DeleteAsync<TodoEntity>(id);
                    return Result.Success();
                });

        public async Task<Result> Update(TodoId id, Todo todo) 
            => await HandleException(async () =>
                {

                    _logger.LogInformation($"Update Todo by Id:{id}");
                    await _context.SaveAsync(TodoEntity.Create(todo));
                    return Result.Success();
                });

        public async Task<Result<IEnumerable<Todo>>> GetAllAsync() 
            => await HandleException(async () =>
                {
                    _logger.LogInformation("Retrieve All Todos");

                    var todos = await _context.ScanAsync<TodoEntity>(new List<ScanCondition>(){
                            new ScanCondition("TypeName", ScanOperator.Equal, NOTE)
                        }).GetRemainingAsync();

                    return Result.Success(todos.Select(x => x.ToTodo()));
                });

        private async Task<Result<T>> HandleException<T>(Func<Task<Result<T>>> dbOperation)
        {
            try
            {
                return await dbOperation();
            }
            catch (AmazonDynamoDBException e)
            {
                _logger.LogError(e.Message);
                return Result.Failure<T>(e.Message);
            }
            catch (AmazonServiceException e)
            {
                _logger.LogError(e.Message);
                return Result.Failure<T>(e.Message);
            };
        }
        private async Task<Result> HandleException(Func<Task<Result>> dbOperation)
        {
            try
            {
                return await dbOperation();
            }
            catch (AmazonDynamoDBException e)
            {
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            }
            catch (AmazonServiceException e)
            {
                _logger.LogError(e.Message);
                return Result.Failure(e.Message);
            };
        }
    }
}