
using System;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TodoApi;
using TodoApi.Domain.Commands;
using TodoApi.Domain.Models;
using Xunit;

namespace TodoAPI.Test.Integration.Domain
{
    public class DeleteTodoCommandHandlerTest: IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        private IServiceScope _scope;
        private readonly IMediator _mediator;
        public DeleteTodoCommandHandlerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _scope = factory.Services.CreateScope();
            _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task DeleteTodoCommandHandler_Should_Handle_DeleteTodoCommandAsync()
        {
            // Arrange
            var todoId = await CreateTodo();

            var todoCommand = new DeleteTodoCommand()
            {
                Id = todoId
            };

            //Act        
            var result = await _mediator.Send(todoCommand);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        
        private async Task<TodoId> CreateTodo()
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
            return createResult.Value.Id;
        }

        public void Dispose()
        => _scope.Dispose();
    }
}