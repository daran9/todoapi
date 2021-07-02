using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Domain.Queries
{
    public class GetTodoQuery : IRequest<TodoItem>
    {
        public long Id { get; set; }
    }

    public class GetTodoQueryHandler : IRequestHandler<GetTodoQuery, TodoItem>
    {
        private readonly ITodoItemRepository _repository;

        public GetTodoQueryHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<TodoItem> Handle(GetTodoQuery request, CancellationToken cancellationToken)
        {
            var item = (await _repository.GetByIdAsync(request.Id))?.ToItem();
            return item;
        }
    }
}