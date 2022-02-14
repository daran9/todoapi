using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Domain.Repository;
using System.Collections.Generic;

namespace TodoApi.Domain.Commands
{
    public class UpdateTodoCommand : IRequest
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class UpdateTodoCommandHandler : AsyncRequestHandler<UpdateTodoCommand>
    {
        private readonly ITodoItemRepository _repository;

        public UpdateTodoCommandHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }
        protected override async Task Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            //TODO: Change return type to Result<TodoItem>
            if(request == null)
                throw new ArgumentNullException($"{nameof(request)} is null");

            var item = await _repository.GetByIdAsync(request.Id, request.Type);
            if(item == null)
                throw new KeyNotFoundException($"{nameof(item)} is not found");
                  
            await _repository.Update(request.Id, request.ToItem());
        }
    }
}