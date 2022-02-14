using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Domain.Queries
{
    public class GetTodoListQuery : IRequest<IEnumerable<Todo>>
    {
    }

    public class GetTodoListQueryHandler : IRequestHandler<GetTodoListQuery, IEnumerable<Todo>>
    {
        private readonly ITodoRepository _repository;

        public GetTodoListQueryHandler(ITodoRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Todo>> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}