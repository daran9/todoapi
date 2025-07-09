using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Mediator;
using TodoApi.Domain.Entities;
using TodoApi.Domain.Repository;

namespace TodoApi.Application.Commands;

public class DeleteTodoCommand : IRequest<Result>
{
    public TodoId Id { get; set; }
}

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, Result>
{
    private readonly ITodoRepository _repository;

    public DeleteTodoCommandHandler(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<Result> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException($"{nameof(request)} is null");
        return await _repository.Delete(request.Id);
    }
}