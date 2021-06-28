using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApi.Domain.Models;
using TodoApi.Domain.Repository;

namespace TodoApi.Domain.Queries
{
    public class GetTodoListQuery : IRequest<IEnumerable<TodoItem>>
    {
    }

    public class GetTodoListQueryHandler : IRequestHandler<GetTodoListQuery, IEnumerable<TodoItem>>
    {
        private readonly ITodoItemRepository _repository;

        public GetTodoListQueryHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<TodoItem>> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
        {
            var items = (await _repository.GetAllAsync()).Select(item => item.ToItem());
            return items;
        }
    }
}