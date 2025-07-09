using System;
using System.Threading.Tasks;
using FluentAssertions;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using TodoApi;
using TodoApi.Application.Commands;
using TodoApi.Domain.Entities;
using Xunit;

namespace TodoAPI.Test.Integration.Application;

public class CreateTodoCommandHandlerTest: IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    private IServiceScope _scope;
    private readonly IMediator _mediator;
    public CreateTodoCommandHandlerTest(CustomWebApplicationFactory<Program> factory)
    {
        _scope = factory.Services.CreateScope();
        _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
    }
        
    [Fact]
    public async Task CreateTodoCommandHandler_Should_Handle_CreateTodoCommandAsync()
    {
        // Arrange
        var todoCommand = new CreateTodoCommand()
        {
            Id = TodoId.New(), 
            Type = "test",
            Name = "test", 
            IsComplete = false
        };

        //Act        
        var result = await _mediator.Send(todoCommand);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<Todo>();
        result.Value.Id.Should().Be(todoCommand.Id);
        result.Value.Name.Should().Be(todoCommand.Name);
    }

    public void Dispose()
        => _scope.Dispose();
}