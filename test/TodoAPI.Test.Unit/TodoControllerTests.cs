using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;
using System;
using TodoApi.Application.Queries;
using TodoApi.Domain.Entities;
using TodoApi.Web.Controllers;

namespace TodoAPI.Test.Unit
{
    public class TodoControllerTests
    {
        [Fact]
        public async Task GetByIdAsync_When_item_does_not_exist_Return_not_found()
        {
            // Act
            var mediatorStub = new Mock<IMediator>();
            mediatorStub.Setup(m => m.Send(It.IsAny<GetTodoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<Todo>("Item not found"));

            var loggerStub = new Mock<ILogger<TodoController>>();

            var controller = new TodoController(mediatorStub.Object, loggerStub.Object);
            
            // Arrange
            var result = await controller.GetByIdAsync(Guid.NewGuid());

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }
    }
}
