using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Domain.Repository;

namespace TodoApi.Domain.Commands
{
    public class DeleteTodoCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteTodoCommandHandler : AsyncRequestHandler<DeleteTodoCommand>
    {
        private readonly ITodoItemRepository _repository;

        public DeleteTodoCommandHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }
        protected override async Task Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            await _repository.Delete(request.Id);
        }
    }
}