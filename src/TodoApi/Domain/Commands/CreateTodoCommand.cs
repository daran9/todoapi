using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Domain.Commands
{
    public class CreateTodoCommand : IRequest<TodoItem>
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, TodoItem>
    {
        private readonly ITodoItemRepository _repository;
        private readonly ILogger<CreateTodoCommandHandler> _logger;

        public CreateTodoCommandHandler(ITodoItemRepository repository, ILogger<CreateTodoCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<TodoItem> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
                throw new ArgumentNullException($"{nameof(request)} is null");

            var todoItemEntity = new TodoItemEntity()
            {
                Id = request.Id,
                Type = request.Type,
                Name = request.Name,
                IsComplete = request.IsComplete
            };
            await _repository.CreateAsync(todoItemEntity);
            return todoItemEntity.ToItem();
        }
    }
}
