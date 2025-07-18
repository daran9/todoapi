using System;
using System.Threading.Tasks;
using FluentAssertions;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using TodoApi;
using TodoApi.Application.Commands;
using TodoApi.Application.Queries;
using TodoApi.Domain.Entities;
using Xunit;

namespace TodoAPI.Test.Integration.Application;

public class GetTodoListQueryHandlerTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    private IServiceScope _scope;
    private readonly IMediator _mediator;
    public GetTodoListQueryHandlerTest(CustomWebApplicationFactory<Program> factory)
    {
        _scope = factory.Services.CreateScope();
        _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task GetTodoListQueryHandler_Should_Handle_GetTodoListQueryAsync()
    {
        // Arrange
        var todoCommand = new GetTodoListQuery();

        //Act        
        var result = await _mediator.Send(todoCommand);

        //Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task GetTodoListQueryHandler_Should_Handle_Return_Single_Item()
    {
        // Arrange
        await CreateTodo();
            
        var todoCommand = new GetTodoListQuery();

        //Act        
        var result = await _mediator.Send(todoCommand);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetTodoListQueryHandler_Should_Handle_Return_Multiple_Items()
    {
        int numberOfItems = 10;
        // Arrange
        await CreateTodos(numberOfItems);
            
        var todoCommand = new GetTodoListQuery();

        //Act        
        var result = await _mediator.Send(todoCommand);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(numberOfItems);
    }

    private async Task CreateTodo()
    {
        var todoCreateCommand = new CreateTodoCommand()
        {
            Id = TodoId.New(), 
            Type = "test",
            Name = "test", 
            IsComplete = false
        };
        var createResult = await _mediator.Send(todoCreateCommand);
        createResult.IsSuccess.Should().BeTrue();
    }

    private async Task CreateTodos(int count)    
    {
        for (int i = 0; i < count; i++)
        {
            await CreateTodo();
        }
    }


    public void Dispose()
        => _scope.Dispose();
}