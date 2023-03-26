using System;
using System.Threading.Tasks;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TodoApi;
using TodoApi.Application.Commands;
using TodoApi.Application.Queries;
using TodoApi.Domain.Entities;
using Xunit;

namespace TodoAPI.Test.Integration.Domain
{
    public class GetTodoQueryHandlerTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {     
        private IServiceScope _scope;
        private readonly IMediator _mediator;
        public GetTodoQueryHandlerTest(CustomWebApplicationFactory<Program> factory)
        {
            _scope = factory.Services.CreateScope();
            _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        }
        
        [Fact]
        public async Task GetTodoQueryHandler_Should_Handle_Return_Todo_NotFound()
        {
             // Arrange
            var todoCommand = new GetTodoQuery()
            {
                Id = TodoId.New()
            };

            //Act        
            var result = await _mediator.Send(todoCommand);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Be("Todo not found");
        }

        [Fact]
        public async Task GetTodoQueryHandler_Should_Handle_Return_Todo()
        {
            // Arrange
            var todoId = await CreateTodo();
            
            var todoCommand = new GetTodoQuery()
            {
                Id = todoId
            };

            //Act        
            var result = await _mediator.Send(todoCommand);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeOfType<Todo>();
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