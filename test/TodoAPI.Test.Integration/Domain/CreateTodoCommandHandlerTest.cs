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
    public class CreateTodoCommandHandlerTest: IClassFixture<CustomWebApplicationFactory<Startup>>, IDisposable
    {
        private IServiceScope _scope;
        private readonly IMediator _mediator;
        public CreateTodoCommandHandlerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _scope = factory.Server.Host.Services.CreateScope();
            _mediator = _scope.ServiceProvider.GetRequiredService<IMediator>();
        }
        
        [Fact]
        public async Task CreateTodoCommandHandler_Should_Handle_CreateTodoCommandAsync()
        {
             // Arrange
            var todoCommand = new CreateTodoCommand()
            {
                Id = 1, 
                Type = "test",
                Name = "test", 
                IsComplete = false
            };

            //Act        
            var result = await _mediator.Send(todoCommand);

            //Assert
            result.Should().BeOfType<TodoItem>();
            result.Id.Should().Be(todoCommand.Id);
            result.Name.Should().Be(todoCommand.Name);
        }

        public void Dispose()
        => _scope.Dispose();
    }
}