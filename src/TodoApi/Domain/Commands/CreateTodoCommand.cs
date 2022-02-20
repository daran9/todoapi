using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Domain.Commands
{
    public class CreateTodoCommand : IRequest<Result<Todo>>
    {
        public TodoId Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Result<Todo>>
    {
        private readonly ITodoRepository _repository;
        private readonly ILogger<CreateTodoCommandHandler> _logger;

        public CreateTodoCommandHandler(ITodoRepository repository, ILogger<CreateTodoCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<Todo>> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new ArgumentNullException($"{nameof(request)} is null");

            var item = request.ToTodo();
            var result = await _repository.CreateAsync(item);
            if(result.IsFailure)
                return Result.Failure<Todo>(result.Error);
            return Result.Success(item);
        }
    }
}
