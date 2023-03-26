using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using MediatR;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Repository;

namespace TodoApi.Application.Queries;

public class GetTodoQuery : IRequest<Result<Todo>>
{
    public TodoId Id { get; set; }
}

public class GetTodoQueryHandler : IRequestHandler<GetTodoQuery, Result<Todo>>
{
    private readonly ITodoRepository _repository;

    public GetTodoQueryHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Todo>> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}