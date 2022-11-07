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
    public class UpdateTodoCommandHandlerTest: IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
    {
        private IServiceScope _scope;
        private readonly IMediator _mediator;
        public UpdateTodoCommandHandlerTest(CustomWebApplicationFactory<Program> factory)
        {
            _scope = factory.Services.CreateScope();
            _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        }
        
        [Fact]
        public async Task CreateTodoCommandHandler_Should_Handle_CreateTodoCommandAsync()
        {
             // Arrange
              var createTodoCommand = new CreateTodoCommand()
            {
                Id = TodoId.New(), 
                Type = "test",
                Name = "test", 
                IsComplete = false
            };
            var createResult = await _mediator.Send(createTodoCommand);
            createResult.IsSuccess.Should().BeTrue();
            createResult.Value.Should().BeOfType<Todo>();

            var updateTodoCommand = new UpdateTodoCommand()
            {
                Id = TodoId.New(),
                Type = "test 2",
                Name = "test 2", 
                IsComplete = true
            };

            //Act        
            var result = await _mediator.Send(updateTodoCommand);

            //Assert
            result.Should().NotBeNull();

        }

        public void Dispose()
        => _scope.Dispose();
    }
}