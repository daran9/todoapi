using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Repository;
using TodoApi.Infrastructure.Extensions;

namespace TodoApi.Infrastructure.Repository;

public class DynamoDBTodoRepository : ITodoRepository
{
    private const string Note = "Note";
    private readonly ILogger<DynamoDBTodoRepository> _logger;
    private readonly IDynamoDBContext _context;

    public DynamoDBTodoRepository(IDynamoDBContext context, ILogger<DynamoDBTodoRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<Todo>> GetByIdAsync(TodoId id, string type = Note)
    {
        return await HandleException(async () =>
        {
            _logger.LogInformation($"Retrieve Todo by Id:{id}");
            // Retrieve the Item.
            var todos = await _context.LoadAsync<TodoDocument>(id.Value, type);
            return Result.Success(todos.ToTodo());
        });
    }

    public async Task<Result> CreateAsync(Todo todo)
    {
        return await HandleException(async () =>
        {
            _logger.LogInformation($"Save Todo by Id:{todo.Id}");
            // Save the Item.
            await _context.SaveAsync(TodoDocument.Create(todo));
            return Result.Success();
        });
    }

    public async Task<Result> Delete(TodoId id)
    {
        return await HandleException(async () =>
        {
            _logger.LogInformation($"Delete Todo by Id:{id}");
            // Delete the Item.
            await _context.DeleteAsync<TodoDocument>(id);
            return Result.Success();
        });
    }

    public async Task<Result> Update(TodoId id, Todo todo)
    {
        return await HandleException(async () =>
        {
            _logger.LogInformation($"Update Todo by Id:{id}");
            await _context.SaveAsync(TodoDocument.Create(todo));
            return Result.Success();
        });
    }

    public async Task<Result<IEnumerable<Todo>>> GetAllAsync()
    {
        return await HandleException(async () =>
        {
            _logger.LogInformation("Retrieve All Todos");

            var todos = await _context.ScanAsync<TodoDocument>(new List<ScanCondition>
            {
                new("TypeName", ScanOperator.Equal, Note)
            }).GetRemainingAsync();

            return Result.Success(todos.Select(x => x.ToTodo()));
        });
    }

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
        }

        ;
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
        }

        ;
    }
}