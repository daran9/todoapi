using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Domain.Repository;

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
            if(request == null)
                throw new ArgumentNullException($"{nameof(request)} is null");
                
            await _repository.Update(request.Id, request.ToItem());
        }
    }
}