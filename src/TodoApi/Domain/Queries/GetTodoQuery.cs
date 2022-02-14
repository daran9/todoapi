using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Domain.Queries
{
    public class GetTodoQuery : IRequest<Todo>
    {
        public TodoId Id { get; set; }
    }

    public class GetTodoQueryHandler : IRequestHandler<GetTodoQuery, Todo>
    {
        private readonly ITodoRepository _repository;

        public GetTodoQueryHandler(ITodoRepository repository)
        {
            _repository = repository;
        }
        public async Task<Todo> Handle(GetTodoQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}