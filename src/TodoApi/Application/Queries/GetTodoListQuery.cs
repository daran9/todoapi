using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Mediator;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Repository;

namespace TodoApi.Application.Queries;

public class GetTodoListQuery : IRequest<Result<IEnumerable<Todo>>>
{
}

public class GetTodoListQueryHandler : IRequestHandler<GetTodoListQuery, Result<IEnumerable<Todo>>>
{
    private readonly ITodoRepository _repository;

    public GetTodoListQueryHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<Result<IEnumerable<Todo>>> Handle(GetTodoListQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}